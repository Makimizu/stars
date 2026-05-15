<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiScreensHeader.aspx.cs" Inherits="FINANCE.Form_Tools.MultiScreensHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:Label ID="LB_CAPTION" runat="server" Font-Bold="True">REPORTS : </asp:Label>
                    &nbsp;<asp:DropDownList ID="DDL_SCREENS" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_SCREENS_SelectedIndexChanged1">
                    </asp:DropDownList>
                    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
