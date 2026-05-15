<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderEntry.aspx.cs" Inherits="HEALTH.Form_Klaim.ProviderEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">KODE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CODE" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox" ReadOnly="True" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TITLE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TITLE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NAMA PROVIDER</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>GRUP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_GRUP" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TIPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>JENIS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_JENIS" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>OWNER</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_OWNER" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">ALAMAT 1</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ALAMAT1" runat="server" CssClass="ASPTextBox" Height="30px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">ALAMAT 2</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ALAMAT2" runat="server" CssClass="ASPTextBox" Height="30px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>KODE POS</td>
                                        <td>
                                            <asp:TextBox ID="TXT_KODEPOS" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>KOTA</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_KOTA" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_KOTA_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PROPINSI</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PROPINSI" runat="server" CssClass="ASPDropDownList" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NEGARA</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_NEGARA" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width:20px;"></td>
                            <td>
                                <table style="border-spacing:0px;">
                                    <tr>
                                        <td style="width: 100px;">TELEPON 1</td>
                                        <td>
                                            <asp:TextBox ID="TXT_TELEPON1" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TELEPON 2</td>
                                        <td>
                                            <asp:TextBox ID="TXT_TELEPON2" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>FAX 1</td>
                                        <td>
                                            <asp:TextBox ID="TXT_FAX1" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>FAX 2</td>
                                        <td>
                                            <asp:TextBox ID="TXT_FAX2" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NO NPWP</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NPWPNO" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NAMA NPWP</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NPWPNAMA" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">ALAMAT NPWP</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NPWPALAMAT" runat="server" CssClass="ASPTextBox" Height="30px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">ALASAN REGISTRASI</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ALASAN" runat="server" CssClass="ASPTextBox" Height="30px" MaxLength="100" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>STATUS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="ASPDropDownList" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>KODE TPA</td>
                                        <td>
                                            <asp:DataGrid ID="DGR_TPA" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                                BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False">
                                                <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <AlternatingItemStyle BackColor="#DCDCDC" />
                                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" />
                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                    Wrap="False" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TPA_VERSION" Visible="False"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_TPAVERSION" runat="server" CssClass="ASPTextBox" MaxLength="20" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                            </asp:DataGrid>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BT_SAVE_PROV" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_PROV_Click" Text="SAVE" Font-Bold="True" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 16%;">
                                <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="LAYANAN" Width="100%" OnClick="BT1_Click" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="PIC" Width="100%" OnClick="BT2_Click" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Text="TARIF" Width="100%" OnClick="BT3_Click" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            </td>
                            <td id="TD_DISC" runat="server" visible="false" style="width: 16%;">
                                <asp:Button ID="BT7" runat="server" CssClass="ASPButton" Text="DISCOUNT" Width="100%" OnClick="BT7_Click" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT4" runat="server" CssClass="ASPButton" Text="REKENING" Width="100%" OnClick="BT4_Click" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT5" runat="server" CssClass="ASPButton" Text="PERIODE" Width="100%" OnClick="BT5_Click" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT6" runat="server" CssClass="ASPButton" Text="ARSIP" Width="100%" OnClick="BT6_Click" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_SUB" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
