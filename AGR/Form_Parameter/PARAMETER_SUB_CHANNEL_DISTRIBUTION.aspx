<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_SUB_CHANNEL_DISTRIBUTION.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_SUB_CHANNEL_DISTRIBUTION" %>

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
                <td class="TDBGColor">AGENT LEVEL</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 200px;">LEVEL CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>LEVEL NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="300"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>DIVISION IN CHARGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_SEGMENT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>DISTRIBUTION CHANNEL</td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>UPPER LEVEL</td>
                            <td>
                                <asp:DropDownList ID="DDL_UPPER" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>#SEQUENCE</td>
                            <td>
                                <asp:TextBox ID="TXT_SEQ" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" Text="INSERT NEW" CssClass="ASPButton" Width="100" OnClick="BT_SAVE_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
                        GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" Width="100%" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#507CD1" VerticalAlign="Top" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="SUB_CODE" HeaderText="LEVEL<BR>CODE">
                                <ItemStyle Width="40" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MARKET_SEGMENT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CD_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UPPER" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="LEVEL<BR>NAME">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="300"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DIVISION<BR>IN CHARGE">
                                <ItemStyle Width="150" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_SEGMENT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DISTRIBUTION<BR>CHANNEL">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="UPPER<BR>LEVEL">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_UPPER" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#SEQ">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle Width="40" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_SEQ" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="60" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" BackColor="Green" ForeColor="White" Text="S" CommandName="Save" ToolTip="Save" />
                                    <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" CommandName="Delete" ToolTip="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
