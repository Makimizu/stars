<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContributionReasIndividual.aspx.cs" Inherits="REAS.Form_Reports.ContributionReasIndividual" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .pager-btn, .pager-link {
        display: inline-block;
        margin: 1px;
        padding: 4px 9px;
        border: 1px solid #4A3C8C;
        border-radius: 4px;
        background-color: #f8f8f8;
        color: #4A3C8C;
        text-decoration: none;
        cursor: pointer;
    }

    .pager-btn:hover, .pager-link:hover {
        background-color: #4A3C8C;
        color: #fff;
    }

    .pager-link.active {
        background-color: #4A3C8C;
        color: white;
        font-weight: bold;
    }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <div id="loading" style="display:none; position:fixed; top:50%; left:50%; transform:translate(-50%, -50%); z-index:9999; background-color: rgba(255, 255, 255, 0.8); padding: 20px; border-radius: 10px; text-align: center;">
            <img src="../Content/loading.gif" alt="Loading..." style="border: 0; width: 100px;" />
            <p>Loading...</p>
        </div>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">REAS NAME</td>
                                        <td>
                                             <asp:DropDownList 
                                                ID="DDL_REAS" 
                                                runat="server" 
                                                AutoPostBack="false" 
                                                CssClass="ASPDropDownList" 
                                                OnSelectedIndexChanged="DDL_REAS_SelectedIndexChanged"  
                                                Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>STATUS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS" runat="server" AutoPostBack="true" CssClass="ASPDropDownList" 
                                                OnSelectedIndexChanged="DDL_STATUS_SelectedIndexChanged"  Width="200px">
                                                <asp:ListItem Value="" ></asp:ListItem>
                                                <asp:ListItem Value="new">NEW</asp:ListItem>
                                                <asp:ListItem Value="technical">TECHNICAL</asp:ListItem>
                                                <asp:ListItem Value="settlement">SETTLEMENT</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="LBL_STATUS" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">AR DATE</td>
                                        <td>
                                           
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                            
                                            <asp:Label ID="Label1" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="XX-Small">-</asp:Label>
                                            
                                            <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodEnd" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                            </ajaxToolkit:CalendarExtender>
                                           
                                        </td>
                                    </tr>
                                   <tr>
                                        
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" OnClientClick="showLoading(false);"/>
                                            <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD" OnClick="BT_DOWNLOAD_Click" Width="100px" BackColor="Aqua"  OnClientClick="showLoading(true);"/>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_JOURNAL" runat="server" CssClass="ASPButton" Text="DRAFT JOURNAL" BackColor="Yellow" OnClick="BT_JOURNAL_Click" Width="100px" OnClientClick="showLoading();" Visible="false"/>
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
                    <div id="notif" style="display:none; 
                        color:#F7F7F7; 
                        background:#4A3C8C;
                        padding:6px 12px; 
                        margin-bottom:6px; 
                        border:1px solid #4A3C8C;
                        font-size:12px;">
                        Data sedang dalam proses Download, Tinggalkan halaman ini jika ingin melanjutkan proses di tempat lain
                    </div>

                    <asp:Label ID="LB_DOWNLOAD" runat="server"></asp:Label>
                    <asp:Label ID="LB_RESULT" runat="server" Font-Size="11px" Font-Bold="false"></asp:Label>
                   
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical"
                        Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" 
                        Font-Size="11px"
                        PageSize="200" >
                        
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" Wrap="false"  Font-Size="13px"/>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />

                        <Columns>
                            <asp:BoundColumn DataField="AR_DATE" HeaderText="AR DATE" DataFormatString="{0:dd MMM yyyy}" />
                            <asp:BoundColumn DataField="ID" HeaderText="ID" >
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS" HeaderText="STATUS" />
                            <asp:BoundColumn DataField="REAS_NAME" HeaderText="REAS NAME" />
                            <asp:BoundColumn DataField="NBRN" HeaderText="NB/RN" />
                            <asp:BoundColumn DataField="KODE_PERUSAHAAN" HeaderText="KODE PERUSAHAAN" />
                            <asp:BoundColumn DataField="LINI_USAHA" HeaderText="LINI USAHA" />
                            <asp:BoundColumn DataField="NO_POLIS" HeaderText="NO POLIS" />
                            <asp:BoundColumn DataField="KODE_PRODUK" HeaderText="KODE PRODUK" />
                            <asp:BoundColumn DataField="NAMA_PESERTA" HeaderText="NAMA PESERTA" />
                            <asp:BoundColumn DataField="TGL_LAHIR_PESERTA" HeaderText="TGL LAHIR PESERTA" DataFormatString="{0:dd MMM yyyy}" />
                            <asp:BoundColumn DataField="PEMEGANG_POLIS" HeaderText="PEMEGANG POLIS" />
                            <asp:BoundColumn DataField="TGL_LAHIR_PEMEGANG_POLIS" HeaderText="TGL LAHIR PEMEGANG POLIS" DataFormatString="{0:dd MMM yyyy}" />
                            <asp:BoundColumn DataField="TANGGAL_MASUK" HeaderText="TANGGAL MASUK" DataFormatString="{0:dd MMM yyyy}" />
                            <asp:BoundColumn DataField="USIA_PESERTA_NB" HeaderText="USIA PESERTA NB" />
                            <asp:BoundColumn DataField="USIA_PESERTA_RN" HeaderText="USIA PESERTA RN" />
                            <asp:BoundColumn DataField="PERIODE_TAHUN" HeaderText="PERIODE TAHUN" />
                            <asp:BoundColumn DataField="PERIODE_BULAN" HeaderText="PERIODE BULAN" />
                            <asp:BoundColumn DataField="EM" HeaderText="EM" />
                            <asp:BoundColumn DataField="EP" HeaderText="EP" />
                            <asp:BoundColumn DataField="KODE_PRODUK_UTAMA" HeaderText="KODE PRODUK UTAMA" />
                            <asp:BoundColumn DataField="JENIS_PLAN" HeaderText="JENIS PLAN" />
                            <asp:BoundColumn DataField="NAMA_PLAN" HeaderText="NAMA PLAN" />
                            <asp:BoundColumn DataField="KODE_RESIKO" HeaderText="KODE RESIKO" />
                            <asp:BoundColumn DataField="FLAG_JENIS_PLAN" HeaderText="FLAG JENIS PLAN" />
                            <asp:BoundColumn DataField="FLAG_MEDIS" HeaderText="FLAG MEDIS" />
                            <asp:BoundColumn DataField="TAHUN_RENEWAL_KE" HeaderText="TAHUN RENEWAL KE" />
                            <asp:BoundColumn DataField="FAKTOR_PENGALI" HeaderText="FAKTOR PENGALI" />
                            <asp:BoundColumn DataField="TABEL_PENURUNAN_MANFAAT" HeaderText="TABEL PENURUNAN MANFAAT" />
                            <asp:BoundColumn DataField="NILAI_PENURUNAN_MANFAAT" HeaderText="NILAI PENURUNAN MANFAAT"  />
                            <asp:BoundColumn DataField="MANFAAT_TAKAFUL_AWAL" HeaderText="MANFAAT TAKAFUL AWAL"  />
                            <asp:BoundColumn DataField="RESIKO_SETELAH_PENGALI" HeaderText="RESIKO SETELAH PENGALI"  />
                            <asp:BoundColumn DataField="RETENSI_AWAL" HeaderText="RETENSI AWAL"  />
                            <asp:BoundColumn DataField="CURR" HeaderText="CURRENCY" />
                            <asp:BoundColumn DataField="BAGIAN_ATK" HeaderText="BAGIAN ATK"  />
                            <asp:BoundColumn DataField="BAGIAN_REAS" HeaderText="BAGIAN REAS"  />
                            <asp:BoundColumn DataField="RATE_REAS_UTAMA" HeaderText="RATE REAS UTAMA"  />
                            <asp:BoundColumn DataField="KONTRIBUSI_STANDARD" HeaderText="KONTRIBUSI STANDARD"  />
                            <asp:BoundColumn DataField="EXTRA_MORTALITY" HeaderText="EXTRA MORTALITY"  />
                            <asp:BoundColumn DataField="EXTRA_PREMI" HeaderText="EXTRA PREMI"  />
                            <asp:BoundColumn DataField="KONTRIBUSI_REAS" HeaderText="KONTRIBUSI REAS"  />
                            <asp:BoundColumn DataField="TABARRU" HeaderText="TABARRU"  />
                            <asp:BoundColumn DataField="UJROH" HeaderText="UJROH"  />
                            <asp:BoundColumn DataField="KODE_BENEFIT_1" HeaderText="KODE BENEFIT 1" />
                            <asp:BoundColumn DataField="JENIS_PLAN_1" HeaderText="JENIS PLAN 1" />
                            <asp:BoundColumn DataField="NAMA_PLAN_1" HeaderText="NAMA PLAN 1" />
                            <asp:BoundColumn DataField="MANFAAT_TAKAFUL_1" HeaderText="MANFAAT TAKAFUL 1"  />
                            <asp:BoundColumn DataField="RETENSI_AWAL_1" HeaderText="RETENSI AWAL 1"  />
                            <asp:BoundColumn DataField="BAGIAN_ATK_1" HeaderText="BAGIAN ATK 1"  />
                            <asp:BoundColumn DataField="BAGIAN_REAS_1" HeaderText="BAGIAN REAS 1"  />
                            <asp:BoundColumn DataField="RATE_REAS_1" HeaderText="RATE REAS 1"  />
                            <asp:BoundColumn DataField="KONTRIBUSI_REAS_1" HeaderText="KONTRIBUSI REAS 1"  />
                            <asp:BoundColumn DataField="TABARRU_REAS_1" HeaderText="TABARRU REAS 1"  />
                            <asp:BoundColumn DataField="UJROH_REAS_1" HeaderText="UJROH REAS 1"  />
                            <asp:BoundColumn DataField="KODE_BENEFIT_2" HeaderText="KODE BENEFIT 2" />
                            <asp:BoundColumn DataField="JENIS_PLAN_2" HeaderText="JENIS PLAN 2" />
                            <asp:BoundColumn DataField="NAMA_PLAN_2" HeaderText="NAMA PLAN 2" />
                            <asp:BoundColumn DataField="MANFAAT_TAKAFUL_2" HeaderText="MANFAAT TAKAFUL 2"  />
                            <asp:BoundColumn DataField="RETENSI_AWAL_2" HeaderText="RETENSI AWAL 2"  />
                            <asp:BoundColumn DataField="BAGIAN_ATK_2" HeaderText="BAGIAN ATK 2"  />
                            <asp:BoundColumn DataField="BAGIAN_REAS_2" HeaderText="BAGIAN REAS 2"  />
                            <asp:BoundColumn DataField="RATE_REAS_2" HeaderText="RATE REAS 2"  />
                            <asp:BoundColumn DataField="KONTRIBUSI_REAS_2" HeaderText="KONTRIBUSI REAS 2"  />
                            <asp:BoundColumn DataField="TABARRU_REAS_2" HeaderText="TABARRU REAS 2"  />
                            <asp:BoundColumn DataField="UJROH_REAS_2" HeaderText="UJROH REAS 2"  />
                            <asp:BoundColumn DataField="KODE_BENEFIT_3" HeaderText="KODE BENEFIT 3" />
                            <asp:BoundColumn DataField="JENIS_PLAN_3" HeaderText="JENIS PLAN 3" />
                            <asp:BoundColumn DataField="NAMA_PLAN_3" HeaderText="NAMA PLAN 3" />
                            <asp:BoundColumn DataField="MANFAAT_TAKAFUL_3" HeaderText="MANFAAT TAKAFUL 3"  />
                            <asp:BoundColumn DataField="RETENSI_AWAL_3" HeaderText="RETENSI AWAL 3"  />
                            <asp:BoundColumn DataField="BAGIAN_ATK_3" HeaderText="BAGIAN ATK 3"  />
                            <asp:BoundColumn DataField="BAGIAN_REAS_3" HeaderText="BAGIAN REAS 3"  />
                            <asp:BoundColumn DataField="RATE_REAS_3" HeaderText="RATE REAS 3"  />
                            <asp:BoundColumn DataField="KONTRIBUSI_REAS_3" HeaderText="KONTRIBUSI REAS 3"  />
                            <asp:BoundColumn DataField="TABARRU_REAS_3" HeaderText="TABARRU REAS 3"  />
                            <asp:BoundColumn DataField="UJROH_REAS_3" HeaderText="UJROH REAS 3"  />
                            <asp:BoundColumn DataField="KODE_BENEFIT_4" HeaderText="KODE BENEFIT 4" />
                            <asp:BoundColumn DataField="JENIS_PLAN_4" HeaderText="JENIS PLAN 4" />
                            <asp:BoundColumn DataField="NAMA_PLAN_4" HeaderText="NAMA PLAN 4" />
                            <asp:BoundColumn DataField="MANFAAT_TAKAFUL_4" HeaderText="MANFAAT TAKAFUL 4"  />
                            <asp:BoundColumn DataField="RETENSI_AWAL_4" HeaderText="RETENSI AWAL 4"  />
                            <asp:BoundColumn DataField="BAGIAN_ATK_4" HeaderText="BAGIAN ATK 4"  />
                            <asp:BoundColumn DataField="BAGIAN_REAS_4" HeaderText="BAGIAN REAS 4"  />
                            <asp:BoundColumn DataField="RATE_REAS_4" HeaderText="RATE REAS 4"  />
                            <asp:BoundColumn DataField="KONTRIBUSI_REAS_4" HeaderText="KONTRIBUSI REAS 4"  />
                            <asp:BoundColumn DataField="TABARRU_REAS_4" HeaderText="TABARRU REAS 4"  />
                            <asp:BoundColumn DataField="UJROH_REAS_4" HeaderText="UJROH REAS 4"  />
                            <asp:BoundColumn DataField="KODE_BENEFIT_5" HeaderText="KODE BENEFIT 5" />
                            <asp:BoundColumn DataField="JENIS_PLAN_5" HeaderText="JENIS PLAN 5" />
                            <asp:BoundColumn DataField="NAMA_PLAN_5" HeaderText="NAMA PLAN 5" />
                            <asp:BoundColumn DataField="MANFAAT_TAKAFUL_5" HeaderText="MANFAAT TAKAFUL 5"  />
                            <asp:BoundColumn DataField="RETENSI_AWAL_5" HeaderText="RETENSI AWAL 5"  />
                            <asp:BoundColumn DataField="BAGIAN_ATK_5" HeaderText="BAGIAN ATK 5"  />
                            <asp:BoundColumn DataField="BAGIAN_REAS_5" HeaderText="BAGIAN REAS 5"  />
                            <asp:BoundColumn DataField="RATE_REAS_5" HeaderText="RATE REAS 5"  />
                            <asp:BoundColumn DataField="KONTRIBUSI_REAS_5" HeaderText="KONTRIBUSI REAS 5"  />
                            <asp:BoundColumn DataField="TABARRU_REAS_5" HeaderText="TABARRU REAS 5"  />
                            <asp:BoundColumn DataField="UJROH_REAS_5" HeaderText="UJROH REAS 5"  />
                            <asp:BoundColumn DataField="CURRENCY" HeaderText="CURRENCY" />
                            <asp:BoundColumn DataField="TIPE_REASURANSI" HeaderText="TIPE REASURANSI" />
                            <asp:BoundColumn DataField="KATEGORI_REAS" HeaderText="KATEGORI REAS" />
                            <asp:BoundColumn DataField="TGL_INPUT" HeaderText="TGL INPUT" DataFormatString="{0:dd MMM yyyy}" />
                            <asp:BoundColumn DataField="TGL_PRODUKSI" HeaderText="TGL PRODUKSI" DataFormatString="{0:dd MMM yyyy}" />
                            <asp:BoundColumn DataField="BULAN_PRODUKSI" HeaderText="BULAN PRODUKSI" />
                            <asp:BoundColumn DataField="KETERANGAN" HeaderText="KETERANGAN" />
                            <asp:BoundColumn DataField="KONTRIBUSI_TOTAL" HeaderText="KONTRIBUSI TOTAL"  />
                            <asp:BoundColumn DataField="TABARRU_TOTAL" HeaderText="TABARRU TOTAL"  />
                            <asp:BoundColumn DataField="UJROH_TOTAL" HeaderText="UJROH TOTAL"  />
                            <asp:BoundColumn DataField="KONTRIBUSI_PER_REAS" HeaderText="KONTRIBUSI (REAS)"  />
                            <asp:BoundColumn DataField="TABARRU_REAS" HeaderText="TABARRU (REAS)"  />
                            <asp:BoundColumn DataField="UJROH_REAS" HeaderText="UJROH (REAS)"  />
                            <asp:BoundColumn DataField="TREATY_ID" HeaderText="TREATY ID" />
                            <asp:BoundColumn DataField="GROUPING" HeaderText="GROUPING" />
                            <asp:BoundColumn DataField="TREATY_DOCNO" HeaderText="TREATY DOCNO" />
                            <asp:BoundColumn DataField="TREATY_DESCRIPTION" HeaderText="TREATY DESCRIPTION" />
                            <asp:BoundColumn DataField="GENERATE_DATE" HeaderText="GENERATE JOURNAL DATE" DataFormatString="{0:dd MMM yyyy}" />
                            <asp:BoundColumn DataField="AGING_DAY" HeaderText="AGING DAY" />
                            <asp:BoundColumn DataField="AGING_MONTH" HeaderText="AGING MONTH" />
                            <asp:BoundColumn DataField="AGING_DESC" HeaderText="AGING DESCR" />
                            <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_1" HeaderText="NO SURAT TECHNICAL 1" />
                            <asp:BoundColumn DataField="AMOUNT_TABBARU_1" HeaderText="AMOUNT TABBARU 1" />
                            <asp:BoundColumn DataField="AMOUNT_UJROH_1" HeaderText="AMOUNT UJROH 1" />
                            <asp:BoundColumn DataField="AMOUNT_TECHNICAL_1" HeaderText="AMOUNT TECHNICAL 1" />
                            <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL_1" HeaderText="TECHNICAL UPDATE DATE 1" DataFormatString="{0:dd MMM yyyy}" />

                            <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_2" HeaderText="NO SURAT TECHNICAL 2" />
                            <asp:BoundColumn DataField="AMOUNT_TABBARU_2" HeaderText="AMOUNT TABBARU 2" />
                            <asp:BoundColumn DataField="AMOUNT_UJROH_2" HeaderText="AMOUNT UJROH 2" />
                            <asp:BoundColumn DataField="AMOUNT_TECHNICAL_2" HeaderText="AMOUNT TECHNICAL 2" />
                            <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL_2" HeaderText="TECHNICAL UPDATE DATE 2" DataFormatString="{0:dd MMM yyyy}" />

                            <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_3" HeaderText="NO SURAT TECHNICAL 3" />
                            <asp:BoundColumn DataField="AMOUNT_TABBARU_3" HeaderText="AMOUNT TABBARU 3" />
                            <asp:BoundColumn DataField="AMOUNT_UJROH_3" HeaderText="AMOUNT UJROH 3" />
                            <asp:BoundColumn DataField="AMOUNT_TECHNICAL_3" HeaderText="AMOUNT TECHNICAL 3" />
                            <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL_3" HeaderText="TECHNICAL UPDATE DATE 3" DataFormatString="{0:dd MMM yyyy}" />

                            <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_4" HeaderText="NO SURAT TECHNICAL 4" />
                            <asp:BoundColumn DataField="AMOUNT_TABBARU_4" HeaderText="AMOUNT TABBARU 4" />
                            <asp:BoundColumn DataField="AMOUNT_UJROH_4" HeaderText="AMOUNT UJROH 4" />
                            <asp:BoundColumn DataField="AMOUNT_TECHNICAL_4" HeaderText="AMOUNT TECHNICAL 4" />
                            <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL_4" HeaderText="TECHNICAL UPDATE DATE 4" DataFormatString="{0:dd MMM yyyy}" />

                            <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_5" HeaderText="NO SURAT TECHNICAL 5" />
                            <asp:BoundColumn DataField="AMOUNT_TABBARU_5" HeaderText="AMOUNT TABBARU 5" />
                            <asp:BoundColumn DataField="AMOUNT_UJROH_5" HeaderText="AMOUNT UJROH 5" />
                            <asp:BoundColumn DataField="AMOUNT_TECHNICAL_5" HeaderText="AMOUNT TECHNICAL 5" />
                            <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL_5" HeaderText="TECHNICAL UPDATE DATE 5" DataFormatString="{0:dd MMM yyyy}" />

                            <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_1" HeaderText="NO SURAT FIN SETTLE 1" />
                            <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_1" HeaderText="AMOUNT FIN SETTLE 1" />
                            <asp:BoundColumn DataField="UPDATE_DATE_SETTLEMENT_1" HeaderText="SETTLEMENT UPDATE DATE 1" DataFormatString="{0:dd MMM yyyy}" />

                            <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_2" HeaderText="NO SURAT FIN SETTLE 2" />
                            <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_2" HeaderText="AMOUNT FIN SETTLE 2" />
                            <asp:BoundColumn DataField="UPDATE_DATE_SETTLEMENT_2" HeaderText="SETTLEMENT UPDATE DATE 2" DataFormatString="{0:dd MMM yyyy}" />

                            <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_3" HeaderText="NO SURAT FIN SETTLE 3" />
                            <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_3" HeaderText="AMOUNT FIN SETTLE 3" />
                            <asp:BoundColumn DataField="UPDATE_DATE_SETTLEMENT_3" HeaderText="SETTLEMENT UPDATE DATE 3" DataFormatString="{0:dd MMM yyyy}" />

                            <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_4" HeaderText="NO SURAT FIN SETTLE 4" />
                            <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_4" HeaderText="AMOUNT FIN SETTLE 4" />
                            <asp:BoundColumn DataField="UPDATE_DATE_SETTLEMENT_4" HeaderText="SETTLEMENT UPDATE DATE 4" DataFormatString="{0:dd MMM yyyy}" />

                            <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_5" HeaderText="NO SURAT FIN SETTLE 5" />
                            <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_5" HeaderText="AMOUNT FIN SETTLE 5" />
                            <asp:BoundColumn DataField="UPDATE_DATE_SETTLEMENT_5" HeaderText="SETTLEMENT UPDATE DATE 5" DataFormatString="{0:dd MMM yyyy}" />

                            
                        </Columns>
                    </asp:DataGrid>
                    <div style="margin-top:6px;text-align:left;">
                        <asp:LinkButton ID="lnkPrev" runat="server" Text="◀ Prev" OnClick="lnkPrev_Click" CssClass="pager-btn" />
                        <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server"
                                    CommandName="Page"
                                    CommandArgument='<%# Eval("PageNumber") %>'
                                    Text='<%# Eval("PageNumber") %>'
                                    CssClass="pager-link" />
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:LinkButton ID="lnkNext" runat="server" Text="Next ▶" OnClick="lnkNext_Click" CssClass="pager-btn" />
                    </div>
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        function showLoading(isDownload) {
            document.getElementById("loading").style.display = "block";
            document.getElementById("notif").style.display = "none";

            if (isDownload === true) {
                // notif langsung muncul
                showNotif();

                // loading AUTO HILANG (karena page tidak reload)
                setTimeout(function () {
                    hideLoading();
                }, 10000); // 5 detik (sesuaikan)
            }

            return true;
        }

        function hideLoading() {
            var loadingElement = document.getElementById("loading");
            if (loadingElement) {
                loadingElement.style.display = "none";
            }
        }

        function showNotif() {
            var notif = document.getElementById("notif");
            notif.style.display = "block";

            setTimeout(function () {
                notif.style.display = "none";
            }, 20000);
        }

        // untuk SEARCH / postback biasa
        window.onload = function () {
            hideLoading();
        };
    </script>

</body>
</html>
