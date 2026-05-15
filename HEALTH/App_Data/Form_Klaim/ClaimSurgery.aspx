<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimSurgery.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimSurgery" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_CLAIMNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TRACK" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; left: 0px; top: 0px; border-spacing: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:TextBox ID="TXT_SURGERY" runat="server" CssClass="ASPTextBox" ReadOnly="true" Font-Bold="true" BackColor="#CCCCCC"></asp:TextBox>
                    <asp:Button ID="BT_CARI_SURGERY" runat="server" CssClass="ASPButton" Text="CARI" />
                    <asp:Button ID="BT_TAMBAH_SURGERY" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_TAMBAH_SURGERY_Click" Text="TAMBAH SURGERY" />

                    <asp:DataGrid ID="DGR_DIAG_AWAL" runat="server" CellPadding="4" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" ForeColor="#333333" AutoGenerateColumns="False" BorderColor="#006600" OnItemCommand="DGR_DIAG_AWAL_ItemCommand">
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" HeaderText="KODE SURGERY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PART_OF_BODY" HeaderText="PART OF BODY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TREATMENT" HeaderText="TREATMENT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CATEGORY" HeaderText="CATEGORY"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

