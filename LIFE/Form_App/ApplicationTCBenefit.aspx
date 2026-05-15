<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationTCBenefit.aspx.cs" Inherits="LIFE.Form_App.ApplicationTCBenefit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:Button ID="BT_BENEFIT" runat="server" CssClass="ASPButton" Text="BENEFIT" Width="100%" OnClick="BT_BENEFIT_Click" />
                    <asp:Button ID="BT_OTHERINFO" runat="server" CssClass="ASPButton" Text="OTHER INFO" Width="100%" OnClick="BT_OTHERINFO_Click" />
                    <asp:Button ID="BT_FUND" runat="server" CssClass="ASPButton" Text="FUND" Width="100%" OnClick="BT_FUND_Click" />
                    <asp:Button ID="BT_ORGBEN" runat="server" CssClass="ASPButton" Text="BENEFICIARY ORGANIZATION" Width="100%" OnClick="BT_ORGBEN_Click" />
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <table id="TBL_BENEFIT" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <iframe id="IF" runat="server" style="width: 100%; height: 1000px; overflow: auto; border: 0;"></iframe>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_OTHERINFO" runat="server" style="border-spacing: 0px; width: 100%;" visible="false">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="2" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" ShowFooter="True" Width="100%" OnItemCommand="DGR_ITEM_ItemCommand">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="READONLY" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="LB_VAL" runat="server" Visible="false" Font-Bold="true"></asp:Label>
                                                <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="150px"></asp:TextBox>
                                                <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" Visible="false"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="BT_SAVE0" runat="server" CssClass="ASPButton" CommandName="Save" Text="SAVE" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr class="TDBGColor">
                            <td>ALAMAT PENGIRIMAN POLIS</td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TBL_POLICY_DELIV" runat="server" cellspacing="0" cellpadding="2" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                    <tr style="color: #4A3C8C; background-color: #E7E7FF">
                                        <td style="width: 15%">TUJUAN PENGIRIMAN POLIS</td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label runat="server" ID="LB_POLICY_DELIV_DEST" CssClass="ASPLabel"></asp:Label></td>
                                    </tr>
                                    <tr style="color: #4A3C8C; background-color: #F7F7F7">
                                        <td style="width: 15%">ALAMAT TUJUAN</td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label runat="server" ID="LB_POLICY_DELIV_ADDRESS" CssClass="ASPLabel"></asp:Label></td>
                                    </tr>
                                    <tr style="color: #4A3C8C; background-color: #E7E7FF">
                                        <td style="width: 15%">STATUS PENGIRIMAN POLIS</td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label runat="server" ID="LB_POLICY_DELIV_STAT" CssClass="ASPLabel"></asp:Label></td>
                                    </tr>
                                    <tr style="color: #4A3C8C; background-color: #F7F7F7">
                                        <td style="width: 15%">TANGGAL TERIMA POLIS</td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label runat="server" ID="LB_POLICY_RECEIVE_DATE" CssClass="ASPLabel"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_FUND" runat="server" style="border-spacing: 0px; width: 100%;" visible="false">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_FUND" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" OnItemCommand="DGR_ITEM_ItemCommand" ForeColor="#333333">
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                                    <ItemStyle BackColor="#E3EAEB" VerticalAlign="Top" Font-Size="XX-Small" />
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <Columns>
                                        <asp:BoundColumn DataField="FUND_DESCR" HeaderText="FUND"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PROCENTAGE" HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_ORGBEN" runat="server" style="border-spacing: 0px; width: 100%;" visible="false">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                    <tr>
                                        <td style="width: 100px;">ORGANIZATION</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ORG" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">CERTIFICATE NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CERNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BANK ACC NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BANK ACC NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BANK</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% RISK BENEFIT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PCTCLM" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% INVESTMENT BENEFIT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PCTINV" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_ORG" runat="server" CssClass="ASPButton" Text="SAVE BENEFICIARY" Width="150px" OnClick="BT_ORG_Click" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
