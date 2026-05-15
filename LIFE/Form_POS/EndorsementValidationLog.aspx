<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementValidationLog.aspx.cs" Inherits="LIFE.Form_POS.EndorsementValidationLog" %>

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
            <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="None" PageSize="20" CssClass="ASPDatagrid" Width="80%" Font-Size="Small" ShowHeader="False" BorderWidth="0px">
                <Columns>
                    <asp:BoundColumn DataField="SEQ">
                        <ItemStyle Width="20" ForeColor="Red" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="REMARK">
                        <ItemStyle ForeColor="Red" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                    </asp:BoundColumn>
                </Columns>
                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
            </asp:DataGrid>
        </center>
    </form>
</body>
</html>
