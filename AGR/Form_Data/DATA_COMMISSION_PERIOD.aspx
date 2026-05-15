<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DATA_COMMISSION_PERIOD.aspx.cs" Inherits="AGR.DATA_COMMISSION_PERIOD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function hourglass() {
            document.body.style.cursor = "wait";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REMUN_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server" Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr>
                            <td style="width: 49%;" class="TDBGColor">INQUIRY</td>
                            <td style="width: 49%;" class="TDBGColor">INSERT NEW RECORD</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                    <tr>
                                        <td>DIVISION IN CHARGE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged" Width="100"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">YEAR</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                    <tr>
                                        <td style="width: 100px;">START DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_START_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>END DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_END_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_INSERT" runat="server" Text="INSERT" CssClass="ASPButton" Width="100" OnClick="BT_INSERT_Click" OnClientClick="hourglass(); return true;" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>



                    <asp:DataGrid ID="DGR" runat="server" CellPadding="1" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%">
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE">
                                <HeaderStyle HorizontalAlign="Center" Width="100" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE">
                                <HeaderStyle HorizontalAlign="Center" Width="100" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_PRODUCTION" HeaderText="#PRODUCTION DATA">
                                <HeaderStyle HorizontalAlign="Center" Width="100" />
                                <ItemStyle HorizontalAlign="Center" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_REMUN" HeaderText="#REMUN DATA">
                                <HeaderStyle HorizontalAlign="Center" Width="100" />
                                <ItemStyle HorizontalAlign="Center" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_REMUN_PROCESSED" HeaderText="#PROCESSED DATA">
                                <HeaderStyle HorizontalAlign="Center" Width="100" />
                                <ItemStyle HorizontalAlign="Center" ForeColor="Purple" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="30" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                    </asp:DataGrid>


                </td>
            </tr>
        </table>
    </form>
</body>
</html>
