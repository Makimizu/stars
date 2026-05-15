<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Track.aspx.cs" Inherits="HEALTH.Form_Tools.Track" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    </link>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing:0px;">
            <tr>
                <td>
                    <asp:Label ID="LB_TIPE" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_OWNER" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" PageSize="20" CssClass="ASPDatagrid" Width="100%" OnItemCommand="DGR_ItemCommand" ShowHeader="False">
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="Aqua" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="Lime" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="USER_STARTBY" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="USER_ENDBY" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="USER_ENDDATE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMMENT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BUTTON" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="LB_TRACK" runat="server"></asp:Label>
                                    <asp:Button ID="BT_STOP" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="STOP" Visible="False" CommandName="Stop" />
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
