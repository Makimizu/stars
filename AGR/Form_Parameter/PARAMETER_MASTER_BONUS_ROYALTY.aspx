<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_BONUS_ROYALTY.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_MASTER_BUSINESS_RECRUITMENT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>

        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                        <tr>
                            <td style="width: 100px;">DESCRIPTION</td>
                            <td>
                                <asp:Label ID="LB_DESCR" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>REMUN TYPE</td>
                            <td>
                                <asp:Label ID="LB_TYPE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                        <tr>
                            <td style="width: 100px;">START DATE</td>
                            <td>
                                <asp:Label ID="LB_STARTDATE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>END DATE</td>
                            <td>
                                <asp:Label ID="LB_ENDDATE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                        <tr>
                            <td style="width: 33%;">
                                <asp:Button ID="BT_SCHEME" runat="server" Text="SCHEME" Width="100%" CssClass="ASPButton" OnClick="BT_SCHEME_Click" /></td>
                            <td style="width: 33%;">
                                <asp:Button ID="BT_AGENCY" runat="server" Text="AGENCY" Width="100%" CssClass="ASPButton" OnClick="BT_AGENCY_Click" /></td>
                            <td style="width: 33%;">
                                <asp:Button ID="BT_PRODUCT" runat="server" Text="PRODUCT" Width="100%" CssClass="ASPButton" OnClick="BT_PRODUCT_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
        </table>

        <div id="DV_SCHEME" runat="server">
            <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                <tr style="vertical-align: top;">
                    <td style="width: 50%;">
                        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                            <tr>
                                <td style="width: 150px;">TRANSACTION TYPE</td>
                                <td>
                                    <asp:DropDownList ID="DDL_TRANSTYPE" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_TRANSTYPE_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%;">
                        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                            <tr>
                                <td style="width: 150px;">
                                    <asp:Button ID="BT_XLS_DOWNLOAD" runat="server" Text="DOWNLOAD TEMPLATE XLS" CssClass="ASPButton" ForeColor="Green" Width="100%" OnClick="BT_XLS_DOWNLOAD_Click" /></td>
                                <td>
                                    <asp:Button ID="BT_PARAM_GUIDANCE" runat="server" Text="DOWNLOAD PARAMETER GUIDANCE" CssClass="ASPButton" OnClick="BT_PARAM_GUIDANCE_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BT_XLS_UPLOAD" runat="server" Text="UPLOAD DATA XLS" CssClass="ASPButton" ForeColor="Blue" Width="100%" OnClick="BT_XLS_UPLOAD_Click" OnClientClick="hourglass(); return true;" /></td>
                                <td>
                                    <asp:FileUpload ID="FU" runat="server" CssClass="ASPButton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:DataGrid ID="DGR" runat="server" CellPadding="1" PageSize="20"
                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand" OnItemDataBound="DGR_ItemDataBound">
                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                <Columns>
                    <asp:BoundColumn DataField="TRANSTYPE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="YEAR" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TRANSTYPE_DESCR" HeaderText="TRANSACTION TYPE"></asp:BoundColumn>
                    <%--<asp:BoundColumn DataField="CHANNEL_DESCR" HeaderText="CHANNEL"></asp:BoundColumn>--%>
                    <asp:BoundColumn DataField="PROMOTED_LEVEL" HeaderText="PROMOTED<BR>LEVEL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="PROMOTING_LEVEL" HeaderText="PROMOTING<BR>LEVEL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="YEAR" HeaderText="#YEAR"></asp:BoundColumn>
                    <asp:BoundColumn DataField="G1_PCT" HeaderText="% G1"></asp:BoundColumn>
                    <asp:BoundColumn DataField="G2_PCT" HeaderText="% G2"></asp:BoundColumn>
                    <asp:BoundColumn DataField="G3_PCT" HeaderText="% G3"></asp:BoundColumn>
                    <asp:BoundColumn DataField="G4_PCT" HeaderText="% G4"></asp:BoundColumn>
                    <asp:BoundColumn DataField="G5_PCT" HeaderText="% G5"></asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderTemplate>
                            <asp:Button ID="BT_ALL" runat="server" Text="DELETE ALL" CssClass="ASPButton" CommandName="DeleteAll" /><br />
                            <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Delete" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CB" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <EditItemStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
            </asp:DataGrid>
        </div>

        <div id="DV_AGENCY" runat="server" visible="false">
            <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                <tr>
                    <td style="width: 50%;" class="TDBGColor">AVAILABLE AGENCY</td>
                    <td style="width: 50%;" class="TDBGColor">SELECTED AGENCY</td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Button ID="BT_AGENCY_ADD" runat="server" Text="ADD" CssClass="ASPButton" Width="80" BackColor="Blue" ForeColor="White" OnClick="BT_AGENCY_ADD_Click" /></td>
                    <td style="text-align: right;">
                        <asp:Button ID="BT_AGENCY_DELETE" runat="server" Text="DELETE" CssClass="ASPButton" Width="80" BackColor="Red" ForeColor="White" OnClick="BT_AGENCY_DELETE_Click" /></td>
                </tr>
                <tr style="vertical-align: top;">
                    <td>
                        <asp:DataGrid ID="DGR_AGENCY_AVAIL" runat="server" CellPadding="1" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <HeaderStyle BackColor="#009900" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="COMPANY CODE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CB_AVAIL_ALL" runat="server" AutoPostBack="True" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                    <td>
                        <asp:DataGrid ID="DGR_AGENCY_SELECTED" runat="server" CellPadding="1" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <HeaderStyle BackColor="#FF9966" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="COMPANY CODE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CB_SELECTED_ALL" runat="server" AutoPostBack="True" OnCheckedChanged="CB_ALL_CheckedChanged1" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB" runat="server" />
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
        </div>

        <div id="DV_PRODUCT" runat="server" visible="false">
            <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                <tr>
                    <td style="width: 50%;" class="TDBGColor">AVAILABLE PRODUCT</td>
                    <td style="width: 50%;" class="TDBGColor">SELECTED PRODUCT</td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="DDL_GROUP_PRODUCT" CssClass="ASPDropDownList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDL_GROUP_PRODUCT_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Button ID="BT_PRODUCT_ADD" runat="server" Text="ADD" CssClass="ASPButton" Width="80" BackColor="Blue" ForeColor="White" OnClick="BT_PRODUCT_ADD_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: right;">
                        <asp:Button ID="BT_PRODUCT_DELETE" runat="server" Text="DELETE" CssClass="ASPButton" Width="80" BackColor="Red" ForeColor="White" OnClick="BT_PRODUCT_DELETE_Click" /></td>
                </tr>
                <tr style="vertical-align: top;">
                    <td>
                        <asp:DataGrid ID="DGR_PRODUCT_AVAIL" runat="server" CellPadding="1" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <HeaderStyle BackColor="#009900" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundColumn DataField="PRODUCT_CODE" HeaderText="PRODUCT CODE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRODUCT_DESCR" HeaderText="PRODUCT NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRODUCT_GROUP_DESCR" HeaderText="PRODUCT GROUP"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CB_AVAIL_ALL" runat="server" AutoPostBack="True" OnCheckedChanged="CB_AVAIL_ALL_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                    <td>
                        <asp:DataGrid ID="DGR_PRODUCT_SELECTED" runat="server" CellPadding="1" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <HeaderStyle BackColor="#FF9966" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundColumn DataField="PRODUCT_CODE" HeaderText="PRODUCT CODE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRODUCT_DESCR" HeaderText="PRODUCT NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRODUCT_GROUP_DESCR" HeaderText="PRODUCT GROUP"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CB_SELECTED_ALL" runat="server" AutoPostBack="True" OnCheckedChanged="CB_SELECTED_ALL_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB" runat="server" />
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
        </div>
    </form>
</body>
</html>
