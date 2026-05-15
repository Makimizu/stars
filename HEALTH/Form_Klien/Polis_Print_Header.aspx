<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Print_Header.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Print_Header" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%; position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
                    <asp:DropDownList ID="DDL_PRINT" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_PRINT_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
