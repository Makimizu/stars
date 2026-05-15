<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationReportHeader.aspx.cs" Inherits="LQ.Form_Client.QuotationReportHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <!-- Bootstrap core CSS-->
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Page level plugin CSS-->
    <link href="../include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="../include/css/sb-admin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_LIST" runat="server" Visible="false"></asp:Label>
        <asp:DataGrid ID="DGR_BUTTON" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="98%" ShowHeader="False" OnItemCommand="DGR_BUTTON_ItemCommand">
            <HeaderStyle VerticalAlign="Top" />
            <ItemStyle VerticalAlign="Top" Font-Size="XX-Small" />
            <Columns>
                <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="BT_PRINT" runat="server" CssClass="ASPButton" CommandName="Print" Width="90%" BackColor="Green" ForeColor="White" BorderColor="Gray"/>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </form>
</body>
</html>
