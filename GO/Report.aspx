<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="GO.Report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:DropDownList ID="DDL_REPORT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_REPORT_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="LB_APP" runat="server" Visible="False"></asp:Label>
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
