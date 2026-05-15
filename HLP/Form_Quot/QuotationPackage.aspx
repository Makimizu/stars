<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationPackage.aspx.cs" Inherits="HLP.Form_Quot.QuotationPackage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Script/PleaseWait.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="LB_QUOTNO" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_VER" runat="server" Visible="False"></asp:Label>
                    <asp:DataGrid ID="DGR0" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" AutoGenerateColumns="False" OnItemDataBound="DGR0_ItemDataBound" OnItemCommand="DGR0_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="PKG_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="PAKET">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PKG_NAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="NAMA PAKET">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PKGNAME0" runat="server" CssClass="ASPTextBox" MaxLength="20"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="PPCODE1" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE2" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE3" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE4" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE5" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE6" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE7" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE8" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE9" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE10" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE11" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE12" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE13" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE14" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE15" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE16" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE17" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE18" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE19" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE20" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="1">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_1" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="2">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_2" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="3">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_3" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="4">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_4" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="5">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_5" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="6">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_6" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="7">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_7" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="8">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_8" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="9">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_9" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="10">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_10" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="11">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_11" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="12">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_12" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="13">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_13" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="14">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_14" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="15">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_15" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="16">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_16" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="17">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_17" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="18">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_18" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="19">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_19" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="20">                                
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_20" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Button ID="BT_SAVEALL" runat="server" CommandName="SaveAll" CssClass="ASPButton" Text="SAVE ALL" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="BT_SAVE0" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />                                
                                    <asp:Button ID="BT_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Text="CLEAR"  Font-Bold="True" ForeColor="Red" />
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
