<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadingCN.aspx.cs" Inherits="GLIFE.Form_Parameter.LoadingCN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False"
            CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
            GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" OnItemCommand="DGR_ItemCommand">
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundColumn DataField="LOADING_CODE" HeaderText="LOADING CODE"></asp:BoundColumn>
                <asp:BoundColumn DataField="LOADING_DESCR" HeaderText="LOADING NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="CN_DESCR" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="CREDIT NOTE">
                    <ItemTemplate>
                        <asp:Label ID="LB_CN" runat="server"></asp:Label>
                        <asp:DropDownList ID="DDL_CN" runat="server" CssClass="ASPDropDownList" Visible="false"></asp:DropDownList>
                        <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" CommandName="Save" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        </asp:DataGrid>


    </form>
</body>
</html>
