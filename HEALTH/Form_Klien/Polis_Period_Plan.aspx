<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_Plan.aspx.cs" Inherits="HEALTH.Form_Klien.Policy_Period_Plan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; left: 0px; top: 0px; position: absolute;">
            <tr>
                <tb>
                    <asp:Label ID="LB_PERIOD" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                </tb>
            </tr>
        </table>
        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" GridLines="None" CssClass="ASPDatagrid" ForeColor="#333333">
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Center" Wrap="False" />
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        </asp:DataGrid>
    </form>
</body>
</html>
