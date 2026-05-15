<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_RPC_List.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Peserta_ReprintCard_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#DEDFDE"
                        BorderWidth="1px" CellPadding="4" ForeColor="Black" OnItemCommand="DGR_ItemCommand"
                        CssClass="ASPDatagrid" AutoGenerateColumns="False" BorderStyle="None" GridLines="Vertical" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True">
                        <ItemStyle BackColor="#F7F7DE" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle ForeColor="White" BackColor="#CE5D5A" Font-Bold="true" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" BackColor="Red" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REGNO" HeaderText="REGNO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="E/C/S" HeaderText="E/C/S"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL LAHIR" HeaderText="TGL LAHIR">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL MULAI" HeaderText="TGL MULAI">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CABANG" HeaderText="CABANG">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BIAYA" HeaderText="BIAYA">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
