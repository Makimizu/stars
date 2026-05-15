<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TRAINING_SUBMODULE.aspx.cs" Inherits="AGR.TRAINING_SUBMODULE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_TRAINING_CODE" runat="server" Visible="false"></asp:Label>

        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
            <Columns>
                <asp:BoundColumn DataField="TRAINING_SUB_CODE" HeaderText="NO">
                    <HeaderStyle HorizontalAlign="Right" Width="30" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TRAINING_SUB_NAME" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="SUBJECT">
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_SUBJECT" CssClass="ASPTextBox" Width="90%" MaxLength="255" runat="server"></asp:TextBox>
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
