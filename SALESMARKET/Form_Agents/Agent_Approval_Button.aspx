<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_Approval_Button.aspx.cs" Inherits="SALESMARKET.Form_Agents.Agent_Approval_Button" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:Button ID="BT_APPROVAL" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_APPROVAL_Click" Text="APPROVE" Width="120px" />
                    &nbsp;<asp:Button ID="BT_REJECT" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_REJECT_Click" Text="REJECT" Width="120px" />
                    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width:350px;">
                                <asp:Label ID="Label1" runat="server" Text="ALASAN :" CssClass="ASPLabel"></asp:Label><br />
                                <asp:TextBox ID="TXT_REASON" runat="server" CssClass="ASPTextBox" Height="50px" MaxLength="1000" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
