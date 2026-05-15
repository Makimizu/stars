<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderRekening.aspx.cs" Inherits="HEALTH.Form_Klaim.ProviderRekening" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table id="TBL_ACC" runat="server" visible="false" style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 70px;">ACC NO</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ACC NAMA</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAMA" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_ACCSAVE" runat="server" CssClass="ASPButton" Text="TAMBAH REKENING" OnClick="BT_ACCSAVE_Click" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_REKENING" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" AutoGenerateColumns="False" CssClass="ASPDatagrid" OnItemCommand="DGR_REKENING_ItemCommand" ShowHeader="False">
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_BANK" HeaderText="ACC NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KODE_BANK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="BANK">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 70px;">ACC NO</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="TXT_ACCNO2" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="120px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>ACC NAMA</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="TXT_ACCNAMA2" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>BANK</td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>STATUS</td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_ACC_STAT" runat="server" CssClass="ASPDropDownList">
                                                    <asp:ListItem Value="1">AKTIF</asp:ListItem>
                                                    <asp:ListItem Value="0">TDK AKTIF</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" /><br />
                                    <asp:Button ID="BT_ACCDELETE" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="DELETE" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
