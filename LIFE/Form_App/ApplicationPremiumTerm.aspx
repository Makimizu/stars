<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPremiumTerm.aspx.cs" Inherits="LIFE.Form_App.ApplicationPremiumTerm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:Button ID="BT_PREMIUM" runat="server" CssClass="ASPButton" Text="PREMIUM SCHEDULE" Width="100%" OnClick="BT_PREMIUM_Click" />
                    <asp:Button ID="BT_BENEFIT" runat="server" CssClass="ASPButton" Text="BENEFIT SCHEDULE" Width="100%" OnClick="BT_BENEFIT_Click" />
                    <asp:Button ID="BT_COI" runat="server" CssClass="ASPButton" Text="COI" Width="100%" OnClick="BT_COI_Click" />
                    <asp:Button ID="BT_TRX" runat="server" CssClass="ASPButton" Text="REALIZATION" Width="100%" OnClick="BT_TRX_Click" />
                    <asp:Button ID="BT_STAT" runat="server" CssClass="ASPButton" Text="PREMIUM STATISTIC" Width="100%" OnClick="BT_STAT_Click" />
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
                        </tr>
                    </table>

                    <asp:DataGrid ID="DGR" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="THEDATE" HeaderText="DUE DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRANS_TYPE" HeaderText="TRANSACTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FOP" HeaderText="FOP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SETTLE_DATE" HeaderText="SETTLE DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SETTLE_AMOUNT" HeaderText="SETTLE AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="WAIVED">
                                <ItemStyle Width="60" HorizontalAlign="Right" ForeColor="Red" Font-Bold="true" />
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <table id="TBL_BENEFIT" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <asp:DataGrid ID="DGR_BENEFIT" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="INSURED_STATUS" HeaderText="INSURED STATUS"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="THEDATE" HeaderText="DUE DATE">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PCT_VAL" HeaderText="% BENEFIT">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT_VAL" HeaderText="AMOUNT BENEFIT">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TOBEPAID" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SETTLE_DATE" HeaderText="SETTLE DATE">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SETTLE_AMOUNT" HeaderText="SETTLE AMOUNT">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DEATH" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SEQ" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="TOBE PAID">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </td>
                            <td style="vertical-align: top">
                                <asp:DataGrid ID="DGR_BENEFIT_DEAD" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#990000" ForeColor="White" VerticalAlign="Top" />
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#FFFBD6" ForeColor="#333333" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="INSURED_STATUS" HeaderText="INSURED STATUS"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="THEDATE" HeaderText="DUE DATE">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PCT_VAL" HeaderText="% BENEFIT">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT_VAL" HeaderText="AMOUNT BENEFIT">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TOBEPAID" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SETTLE_DATE" HeaderText="SETTLE DATE">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SETTLE_AMOUNT" HeaderText="SETTLE AMOUNT">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DEATH" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SEQ" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="TOBE PAID">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>

                    <asp:DataGrid ID="DGR_COI" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="THEDATE" HeaderText="COI DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUMINS" HeaderText="SUM INSURED">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE" HeaderText="&permil; RATE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COI" HeaderText="COI">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <iframe id="IF" runat="server" style="width: 98%; height: 98vh; overflow: auto;"></iframe>
                    <table id="TBL_STAT" runat="server" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <center>
                                    <br />
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>PAYMENT PERIOD</td>
                                            <td style="text-align:right;">
                                                <asp:Label ID="LB_TENOR" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px;">LENGTH OF MEMBERSHIP</td>
                                            <td style="width: 100px;text-align:right;">
                                                <asp:Label ID="LB_LOM" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>REMAINING PAYMENT PERIOD</td>
                                            <td style="text-align:right; color:red;">
                                                <asp:Label ID="LB_REMAINING" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>ANNUAL PREMIUM</td>
                                            <td style="text-align:right; color:green;">
                                                <asp:Label ID="LB_ANNUAL_PREMIUM" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </td>
                            <td style="width: 50%;">
                                <center>
                                    <br />
                                    <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 150px;"></td>
                                        <td style="width: 100px;" class="TDBGColor">#TERM</td>
                                        <td style="width: 100px;" class="TDBGColor">AMOUNT</td>
                                    </tr>
                                    <tr>
                                        <td>MONTH TO DATE OVERDUE</td>
                                        <td style="text-align: center; color: blue;">
                                            <asp:Label ID="LB_MTD_TERM_EXPECTED" runat="server"></asp:Label></td>
                                        <td style="text-align: right; color: green;">
                                            <asp:Label ID="LB_MTD_AMOUNT_EXPECTED" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>MONTH TO DATE PAID</td>
                                        <td style="text-align: center; color: blue;">
                                            <asp:Label ID="LB_MTD_TERM_PAID" runat="server"></asp:Label></td>
                                        <td style="text-align: right; color: green;">
                                            <asp:Label ID="LB_MTD_AMOUNT_PAID" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr style="color: red;">
                                        <td>MONTH TO DATE OUTSTANDING</td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="LB_MTD_TERM_OUTSTANDING" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_MTD_AMOUNT_OUTSTANDING" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>TOTAL EXPECTED</td>
                                        <td style="text-align: center; color: blue;">
                                            <asp:Label ID="LB_TOTAL_TERM_EXPECTED" runat="server"></asp:Label></td>
                                        <td style="text-align: right; color: green;">
                                            <asp:Label ID="LB_TOTAL_AMOUNT_EXPECTED" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>TOTAL PAID</td>
                                        <td style="text-align: center; color: blue;">
                                            <asp:Label ID="LB_TOTAL_TERM_PAID" runat="server"></asp:Label></td>
                                        <td style="text-align: right; color: green;">
                                            <asp:Label ID="LB_TOTAL_AMOUNT_PAID" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr style="color: red;">
                                        <td>TOTAL INCOMPLETED</td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="LB_TOTAL_TERM_INCOMPLETED" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_TOTAL_AMOUNT_INCOMPLETED" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                </center>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
