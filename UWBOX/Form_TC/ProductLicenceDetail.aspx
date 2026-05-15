<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductLicenceDetail.aspx.cs" Inherits="UWBOX.Form_TC.ProductLicenceDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LICENCE_NO" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 150px;">PRODUCT GROUP</td>
                <td>
                    <asp:DropDownList ID="DDL_GROUP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True"
                        OnSelectedIndexChanged="DDL_GROUP_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SAVE" CssClass="ASPButton" runat="server" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" /></td>
            </tr>
        </table>
        <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
        <asp:Label ID="LB_STATUS" runat="server" Font-Bold="true"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="5" AutoGenerateColumns="False"
            Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" OnPageIndexChanged="DGR_PageIndexChanged"
            PagerStyle-Mode="NextPrev"
            PagerStyle-Position="Bottom"
            PagerStyle-HorizontalAlign="Center"
            PagerStyle-NextPageText="Next"
            PagerStyle-PrevPageText="Prev">
            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle
                Wrap="False" BackColor="#1C5E55" ForeColor="White" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="PRODUCT_CODE" HeaderText="PRODUCT CODE"></asp:BoundColumn>
                <asp:BoundColumn DataField="PRODUCT_DESCR" HeaderText="PRODUCT NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="START_DATE" HeaderText="START_DATE"></asp:BoundColumn>
                <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                <asp:BoundColumn DataField="STAT" HeaderText="STATUS"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="30" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CB" runat="server" OnCheckedChanged="CB_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        </asp:DataGrid>
    </form>
</body>
</html>
