<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicalLabButton.aspx.cs" Inherits="UWBOX.Form_Partners.MedicalLabButton" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 33%; text-align:center;">
                                <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="MCU ITEM PRICE" Width="95%" OnClick="BT1_Click" />
                            </td>
                            <td style="width: 33%; text-align:center;">
                                <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="MCU PACKAGE PRICE" Width="95%" OnClick="BT2_Click"  />
                            </td>
                            <td style="width: 33%; text-align:center;">
                                <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Text="ARCHIEVE" Width="95%" OnClick="BT3_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">
                    <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
