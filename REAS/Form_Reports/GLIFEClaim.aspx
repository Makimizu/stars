<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GLIFEClaim.aspx.cs" Inherits="REAS.Form_Reports.GLIFEClaim" %>
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
                        <asp:BoundColumn DataField="AR_DATE" HeaderText="AR DATE"  DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundColumn DataField="ID" HeaderText="ID">
                            <ItemStyle Font-Bold="True" ForeColor="Green" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATUS_PROCESS" HeaderText="STATUS" />
                        
                        <asp:BoundColumn DataField="REAS_KOAS_NAME" HeaderText="REAS NAME" />
                        <asp:BoundColumn DataField="NAMA_PESERTA" HeaderText="NAMA PESERTA" />
                        <asp:BoundColumn DataField="NO_POLIS" HeaderText="NO POLIS" />
                        <asp:BoundColumn DataField="NAMA_LEMBAGA" HeaderText="NAMA LEMBAGA" />
                        <asp:BoundColumn DataField="AWAL_KONTRAK" HeaderText="AWAL KONTRAK"  DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundColumn DataField="AKHIR_KONTRAK" HeaderText="AKHIR KONTRAK"  DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundColumn DataField="TGL_LAHIR" HeaderText="TGL LAHIR"  DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundColumn DataField="TGL_MENINGGAL" HeaderText="TGL MENINGGAL"  DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundColumn DataField="TGL_KLAIM" HeaderText="TGL KLAIM"  DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundColumn DataField="MANFAAT" HeaderText="MANFAAT" />
                        <asp:BoundColumn DataField="BEBAN_REAS" HeaderText="BEBAN REAS" />
                        <asp:BoundColumn DataField="JUMLAH_KLAIM" HeaderText="JUMLAH KLAIM" />
                        <asp:BoundColumn DataField="SHARE_REAS" HeaderText="SHARE REAS" />
                        <asp:BoundColumn DataField="KETERANGAN" HeaderText="KETERANGAN" />
                        <asp:BoundColumn DataField="NO_KLAIM" HeaderText="NO KLAIM" />
                        <asp:BoundColumn DataField="MONTH" HeaderText="MONTH" />
                        <asp:BoundColumn DataField="YEAR" HeaderText="YEAR" />
                        <asp:BoundColumn DataField="NP_TREATY" HeaderText="NP TREATY" />
                        <asp:BoundColumn DataField="PERIODE_LAPORAN" HeaderText="PERIODE LAPORAN" />
                        <asp:BoundColumn DataField="CHANNEL" HeaderText="CHANNEL" />
                        <asp:BoundColumn DataField="STATUS_ATK" HeaderText="STATUS ATK" />
                        <asp:BoundColumn DataField="PERIODE_PELAPORAN_WEEKLY" HeaderText="PERIODE PELAPORAN WEEKLY"  DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundColumn DataField="STATUS_PELAPORAN" HeaderText="STATUS PELAPORAN" />
                        <asp:BoundColumn DataField="GENERATE_DATE" HeaderText="GENERATE JOURNAL DATE" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundColumn DataField="AGING_DAY" HeaderText="AGING DAY" />
                        <asp:BoundColumn DataField="AGING_MONTH" HeaderText="AGING MONTH" />
                        <asp:BoundColumn DataField="AGING_DESC" HeaderText="AGING DESC" />
                        <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_1" HeaderText="NO SURAT TECHNICAL 1" />
                        <asp:BoundColumn DataField="AMOUNT_TECHNICAL_1" HeaderText="AMOUNT TECHNICAL 1" />
                        <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL_1" HeaderText="TECHNICAL UPDATE DATE 1" DataFormatString="{0:dd MMM yyyy}" />

                        <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_2" HeaderText="NO SURAT TECHNICAL 2" />
                        <asp:BoundColumn DataField="AMOUNT_TECHNICAL_2" HeaderText="AMOUNT TECHNICAL 2" />
                        <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL_2" HeaderText="TECHNICAL UPDATE DATE 2" DataFormatString="{0:dd MMM yyyy}" />

                        <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_3" HeaderText="NO SURAT TECHNICAL 3" />
                        <asp:BoundColumn DataField="AMOUNT_TECHNICAL_3" HeaderText="AMOUNT TECHNICAL 3" />
                        <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL_3" HeaderText="TECHNICAL UPDATE DATE 3" DataFormatString="{0:dd MMM yyyy}" />

                        <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_4" HeaderText="NO SURAT TECHNICAL 4" />
                        <asp:BoundColumn DataField="AMOUNT_TECHNICAL_4" HeaderText="AMOUNT TECHNICAL 4" />
                        <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL_4" HeaderText="TECHNICAL UPDATE DATE 4" DataFormatString="{0:dd MMM yyyy}" />

                        <asp:BoundColumn DataField="NO_SURAT_TECHNICAL_5" HeaderText="NO SURAT TECHNICAL 5" />
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

                        <asp:BoundColumn DataField="STATUS_DATA" HeaderText="STATUS DATA">
                            <ItemStyle Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VERIFIED_DATE" HeaderText="VERIFIED DATE" DataFormatString="{0:dd MMM yyyy}" />

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


