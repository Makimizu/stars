<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DATA_COMMISSION_SUMMARY_REPORT.aspx.cs" Inherits="AGR.Form_Data.DATA_COMMISSION_SUMMARY_REPORT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td class="TDBGColor">PERIOD</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>CHANNEL</td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged" Width="100px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>YEAR</td>
                            <td>
                                <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                    </table>
                    <div style="height: 300px; overflow: auto; border: inset;">
                        <asp:DataGrid ID="DGR_PERIOD" runat="server" CellPadding="4" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" Width="100%">
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="ID">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
