<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceNoteCommission.aspx.cs" Inherits="FINANCE.Form_Parameter.InvoiceNoteCommission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="text-align: right;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; font-weight: bold;" class="TDBGColor">INCLUSION</td>
                            <td style="width: 50%; font-weight: bold;" class="TDBGColor">EXCLUSION</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:DataGrid ID="DGR_CN" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="40"
                                    GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ShowHeader="False" Width="90%">
                                    <ItemStyle Wrap="False" BackColor="#FFFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="#FFFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE">
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB_CN" runat="server" AutoPostBack="True" OnCheckedChanged="CB_CN_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                            <td>
                                <asp:DataGrid ID="DGR_DN" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="40"
                                    GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ShowHeader="False" Width="90%">
                                    <ItemStyle Wrap="False" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="#99FF66" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE">
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB_DN" runat="server" AutoPostBack="True" OnCheckedChanged="CB_DN_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
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
