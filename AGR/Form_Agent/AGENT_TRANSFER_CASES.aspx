<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_TRANSFER_CASES.aspx.cs" Inherits="AGR.Form_Agent.AGENT_TRANSFER_CASES" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                        <tr>
                            <td style="width: 130px;">AGENT CODE</td>
                            <td>
                                <asp:Label ID="LB_CODE" runat="server"></asp:Label>
                                <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:Label ID="LB_FULLNAME" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>CHANNEL</td>
                            <td>
                                <asp:Label ID="LB_CHANNEL" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>LEVEL</td>
                            <td>
                                <asp:Label ID="LB_LEVEL" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%;" class="TDBGColor">UNSELECTED ITEMS</td>
                            <td style="width: 50%;" class="TDBGColor">SELECTED ITEMS</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">POLICY NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICYNO_UNSELECTED" runat="server" CssClass="ASPTextBox" Width="200" AutoPostBack="true" OnTextChanged="TXT_POLICYNO_UNSELECTED_TextChanged"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>INSURED NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_INSUREDNAME_UNSELECTED" runat="server" CssClass="ASPTextBox" Width="200" AutoPostBack="true" OnTextChanged="TXT_INSUREDNAME_UNSELECTED_TextChanged"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PRODUCTNAME_UNSELECTED" runat="server" CssClass="ASPTextBox" Width="200" AutoPostBack="true" OnTextChanged="TXT_PRODUCTNAME_UNSELECTED_TextChanged"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">POLICY NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICYNO_SELECTED" runat="server" CssClass="ASPTextBox" Width="200" AutoPostBack="true" OnTextChanged="TXT_POLICYNO_SELECTED_TextChanged"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>INSURED NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_INSUREDNAME_SELECTED" runat="server" CssClass="ASPTextBox" Width="200" AutoPostBack="true" OnTextChanged="TXT_INSUREDNAME_SELECTED_TextChanged"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PRODUCTNAME_SELECTED" runat="server" CssClass="ASPTextBox" Width="200" AutoPostBack="true" OnTextChanged="TXT_PRODUCTNAME_SELECTED_TextChanged"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <tr>
                                <td>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LB_RECORDS_UNSELECTED" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Button ID="BT_SELECT" runat="server" Text="SELECT" CssClass="ASPButton" Width="100" OnClick="BT_SELECT_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LB_RECORDS_SELECTED" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Button ID="BT_UNSELECT" runat="server" Text="UNSELECT" CssClass="ASPButton" Width="100" OnClick="BT_UNSELECT_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:DataGrid ID="DGR_UNSELECTED" runat="server" CellPadding="4" PageSize="20"
                                    GridLines="None" CssClass="ASPDatagrid" AllowPaging="True" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnPageIndexChanged="DGR_UNSELECTED_PageIndexChanged">
                                    <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="X-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                                    <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PRODUCT_CODE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="INSURED_NAME" HeaderText="INSURED NAME - PRODUCT NAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PERIOD" HeaderText="PERIOD"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="30" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CB_ALL_UNSELECTED" runat="server" AutoPostBack="true" OnCheckedChanged="CB_UNSELECTED_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB_UNSELECTED" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                            <td>
                                <asp:DataGrid ID="DGR_SELECTED" runat="server" CellPadding="4" PageSize="20"
                                    GridLines="None" CssClass="ASPDatagrid" AllowPaging="True" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnPageIndexChanged="DGR_SELECTED_PageIndexChanged">
                                    <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="X-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                                    <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="Navy" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PRODUCT_CODE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="INSURED_NAME" HeaderText="INSURED NAME - PRODUCT NAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PERIOD" HeaderText="PERIOD"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="30" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CB_ALL_SELECTED" runat="server" AutoPostBack="true" OnCheckedChanged="CB_SELECTED_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB_SELECTED" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
