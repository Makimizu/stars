<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="REPORTHEADER.aspx.cs" Inherits="AGR.REPORTHEADER" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        // SP_REPORT_LIST renders buttons with legacy inline JS (usually pointing to ReportViewer/Viewer.aspx).
        // For Reports -> List, open a full-page report view with a Back button.
        (function () {
            function extractHref(js) {
                if (!js) return null;
                // Supports:
                // - location.href='...'
                // - location.href="..."
                // - location.href=...;
                var m = /location\.href\s*=\s*(?:'([^']+)'|"([^"]+)"|([^\s;]+))/i.exec(js);
                return m ? (m[1] || m[2] || m[3]) : null;
            }

            function toReportViewUrl(href) {
                try {
                    // Pass the full target URL; REPORTVIEW.aspx will load it into its iframe.
                    return (
                        "REPORTVIEW.aspx?href=" +
                        encodeURIComponent(href) +
                        "&ReturnUrl=" +
                        encodeURIComponent("REPORTHEADER.aspx")
                    );
                } catch (e) {
                    return href;
                }
            }

            // Convert legacy submit buttons to normal buttons when possible.
            function normalizeSubmitButtons() {
                try {
                    var nodes = document.querySelectorAll("button[type='submit'], input[type='submit'], input[type='image']");
                    for (var i = 0; i < nodes.length; i++) {
                        // Changing type prevents implicit form submission navigation.
                        if (nodes[i].tagName === "BUTTON") nodes[i].type = "button";
                        if (nodes[i].tagName === "INPUT" && nodes[i].type) nodes[i].type = "button";
                    }
                } catch (e) { }
            }

            // Rewrite legacy inline onclick handlers so they can't navigate the left pane.
            function rewireLegacyOnclicks() {
                try {
                    var nodes = document.querySelectorAll("button[onclick], input[onclick], a[onclick]");
                    for (var i = 0; i < nodes.length; i++) {
                        var oc = nodes[i].getAttribute("onclick");
                        if (!oc || oc.indexOf("location.href") === -1) continue;
                        var href = extractHref(oc);
                        if (!href) continue;
                        var nextUrl = toReportViewUrl(href);

                        // Remove inline handler and replace with our routing handler.
                        nodes[i].setAttribute("onclick", "return false;");
                        (function (el, url) {
                            el.addEventListener(
                                "click",
                                function (evt) {
                                    evt.preventDefault();
                                    evt.stopPropagation();
                                    // Some browsers still run inline handlers; stop immediately.
                                    if (evt.stopImmediatePropagation) evt.stopImmediatePropagation();
                                    window.location.href = url;
                                    return false;
                                },
                                true
                            );
                        })(nodes[i], nextUrl);
                    }
                } catch (e) { }
            }

            if (document.readyState === "loading") {
                document.addEventListener("DOMContentLoaded", function () {
                    normalizeSubmitButtons();
                    rewireLegacyOnclicks();
                });
            } else {
                normalizeSubmitButtons();
                rewireLegacyOnclicks();
            }
        })();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_CARDS" runat="server"></asp:Label>
    </form>
</body>
</html>
