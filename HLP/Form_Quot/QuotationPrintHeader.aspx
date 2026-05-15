<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationPrintHeader.aspx.cs" Inherits="HLP.Form_Quot.QuotationPrintHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:DropDownList ID="DDL_REPORT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_REPORT_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="BT_EXPORT" runat="server" CssClass="ASPButton" OnClick="BT_EXPORT_Click" Text="EXPORT TO :" />
                    <asp:DropDownList ID="DDL_FORMAT" runat="server" CssClass="ASPDropDownList">
                        <asp:ListItem>PDF</asp:ListItem>
                        <asp:ListItem>EXCEL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="LB_QUOTNO" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_VERNO" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
