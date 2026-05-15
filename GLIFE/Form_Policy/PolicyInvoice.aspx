<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyInvoice.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyInvoice" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        &nbsp;<asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:DropDownList ID="DDL_MODE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_MODE_SelectedIndexChanged">
                        <asp:ListItem Value="1">OUTSTANDING &gt; 0</asp:ListItem>
                        <asp:ListItem Value="2">OUTSTANDING = 0</asp:ListItem>
                    </asp:DropDownList>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td># INVOICE</td>
                                        <td>:</td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_INVOICE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BILLED</td>
                                        <td>:</td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_AMOUNT" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>CREDIT NOTE</td>
                                        <td>:</td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_CREDIT" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DEBET NOTE</td>
                                        <td>:</td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_DEBET" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>PAYMENT AMOUNT</td>
                                        <td>:</td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_PAYMENT" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>OUTSTANDING</td>
                                        <td>:</td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_OUSTANDING" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
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
                        BorderColor="#CCCCCC" CellPadding="4" Font-Names="Tahoma"
                        Font-Size="X-Small" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" AllowPaging="True" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20" Width="100%">
                        <ItemStyle BackColor="#EFF3FB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_INVOICE" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="I" />
                                    <asp:Button ID="BT_RECEIPT" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="R" Visible="False" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE NO.">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_DATE" HeaderText="INVOICE DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE_DESCR" HeaderText="TYPE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MEMBER_NAME" HeaderText="MEMBER NAME">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="PREMIUM GROSS">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NK_AMOUNT" HeaderText="CREDIT NOTE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ND_AMOUNT" HeaderText="DEBET NOTE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAYMENT_AMOUNT" HeaderText="PAYMENT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RECEIPT_URL" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
