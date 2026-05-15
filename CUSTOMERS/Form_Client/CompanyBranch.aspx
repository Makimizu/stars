<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyBranch.aspx.cs" Inherits="CUSTOMERS.Form_Client.CompanyBranch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width:250px;">
                    <asp:TextBox ID="TXT_BRANCH_SEARCH" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_BRANCH_SEARCH_TextChanged" Width="100%"></asp:TextBox>
                    <br />
                    <asp:ListBox ID="LB_BRANCH" runat="server" CssClass="ASPDropDownList" Height="400px" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="LB_BRANCH_SelectedIndexChanged"></asp:ListBox>
                    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td bgcolor="#99FFCC" style="width: 150px;">KODE CABANG
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_KDCAB" runat="server" Enabled="False" MaxLength="30" Width="179px"
                                                CssClass="ASPTextBox"></asp:TextBox>
                                            <asp:Button ID="BT_NEWCAB" runat="server" OnClick="BT_NEWCAB_Click" Text="NEW" CssClass="ASPButton" />
                                            &nbsp;<asp:Button ID="BT_SAVE_CAB" runat="server" OnClick="BT_SAVE_CAB_Click" Text="SIMPAN CABANG"
                                                Height="21px" CssClass="ASPButton" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">NAMA CABANG
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_NAMACAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">KODE CABANG DARI KLIEN
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_KODECAB2" runat="server" MaxLength="25" Width="179px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">ALAMAT 1
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_ALAMATCAB1" runat="server" MaxLength="255" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">ALAMAT 2
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_ALAMATCAB2" runat="server" MaxLength="255" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">TELEPON
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_PHNCAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">FAX
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_FAXCAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">EMAIL
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_EMAILCAB" runat="server" MaxLength="100" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">PIC
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_PICCAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">JABATAN PIC
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_PICTITLECAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">EMAIL PIC
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_PICEMAILCAB" runat="server" MaxLength="100" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">KOTAMADYA
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_KOTAMADYACAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">PROPINSI&nbsp;
                                        </td>
                                        <td class="style6">
                                            <asp:DropDownList ID="DDL_PROPCAB" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">KODE WIL&nbsp;
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="TXT_KODEWIL" runat="server" MaxLength="50" Width="100px"
                                                CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">KODE POS
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_KODEPOSCAB" runat="server" MaxLength="10" Width="100px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#99FFCC" class="style8">&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;<asp:Label ID="LB_ERR" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red"
                                            CssClass="ASPLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_ACCOUNT" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                                    BorderWidth="1px" CellPadding="2" ForeColor="Black" OnItemCommand="DGR_ACCOUNT_ItemCommand"
                                    CssClass="ASPDatagrid" AutoGenerateColumns="False">
                                    <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <AlternatingItemStyle BackColor="PaleGoldenrod" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TIPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPE_DESCR" HeaderText="TIPE AKUN"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ACCNO" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ACCNAME" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ACCBANK" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="#ACC">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_ACCNO0" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="NAMA ACC">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_NAMAREK0" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="BANK">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_BANK0" runat="server" CssClass="ASPDropDownList">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>


                                    </Columns>
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center"
                                        Mode="NumericPages" />
                                </asp:DataGrid>
                                <br />
                                <asp:DataGrid ID="DGR_KORESPONDEN" runat="server" CellPadding="4" ForeColor="#333333" OnItemCommand="DGR_ACCOUNT_ItemCommand"
                                    CssClass="ASPDatagrid" AutoGenerateColumns="False" GridLines="None">
                                    <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TIPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPE_DESCR" HeaderText="TIPE KORESPONDEN"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PIC_NAMA" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PIC_SALUTATION" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PIC_TITLE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PIC_PHONE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PIC_EMAIL" Visible="False"></asp:BoundColumn>

                                        <asp:TemplateColumn HeaderText="SALUTATION">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_PICSALUTATION" runat="server" CssClass="ASPDropDownList">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="NAMA">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_PICNAMA" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="100"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="JABATAN">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_PICTITLE" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="30"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="TELEPON">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_PICPHONE" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="100"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="EMAIL">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_PICEMAIL" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="1000"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                    </Columns>
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <ItemStyle BackColor="#E3EAEB" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
