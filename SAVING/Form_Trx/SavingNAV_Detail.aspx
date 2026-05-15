<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SavingNAV_Detail.aspx.cs" Inherits="SAVING.Form_Trx.SavingNAV_Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
            PageSize="40" Width="100%" ItemStyle-Wrap="true" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" ForeColor="#333333">
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#E3EAEB" Wrap="True" VerticalAlign="Top" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        </asp:DataGrid>

    </form>
</body>
</html>
