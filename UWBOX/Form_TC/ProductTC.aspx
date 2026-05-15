<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductTC.aspx.cs" Inherits="UWBOX.Form_TC.ProductTC" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle Wrap="False" BackColor="#1C5E55" ForeColor="White" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="URL_LINK_ONLY" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="URL_LINK" HeaderText="TC NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="TC_CODE" HeaderText="TC CODE">
                    <ItemStyle Width="50" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="30" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        </asp:DataGrid>
    </form>
</body>
</html>
