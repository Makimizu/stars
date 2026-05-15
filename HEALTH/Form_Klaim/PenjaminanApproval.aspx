<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenjaminanApproval.aspx.cs" Inherits="HEALTH.Form_Klaim.PenjaminanApproval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <center>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <center>
                                <asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Green" OnClick="BT_APPROVE_Click" Text="APPROVE" />
                                &nbsp;<asp:Button ID="BT_REJECT" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" OnClick="BT_REJECT_Click" Text="REJECT" />
                                                        <asp:Label ID="LB_ID" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                                            <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <center>
                                <asp:Label ID="Label19" runat="server" Text="ALASAN REJECT :" Font-Bold="True" CssClass="ASPLabel"></asp:Label><br />
                                <asp:TextBox ID="TXT_REJECT" runat="server" CssClass="ASPTextBox" Width="500px" MaxLength="1000" Height="44px" BackColor="Yellow" TextMode="MultiLine"></asp:TextBox>
                                </center>
                            </td>
                        </tr>
                    </table>
                    </center>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
