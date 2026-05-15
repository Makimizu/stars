<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GLIFEContribution.aspx.cs" Inherits="REAS.Form_Reports.GLIFEContribution" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
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
                                            <%--<asp:TextBox ID="TXT_REASNAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>--%>
                                            <asp:DropDownList 
                                                ID="DDL_REAS" 
                                                runat="server" 
                                                AutoPostBack="False" 
                                                CssClass="ASPDropDownList" 
                                                OnSelectedIndexChanged="DDL_REAS_SelectedIndexChanged"  
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>STATUS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" 
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
                                    <%--<tr>
                                        <td style="width: 100px;"></td>
                                        <td>
                                            <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                            <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD" OnClick="BT_DOWNLOAD_Click" Width="100px" />
                                            
                                        </td>
                                    </tr>--%>
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
                                            <td style="width: 100px;">UPDATE DATE</td>
                                            <td>
                                                 <asp:CheckBox ID="CheckBox1" runat="server" oncheckedchanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                                <asp:TextBox ID="TXT_STARTDATE_UPDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceUpdatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE_UPDATE">
                                                </ajaxToolkit:CalendarExtender>
                
                                                <asp:Label ID="Label2" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="XX-Small">-</asp:Label>
                
                                                <asp:TextBox ID="TXT_ENDDATE_UPDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceUpdatePeriodEnd" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE_UPDATE">
                                                </ajaxToolkit:CalendarExtender>
               
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 30px;"></td>
                                <td>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" OnClientClick="showLoading();"/>
                                                <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD" OnClick="BT_DOWNLOAD_Click" Width="100px" BackColor="Aqua"/>
                
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <asp:Button ID="BT_JOURNAL" runat="server" CssClass="ASPButton" Text="DRAFT JOURNAL" BackColor="Yellow" OnClick="BT_JOURNAL_Click" Width="100px" OnClientClick="showLoading();"/>
                                            </td>
                                        </tr>--%>
                                    </table>
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_DOWNLOAD" runat="server"></asp:Label>
                    <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" 
                        OnItemCommand="DGR_ItemCommand" Width="100%" ItemStyle-Wrap="true" 
                        AutoGenerateColumns="False" AllowPaging="True" 
                        Font-Size="11px"
                        OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20" CssClass="ASPDatagrid">
             
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" Wrap="false"/>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Left" Mode="NumericPages" />

                        <Columns>
    <asp:BoundColumn DataField="AR_DATE" HeaderText="AR_DATE" DataFormatString="{0:dd MMM yyyy}" />
    
    <asp:BoundColumn DataField="ID" HeaderText="ID">
        <ItemStyle Font-Bold="True" ForeColor="Green" HorizontalAlign="Right" />
    </asp:BoundColumn>
    <asp:BoundColumn DataField="STATUS" HeaderText="STATUS"/>
    <asp:BoundColumn DataField="REAS_COMPANY" HeaderText="REAS NAME" />
    <asp:BoundColumn DataField="TIPE" HeaderText="TIPE" />
    <asp:BoundColumn DataField="NO_POLIS" HeaderText="NO POLIS" />
    <asp:BoundColumn DataField="PERUSAHAAN" HeaderText="PERUSAHAAN" />
    <asp:BoundColumn DataField="TGL_MULAI_POLIS" HeaderText="TGL MULAI POLIS" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="TGL_AKHIR_POLIS" HeaderText="TGL AKHIR POLIS" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="MEMBER_ID" HeaderText="MEMBER ID" />
    <asp:BoundColumn DataField="NAMA_PESERTA" HeaderText="NAMA PESERTA" />
    <asp:BoundColumn DataField="TGL_LAHIR" HeaderText="TGL LAHIR" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="SEX" HeaderText="SEX" />
    <asp:BoundColumn DataField="FAMILY_GROUP" HeaderText="FAMILY GROUP" />
    <asp:BoundColumn DataField="AGE_REG" HeaderText="AGE REG" />
    <asp:BoundColumn DataField="PAKET" HeaderText="PAKET" />
    <asp:BoundColumn DataField="PLAN_CODE" HeaderText="PLAN CODE" />
    <asp:BoundColumn DataField="TABARRU_FULL_NET_SETELAH_DISC" HeaderText="TABARRU FULL (Net Setelah Disc)" />
    <asp:BoundColumn DataField="SHARE_RETAKAFUL" HeaderText="SHARE RETAKAFUL" />
    <asp:BoundColumn DataField="SHARE_ATK" HeaderText="SHARE ATK" />
    <asp:BoundColumn DataField="CONTRIBUTION_RE" HeaderText="CONTRIBUTION RE" />
    <asp:BoundColumn DataField="TABARRU_RE" HeaderText="TABARRU RE" />
    <asp:BoundColumn DataField="UJROH_RE" HeaderText="UJROH RE" />
    <asp:BoundColumn DataField="PERIODE_PRODUKSI" HeaderText="PERIODE PRODUKSI" />
    <asp:BoundColumn DataField="BULAN_PRODUKSI" HeaderText="BULAN PRODUKSI" />
    <asp:BoundColumn DataField="TAHUN_PRODUKSI" HeaderText="TAHUN PRODUKSI" />
    <asp:BoundColumn DataField="PERIODE_AKAD" HeaderText="PERIODE AKAD" />
    <asp:BoundColumn DataField="BULAN_AKAD" HeaderText="BULAN AKAD" />
    <asp:BoundColumn DataField="TAHUN_AKAD" HeaderText="TAHUN AKAD" />
    <asp:BoundColumn DataField="CARA_BAYAR" HeaderText="CARA BAYAR" />
    <asp:BoundColumn DataField="GENERATE_DATE" HeaderText="GENERATE DATE" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="AGING_NEW" HeaderText="AGING NEW" />
    <asp:BoundColumn DataField="SURAT_PENGAJUAN" HeaderText="SURAT PENGAJUAN" />
    <asp:BoundColumn DataField="STATUS_TECHNICAL" HeaderText="STATUS TECHNICAL" />
    <asp:BoundColumn DataField="NO_SURAT_TECHNICAL" HeaderText="NO SURAT TECHNICAL" />
    <asp:BoundColumn DataField="PERIHAL_SURAT_TECHNICAL" HeaderText="PERIHAL SURAT TECHNICAL" />
    <asp:BoundColumn DataField="AMOUNT_TECHNICAL" HeaderText="AMOUNT TECHNICAL" />
    <asp:BoundColumn DataField="UPDATE_DATE_TECHNICAL" HeaderText="UPDATE DATE TECHNICAL" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="B" HeaderText="B" />
    <asp:BoundColumn DataField="STATUS_FIN_SETTLE" HeaderText="STATUS FIN SETTLE" />
    <asp:BoundColumn DataField="PERIHAL_SURAT_FIN_SETTLE" HeaderText="PERIHAL SURAT FIN SETTLE" />
    <asp:BoundColumn DataField="NO_SURAT_FIN_SETTLE" HeaderText="NO SURAT FIN SETTLE" />
    <asp:BoundColumn DataField="AMOUNT_FIN_SETTLEMENT" HeaderText="AMOUNT FIN SETTLEMENT" />
    <asp:BoundColumn DataField="UPDATE_DATE_FIN_SETTLEMENT" HeaderText="UPDATE DATE FIN SETTLEMENT" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="NO_SURAT_FIN_SETTLE_1" HeaderText="NO SURAT FIN SETTLE 1" />
    <asp:BoundColumn DataField="AMOUNT_FIN_SETTLEMENT_1" HeaderText="AMOUNT FIN SETTLEMENT 1" />
    <asp:BoundColumn DataField="NO_SURAT_FIN_SETTLE_2" HeaderText="NO SURAT FIN SETTLE 2" />
    <asp:BoundColumn DataField="AMOUNT_FIN_SETTLEMENT_2" HeaderText="AMOUNT FIN SETTLEMENT 2" />
    <asp:BoundColumn DataField="EM_NO" HeaderText="EM NO" />
    <asp:BoundColumn DataField="IM_NO" HeaderText="IM NO" />
    <asp:BoundColumn DataField="AMOUNT1" HeaderText="AMOUNT 1" />
    <asp:BoundColumn DataField="UPDATEDATE1" HeaderText="UPDATE DATE 1" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="EM_NO1" HeaderText="EM NO 1" />
    <asp:BoundColumn DataField="IM_NO1" HeaderText="IM NO 1" />
    <asp:BoundColumn DataField="AMOUNT2" HeaderText="AMOUNT 2" />
    <asp:BoundColumn DataField="UPDATEDATE2" HeaderText="UPDATE DATE 2" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="EM_NO2" HeaderText="EM NO 2" />
    <asp:BoundColumn DataField="IM_NO2" HeaderText="IM NO 2" />
    <asp:BoundColumn DataField="AMOUNT3" HeaderText="AMOUNT 3" />
    <asp:BoundColumn DataField="UPDATEDATE3" HeaderText="UPDATE DATE 3" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="EM_NO3" HeaderText="EM NO 3" />
    <asp:BoundColumn DataField="IM_NO3" HeaderText="IM NO 3" />
    <asp:BoundColumn DataField="AMOUNT4" HeaderText="AMOUNT 4" />
    <asp:BoundColumn DataField="UPDATEDATE4" HeaderText="UPDATE DATE 4" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="EM_NO4" HeaderText="EM NO 4" />
    <asp:BoundColumn DataField="IM_NO4" HeaderText="IM NO 4" />
    <asp:BoundColumn DataField="AMOUNT5" HeaderText="AMOUNT 5" />
    <asp:BoundColumn DataField="UPDATEDATE5" HeaderText="UPDATE DATE 5" DataFormatString="{0:dd MMM yyyy}" />
    <asp:BoundColumn DataField="EM_NO5" HeaderText="EM NO 5" />
    <asp:BoundColumn DataField="IM_NO5" HeaderText="IM NO 5" />
</Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        function showLoading() {
            document.getElementById("loading").style.display = "block";
        }

        function hideLoading() {
            document.getElementById("loading").style.display = "none";
        }

        // Menyembunyikan loading setelah halaman selesai dimuat
        window.onload = function () {
            hideLoading();
        };
    </script>

   
</body>
</html>
