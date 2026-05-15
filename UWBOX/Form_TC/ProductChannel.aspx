<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductChannel.aspx.cs" Inherits="UWBOX.Form_TC.ProductChannel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

                    <asp:DataGrid ID="DGR_CHANNEL" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="50%" ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
                        <ItemStyle VerticalAlign="Top" BackColor="#FFFBD6" ForeColor="#333333" />
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="SUB_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="30px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" AutoPostBack="True" OnCheckedChanged="CB_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>

