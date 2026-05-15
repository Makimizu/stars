<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationOtherInsurance.aspx.cs" Inherits="LQ.Form_Client.QuotationOtherInsurance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_MEMBERID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td class="TDBGColor">POLIS DI ASURANSI LAIN</td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="3" Font-Names="Tahoma" Font-Size="8pt" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" Width="100%" CssClass="ASPDatagrid" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" VerticalAlign="Top" ForeColor="#4A3C8C" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="NOMOR POLIS">
                                <ItemStyle Width="200" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_POLICYNO" runat="server" MaxLength="100" Width="200" CssClass="ASPTextBox"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ASURANSI">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_INSURANCE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TGL MULAI">
                                <HeaderStyle HorizontalAlign="Center" Width="80" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TGL AKHIR">
                                <HeaderStyle HorizontalAlign="Center" Width="80" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
