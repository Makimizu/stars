<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Endorsement_Premium.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Endorsement_Premium" %>

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
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="3" PageSize="40" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" Wrap="False" HorizontalAlign="Center" ForeColor="#4A3C8C" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_PERIOD_PACKAGE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_PERIOD_BENEFIT_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_PERIOD_PACKAGE_SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PACKAGE_DESCR" HeaderText="PAKET">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_DESCR" HeaderText="BENEFIT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN_CODE" HeaderText="PLAN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PRIA">
                                <ItemTemplate>
                                    <asp:DataGrid ID="DGR_M" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="ASPDatagrid" GridLines="Horizontal" PageSize="40" OnItemCommand="DGR_M_ItemCommand">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <HeaderStyle Font-Bold="true" BackColor="#006600" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MIN_AGE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MAX_AGE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PREMIUM" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="POLICY_PERIOD_PACKAGE_PLAN_ID" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="MIN AGE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MINAGE" runat="server" Style="text-align: center" CssClass="ASPTextBox" Width="20px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="MAX AGE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MAXAGE" runat="server" Style="text-align: center" CssClass="ASPTextBox" Width="20px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PREMIUM">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PREMIUM" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_ADD" runat="server" BackColor="Blue" CommandName="Add" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="+" />
                                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    </asp:DataGrid>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="WANITA">
                                <ItemTemplate>
                                    <asp:DataGrid ID="DGR_F" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="ASPDatagrid" GridLines="Horizontal" PageSize="40" OnItemCommand="DGR_F_ItemCommand">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <HeaderStyle Font-Bold="true" BackColor="Pink" ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MIN_AGE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MAX_AGE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PREMIUM" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="POLICY_PERIOD_PACKAGE_PLAN_ID" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="MIN AGE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MINAGE" runat="server" Style="text-align: center" CssClass="ASPTextBox" Width="20px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="MAX AGE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MAXAGE" runat="server" Style="text-align: center" CssClass="ASPTextBox" Width="20px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PREMIUM">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PREMIUM" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_ADD" runat="server" BackColor="Blue" CommandName="Add" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="+" />
                                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    </asp:DataGrid>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ANAK PRIA">
                                <ItemTemplate>
                                    <asp:DataGrid ID="DGR_C" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="ASPDatagrid" GridLines="Horizontal" PageSize="40" OnItemCommand="DGR_C_ItemCommand">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <HeaderStyle Font-Bold="true" BackColor="#006600" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MIN_AGE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MAX_AGE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PREMIUM" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="POLICY_PERIOD_PACKAGE_PLAN_ID" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="MIN AGE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MINAGE" runat="server" Style="text-align: center" CssClass="ASPTextBox" Width="20px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="MAX AGE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MAXAGE" runat="server" Style="text-align: center" CssClass="ASPTextBox" Width="20px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PREMIUM">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PREMIUM" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_ADD" runat="server" BackColor="Blue" CommandName="Add" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="+" />
                                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    </asp:DataGrid>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ANAK WANITA">
                                <ItemTemplate>
                                    <asp:DataGrid ID="DGR_D" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="ASPDatagrid" GridLines="Horizontal" PageSize="40" OnItemCommand="DGR_D_ItemCommand">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <HeaderStyle Font-Bold="true" BackColor="Pink" ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MIN_AGE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MAX_AGE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PREMIUM" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="POLICY_PERIOD_PACKAGE_PLAN_ID" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="MIN AGE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MINAGE" runat="server" Style="text-align: center" CssClass="ASPTextBox" Width="20px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="MAX AGE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MAXAGE" runat="server" Style="text-align: center" CssClass="ASPTextBox" Width="20px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PREMIUM">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PREMIUM" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_ADD" runat="server" BackColor="Blue" CommandName="Add" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="+" />
                                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    </asp:DataGrid>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
