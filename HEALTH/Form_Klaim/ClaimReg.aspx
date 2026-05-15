<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimReg.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimReg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="../standard/CommonStyle.css" />
   <%-- <link rel="stylesheet" href="Content/sweetalert2.min.css"/>
    <script src="Scripts/sweetalert2.all.min.js"></script>--%>
   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
 <%--<script type="text/javascript">
        function confirmSweetAlertPostback(action, claimNo, btn) {
            Swal.fire({
                title: 'Konfirmasi',
                text: `Anda yakin untuk ${action} ${claimNo}?`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ya, lanjutkan',
                cancelButtonText: 'Batal'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack(btn.name, '');
                }
            });
            return false; // cegah postback default
        }
 </script>--%>


<script type = "text/javascript">
    function alertMessage() {
        Swal.fire({
            icon: 'info',
            title: 'Informasi',
            text: 'Data sudah paling atas'
        });
    }
    //function alertMessage() {
    //    alert('Data sudah paling atas');
    //}
</script>

</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">DOC. SOURCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_DOC_SOURCE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NOMOR SM</td>
                                        <td>
                                            <asp:TextBox ID="TXT_SM" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                                            <asp:Label ID="LB_BATCHID" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TGL SM</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_TGLSM" runat="server" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd-MM-yyyy" TargetControlID="TXT_TGLSM">
                                                    <%--<ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TGLSM">--%>
                                                    </ajaxToolkit:CalendarExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>P / R</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PR" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_PR_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PROVIDER</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PROV" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_PROV_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:Button ID="BT_CARIPROVIDER" runat="server" CssClass="ASPButton" Text="CARI" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLIS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_POLIS" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_POLIS_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:Button ID="BT_CARIPOLIS" runat="server" CssClass="ASPButton" Text="CARI" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style6">
                                            <asp:Button ID="BT_NOREK" runat="server" Text="COPY REK DARI :" CssClass="ASPButton" OnClick="BT_NOREK_Click" Width="120px" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_NOREK" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_NOREK_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style6">ACC NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style6">ACC NAMA</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ACCNAMA" runat="server" CssClass="ASPTextBox" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style6">ACC BANK</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" />

                                            <asp:Button ID="BT_INQUIRY" BackColor="Green" ForeColor="White" runat="server" CssClass="ASPButton" Text="INQUIRY" OnClick="BT_INQUIRY_Click" />

                                        </td>
                                    </tr>
                                </table>

                                <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                           
                                  <br />

                                <table>
                                     <tr>
                                        <td>

                                            <span id="spanInquery" runat="server" visible="false">
                                              <table style="width:150%;">
                                                  <tr>
                                                      <td class="auto-style1">DESTINATION NAME</td>
                                                      <td>:</td>
                                                      <td class="auto-style2"><asp:Label ID="lblDestName" runat="server" Font-Bold="true" /></td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style1">DESTINATION ACCOUNT NO</td>
                                                      <td>:</td>
                                                      <td class="auto-style2"><asp:Label ID="lblDestAccNo" runat="server" Font-Bold="true" /></td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style1">DESTINATION BANK</td>
                                                      <td>:</td>
                                                      <td class="auto-style2"><asp:Label ID="lblDestBank" runat="server" Font-Bold="true" /></td>
                                                  </tr>

                                              </table>
                                            </span>
                               
                                        </td>
                                      </tr>
                                </table>
                                
                                
                                
                            </td>
                            <td>
                                <table id="TBL_ARSIP" runat="server" visible="false" style="border-spacing: 0px;">
                                    <tr>
                                        <td class="TDBGColor">
                                            <strong>ARSIP</asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <iframe id="I1" runat="server" frameborder="no" height="150" name="I1" scrolling="auto" width="500"></iframe>
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
                    <hr>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_NEW_Click" Text="BUAT PENGAJUAN BARU" /><br />
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">NAMA PESERTA</td>
                            <td>
                                <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PERUSAHAAN</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PROSES</td>
                            <td>
                                <asp:DropDownList ID="DDL_DONE" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="1">DONE</asp:ListItem>
                                    <asp:ListItem Value="0">UNDONE</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="BT_CARI" runat="server" Text="Cari" CssClass="ASPButton" OnClick="BT_CARI_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:Label ID="LB_CNT" runat="server" Font-Bold="True"></asp:Label><br />
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" BorderColor="#999999" OnItemCommand="DGR_ItemCommand" BackColor="White" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Button ID="BT_DONE" runat="server" Text="DONE/UNDONE" CssClass="ASPButton" Font-Bold="true" BackColor="#66FF99" ForeColor="Blue" OnClick="BT_DONE_Click" /><br />
                                    <asp:LinkButton ID="LBT_ALL" runat="server" CommandName="All" CssClass="ASPLabel" Font-Bold="True" ForeColor="White">ALL</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" Style="Height: 10px;" />
                                </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CLAIM_NO" HeaderText="NOREG CLAIM" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="NOREG CLAIM">
                                <ItemTemplate>
                                    <asp:Label ID="LB_CLAIMNO" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_CLAIM_DESCR" HeaderText="TIPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_KLAIM" HeaderText="TGL KLAIM">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_TRACK_DESCR" HeaderText="TRACK">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_PENGAJUAN" HeaderText="PENGAJUAN">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_CASH" HeaderText="CASH">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_BAYAR" HeaderText="BAYAR">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="JUMLAH_TOLAK" HeaderText="TOLAK">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EKSES" HeaderText="EXCESS">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REFUND" HeaderText="REFUND">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAID_DATE" HeaderText="PAID DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DONE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_TRACK" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Delete Remark"> <ItemStyle Width="400px" />
                                <ItemTemplate>
                                 <asp:TextBox 
                                     ID="deleteReason" 
                                     runat="server"
                                     TextMode="MultiLine" 
                                     Rows="5" 
                                     Columns="50"
                                    style="width:100%; height:100px;"
                                    CssClass="delete-remark-box" />
                                     </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                     <asp:Button ID="BT_CLONE" runat="server"
                                        CommandName="Clone"
                                        CommandArgument='<%# Eval("CLAIM_NO") %>'
                                        CssClass="ASPButton" Text="COPY" />&nbsp;
                                    <asp:Button ID="BT_DEL" runat="server"
                                        CommandName="Delete"
                                        CommandArgument="<%# Container.ItemIndex %>"
                                        Text="DEL"
                                        ForeColor="White"
                                        BackColor="Red"
                                        CssClass="ASPButton" />
                               <%--   <asp:Button ID="BT_DEL" runat="server"
                                        CommandName="Delete"
                                        CommandArgument='<%# Eval("CLAIM_NO") %>' 
                                        Text="DEL"
                                        ForeColor="White"
                                        BackColor="Red"
                                        CssClass="ASPButton"
                                       />--%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="Lime" ForeColor="Black" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
