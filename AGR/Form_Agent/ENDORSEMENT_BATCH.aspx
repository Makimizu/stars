<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ENDORSEMENT_BATCH.aspx.cs" Inherits="AGR.Form_Agent.ENDORSEMENT_BATCH" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
        </table>
        <div id="DV_UPLOAD" runat="server">
            <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                <tr>
                    <td style="width: 130px;">UPLOAD TEMPLATE</td>
                    <td>
                        <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="DOWNLOAD" BackColor="Green" ForeColor="White" Width="100px" OnClick="BT_XLS_Click" />
                    </td>
                </tr>
                <tr style="vertical-align: top;">
                    <td>UPLOAD DATA</td>
                    <td>
                        <asp:FileUpload ID="FU1" runat="server" CssClass="ASPButton" /><br />
                        <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" Text="UPLOAD" BackColor="Blue" ForeColor="White" Width="100" OnClick="BT_UPLOAD_Click" />
                        &nbsp;&nbsp;<asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label></td>
                </tr>
            </table>
        </div>
        <br />


        <asp:DataGrid ID="DGR" runat="server" CellPadding="2" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Xx-Small" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" VerticalAlign="Top" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
            <Columns>
                <asp:BoundColumn DataField="BATCH_ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="DESCR" HeaderText="ENDORSEMENT TYPE"></asp:BoundColumn>
                <asp:BoundColumn DataField="RECORDS" HeaderText="#RECORDS">
                    <HeaderStyle HorizontalAlign="Center" Width="80" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CREATEBY" HeaderText="SUBMITTED BY"></asp:BoundColumn>
                <asp:BoundColumn DataField="DECISION" HeaderText="DECISION">
                    <HeaderStyle Width="100" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AUTHOR" HeaderText="AUTHORIZE BY"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Right" Width="130" />
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Button ID="BT_ARCHIEVE" runat="server" Text="ARCHIEVE" Font-Size="XX-Small" CommandName="Archieve" />
                        <asp:Button ID="BT_APPROVE" runat="server" Text="APPROVE" BackColor="Green" ForeColor="White" Font-Size="XX-Small" CommandName="Approve" />
                        <asp:Button ID="BT_X" runat="server" Text="X" BackColor="Red" ForeColor="White" Font-Size="XX-Small" CommandName="Delete" />
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
