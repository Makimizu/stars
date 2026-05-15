<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Penjaminan.aspx.cs" Inherits="HEALTH.Form_Klaim.Penjaminan" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width:100%;">
            <tr id="TR_BUTTONS" runat="server">
                <td  class="TDBGColor">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 11%;">
                                <asp:Button ID="BT_INFO_AWAL" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="INFO AWAL" Width="100%" OnClick="BT_INFO_AWAL_Click" />
                            </td>
                            <td style="width: 11%;">
                                <asp:Button ID="BT_DIAG_AWAL" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="DIAGNOSA AWAL" Width="100%" OnClick="BT_DIAG_AWAL_Click" />
                            </td>
                            <td id="TD_MONLIST" runat="server" style="width: 11%;">
                                <asp:Button ID="BT_MONLIST" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="MONITORING" Width="100%" OnClick="BT_MONLIST_Click" />
                            </td>
                            <td style="width: 11%;">
                                <asp:Button ID="BT_ARSIP" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="ARSIP" Width="100%" OnClick="BT_ARSIP_Click" />
                            </td>
                            <td style="width: 11%;">
                                <asp:Button ID="BT_TRACK" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="TRACK" Width="100%" OnClick="BT_TRACK_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 11%;">
                                <asp:Button ID="BT_REMARK" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="REMARK" Width="100%" OnClick="BT_REMARK_Click" />
                            </td>
                            <td id="TD_KLAIM" runat="server" style="width: 11%;">
                                <asp:Button ID="BT_CLAIM" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="TRX KLAIM" Width="100%" OnClick="BT_CLAIM_Click" BackColor="Blue" ForeColor="White" />
                            </td>
                            <td style="width: 11%;">
                                <asp:Button ID="BT_SURAT" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="SURAT" Width="100%" OnClick="BT_SURAT_Click" />
                            </td>
                            <td id="TD_AKHIR" runat="server" style="width: 11%;">
                                <asp:Button ID="BT_AKHIR" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="AKHIR" Width="100%" OnClick="BT_AKHIR_Click" BackColor="Red" ForeColor="White" />
                            </td>
                            <td style="width: 11%;">
                                <asp:Button ID="BT_CP" runat="server" Height="30px" CssClass="ASPButton" Font-Bold="True" Text="CLINICAL PATHWAY" Width="100%" OnClick="BT_CP_LIST" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <table style="border-spacing: 0px;">
                                                <tr>
                                                    <td style="width: 100px;">
                                                        <b>NO JAMINAN :</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="LB_NOSURAT" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox" Font-Bold="True" ReadOnly="True" Width="200px" Style="text-align: center;"></asp:TextBox>
                                                        <asp:Label ID="LB_ID" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <table style="border-spacing: 0px;">
                                                <tr style="vertical-align: top;">
                                                    <td>
                                                        <asp:DataGrid ID="DGR_INFO_PESERTA" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="350px">
                                                            <ItemStyle BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                                                            <HeaderStyle
                                                                Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundColumn DataField="CODE">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DESCR">
                                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                </asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                        <asp:Label ID="LB_REGNO" runat="server" Visible="False"></asp:Label>
                                                        <table id="TBL_CARI_PESERTA" runat="server" visible="false" style="background-color: green;">
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td class="auto-style12">PERUSAHAAN</td>
                                                                            <td>
                                                                                <asp:TextBox ID="TXT_CARI_COMPANY" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="auto-style12">NAMA PESERTA</td>
                                                                            <td>
                                                                                <asp:TextBox ID="TXT_CARI_PESERTA" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="auto-style12">NO PESERTA</td>
                                                                            <td>
                                                                                <asp:TextBox ID="TXT_CARI_REGNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="auto-style2">&nbsp;</td>
                                                                            <td>
                                                                                <asp:Button ID="BT_CARI_REGNO" runat="server" CssClass="ASPButton" Text="CARI" OnClick="BT_CARI_REGNO_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DataGrid ID="DGR_CARI_PESERTA" runat="server" CellPadding="4" CssClass="ASPDatagrid" GridLines="None"
                                                                        OnItemCommand="DGR_CARI_PESERTA_ItemCommand" Width="100%" ForeColor="#333333" AllowPaging="True" OnPageIndexChanged="DGR_CARI_PESERTA_PageIndexChanged" PageSize="15" ShowHeader="False">
                                                                        <EditItemStyle BackColor="#7C6F57" />
                                                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                        <AlternatingItemStyle BackColor="White" />
                                                                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                                                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                                                                            Wrap="False" />
                                                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                                                        <Columns>
                                                                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                                                                <HeaderStyle Width="40px" />
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:ButtonColumn>
                                                                        </Columns>
                                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BT_CARI2" runat="server" CssClass="ASPButton" Text="M" OnClick="BT_CARI2_Click" BackColor="Green" Font-Bold="True" ForeColor="White" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <table style="border-spacing: 0px;">
                                                <tr style="vertical-align: top;">
                                                    <td>

                                                        <asp:DataGrid ID="DGR_INFO_PROVIDER" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="350px">
                                                            <ItemStyle BackColor="#FFFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                                                            <HeaderStyle
                                                                Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundColumn DataField="CODE">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DESCR">
                                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                </asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                        <asp:Label ID="LB_KODE_PROVIDER" runat="server" Visible="False"></asp:Label>
                                                        <table id="TBL_PROVIDER" runat="server" visible="false" style="background-color: green;">
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td class="auto-style3">NAMA PROVIDER</td>
                                                                            <td>
                                                                                <asp:TextBox ID="TXT_NAMA_PROVIDER" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="auto-style12">STATUS</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="DDL_STAT_PROVIDER" runat="server" CssClass="ASPDropDownList">
                                                                                    <asp:ListItem Selected="True" Value="1">AKTIF</asp:ListItem>
                                                                                    <asp:ListItem Value="0">NON AKTIF</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="auto-style2">&nbsp;</td>
                                                                            <td>
                                                                                <asp:Button ID="BT_CARI_PROVIDER" runat="server" CssClass="ASPButton" Text="CARI" OnClick="BT_CARI_PROVIDER_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DataGrid ID="DGR_CARI_PROVIDER" runat="server" CellPadding="4" CssClass="ASPDatagrid" GridLines="None"
                                                                        OnItemCommand="DGR_CARI_PROVIDER_ItemCommand" Width="100%" ForeColor="#333333" AllowPaging="True" OnPageIndexChanged="DGR_CARI_PROVIDER_PageIndexChanged" PageSize="15" ShowHeader="False">
                                                                        <EditItemStyle BackColor="#7C6F57" />
                                                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                        <AlternatingItemStyle BackColor="White" />
                                                                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                                                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                                                                            Wrap="False" />
                                                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                                                        <Columns>
                                                                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                                                                <HeaderStyle Width="40px" />
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:ButtonColumn>
                                                                        </Columns>
                                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BT_CARI3" runat="server" CssClass="ASPButton" Text="P" OnClick="BT_CARI3_Click" BackColor="Blue" Font-Bold="True" ForeColor="White" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 150px;">
                                            <asp:Label ID="Label3" runat="server" Text="TGL MASUK" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_TGLMASUK" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TGLMASUK">
                                                    </ajaxToolkit:CalendarExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="ESTIMASI INAP" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_INAP" runat="server" CssClass="ASPTextBox" Width="30px" Style="text-align: center;"></asp:TextBox>
                                            &nbsp;<asp:Label ID="Label20" runat="server" Text="HARI" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="TGL AKHIR SEMENTARA" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="LB_TGL_AKHIR_SEMENTARA" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox" Font-Bold="True" ReadOnly="True" Width="80px" Style="text-align: center;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="PIC PASIEN" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_PIC_PASIEN" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="PIC PASIEN PHONE" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_PIC_PASIEN_PHONE" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="TIPE JAMINAN" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_TIPE_onchange"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="KELAS KAMAR" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_KELAS" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="NAMA KAMAR" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAMAKAMAR" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="HARGA KAMAR" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_HARGAKAMAR" runat="server" CssClass="ASPTextBox" Width="80"></asp:TextBox>
                                            &nbsp;
                                            <asp:Label ID="Label21" runat="server" Text="RUPIAH" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="ALASAN NAIK KELAS" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ALASANNAIKKELAS" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label13" runat="server" Text="NO REKAM MED" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="TXT_NOREK_MEDIS" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="DOKTER RAWAT" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_DOKTER" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="GELAR DOKTER" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_GELAR" runat="server" CssClass="ASPDropDownList" Width="180px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="KONTAK" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_KONTAK" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="PLAFON" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_PLAFON" runat="server" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                                            &nbsp;
                                            <asp:Label ID="Label18" runat="server" Text="RUPIAH" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="TR_SMS" runat="server">
                                        <td>
                                            <asp:Button ID="BT_SMS" runat="server" CssClass="ASPButton" OnClick="BT_SMS_Click" Text="SMS EXCEL" Height="17px" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_SMS" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" />
                                            &nbsp;<asp:Button ID="BT_CANCEL" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_CANCEL_Click" Text="CANCEL" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <asp:Button runat="server" Height="30px" Font-Size="14px" ID="BUKU_TARIF_LIST" CssClass="ASPButton" BackColor="White" Text="BUKU TARIF" OnClick="BT_BUKU_TARIF_LIST"/>
                                <asp:Button runat="server" Height="30px" Font-Size="14px" ID="CPStatus" CssClass="ASPButton" BackColor="White" Text="Loading Status CP" OnClick="BT_CP_Click"/>

                                <asp:DataGrid ID="DGR_TARIF" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_TARIF_ItemCommand">
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <AlternatingItemStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                        <%--<asp:BoundColumn DataField="KODE_TARIF_DESCR" HeaderText="JENIS TARIF"></asp:BoundColumn>--%>
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
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="TD_WARNING" runat="server" style="padding:15px;border:2px solid red;" visible="false">
                    <asp:Label ID="LB_WARNING" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr id="TR_APPROVAL" runat="server">
                <td style="text-align: center;">
                    <br />
                    <center>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <center>
                                <asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Green" OnClick="BT_APPROVE_Click" Text="APPROVE" />
                                &nbsp;<asp:Button ID="BT_REJECT" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" OnClick="BT_REJECT_Click" Text="REJECT" />
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <center>
                                <asp:Label ID="Label19" runat="server" Text="ALASAN REJECT/CANCEL" Font-Bold="true" CssClass="ASPLabel"></asp:Label><br />
                                <asp:TextBox ID="TXT_REJECT" runat="server" CssClass="ASPTextBox" Width="500px" MaxLength="1000" Height="44px" BackColor="Yellow" TextMode="MultiLine"></asp:TextBox>
                                </center>
                            </td>
                        </tr>
                    </table>
                    </center>
                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="540px" Width="960px" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%; Height: 100%;">
                    <tr>
                        <td>
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                        <td class="TDBGColor">
                            <asp:LinkButton ID="LB_TITLE" runat="server" Font-Size="Medium" CssClass="ASPButton" Font-Bold="True" ForeColor="Black"></asp:LinkButton>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <center>
                            <iframe id="ifClaim" runat="server" src=""  width="100%" height="500"></iframe>
                            </center>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
