<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurplusUWPreviewHeader.aspx.cs" Inherits="UWBOX.Form_Tools.SurplusUWPreviewHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });

        document.onload = function () {
            var state = document.readyState
            if (state == 'interactive') {
                ShowProgress();
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('DV_LOADING').style.visibility = "hidden";
                }, 1000);
            }
        }

        function hourglass() {
            document.body.style.cursor = "wait";
        }
    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.6;
            filter: alpha(opacity=80);
            -moz-opacity: 0.6;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: transparent;
            z-index: 999;
        }

        .fa {
            display: inline-block;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: inherit;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 160px;">BATCH ID</td>
                                        <td>
                                            <asp:Label ID="LB_BATCH_ID" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 100px;">UPLOAD BY</td>
                                        <td>
                                            <asp:Label ID="LB_USERBY" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">SURPLUS GROUP</td>
                                        <td>
                                            <asp:Label ID="LB_SURPLUS_GROUP" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>UPLOAD DATE</td>
                                        <td>
                                            <asp:Label ID="LB_USERDATE" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>    
                                </table>
                            </td>
                            <td style="width: 200px;">
                                <asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" Text="APPROVE" Width="100%" OnClick="BT_APPROVE_Click" OnClientClick="ShowProgress()"/>
                                <asp:Button ID="BT_REJECT" runat="server" CssClass="ASPButton" Text="REJECT" Width="100%" OnClick="BT_REJECT_Click" OnClientClick="ShowProgress()"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
