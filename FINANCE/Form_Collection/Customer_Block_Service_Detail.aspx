<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer_Block_Service_Detail.aspx.cs" Inherits="FINANCE.Form_Collection.Customer_Block_Service_Detail" %>

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
                            <td style="width: 130px;">APPLICATION</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="LB_APPDESCR" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>POLICY NO</td>
                            <td>:</td>
                            <td><asp:Label ID="LB_NO" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>COMPANY</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>TOTAL OUTSTANDING</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="LB_OUTSTANDING" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_APP" runat="server" CssClass="ASPLabel" Visible="false"></asp:Label>                    
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" BorderColor="#003300" CellPadding="4" ForeColor="#333333" GridLines="Vertical">
                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" Wrap="false" />
                        <Columns>
                            <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE_DESCR" HeaderText="TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PAYMENT_AMOUNT" HeaderText="PAYMENT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_DATE" HeaderText="INVOICE DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="false" />
                        <HeaderStyle
                            Wrap="False" BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
