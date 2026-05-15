<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductFamilyPlan.aspx.cs" Inherits="HLP.Form_Parameter.ProductFamilyPlan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="LB_BENEFITID" runat="server" Visible="False"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%;" class="TDBGColor">UNSELECTED PLAN</td>
                            <td style="width: 50%;" class="TDBGColor">SELECTED PLAN</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <div style="width: 100%; height: 100px; overflow: auto;">
                                    <asp:DataGrid ID="DGR_UNSELECTED" runat="server" BackColor="White"
                                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="2" Font-Size="X-Small" GridLines="None"
                                        PageSize="20" AutoGenerateColumns="False" OnItemCommand="DGR_UNSELECTED_ItemCommand" ShowHeader="False" Width="100%">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PLAN_VALUE" HeaderText="PLAN"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle Width="80" />
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_SELECT1" runat="server" Text="SELECT" CssClass="ASPButton" Width="80" CommandName="Select" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                            Mode="NumericPages" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                            <td>
                                <div style="width: 100%; height: 100px; overflow: auto;">
                                    <asp:DataGrid ID="DGR_SELECTED" runat="server" BackColor="White"
                                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="2" Font-Size="X-Small" GridLines="None"
                                        PageSize="20" AutoGenerateColumns="False" OnItemCommand="DGR_SELECTED_ItemCommand" ShowHeader="False" Width="100%">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PLAN_VALUE" HeaderText="PLAN"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" CommandName="Delete" BackColor="Red" ForeColor="White" />
                                                    <asp:Button ID="BT_BENEFIT" runat="server" Text="BENEFIT" CssClass="ASPButton" CommandName="Benefit" />
                                                    <asp:Button ID="BT_PREMIUM" runat="server" Text="PREMIUM" CssClass="ASPButton" CommandName="Premium" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                            Mode="NumericPages" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border-top: ridge;">
                    <table id="TBL_FAMILYPLAN" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">BENEFIT PLAN</td>
                                        <td>
                                            <asp:Label ID="LB_BENEFITPLAN" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PLAN VALUE</td>
                                        <td>
                                            <asp:Label ID="LB_PLANCODE" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LB_PLANVALUE" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TR_BENEFIT" runat="server" visible="false">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 50%;" class="TDBGColor">UNSELECTED BENEFIT</td>
                                        <td style="width: 50%;" class="TDBGColor">SELECTED BENEFIT</td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:DataGrid ID="DGR_BENEFIT_UNSELECTED" runat="server" BackColor="White"
                                                    BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="2" Font-Size="X-Small" GridLines="None"
                                                    PageSize="20" AutoGenerateColumns="False" ShowHeader="False" Width="100%" OnItemCommand="DGR_BENEFIT_UNSELECTED_ItemCommand">
                                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="CODE">
                                                            <ItemStyle Width="40" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="BT_SELECT" runat="server" Text="V" CssClass="ASPButton" CommandName="Select" BackColor="Green" ForeColor="White" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                                        Mode="NumericPages" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:DataGrid ID="DGR_BENEFIT_SELECTED" runat="server" BackColor="White"
                                                    BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="2" Font-Size="X-Small" GridLines="None"
                                                    PageSize="20" AutoGenerateColumns="False" ShowHeader="False" Width="100%" OnItemCommand="DGR_BENEFIT_SELECTED_ItemCommand">
                                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="CODE">
                                                            <ItemStyle Width="40" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="BT_DELETE" runat="server" Text="X" CssClass="ASPButton" CommandName="Delete" BackColor="Red" ForeColor="White" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                                        Mode="NumericPages" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TR_PREMIUM" runat="server" visible="false">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td class="TDBGColor">PREMIUM & SUM INSURED</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="DGR_PREMIUM" runat="server" BackColor="White"
                                                BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="2" Font-Size="X-Small" GridLines="None"
                                                PageSize="20" AutoGenerateColumns="False" Width="500px">
                                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" VerticalAlign="Top" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="MARITAL_STATUS_CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="MARITAL_STATUS_DESCR" HeaderText="MARITAL STATUS">
                                                        <ItemStyle Width="150" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PREMIUM" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="MAX_ANNUAL_SUMINS" Visible="False"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="PREMIUM">
                                                        <HeaderStyle Width="100" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_PREMIUM" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="MAX ANNUAL<BR>SUM INSURED">
                                                        <HeaderStyle Width="100" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_SUMINS" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                                    Mode="NumericPages" />
                                            </asp:DataGrid>
                                            <asp:Button ID="BT_SAVE_PREMIUM" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_PREMIUM_Click" Text="SAVE" Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
