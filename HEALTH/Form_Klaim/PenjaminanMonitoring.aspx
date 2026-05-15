<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenjaminanMonitoring.aspx.cs" Inherits="HEALTH.Form_Klaim.PenjaminanMonitoring" %>

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
        <asp:Label ID="LB_ID" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <strong>
                        <asp:Label ID="Label22" runat="server" CssClass="ASPLabel" Font-Bold="True" Text="TGL MONITORING :"></asp:Label>
                    </strong></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TXT_MON_ADDTGL" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="ceDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_MON_ADDTGL">
                    </ajaxToolkit:CalendarExtender>
                    <asp:Button ID="BT_MON_ADDTGL" runat="server" CssClass="ASPButton" OnClick="BT_MON_ADDTGL_Click" Text="TAMBAH TGL" Height="17px" /></td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR_MON" runat="server" CellPadding="3" PageSize="20" GridLines="Horizontal" CssClass="ASPDatagrid" BorderColor="#E7E7FF" OnItemCommand="DGR_MON_ItemCommand" BackColor="White" BorderStyle="None" BorderWidth="1px" Width="500px">
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
