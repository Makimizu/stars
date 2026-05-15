<%@ Page Language="C#" AutoEventWireup="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Report</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        html, body, form { height: 100%; margin: 0; }
        .toolbar {
            height: 42px;
            display: flex;
            align-items: center;
            gap: 8px;
            padding: 0 10px;
            box-sizing: border-box;
            border-bottom: 1px solid #d0d0d0;
            background: #f7f7f7;
            font: 12px Arial, Helvetica, sans-serif;
        }
        .btn {
            display: inline-block;
            padding: 6px 10px;
            border: 1px solid #bdbdbd;
            background: #ffffff;
            color: #000;
            text-decoration: none;
            border-radius: 3px;
        }
        .btn:hover { background: #f0f0f0; }
        .frame {
            height: calc(100% - 42px);
        }
        .frame iframe {
            width: 100%;
            height: 100%;
            border: 0;
            display: block;
        }
    </style>
</head>
<body>
    <div class="toolbar">
        <a class="btn" id="BackLink" href="#">Back</a>
        <span id="Info" style="display:none"></span>
    </div>
    <div class="frame">
        <iframe id="ReportFrame" src="about:blank"></iframe>
    </div>

    <script type="text/javascript">
        (function () {
            // If opened inside frames, break out to full page.
            try {
                if (window.top && window.top !== window.self) {
                    window.top.location.href = window.location.href;
                    return;
                }
            } catch (e) { }

            function qs(name) {
                var p = new URLSearchParams(window.location.search);
                return p.get(name) || p.get(name.toLowerCase()) || "";
            }

            var returnUrl = qs("ReturnUrl") || "REPORTHEADER.aspx";
            var href = qs("href");

            var back = document.getElementById("BackLink");
            if (back) {
                back.setAttribute("href", returnUrl || "#");
                back.addEventListener("click", function (e) {
                    // Prefer history back so we return to the same list.
                    try {
                        e.preventDefault();
                        window.history.back();
                    } catch (ex) { }
                });
            }

            var info = document.getElementById("Info");
            if (href) {
                try {
                    var abs = new URL(href, window.location.href).toString();
                    document.getElementById("ReportFrame").setAttribute("src", abs);
                    if (info) info.textContent = abs;
                } catch (e) {
                    document.getElementById("ReportFrame").setAttribute("src", href);
                    if (info) info.textContent = href;
                }
            } else {
                if (info) info.textContent = "Missing report URL";
            }
        })();
    </script>
</body>
</html>

