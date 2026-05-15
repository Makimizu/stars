<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice_Auto_Settle.aspx.cs" Inherits="FINANCE.Form_Collection.Invoice_Auto_Settle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">METODA MATCH</td>
                            <td>
                                <asp:DropDownList ID="DDL_METODA" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >TIPE INVOICE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TIPEINV" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="3">PREMIUM</asp:ListItem>
                                    <asp:ListItem Value="4">EXCESS</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >NO POLIS</td>
                            <td>
                                <asp:TextBox ID="TXT_NOPOL" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >PERUSAHAAN</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >CABANG</td>
                            <td>
                                <asp:TextBox ID="TXT_BRANCH" runat="server" CssClass="ASPTextBox" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>SHORT BY</td>
                            <td>
                                <asp:DropDownList ID="DDL_SHORTITEM" runat="server" CssClass="ASPDropDownList" AutoPostBack="true"></asp:DropDownList>
                                <asp:DropDownList ID="DDL_SHORT" runat="server" CssClass="ASPDropDownList" AutoPostBack="true">
                                    <asp:ListItem Value="DESC">DESCENDING</asp:ListItem>
                                    <asp:ListItem Value="ASC">ASCENDING</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_CARI" runat="server" CssClass="ASPButton" OnClick="BT_CARI_Click" Text="CARI" />
                            &nbsp;<asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_SETTLE" runat="server" BackColor="#CCFF99" CssClass="ASPButton" Font-Bold="True" ForeColor="#006600" OnClick="BT_SETTLE_Click" Text="SETTLE CHECKED ITEMS" Visible="False" />
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20"
                        GridLines="Vertical" CssClass="ASPDatagrid" OnItemCommand="DGR_ItemCommand" AutoGenerateColumns="False">
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <Columns>
                            <asp:TemplateColumn>                               
                                <HeaderTemplate>
                                    <asp:Button ID="BT_ALL" runat="server" CommandName="All" CssClass="ASPButton" Font-Bold="True" Text="ALL" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="INVOICENO" HeaderText="NO INVOICE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_DATE" HeaderText="TGL INVOICE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PERUSAHAAN">
                                <ItemTemplate>
                                    <asp:Label ID="LB_COMPANY" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CUSTOMER_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VACC" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CABANG" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_INVOICE" HeaderText="TIPE INVOICE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TAGIHAN" HeaderText="TAGIHAN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUT<BR>STANDING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE RK">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" BackColor="#006600" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#003300" BackColor="#CCFFCC" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RK_DATE" HeaderText="TGL RK">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" BackColor="#006600" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" BackColor="#CCFFCC" ForeColor="#003300" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="KETERANGAN_RK" HeaderText="BERITA RK" ItemStyle-Wrap="True" >
                                <HeaderStyle Width="400px" BackColor="#006600" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#CCFFCC" ForeColor="#003300" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RK_ID" HeaderText="ID RK">
                                <HeaderStyle BackColor="#006600" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#003300" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <ItemStyle Wrap="False" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
