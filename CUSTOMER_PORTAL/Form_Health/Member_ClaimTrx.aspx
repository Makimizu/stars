<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member_ClaimTrx.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Health.Member_ClaimTrx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="LB_REGNO" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_PERIOD" runat="server" Visible="False"></asp:Label>

                    <asp:DataGrid ID="DGR_CLAIMTRX" runat="server" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" BorderColor="#999999" BackColor="White" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundColumn DataField="NO CLAIM" HeaderText="NO CLAIM"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PENGAJUAN" HeaderText="PENGAJUAN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOLAK" HeaderText="TOLAK">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BAYAR" HeaderText="BAYAR">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="KETERANGAN" HeaderText="KETERANGAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KETERANGAN&lt;BR&gt;TAMBAHAN" HeaderText="KETERANGAN&lt;BR&gt;TAMBAHAN"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>

