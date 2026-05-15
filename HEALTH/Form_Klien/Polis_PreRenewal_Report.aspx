<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_PreRenewal_Report.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_PreRenewal_Report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <strong>REPORT :</strong>&nbsp;
                    <asp:DropDownList ID="DDL_SCREENS" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_SCREENS_SelectedIndexChanged1">
                    </asp:DropDownList>
                    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
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