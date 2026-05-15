<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementReinstatement.aspx.cs" Inherits="LIFE.Form_POS.EndorsementReinstatement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:Label ID="LB_TYPE_DESCR" runat="server"></asp:Label>&nbsp;REASON :<br />
                    <asp:TextBox ID="TXT_REASON" runat="server" CssClass="ASPTextBox" MaxLength="255" TextMode="MultiLine" Width="90%" Height="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="100" OnClick="BT_SAVE_Click" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
