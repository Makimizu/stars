<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationRemark.aspx.cs" Inherits="LQ.Form_Client.QuotationRemark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBoxUPPER" Width="90%" TextMode="MultiLine" Height="80px" Font-Size="x-Small" placeholder="Catatan ..." BackColor="#E4E4E4"></asp:TextBox>
        <asp:Button ID="BT_REMARK_SAVE" runat="server" CssClass="ASPButton" Text="TAMBAH CATATAN" Width="130px" OnClick="BT_REMARK_SAVE_Click" />

        <asp:DataGrid ID="DGR_REMARK" runat="server" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
            CellSpacing="1" Font-Names="Tahoma" Font-Size="8pt" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="90%" OnItemCommand="DGR_REMARK_ItemCommand" ShowHeader="False">
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#666666" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <Columns>
                <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="REMARK"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemStyle Width="30px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LBT_REMARKDELETE" runat="server" CommandName="Delete" ToolTip="Delete">
                                                <span class="fa fa-remove" style="color:red;"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        </asp:DataGrid>
    </form>
</body>
</html>
