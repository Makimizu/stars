<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimValidationLog.aspx.cs" Inherits="LIFE.Form_Claim.ClaimValidationLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <center>
        <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="2" GridLines="None" PageSize="20" CssClass="ASPDatagrid" Width="80%" Font-Size="Small" ShowHeader="False" CellSpacing="2">
            <Columns>
                <asp:BoundColumn DataField="SEQ">
                    <ItemStyle Width="20" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REMARK">
                    <ItemStyle ForeColor="Red" Font-Bold="true" />
                </asp:BoundColumn>
            </Columns>
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" />
        </asp:DataGrid>
            <br />
            <asp:Button ID="BT_BACK" runat="server" CssClass="ASPButton" Text="BACK" BackColor="Red" ForeColor="White" Width="100" OnClick="BT_BACK_Click" />
        </center>
    </form>
</body>
</html>
