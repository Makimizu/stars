<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginStatistic.aspx.cs" Inherits="GO.LoginStatistic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">STATISTIC</td>
                            <td>
                                <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>LOGIN DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-
                                            <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_CARI" runat="server" CssClass="ASPButton" OnClick="BT_CARI_Click" Text="SEARCH" />
                                            <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" OnClick="BT_XLS_Click" BackColor="Green" Font-Bold="True" ForeColor="White" Visible="False" />

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="15" Width="350px" BackColor="White" BorderColor="#000066" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <ItemStyle VerticalAlign="Top" BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle Wrap="False" BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
