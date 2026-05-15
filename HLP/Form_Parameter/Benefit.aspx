<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Benefit.aspx.cs" Inherits="HLP.Form_Parameter.Benefit" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">BENEFIT</td>
                            <td>
                                <asp:DropDownList ID="DDL_BENEFIT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_BENEFIT_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>KODE</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NAMA BENEFIT</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NAMA BENEFIT (ENG)</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCR_ENG" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>UNIT</td>
                            <td>
                                <asp:DropDownList ID="DDL_UNIT" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>MAX</td>
                            <td>
                                <asp:TextBox ID="TXT_MAX" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="40px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_INSERT" runat="server" CssClass="ASPButton" OnClick="BT_INSERT_Click" Text="TAMBAH BARU" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" HeaderText="KODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR_ENG" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNIT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MAX" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="BENEFIT">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>IND</td>
                                            <td>
                                                <asp:TextBox ID="TXT_DSCR" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="400px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>ENG</td>
                                            <td>
                                                <asp:TextBox ID="TXT_DSCR_ENG" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="400px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="UNIT">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_UNT" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="MAX">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_MX" runat="server" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" CommandName="Save" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
