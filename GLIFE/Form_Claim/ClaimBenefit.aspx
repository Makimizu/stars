<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimBenefit.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimBenefit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Class/Global.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>

                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INCURRED_AMOUNT" Visible="False">
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                <ItemStyle ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUMINS" HeaderText="SUM INSURED">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_PCT" HeaderText="% BENEFIT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CALCULATED_AMOUNT" HeaderText="CALCULATED AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" Width="150px" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="INCURRED AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" Width="120px" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AMOUNT" runat="server" Width="100px" BackColor="#d2e9ff" Style="FONT-FAMILY: Tahoma; FONT-SIZE: xx-small; text-align: right; -webkit-border-radius: 5px; -moz-border-radius: 5px;"
                                        onkeypress="return CheckNumeric();" onkeyup="FormatCurrency(this);"></asp:TextBox>
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
                                <asp:DataGrid ID="DGR_CHARGE" runat="server" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" ShowFooter="True" ShowHeader="False" OnItemDataBound="DGR_CHARGE_ItemDataBound">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#666666" />
                                    <FooterStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MODE" Visible="False"></asp:BoundColumn>
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
                        <tr>
                            <td></td>
                            <td style="text-align: right;">
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
