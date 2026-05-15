using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace AGR.Form_Tool
{
    public partial class REPORTVIEW : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var returnUrl = (Request.QueryString["ReturnUrl"] ?? "REPORTHEADER.aspx").Trim();
            BackLink.HRef = returnUrl;

            var appId = (Request.QueryString["APPID"] ?? Request.QueryString["appid"] ?? string.Empty).Trim();
            var code = (Request.QueryString["CODE"] ?? Request.QueryString["code"] ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(code))
            {
                LB_INFO.Text = "Missing APPID/CODE";
                ReportFrame.Attributes["src"] = "about:blank";
                return;
            }

            // Build Viewer.aspx URL, preserving extra parameters (CD, PERIOD, etc.)
            var viewerUrl = "../ReportViewer/Viewer.aspx?APPID=" + HttpUtility.UrlEncode(appId) +
                            "&CODE=" + HttpUtility.UrlEncode(code);

            var extra = BuildExtraQuery(Request.QueryString);
            if (!string.IsNullOrEmpty(extra))
                viewerUrl += "&" + extra;

            LB_INFO.Text = HttpUtility.HtmlEncode(appId + " / " + code);
            ReportFrame.Attributes["src"] = viewerUrl;
        }

        private static string BuildExtraQuery(NameValueCollection qs)
        {
            var keys = qs.AllKeys
                .Where(k => !string.IsNullOrEmpty(k))
                .Where(k => !k.Equals("APPID", StringComparison.OrdinalIgnoreCase))
                .Where(k => !k.Equals("CODE", StringComparison.OrdinalIgnoreCase))
                .Where(k => !k.Equals("ReturnUrl", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (keys.Length == 0) return string.Empty;

            var parts = keys.Select(k =>
            {
                var v = qs[k] ?? string.Empty;
                return HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(v);
            });

            return string.Join("&", parts);
        }
    }
}

