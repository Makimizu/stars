<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member_ClaimHistory.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Health.Member_ClaimHistory" %>

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
                    <asp:DataGrid ID="DGR_CLAIM" runat="server" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" BorderColor="#999999" BackColor="White" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundColumn DataField="NO CLAIM" HeaderText="NO CLAIM"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NO SM" HeaderText="NO SM"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NO SK" HeaderText="NO SK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRACK" HeaderText="TRACK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TAHUN POLIS" HeaderText="TAHUN POLIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE KLAIM" HeaderText="TIPE KLAIM"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PENGAJUAN" HeaderText="PENGAJUAN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL RAWAT" HeaderText="TGL RAWAT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DIAGNOSA" HeaderText="DIAGNOSA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVIDER" HeaderText="PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL BAYAR" HeaderText="TGL BAYAR"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
