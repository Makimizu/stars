<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DATA_COMMISSION_PERPERIOD.aspx.cs" Inherits="AGR.Form_Data.DATA_COMMISSION_PERPERIOD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_CD" runat="server" Visible="false"></asp:Label>
        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                                    <tr>
                                        <td style="width: 100px;">MARKET SEGMENT</td>
                                        <td>
                                            <asp:Label ID="LB_MARKET_SEGMENT" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>START DATE</td>
                                        <td>
                                            <asp:Label ID="LB_STARTDATE" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>END DATE</td>
                                        <td>
                                            <asp:Label ID="LB_ENDDATE" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                                    <tr>
                                        <td style="width: 100px;">AGENT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="98%" AutoPostBack="true" OnTextChanged="TXT_FULLNAME_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>AGENT LEVEL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_LEVEL" runat="server" CssClass="ASPTextBox" Width="98%" AutoPostBack="true" OnTextChanged="TXT_LEVEL_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>AGENCY</td>
                                        <td>
                                            <asp:TextBox ID="TXT_AGENCY" runat="server" CssClass="ASPTextBox" Width="98%" AutoPostBack="true" OnTextChanged="TXT_AGENCY_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="2" style="color: #333333; font-size: X-Small; font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;">
            <tbody>
                <tr valign="top" style="color: #000000; background-color: #cfd4f2; font-size: XX-Small;">
                    <td colspan="7">Total Record : <asp:Label ID="LB_RECORDS" runat="server" Style="font-size: xx-small"></asp:Label></td>                    
                    <td align="right"><asp:HyperLink ID="ExportLink" runat="server" NavigateUrl="../../ReportViewer/Viewer.aspx?APPID=AGR&CODE=49" Font-Bold="True">Export</asp:HyperLink> </td>
                    <td align="center" style="font-weight:bold;">&nbsp;&nbsp;</td>
                </tr>
            </tbody>
        </table>
        <asp:DataGrid ID="DGR_PERIOD" runat="server" CellPadding="2" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%" AllowPaging="True" OnPageIndexChanged="DGR_PERIOD_PageIndexChanged">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="AGENT_CODE" HeaderText="AGENT CODE"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENCY_NAME" HeaderText="AGENCY NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="GROSS_AMOUNT" HeaderText="GROSS AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TAX" HeaderText="TAX">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OTH" HeaderText="OTHER DEDUCTION">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NETT" HeaderText="NETT AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="HOLD" HeaderText="HOLD AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Black" />
                </asp:BoundColumn>
            </Columns>
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Top" Font-Size="XX-Small" />
            <ItemStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
        </asp:DataGrid>
    </form>
</body>
</html>
