<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_Finance_Header.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Period_Finance_Header" %>

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
                    <asp:DropDownList ID="DDL_URL" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_URL_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
