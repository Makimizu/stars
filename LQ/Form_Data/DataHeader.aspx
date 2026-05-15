<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataHeader.aspx.cs" Inherits="LQ.Form_Data.DataHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%; font-size: small;">
            <tr>
                <td style="text-align: center;">YEAR&nbsp;:&nbsp;<asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged" Font-Size="Small"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_PERIOD" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" Width="100%" CellSpacing="2">
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="URL"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
