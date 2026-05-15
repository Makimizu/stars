<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationLoadingShare.aspx.cs" Inherits="LQ.Form_App.ApplicationLoadingShare" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <script>
        function resizeIframe(obj) {
            obj.style.height = 40 + obj.contentWindow.document.documentElement.scrollHeight + 'px';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_PRODUCT_CODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TC" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:Button ID="BT_CHARGES" runat="server" CssClass="ASPButton" Text="CHARGES" Width="100%" OnClick="BT_CHARGES_Click" />
                    <asp:Button ID="BT_COMMISSION" runat="server" CssClass="ASPButton" Text="COMMISSION" Width="100%" OnClick="BT_COMMISSION_Click" />
                    <asp:Button ID="BT_LOADING" runat="server" CssClass="ASPButton" Text="LOADING" Width="100%" OnClick="BT_LOADING_Click" />
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <iframe id="IF" runat="server" style="width: 100%; overflow: auto; border-style: none;"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

