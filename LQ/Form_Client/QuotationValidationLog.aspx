<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationValidationLog.aspx.cs" Inherits="LQ.Form_Client.QuotationValidationLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <center>
        <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="2" GridLines="None" PageSize="20" CssClass="ASPDatagrid" Width="80%" Font-Size="Small" ShowHeader="False" CellSpacing="2">
            <Columns>
                <asp:BoundColumn DataField="SEQ">
                    <ItemStyle Width="40" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REMARK">
                    <ItemStyle ForeColor="Red" Font-Bold="true" />
                </asp:BoundColumn>
            </Columns>
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
        </asp:DataGrid>
        </center>
    </form>
</body>
</html>
