using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Net;
using System.Net.Security;
using System.Web;
using System.Web.UI;
using DMS.DBConnection;

namespace AGR.ReportViewer
{
    public partial class Proxy : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var appId = (Request.QueryString["APPID"] ?? Request.QueryString["appid"] ?? string.Empty).Trim();
            var code = (Request.QueryString["CODE"] ?? Request.QueryString["code"] ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(code))
            {
                Response.StatusCode = 400;
                Response.Write("Missing APPID/CODE.");
                Response.End();
                return;
            }

            string baseUrl = null;
            try
            {
                var conn = new Connection(GlobalUse.GetConnString(appId));
                conn.QueryString = "select top 1 URL from V_LINK_SC_REPORT_LIST where APP_ID='" +
                                   appId.Replace("'", "''") + "' and CODE='" + code.Replace("'", "''") + "'";
                conn.ExecuteQuery();
                baseUrl = conn.GetFieldValue("URL")?.ToString();
            }
            catch
            {
                baseUrl = null;
            }

            if (string.IsNullOrEmpty(baseUrl))
            {
                Response.StatusCode = 404;
                Response.Write("Report URL not found.");
                Response.End();
                return;
            }

            // Render as PDF to avoid rewriting SSRS HTML links and to keep everything same-origin.
            var extra = BuildExtraQuery(Request.QueryString);
            var targetUrl = AppendQuery(baseUrl, extra);
            targetUrl = AppendQuery(targetUrl, "rs:Command=Render&rs:Format=PDF");

            try
            {
                // SSRS endpoints commonly require TLS 1.2. Force protocol for older .NET defaults.
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.Expect100Continue = false;

                // Optional: allow local dev to bypass SSL validation (NOT recommended for production).
                var ignoreSsl = (ConfigurationManager.AppSettings["ssrsProxyIgnoreSslErrors"] ?? "false")
                    .Equals("true", StringComparison.OrdinalIgnoreCase);
                if (ignoreSsl)
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                        new RemoteCertificateValidationCallback((s, cert, chain, sslPolicyErrors) => true);
                }

                var req = (HttpWebRequest)WebRequest.Create(targetUrl);
                req.Method = "GET";
                req.AllowAutoRedirect = true;
                req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                req.UserAgent = "AGR-ReportProxy";
                req.Accept = "application/pdf,application/octet-stream;q=0.9,*/*;q=0.8";
                req.Timeout = 60000;
                req.ReadWriteTimeout = 60000;
                req.KeepAlive = true;

                // Credentials:
                // - default: use default credentials (IIS identity) for intranet SSRS
                // - override: set ssrsProxyUser/ssrsProxyPassword[/ssrsProxyDomain] in Web.config for local dev
                var user = ConfigurationManager.AppSettings["ssrsProxyUser"];
                var pwd = ConfigurationManager.AppSettings["ssrsProxyPassword"];
                var domain = ConfigurationManager.AppSettings["ssrsProxyDomain"];
                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pwd))
                {
                    req.UseDefaultCredentials = false;
                    req.Credentials = string.IsNullOrEmpty(domain)
                        ? new NetworkCredential(user, pwd)
                        : new NetworkCredential(user, pwd, domain);
                }
                else
                {
                    req.UseDefaultCredentials = true;
                    req.Credentials = CredentialCache.DefaultCredentials;
                }

                using (var resp = (HttpWebResponse)req.GetResponse())
                using (var stream = resp.GetResponseStream())
                {
                    if (stream == null) throw new InvalidOperationException("Empty response stream.");

                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "inline; filename=report.pdf");

                    stream.CopyTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (WebException wex)
            {
                Response.StatusCode = 502;
                Response.ContentType = "text/plain";
                Response.Write("Failed to fetch report.\n\n");
                Response.Write("Target: " + targetUrl + "\n\n");
                Response.Write(wex.GetType().FullName + ": " + wex.Message + "\n\n");

                try
                {
                    var http = wex.Response as HttpWebResponse;
                    if (http != null)
                    {
                        Response.Write("HTTP Status: " + (int)http.StatusCode + " " + http.StatusDescription + "\n");
                        Response.Write("Authenticate: " + http.Headers["WWW-Authenticate"] + "\n");
                        using (var rs = http.GetResponseStream())
                        {
                            if (rs != null)
                            {
                                using (var reader = new StreamReader(rs))
                                {
                                    var body = reader.ReadToEnd();
                                    if (!string.IsNullOrEmpty(body))
                                    {
                                        Response.Write("\n--- Response body (truncated) ---\n");
                                        Response.Write(body.Length > 2000 ? body.Substring(0, 2000) : body);
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // ignore secondary errors while formatting error response
                }

                Response.End();
            }
        }

        private static string BuildExtraQuery(NameValueCollection qs)
        {
            var keys = qs.AllKeys
                .Where(k => !string.IsNullOrEmpty(k))
                .Where(k => !k.Equals("APPID", StringComparison.OrdinalIgnoreCase))
                .Where(k => !k.Equals("CODE", StringComparison.OrdinalIgnoreCase))
                .Where(k => !k.Equals("rs:Command", StringComparison.OrdinalIgnoreCase))
                .Where(k => !k.Equals("rs:Format", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (keys.Length == 0) return string.Empty;

            var parts = keys.Select(k =>
            {
                var v = qs[k] ?? string.Empty;
                return HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(v);
            });

            return string.Join("&", parts);
        }

        private static string AppendQuery(string url, string query)
        {
            if (string.IsNullOrEmpty(query)) return url;
            if (url.Contains("?")) return url.EndsWith("?") || url.EndsWith("&") ? url + query : url + "&" + query;
            return url + "?" + query;
        }
    }
}

