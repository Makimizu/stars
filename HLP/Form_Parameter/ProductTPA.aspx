<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductTPA.aspx.cs" Inherits="HLP.Form_Parameter.ProductTPA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
    <table style="position:absolute; left:0px; top: 0px; border-spacing:0px;">
        <tr>
            <td>

                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" />

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" ItemStyle-Wrap="true" AutoGenerateColumns="False">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" HeaderText="CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="TPA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BASIC_CHG" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EXT1_CHG" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EXT2_CHG" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="BASIC CHARGE">                                
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL1" runat="server" CssClass="ASPTextBoxNumber" Width="130px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="EXT 1 CHARGE">                                
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL2" runat="server" CssClass="ASPTextBoxNumber" Width="130px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="EXT 2 CHARGE">                                
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL3" runat="server" CssClass="ASPTextBoxNumber" Width="130px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

            </td>
        </tr>
    </table>
    </form>
</body>
</html>
