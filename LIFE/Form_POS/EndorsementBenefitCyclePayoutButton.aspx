<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementBenefitCyclePayoutButton.aspx.cs" Inherits="LIFE.Form_POS.EndorsementBenefitCyclePayoutButton" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 50%;">
                    <asp:Button ID="BT1" runat="server" Text="DETAIL INSTALLMENT" Width="100%" Font-Size="X-Small" OnClick="BT1_Click" />
                </td>
                <td style="width: 50%;">
                    <asp:Button ID="BT2" runat="server" Text="PAYOUT ITEMS" Width="100%" Font-Size="X-Small" OnClick="BT2_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
