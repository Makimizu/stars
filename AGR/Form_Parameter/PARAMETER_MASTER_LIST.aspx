<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_LIST.aspx.cs" Inherits="AGR.PARAMETER_MASTER_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
        <table style="width: 100%; border-spacing: 0px; font-size: x-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                        <tr>
                            <td style="width: 100px;">DESCRIPTION</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCRIPTION" runat="server" CssClass="ASPTextBox" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PERIOD</td>
                            <td>
                                <asp:TextBox ID="TXT_START_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE">
                                </ajaxToolkit:CalendarExtender>
                                - 
                                <asp:TextBox ID="TXT_END_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" Width="80" OnClick="BT_SEARCH_Click" />
                                &nbsp;<asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="1" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" OnPageIndexChanged="DGR_PageIndexChanged" OnItemCommand="DGR_ItemCommand" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellSpacing="1">
                        <ItemStyle Wrap="False" BackColor="#E7E7FF" Font-Size="XX-Small" ForeColor="#4A3C8C" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#738A9C" ForeColor="#F7F7F7" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="False" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_NAME" runat="server" CommandName="Select" HeaderText="PARAMETER NAME"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RECORDS" HeaderText="#RECORDS">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCTS" HeaderText="#PRODUCTS">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DELETE" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" ForeColor="White" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Font-Size="X-Small" Mode="NumericPages" />
                    </asp:DataGrid>


                </td>
            </tr>
        </table>
    </form>
</body>
</html>
