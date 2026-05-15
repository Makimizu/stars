<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimBenTrx.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimBenTrx" MaintainScrollPositionOnPostback="true" %>

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
        <table style="position: absolute; left: 0px; top: 0px; border-spacing: 0px;">
            <tr id="TR_ADDBEN" runat="server">
                <td>

                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:DropDownList ID="DDL_BENEFIT" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_BENEFIT_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="DVBENEFIT" runat="server" style="OVERFLOW: auto; HEIGHT: 100px; width: 100%;">
                                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" CellPadding="3" CssClass="ASPDatagrid" GridLines="None" PageSize="15" AutoGenerateColumns="False" BorderColor="#E7E7FF" OnItemCommand="DGR_BENEFIT_ItemCommand" ShowHeader="False" BackColor="White" BorderStyle="None" BorderWidth="1px" Width="100%">
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LB_BENEFITDETAIL" runat="server" CommandName="Detail"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MORBIDITY_ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DIS_BENEFIT_DETAIL_NAME"></asp:BoundColumn>
                                        </Columns>
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" VerticalAlign="Top" />
                                        <ItemStyle BackColor="#E7E7FF" VerticalAlign="Top" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Width="100px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_PRINT" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_PRINT_Click" Text="P" />
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" CssClass="ASPDatagrid" GridLines="Vertical" ForeColor="#333333" PageSize="15" AutoGenerateColumns="False" BorderColor="#006600" ShowFooter="True" OnItemDataBound="DGR_ItemDataBound" OnItemCommand="DGR_ItemCommand">
                        <Columns>
                            <asp:BoundColumn DataField="RECID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_DETAIL_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_DETAIL_DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_PENGAJUAN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_CASH" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_COVERED" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_UNPAID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_EXCESS" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_REFUND" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_BAYAR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FREQ_DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FREQ_VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CODE">
                                <ItemTemplate>
                                    <asp:Label ID="LB_CODE" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ITEM">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr style="vertical-align: top;">
                                            <td>
                                                <asp:Label ID="LB_DESCR" runat="server" CssClass="ASPLabel"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="250px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="AMOUNT ENTRY">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>
                                                <table style="border-spacing: 0px;">
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 60px;">INCURRED</td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox ID="TXT_PENGAJUAN" runat="server" CssClass="ASPTextBoxNumber" Width="70px" BackColor="Yellow"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>CASH</td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox ID="TXT_CASH" runat="server" CssClass="ASPTextBoxNumber" Width="70px" BackColor="Yellow"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_FREQ" runat="server" CssClass="ASPLabel"></asp:Label>
                                                <asp:TextBox ID="TXT_FREQ" runat="server" CssClass="ASPTextBoxNumber" Width="30px" BackColor="Yellow"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 60px;">INCURRED</td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_PENGAJUAN_TOTAL" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>CASH</td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_CASH_TOTAL" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" ForeColor="Black" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RESULT">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr style="vertical-align: top;">
                                            <td style="width: 60px;">COVERED</td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="TXT_COVERED" runat="server" CssClass="ASPTextBoxNumber" Width="70px" BackColor="Cyan" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>UNPAID</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UNPAID" runat="server" CssClass="ASPTextBoxNumber" Width="70px" BackColor="Pink"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>EXCESS</td>
                                            <td>
                                                <asp:TextBox ID="TXT_EXCESS" runat="server" CssClass="ASPTextBoxNumber" Width="70px" BackColor="Pink" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>REFUND</td>
                                            <td>
                                                <asp:TextBox ID="TXT_REFUND" runat="server" CssClass="ASPTextBoxNumber" Width="70px" BackColor="LightGreen" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>PAID</td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="TXT_PAID" runat="server" CssClass="ASPTextBoxNumber" Width="70px" BackColor="Cyan" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 60px;">UNPAID</td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_UNPAID_TOTAL" runat="server" CssClass="ASPLabel" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>EXCESS</td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_EXCESS_TOTAL" runat="server" CssClass="ASPLabel" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>REFUND</td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_REFUND_TOTAL" runat="server" CssClass="ASPLabel" Font-Bold="true" ForeColor="Green"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>DISCOUNT</td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_DISCOUNT" runat="server" CssClass="ASPLabel" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>PAID</td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_PAID_TOTAL" runat="server" CssClass="ASPLabel" Font-Bold="true" ForeColor="Blue"></asp:Label></td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" ForeColor="Black" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False" HorizontalAlign="Center" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <FooterStyle BackColor="Lime" ForeColor="White" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

