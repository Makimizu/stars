<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PesertaInfo_PREMIUM.aspx.cs" Inherits="HEALTH.Form_Member.PesertaInfo_PREMIUM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="LB_PERIOD" runat="server" Visible="False"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 49%;" class="TDBGColor">PREMIUM</td>
                <td></td>
                <td style="width: 49%;" class="TDBGColor">ENDORSEMENT</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:DataGrid ID="DGR_PREMIUM" runat="server" CellPadding="4" PageSize="20" GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" Width="100%">
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="BENEFIT" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM" HeaderText="PREMIUM">
                                <HeaderStyle Width="80" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
                <td></td>
                <td>
                    <asp:DataGrid ID="DGR_ENDORSEMENT" runat="server" CellPadding="4" PageSize="20" GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" Width="100%">
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="BATCH_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENDORSEMENT" HeaderText="ENDORSEMENT"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="BATCH">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_BATCH" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="AR_DATE" HeaderText="AR DATE">
                                <HeaderStyle Width="80" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AP_DATE" HeaderText="AP DATE">
                                <HeaderStyle Width="80" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PACKAGE" HeaderText="PACKAGE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICENO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Width="80" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td class="TDBGColor">INVOICE</td>
                <td></td>
                <td class="TDBGColor"></td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:DataGrid ID="DGR_INVOICE" runat="server" CellPadding="4" PageSize="20" GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" Width="100%">
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="INVOICENO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_INVOICE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="INVOICE NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_INVOICE" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="SEQ" HeaderText="#SEQ">
                                <HeaderStyle Width="40" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_DATE" HeaderText="INVOICE DATE">
                                <HeaderStyle Width="80" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE" HeaderText="INVOICE TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM" HeaderText="PREMIUM">
                                <HeaderStyle Width="80" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                <HeaderStyle Width="80" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
