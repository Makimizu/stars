<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TRAINING_REMARK.aspx.cs" Inherits="AGR.TRAINING_REMARK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_TRAINING_CODE" runat="server" Visible="false"></asp:Label>

        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
            <Columns>
                <asp:BoundColumn DataField="TRAINING_REMARK_SEQ" HeaderText="NO">
                    <ItemStyle HorizontalAlign="Right" Width="30" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TRAINING_REMARK" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="REMARK">
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_REMARK" CssClass="ASPTextBox" Width="90%" MaxLength="1000" runat="server" TextMode="MultiLine" Height="100"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
        </asp:DataGrid>
        <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="80" OnClick="BT_SAVE_Click" />
    </form>
</body>
</html>
