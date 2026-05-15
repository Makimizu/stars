<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductGroupEndorsement.aspx.cs" Inherits="UWBOX.Form_TC.ProductGroupEndorsement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 400px; text-align: left;" class="TDBGColor">PRODUCT GROUP</td>
                <td style="text-align: left;" class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:DataGrid ID="DGR_GROUP" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False"
                        Width="100%" GridLines="None"
                        PagerStyle-Mode="NextPrev"
                        PagerStyle-Position="Bottom"
                        PagerStyle-HorizontalAlign="Center"
                        PagerStyle-NextPageText="Next"
                        PagerStyle-PrevPageText="Prev" OnItemCommand="DGR_GROUP_ItemCommand" ShowHeader="False" CellSpacing="2">
                        <ItemStyle VerticalAlign="Top" />
                        <HeaderStyle
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="PAYDI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNITIZE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEGMENT"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle />
                    </asp:DataGrid>
                </td>
                <td>
                    <asp:DataGrid ID="DGR_ENDORSEMENT" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" GridLines="None"
                        PagerStyle-Mode="NextPrev"
                        PagerStyle-Position="Bottom"
                        PagerStyle-HorizontalAlign="Center"
                        PagerStyle-NextPageText="Next"
                        PagerStyle-PrevPageText="Prev" ShowHeader="False">
                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENDORSEMENT_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_GROUP_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR">
                                <ItemStyle Width="300" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="GROUP_DESCR"></asp:BoundColumn>
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
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
