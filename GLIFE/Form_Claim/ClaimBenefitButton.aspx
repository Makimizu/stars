<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimBenefitButton.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimBenefitButton" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="LB_READONLY" runat="server" Visible="false"></asp:Label>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="CLAIM DIAGNOSE" Width="100%" OnClick="BT1_Click" />
                            </td>
                            <td style="width: 50%;">
                                <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="SUM INSURED TIMELINE" Width="100%" OnClick="BT2_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
