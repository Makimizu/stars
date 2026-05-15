<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderRegistrasi.aspx.cs" Inherits="HEALTH.Form_Klaim.ProviderRegistrasi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td style="vertical-align: top;">
                    <table style="border-spacing: 0px">
                        <tr>
                            <td>KODE</td>
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
                                <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" Width="400px" MaxLength="100"></asp:TextBox>
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
                        <td>TANGGAL PENGAJUAN</td>
                        <td>
                            <asp:TextBox ID="TXT_TGL_REGISTRASI" runat="server" CssClass="ASPTextBox" Width="200px" MaxLength="10" Placeholder="dd-mm-yyyy"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender 
                                ID="CalendarExtender1" 
                                runat="server" 
                                TargetControlID="TXT_TGL_REGISTRASI" 
                                Format="dd-MM-yyyy" />
                        </td>
                    </tr>
                        <tr>
                            <td>NO SURAT</td>
                            <td>
                                <asp:TextBox ID="TXT_NOSURAT" runat="server" CssClass="ASPTextBox" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NO PKS SEMENTARA</td>
                            <td>
                                <asp:TextBox ID="TXT_NOPKSPROVIDER" runat="server" CssClass="ASPTextBox" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>KODE ADMEDIKA</td>
                            <td>
                                <asp:TextBox ID="TXT_ADMEDIKA" runat="server" CssClass="ASPTextBox" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">ALAMAT 1</td>
                            <td>
                                <asp:TextBox ID="TXT_ALAMAT1" runat="server" CssClass="ASPTextBox" Height="30px" TextMode="MultiLine" Width="400px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">ALAMAT 2</td>
                            <td>
                                <asp:TextBox ID="TXT_ALAMAT2" runat="server" CssClass="ASPTextBox" Height="30px" TextMode="MultiLine" Width="400px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>KODE POS</td>
                            <td>
                                <asp:TextBox ID="TXT_KODEPOS" runat="server" CssClass="ASPTextBox" Width="100px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TELEPON 1</td>
                            <td>
                                <asp:TextBox ID="TXT_TELEPON1" runat="server" CssClass="ASPTextBox" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TELEPON 2</td>
                            <td>
                                <asp:TextBox ID="TXT_TELEPON2" runat="server" CssClass="ASPTextBox" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>FAX 1</td>
                            <td>
                                <asp:TextBox ID="TXT_FAX1" runat="server" CssClass="ASPTextBox" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>FAX 2</td>
                            <td>
                                <asp:TextBox ID="TXT_FAX2" runat="server" CssClass="ASPTextBox" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>KOTA</td>
                            <td>
                                <asp:DropDownList ID="DDL_KOTA" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_KOTA_SelectedIndexChanged">
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
                            <td>NO NPWP</td>
                            <td>
                                <asp:TextBox ID="TXT_NPWPNO" runat="server" CssClass="ASPTextBox" Width="400px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NAMA NPWP</td>
                            <td>
                                <asp:TextBox ID="TXT_NPWPNAMA" runat="server" CssClass="ASPTextBox" Width="400px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">ALAMAT NPWP</td>
                            <td>
                                <asp:TextBox ID="TXT_NPWPALAMAT" runat="server" CssClass="ASPTextBox" Height="30px" TextMode="MultiLine" Width="400px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">ALASAN REGISTRASI</td>
                            <td>
                                <asp:TextBox ID="TXT_ALASAN" runat="server" CssClass="ASPTextBox" Height="30px" TextMode="MultiLine" Width="400px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                                        <td>UPLOAD DOCUMENT</td>
                                        <td>
                                            <asp:FileUpload ID="FileUploadPDF" runat="server" CssClass="ASPTextBox" ClientIDMode="Static" />
                                            <asp:Label ID="LblUploadResult" runat="server" ForeColor="Red" />
                                            <asp:Label ID="LblUploadedFileName" runat="server" ForeColor="Gray" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:FileUpload ID="FileUploadPDF2" runat="server" CssClass="ASPTextBox" ClientIDMode="Static" />
                                            <asp:Label ID="LblUploadResult2" runat="server" ForeColor="Red" />
                                            <asp:Label ID="LblUploadedFileName2" runat="server" ForeColor="Gray" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:FileUpload ID="FileUploadPDF3" runat="server" CssClass="ASPTextBox" ClientIDMode="Static" />
                                            <asp:Label ID="LblUploadResult3" runat="server" ForeColor="Red" />
                                            <asp:Label ID="LblUploadedFileName3" runat="server" ForeColor="Gray" />
                                        </td>
                                    </tr>
                        <tr>
                        <td>GENERATE SURAT</td>
                        <td>
                            <asp:DropDownList ID="DDL_TUJUAN" runat="server" CssClass="ASPDropDownList">
                                <asp:ListItem Text="-- Pilih Tujuan --" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Draft PKS Klinik Provider" Value="PKS_Klinik"></asp:ListItem>
                                <asp:ListItem Text="Draft PKS Rumah Sakit Provider" Value="PKS_RumahSakit"></asp:ListItem>
                                <asp:ListItem Text="Draft Addendum PKS Rumah Sakit" Value="Adendum"></asp:ListItem>
                                <asp:ListItem Text="NDA Takaful Keluarga" Value="NDA"></asp:ListItem>
                                <asp:ListItem Text="NDA Indonesia" Value="NDA_Nasional"></asp:ListItem>
                                <asp:ListItem Text="Perjanjian Kerja Sama Sementara" Value="Perpanjangan_PKS"></asp:ListItem>
                                <asp:ListItem Text="Surat Penawaran Kerja Sama" Value="Kerjasama_Sementara"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                      <%--  <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BtnUpload" runat="server" Text="Upload" OnClick="BtnUpload_Click" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="vertical-align: top;" class="auto-style2"></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" />
                                &nbsp;<asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" OnClick="BT_APPROVE_Click" Text="APPROVE" Font-Bold="True" ForeColor="Blue" />
                                <asp:Button ID="BT_REJECT" runat="server" CssClass="ASPButton" OnClick="BT_REJECT_Click" Text="REJECT" Font-Bold="True" ForeColor="Red" />
                                <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;" class="auto-style2"></td>
                            <td>
                                <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" OnClick="BT_DOWNLOAD_Click" Text="GENERATE" />
                                <asp:Label ID="LB_DOWNLOAD" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>

                        <tr id="TR_REJECT" runat="server">
                            <td class="auto-style1" style="vertical-align: top;">ALASAN REJECT</td>
                            <td style="vertical-align: top;">
                                <asp:TextBox ID="TXT_REJECT" runat="server" CssClass="ASPTextBox" Width="413px" MaxLength="1000" Height="44px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top;">

                    <asp:DataGrid ID="DGR_SIMILARITY" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False">
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="% SIMILARITY" HeaderText="% SIMILARITY">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ALAMAT" HeaderText="ALAMAT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS" HeaderText="STATUS">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" ForeColor="Blue" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
