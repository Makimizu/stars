<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PERIOD_DETAIL.aspx.cs" Inherits="AGR.Form_Data.PERIOD_DETAIL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td style="width: 150px;">CHANNEL</td>
                <td>
                    <asp:Label ID="LB_CHANNEL_DESCR" runat="server" Font-Size="X-Small"></asp:Label>
                    <asp:Label ID="LB_CHANNEL" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>PERIOD</td>
                <td>
                    <asp:Label ID="LB_STARTDATE" runat="server" Font-Size="X-Small"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="LB_ENDDATE" runat="server" Font-Size="X-Small"></asp:Label></td>
            </tr>
            <tr>
                <td>SEARCH AGENT NAME</td>
                <td>
                    <asp:TextBox ID="TXT_AGENT" runat="server" CssClass="ASPTextBox" Width="300px" AutoPostBack="true" OnTextChanged="TXT_AGENT_TextChanged"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Label ID="LB_RECORDS" runat="server" Font-Size="X-Small"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" Width="90%">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
        </asp:DataGrid>


    </form>
</body>
</html>
