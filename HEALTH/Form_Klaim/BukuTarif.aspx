<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BukuTarif.aspx.cs" Inherits="HEALTH.Form_Klaim.BukuTarif" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                        <asp:DataGrid ID="DGR_TARIF" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                            BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_TARIF_ItemCommand">
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <AlternatingItemStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="KODE_TARIF_DESCR" HeaderText="JENIS TARIF"></asp:BoundColumn>
                                <asp:BoundColumn DataField="KELAS_KAMAR_DESCR" HeaderText="KELAS KAMAR"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SUB_TARIF_DESCR" HeaderText="SUB TARIF"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TARIF_DESCR" HeaderText="KETERANGAN"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TARIF" HeaderText="TARIF" Visible="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TGL_BERLAKU" HeaderText="TGL BERLAKU"></asp:BoundColumn>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                Wrap="False" />
                            <PagerStyle BackColor="#999999" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left" Mode="NumericPages" />
                            <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid>
                </td>
                <td>
                                <asp:Button runat="server" ID="BT_TRF" CssClass="ASPButton" Height="30px" Font-Size="14px" Text="Buku Tarif"  OnClick="BT_TRF_Click"/>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
