using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using DMS.DBConnection;

namespace AGR.ReportViewer
{
    public partial class Viewer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Expecting /ReportViewer/Viewer.aspx?APPID=AGR&CODE=xx[&...]
            var appId = (Request.QueryString["APPID"] ?? Request.QueryString["appid"] ?? string.Empty).Trim();
            var code = (Request.QueryString["CODE"] ?? Request.QueryString["code"] ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(code))
            {
                Response.StatusCode = 400;
                Response.Write("Missing APPID/CODE.");
                Response.End();
                return;
            }

            string targetUrl = null;
            try
            {
                var conn = new Connection(GlobalUse.GetConnString(appId));
                conn.QueryString = "select top 1 URL from V_LINK_SC_REPORT_LIST where APP_ID='" +
                                   appId.Replace("'", "''") + "' and CODE='" + code.Replace("'", "''") + "'";
                conn.ExecuteQuery();
                targetUrl = conn.GetFieldValue("URL")?.ToString();
            }
            catch
            {
                targetUrl = null;
            }

            if (string.IsNullOrEmpty(targetUrl))
            {
                Response.StatusCode = 404;
                Response.Write("Report URL not found for APPID=" + HttpUtility.HtmlEncode(appId) +
                               " CODE=" + HttpUtility.HtmlEncode(code));
                Response.End();
                return;
            }

            // Build final SSRS URL (interactive) + keep extra parameters.
            var extraQuery = BuildExtraQuery(Request.QueryString);
            if (!string.IsNullOrEmpty(extraQuery))
                targetUrl = AppendQuery(targetUrl, extraQuery);

            // If V_LINK_SC_REPORT_LIST stores an absolute URL to the same host, strip scheme/host
            // to avoid mixed-scheme issues (http vs https) and keep the origin identical.
            string iframeUrl = targetUrl;
            var canSameOriginFrame = false;
            try
            {
                if (Uri.TryCreate(targetUrl, UriKind.Absolute, out var abs))
                {
                    if (Request?.Url != null &&
                        abs.Host.Equals(Request.Url.Host, StringComparison.OrdinalIgnoreCase))
                    {
                        iframeUrl = abs.PathAndQuery; // keep same host+scheme as current request
                        canSameOriginFrame = true;
                    }
                }
                else
                {
                    // relative URL => same-origin by definition
                    canSameOriginFrame = true;
                }
            }
            catch
            {
                canSameOriginFrame = false;
            }

            // By default we DO NOT proxy/convert to PDF because users expect the interactive SSRS viewer.
            // If cross-origin, embedding will be blocked by the browser (X-Frame-Options:SAMEORIGIN).
            // Optionally, allow a proxy fallback via config for local dev only.
            var allowProxyFallback = (ConfigurationManager.AppSettings["ssrsProxyEnabled"] ?? "false")
                .Equals("true", StringComparison.OrdinalIgnoreCase);
            // Auto-enable proxy on localhost/loopback so local testing doesn't "break" menus.
            var isLoopback = false;
            try
            {
                isLoopback = Request?.Url != null && Request.Url.IsLoopback;
            }
            catch { isLoopback = false; }

            if (!canSameOriginFrame && (allowProxyFallback || isLoopback))
            {
                iframeUrl = "Proxy.aspx?APPID=" + HttpUtility.UrlEncode(appId) + "&CODE=" + HttpUtility.UrlEncode(code);
                if (!string.IsNullOrEmpty(extraQuery))
                    iframeUrl = AppendQuery(iframeUrl, extraQuery);
                canSameOriginFrame = true;
            }

            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("<!doctype html><html><head><meta charset=\"utf-8\"><title>Report</title>");
            Response.Write("<style>html,body{height:100%;margin:0}iframe{border:0;width:100%;height:100%} .msg{font-family:Arial,Helvetica,sans-serif;padding:16px;font-size:13px}</style>");
            Response.Write("</head><body>");

            if (!canSameOriginFrame)
            {
                Response.Write("<div class=\"msg\">");
                Response.Write("<b>Report cannot be displayed here.</b><br/><br/>");
                Response.Write("This SSRS viewer can only be embedded when it is <b>same-origin</b> with the application (due to <code>X-Frame-Options: SAMEORIGIN</code>).<br/>");
                Response.Write("Open the application using the same host as the SSRS link (e.g. <b>starsuat.takaful.com</b>), or enable proxy fallback in <code>Web.config</code> (<code>ssrsProxyEnabled=true</code>).");
                Response.Write("</div>");
            }
            else
            {
                Response.Write("<iframe src=\"" + HttpUtility.HtmlAttributeEncode(iframeUrl) + "\"></iframe>");
            }

            Response.Write("</body></html>");
            Response.End();
        }

        private static string BuildExtraQuery(NameValueCollection qs)
        {
            // Preserve additional parameters (e.g. CD, PERIOD, etc.), excluding APPID/CODE.
            var keys = qs.AllKeys
                .Where(k => !string.IsNullOrEmpty(k))
                .Where(k => !k.Equals("APPID", StringComparison.OrdinalIgnoreCase))
                .Where(k => !k.Equals("CODE", StringComparison.OrdinalIgnoreCase))
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

