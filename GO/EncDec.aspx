<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncDec.aspx.cs" Inherits="GO.EncDec" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="TXT" runat="server" CssClass="ASPTextBox" Width="800px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_ENC" runat="server" CssClass="ASPButton" OnClick="BT_ENC_Click" Text="ENC" Width="83px" />
                    &nbsp;<asp:Button ID="BT_DEC" runat="server" CssClass="ASPButton" OnClick="BT_DEC_Click" Text="DEC" Width="83px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
