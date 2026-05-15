<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Endorsement_Package.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Endorsement_Package" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:Button ID="BT_ADD" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_ADD_Click" Text="TAMBAH PAKET" />
                    &nbsp;<asp:Button ID="BT_SAVE" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_SAVE_Click" Text="SAVE PAKET" />
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="ASPDatagrid" ForeColor="#333333" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" OnItemDataBound="DGR_ItemDataBound">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN01" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN02" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN03" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN04" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN05" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN06" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN07" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN08" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN09" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN10" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN11" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN12" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN13" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN14" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN15" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN16" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN17" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN18" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN19" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN20" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PAKET">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PACKAGE" runat="server" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN1" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN1" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN1" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN2" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN2" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN2" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN3" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN3" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN3" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN4" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN4" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN4" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN5" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN5" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN5" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN6" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN6" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN6" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN7" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN7" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN7" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN8" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN8" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN8" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN9" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN9" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN9" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN10" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN10" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN10" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN11" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN11" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN11" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN12" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN12" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN12" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN13" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN13" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN13" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN14" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN14" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN14" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN15" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN15" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN15" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN16" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN16" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN16" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN17" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN17" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN17" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN18" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN18" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN18" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN19" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN19" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN19" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="LB_BEN20" runat="server" CssClass="ASPLabel"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLAN20" runat="server" CssClass="ASPTextBox" style="text-align:center" Width="40px"></asp:TextBox>
                                    <asp:Label ID="LB_PLAN20" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                    <asp:Label ID="LB_ERROR" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <br />
                    <asp:Button ID="BT_SAVEBEN" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_SAVEBEN_Click" Text="SAVE BENEFIT" />
                    <br />
                    <asp:DataGrid ID="DGR_BEN" runat="server" CellPadding="4" PageSize="40" GridLines="None" CssClass="ASPDatagrid" ForeColor="#333333" AutoGenerateColumns="False">
                        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <ItemStyle BackColor="#FFFBD6" Wrap="False" HorizontalAlign="Center" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" Wrap="False" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_PCT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DISCOUNT_PCT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVIDER" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REIMBURSE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ASO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="BENEFIT (%)">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_BENPCT" runat="server" CssClass="ASPTextBoxNumber" style="margin-bottom: 0px" Width="50px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DISCOUNT (%)">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_DISCPCT" runat="server" CssClass="ASPTextBoxNumber" style="margin-bottom: 0px" Width="50px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PROVIDER">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB_P" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="REIMBURSE">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB_R" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ASO">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB_ASO" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="REMARK">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" Height="80px" MaxLength="4000" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
