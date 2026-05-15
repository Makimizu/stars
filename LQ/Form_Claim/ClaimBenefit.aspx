<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimBenefit.aspx.cs" Inherits="LQ.Form_Claim.ClaimBenefit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <script src="../Class/Global.js?ver=20220419"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <asp:Button ID="BT_RISK" runat="server" CssClass="ASPButton" Text="RISK BENEFIT" Width="100%" OnClick="BT_RISK_Click" />
                            </td>
                            <td style="width: 50%;">
                                <asp:Button ID="BT_INV" runat="server" CssClass="ASPButton" Text="SAVING BENEFIT" Width="100%" OnClick="BT_INV_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="TR_RISK" runat="server">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_BENEFIT" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" Enabled="False">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="INCURRED_AMOUNT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT<BR>INSURED PERSON">
                                            <ItemStyle Width="250" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SUMINS" HeaderText="SUM<BR>INSURED">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BENEFIT_PCT" HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CALCULATED_AMOUNT" HeaderText="CALCULATED<BR>AMOUNT">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="INCURRED<BR>AMOUNT">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_AMOUNT" runat="server" Width="100px" BackColor="#d2e9ff" Style="FONT-FAMILY: Tahoma; FONT-SIZE: xx-small; text-align: right; -webkit-border-radius: 5px; -moz-border-radius: 5px;"
                                                    onkeypress="return CheckNumeric();" onkeyup="FormatCurrency(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>

                                <asp:DataGrid ID="DGR_BENEFIT_PAYOR" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" ShowHeader="False" Enabled="False">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CHECKED" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT<BR>INSURED PERSON">
                                            <ItemStyle Width="250" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE">
                                            <ItemStyle ForeColor="Red" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="TAKEN">
                                            <HeaderStyle HorizontalAlign="Right" Width="30px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr style="vertical-align: top;">
                                        <td>
                                            <asp:Label ID="LB_SAVING_ALERT" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td style="width: 270px;">
                                            <asp:DataGrid ID="DGR_CHARGE" runat="server" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" ShowFooter="True" ShowHeader="False" OnItemDataBound="DGR_CHARGE_ItemDataBound" Enabled="False">
                                                <ItemStyle Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#666666" />
                                                <FooterStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="MODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="READONLY" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR">
                                                        <FooterStyle HorizontalAlign="Left" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="INCURRED AMOUNT">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_CHARGE" runat="server" Width="100px" BackColor="#d9ffd9" Style="FONT-FAMILY: Tahoma; FONT-SIZE: xx-small; text-align: right; -webkit-border-radius: 5px; -moz-border-radius: 5px;"
                                                                onkeypress="return CheckNumeric();" onkeyup="FormatCurrency(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_INV" runat="server" visible="false">
                <td>
                    <asp:DataGrid ID="DGR_BENEFIT_INV" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="DC" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="TRANSACTION TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Width="130" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">PAYABLE COMPONENT</td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_BENEFICIARY" runat="server" CellPadding="4" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" ForeColor="#333333">
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <Columns>
                            <asp:BoundColumn DataField="DESCR" HeaderText="BENEFICIARY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PCT" HeaderText="%">
                                <HeaderStyle Width="40" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Width="100" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:DataGrid ID="DGR_PAYABLE" runat="server" CellPadding="2" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" ForeColor="#333333" OnItemCommand="DGR_PAYABLE_ItemCommand">
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#1C5E55" ForeColor="White" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <Columns>
                            <asp:BoundColumn DataField="PAYABLE_COMPONENT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DC" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="ITEMS"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="50" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_SET" runat="server" CssClass="ASPButton" CommandName="Set" Text="SET" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Width="100" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
