<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Policy_Finance.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Health.Policy_Finance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_PERIOD" runat="server" Visible="False"></asp:Label>
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td style="width:100px;">PREMIUM EARNED</td><td>:</td>
                            <td style="width:100px; text-align:right;">
                                <asp:Label ID="LB_RSV_EARNED" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>% LOADING</td><td>:</td>
                            <td style="text-align:right;">
                                <asp:Label ID="LB_LOADINGPCT" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>% TABARRU</td><td>:</td>
                            <td style="text-align:right;">
                                <asp:Label ID="LB_TABARRUPCT" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>LOADING AMOUNT</td><td>:</td>
                            <td style="text-align:right;">
                                <asp:Label ID="LB_LOADINGAMT" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>TABBARU AMOUNT</td><td>:</td>
                            <td style="text-align:right;">
                                <asp:Label ID="LB_TABARRUAMT" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>CLAIM</td><td>:</td>
                            <td style="text-align:right;">
                                <asp:Label ID="LB_CLAIM" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>REFUND PREMIUM</td><td>:</td>
                            <td style="text-align:right;">
                                <asp:Label ID="LB_REFUNDPRM" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>RESERVE BALANCE</td><td>:</td>
                            <td style="text-align:right;">
                                <asp:Label ID="LB_BALANCE" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server"
                        BackColor="White"
                        BorderColor="#3366CC" BorderStyle="None"
                        BorderWidth="1px" CellPadding="4" Font-Names="Tahoma"
                        Font-Size="X-Small" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True">
                        <ItemStyle BackColor="White" ForeColor="#003399" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_INVOICE" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="I" />
                                    <asp:Button ID="BT_RECEIPT" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="R" Visible="False" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_DATE" HeaderText="TGL INVOICE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE_DESCR" HeaderText="TIPE INVOICE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="TAGIHAN">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#006600" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#006600" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NK_AMOUNT" HeaderText="NOTA KREDIT">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#0033CC" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#0033CC" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ND_AMOUNT" HeaderText="NOTA DEBET">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#FF9900" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#FF9900" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAYMENT_AMOUNT" HeaderText="PEMBAYARAN">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#000099" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#000099" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_INVOICE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_RECEIPT" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>

