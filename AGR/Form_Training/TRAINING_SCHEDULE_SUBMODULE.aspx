<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TRAINING_SCHEDULE_SUBMODULE.aspx.cs" Inherits="AGR.TRAINING_SCHEDULE_SUBMODULE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_SCHEDULE_CODE" runat="server" Visible="false"></asp:Label>

        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
            <Columns>
                <asp:BoundColumn DataField="TRAINING_CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="TRAINING_SUB_CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="TRAINING_SUB_NAME"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="30" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
        </asp:DataGrid>
    </form>
</body>
</html>
