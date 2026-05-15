<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentAcc.aspx.cs" Inherits="LQ.Form_Tools.PaymentAcc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <script src="../Class/Global.js?ver=20220419"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_DISABLE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">PAYABLE ACCOUNT
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 100px">ACCOUNT NO.</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="90%" onkeypress="return CheckNumeric();" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ACCOUNT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="90%" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK</td>
                            <td>
                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList" Width="90%" Enabled="False"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
