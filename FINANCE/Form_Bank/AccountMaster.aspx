<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountMaster.aspx.cs" Inherits="FINANCE.Form_Bank.AccountMaster" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing:0px;">
            <tr>
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td style="width:100px;">
                                ACC NO</td>
                            <td>
                                <asp:TextBox ID="TXT_NOREK" runat="server" CssClass="ASPTextBox" MaxLength="100"
                                    Width="256px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                BOOK NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_BANK" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="433px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ACC NAME
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="433px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                BANK NAME</td>
                            <td>
                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                BRANCH</td>
                            <td>
                                <asp:TextBox ID="TXT_NAMA_CABANG" runat="server" CssClass="ASPTextBox" MaxLength="255"
                                    Width="433px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ADDRESS 1</td>
                            <td>
                                <asp:TextBox ID="TXT_ALAMAT_1" runat="server" CssClass="ASPTextBox" MaxLength="255"
                                    Width="433px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ADDRESS 2</td>
                            <td>
                                <asp:TextBox ID="TXT_ALAMAT_2" runat="server" CssClass="ASPTextBox" MaxLength="255"
                                    Width="433px"></asp:TextBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td>COLLECTION</td>
                            <td>
                                <asp:DropDownList ID="DDL_COL" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>SETTLEMENT</td>
                            <td>
                                <asp:DropDownList ID="DDL_STL" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ACCOUNT TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_ACCTYPE" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>COA</td>
                            <td>
                                <asp:DropDownList ID="DDL_GLCOA" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="BT_SIMPAN" runat="server" CssClass="ASPButton" OnClick="BT_SIMPAN_Click"
                                    Text="SAVE" Width="76px" />
                                <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20" OnItemCommand="DGR_ItemCommand"
                        GridLines="Vertical" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True" CssClass="ASPDatagrid"
                        AutoGenerateColumns="False">
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="ACC NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Select" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="NOREK" HeaderText="ACC NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK" HeaderText="BOOK NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="ACC NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK_DESCR" HeaderText="BANK NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COL" HeaderText="COL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="STL" HeaderText="STL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COA_DESCR" HeaderText="COA"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_MUTASI" runat="server" BackColor="Green" CommandName="Mutasi" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="$" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <FooterStyle BackColor="Gray" ForeColor="Black" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
