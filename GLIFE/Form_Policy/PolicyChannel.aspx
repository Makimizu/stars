<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyChannel.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyChannel" %>

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
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <div style="width: 100%; height: 200px; overflow: auto;">
                        <asp:DataGrid ID="DGR_CHANNEL" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <ItemStyle VerticalAlign="Top" BackColor="#EFF3FB" />
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle
                                Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="SUB_CODE" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MARKET_SEGMENT_DESCR"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB" runat="server" AutoPostBack="True" OnCheckedChanged="CB_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
