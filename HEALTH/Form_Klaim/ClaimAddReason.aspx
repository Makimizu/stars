<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimAddReason.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimAddReason" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
        <div>
            <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <asp:DropDownList ID="DDL_TIPE_ADD" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_TIPE_ADD_SelectedIndexChanged"></asp:DropDownList><br />
                        <asp:TextBox ID="TXT_ALASAN_CARI" CssClass="ASPTextBox" runat="server" Width="400px"></asp:TextBox>
                        <asp:Button ID="BT_CARI" runat="server" Text="CARI" CssClass="ASPButton" OnClick="BT_CARI_Click" /><br />
                        <asp:ListBox ID="LB_TP" runat="server" BackColor="#FFFF99" CssClass="ASPDropDownList" Height="120px"></asp:ListBox>
                    </td>
                    <td style="vertical-align:top;">
                        <asp:DataGrid ID="DGR" runat="server" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" BorderColor="#999999" AutoGenerateColumns="False" BackColor="White" BorderStyle="None" BorderWidth="1px" OnItemCommand="DGR_ItemCommand">
                            <AlternatingItemStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:BoundColumn DataField="CLAIM_NO" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPE" HeaderText="TIPE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CATEGORY" HeaderText="CATEGORY"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR" HeaderText="REASON"></asp:BoundColumn>
                                <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="KETERANGAN">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" MaxLength="1000" Width="400px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="BT_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="DELETE" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                            <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

