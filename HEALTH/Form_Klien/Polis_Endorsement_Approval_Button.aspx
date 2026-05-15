<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Endorsement_Approval_Button.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Endorsement_Approval_Button" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;vertical-align:middle; height:100%;">
            <tr>
                <td>
                    <center>
                    <asp:Button ID="BT_APPROVE" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" Font-Size="X-Small" ForeColor="White" OnClick="BT_APPROVE_Click" Text="APPROVE" />
                    &nbsp;
                    <asp:Button ID="BT_ROLLBACK" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" Font-Size="X-Small" ForeColor="White" OnClick="BT_ROLLBACK_Click" Text="ROLLBACK" />
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" CssClass="ASPLabel" ForeColor="Red"></asp:Label>
                    </center>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
