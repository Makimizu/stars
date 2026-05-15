<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimDiscount.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimDiscount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_CLAIMNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TRACK" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" />
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="false"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" CssClass="ASPDatagrid" GridLines="Vertical" ForeColor="#333333" PageSize="15" AutoGenerateColumns="False" BorderColor="Maroon">
                        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="true" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" Wrap="true" />
                        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DISCOUNT">
                                <HeaderStyle Width="300px" />
                                <ItemStyle Wrap="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_DISCOUNT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="AMOUNT">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="90px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="PCT" HeaderText="% AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="TYPE">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
