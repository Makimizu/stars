<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Maker_Zakat.aspx.cs" Inherits="LIFE.Form_POS.Maker_Zakat" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

     <!-- CSS -->
    <link href="../include/css/Maker_Zakat.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" />
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    
    <!-- JS -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#datepickerTanggalNAV').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,
                forceParse: true 
            });

            $('#<%= txtTanggalNAV.ClientID %>').on('blur', function () {
                let val = $(this).val();
                if (val.trim() === "") return;

                let regex = /^([0-2][0-9]|3[0-1])\/(0[1-9]|1[0-2])\/\d{4}$/;
                if (!regex.test(val)) {
                    Swal.fire({
                        icon: "warning",
                        title: "Format tanggal salah",
                        text: "Gunakan format dd/mm/yyyy",
                        timer: 2000,
                        showConfirmButton: false
                    });
                    $(this).val(""); 
                }
            });
        });

        function formatCurrencyDecimal(el) {
            if (!el) return;

            let value = el.value;

            // normalize ke format angka
            value = value.replace(/[^0-9,]/g, '');

            let parts = value.split(',');

            let integerPart = parts[0];
            let decimalPart = parts[1] || '';

            // format ribuan
            integerPart = integerPart.replace(/\B(?=(\d{3})+(?!\d))/g, ".");

            el.value = decimalPart
                ? integerPart + "," + decimalPart.substring(0, 2)
                : integerPart;
        }

        function applyDecimalFormat(id) {
            var el = document.getElementById(id);
            if (!el) return;

            el.addEventListener('input', function () { formatCurrencyDecimal(el); });
            el.addEventListener('blur', function () { formatCurrencyDecimal(el); });
        }

        function allowOnlyInt(el) { el.value = el.value.replace(/\D/g, ''); }

        function parseCurrencyToDecimal(s) {
            if (!s) return 0;
            var cleaned = s.replace(/\./g, '').replace(/,/g, '.').trim();
            var val = parseFloat(cleaned);
            return isNaN(val) ? 0 : val;
        }

        // client validation for add detail (overlap & total check)
       <%-- function clientValidateAddDetail() {
            var jumlahPeserta = parseInt($("#<%= txtJumlahPeserta.ClientID %>").val() || '0');
            var totalBayar = parseCurrencyToDecimal($("#<%= txtTotalBayar.ClientID %>").val() || '0');

            var dari = parseInt($("#<%= txtAddRangeDari.ClientID %>").val() || '0');
            var sampai = parseInt($("#<%= txtAddRangeSampai.ClientID %>").val() || '0');
            if (!dari || !sampai) { Swal.fire('Range invalid', 'Range harus diisi dan berupa angka.', 'error'); return false; }
            if (sampai < dari) { Swal.fire('Range invalid', 'Range Sampai harus >= Range Dari', 'error'); return false; }
            if (sampai > jumlahPeserta) { Swal.fire('Range invalid', `Range Sampai tidak boleh melebihi jumlah peserta (${jumlahPeserta}).`, 'error'); return false; }

            var newJumlah = parseCurrencyToDecimal($("#<%= txtAddJumlah.ClientID %>").val() || '0');
            if (newJumlah <= 0) { Swal.fire('Jumlah invalid', 'Jumlah harus > 0', 'error'); return false; }

            // collect existing ranges and totals
            var existingRanges = [];
            var totalExisting = 0;
            $("#<%= gvDetail.ClientID %> tbody tr").each(function () {
                var cols = $(this).find('td');
                if (cols.length < 8) return;
                var rf = parseInt($(cols[5]).text().replace(/\D/g,'') || 0);
                var rt = parseInt($(cols[6]).text().replace(/\D/g,'') || 0);
                var j = parseCurrencyToDecimal($(cols[7]).text() || '0');
                if (rf && rt) existingRanges.push({ f: rf, t: rt });
                totalExisting += j;
            });

            for (var i=0;i<existingRanges.length;i++){
                var r = existingRanges[i];
                if (!(sampai < r.f || dari > r.t)) { Swal.fire('Range Overlap','Range overlap dengan baris lain.','error'); return false; }
            }

            if (totalExisting + newJumlah > totalBayar) { Swal.fire('Jumlah melebihi','Total detail melebihi Total Bayar header','error'); return false; }

            return true;
        }--%>

        // confirm send
       <%-- function confirmSend() {
            Swal.fire({
                title: 'Kirim Data?',
                text: "Setelah dikirim (SENT), data tidak dapat diedit lagi!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Ya, Kirim!',
                cancelButtonText: 'Batal'
            }).then((result) => {
                if (result.isConfirmed) {
                   
                }
            });
        }--%>

        var isConfirmed = false;
        function confirmVerification(btn) {
            if (isConfirmed) {
                return true; // Izinkan postback jika sudah dikonfirmasi
            }

            Swal.fire({
                title: 'Kirim ke Verification?',
                text: 'Data akan dikirim ke checker.',
                icon: 'question',
                showCancelButton: true,
                confirmButtonText: 'Ya, Kirim',
                cancelButtonText: 'Batal'
            }).then((result) => {
                if (result.isConfirmed) {
                    isConfirmed = true; // Set flag menjadi true
                    btn.click(); // Trigger click kembali
                }
            });
            return false; // Tahan postback pertama kali
        }

        // on load: apply formatting
        window.addEventListener('load', function () {
            applyDecimalFormat('<%= txtTotalZakat.ClientID %>');
            applyDecimalFormat('<%= txtTotalBiaya.ClientID %>');
            applyDecimalFormat('<%= txtTotalBayar.ClientID %>');
         
            document.getElementById('<%= txtJumlahPeserta.ClientID %>').addEventListener('input', function () { allowOnlyInt(this); });
        });

        function toggleDetail(link) {
            var panel = link.nextElementSibling; 
            if (panel.style.display === "none" || panel.style.display === "") {
                panel.style.display = "block";
                link.innerText = "[-]";
            } else {
                panel.style.display = "none";
                link.innerText = "[+]";
            }
        }

        $(document).on("click", ".btn-delete", function (e) {
            e.preventDefault();
            var $btn = $(this);                     
            var idDetail = $btn.data("id");         

            if (!idDetail) {
                Swal.fire('Error', 'ID detail tidak ditemukan.', 'error');
                return;
            }

            Swal.fire({
                title: 'Yakin mau hapus?',
                text: "Data yang sudah dihapus tidak bisa dikembalikan!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Ya, hapus',
                cancelButtonText: 'Batal'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "POST",
                        url: "Maker_Zakat.aspx/DeleteDetail",   // webmethod di code-behind
                        data: JSON.stringify({ id: idDetail }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var res = response.d;
                            if (res === "OK") {
                                Swal.fire('Berhasil!', 'Data berhasil dihapus.', 'success').then(() => {
                                    // hapus baris di client-side
                                    //var $row = $btn.closest("tr");
                                    //$row.fadeOut(200, function () { $(this).remove(); });

                                    var noIom = $btn.closest("table")     // naik ke gvDetail
                                                    .closest("tr")        // naik ke row header
                                                    .data("iom");

                                    __doPostBack('RefreshDetail', noIom);
                            });
                        } else {
                            Swal.fire('Gagal!', res, 'error');
                        }
                    },
                    error: function (xhr, status, err) {
                        Swal.fire('Error!', (err || 'Terjadi kesalahan saat request'), 'error');
                    }
                });
            }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:LinkButton ID="btnHiddenDeleteHeader" runat="server" OnClick="btnHiddenDeleteHeader_Click" Style="display:none;" />
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td><b>Periode Zakat</b></td>
                <td><asp:DropDownList ID="ddlPeriode" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriode_SelectedIndexChanged"></asp:DropDownList></td>
                <td style="width:18%"><b>Tanggal NAV</b></td>
                <td style="width:32%">
                   <div class="input-group date" id="datepickerTanggalNAV">
                        <asp:TextBox ID="txtTanggalNAV" runat="server" CssClass="form-control form-control-sm" placeholder="" />
                        <div class="input-group-append">
                            <button class="btn btn-outline-secondary" type="button">
                                <i class="fa fa-calendar"></i>
                            </button>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td><b>Total Biaya</b></td>
                <td><asp:TextBox ID="txtTotalBiaya" runat="server" CssClass="form-control form-control-sm" /></td>
                <td><b>Jumlah Peserta</b></td>
                <td><asp:TextBox ID="txtJumlahPeserta" runat="server" CssClass="form-control form-control-sm" /></td>
            </tr>
            <tr>
                <td><b>Total Yang Harus Dibayarkan</b></td>
                <td><asp:TextBox ID="txtTotalZakat" runat="server" CssClass="form-control form-control-sm" /></td>
                <td><b>Total Zakat + Biaya</b></td>
                <td><asp:TextBox ID="txtTotalBayar" runat="server" CssClass="form-control form-control-sm" /></td>
            </tr>
            <tr>
                <td colspan="4" style="border:none; padding-top:8px;">
                    <asp:Button ID="btnSaveHeader" runat="server" Text="Simpan Header" CssClass="btn btn-primary" OnClick="btnSaveHeader_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvHeader" runat="server" AutoGenerateColumns="False"
            CssClass="ASPDatagrid"
            GridLines="Both"
            BorderStyle="Solid"
            BorderWidth="1px"
            BorderColor="#dee2e6"
            CellPadding="6"
            CellSpacing="0"
            OnRowDataBound="gvHeader_RowDataBound"
            OnRowCommand="gvHeader_RowCommand">
            <HeaderStyle BackColor="#f1f3f5" Font-Bold="true" ForeColor="#333" />
            <RowStyle BackColor="#ffffff" ForeColor="#333" />
            <AlternatingRowStyle BackColor="#fafafa" />
            <FooterStyle BackColor="#f8f9fa" Font-Bold="true" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnExpand" runat="server" Text="+"
                            CommandName="Expand" CommandArgument='<%# Eval("NO_IOM") %>'
                            CssClass="btn btn-sm btn-secondary" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NO_IOM" HeaderText="No IOM" />
                <asp:BoundField DataField="JUMLAH_PESERTA" HeaderText="Jumlah Peserta" />
                <asp:BoundField DataField="TOTAL_ZAKAT" HeaderText="Total Zakat" DataFormatString="{0:N2}" />

                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:Panel ID="pnlDetail" runat="server" Visible="false" CssClass="p-2">
                            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="ID_DETAIL"
                                CssClass="ASPDatagrid" 
                                GridLines="Both"
                                BorderStyle="Solid"
                                BorderWidth="1px"
                                BorderColor="#dee2e6"
                                CellPadding="6"
                                CellSpacing="0"
                                ShowFooter="true"
                                OnRowCommand="gvDetail_RowCommand"
                                OnRowDataBound="gvDetail_RowDataBound">
                                <HeaderStyle BackColor="#f1f3f5" Font-Bold="true" ForeColor="#333" />
                                <RowStyle BackColor="#ffffff" ForeColor="#333" />
                                <AlternatingRowStyle BackColor="#fafafa" />
                                <FooterStyle BackColor="#f8f9fa" Font-Bold="true" />
                                <Columns>
                                <asp:BoundField DataField="ID_DETAIL" HeaderText="ID" Visible="false" ReadOnly="true" />
                                  <asp:TemplateField HeaderText="No">
                                      <ItemTemplate>
                                          <%# Container.DataItemIndex + 1 %>
                                      </ItemTemplate>
                                  </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Transfer Ke">
                                        <ItemTemplate>
                                            <%# Eval("TRANSFER_KE") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewTransferKe" runat="server" CssClass="input-sm" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="No Rekening">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoRekening" runat="server" Text='<%# Eval("NO_REKENING") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewNoRek" runat="server" CssClass="input-sm" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nama Rekening">
                                        <ItemTemplate>
                                              <asp:Label 
                                                ID="lblNamaRekening" 
                                                runat="server" 
                                                Text='<%# Eval("NAMA_REKENING") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewNamaRek" runat="server" CssClass="input-sm" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bank">
                                        <ItemTemplate>
                                           <asp:Label 
                                              ID="lblBank" 
                                              runat="server" 
                                              Text='<%# Eval("BANK_NAME") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlNewBank" runat="server" CssClass="input-sm">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Range Dari">
                                        <ItemTemplate>
                                            <%# Eval("RANGE_DARI") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewRangeDari" runat="server" CssClass="input-sm" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Range Sampai">
                                        <ItemTemplate>
                                            <%# Eval("RANGE_SAMPAI") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewRangeSampai" runat="server" CssClass="input-sm" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Jumlah">
                                        <ItemTemplate>
                                            <%# String.Format("{0:N2}", Eval("JUMLAH")) %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewJumlah" runat="server" CssClass="input-sm" Enabled="false" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inquiry">
                                    <FooterTemplate>
                                        <asp:LinkButton 
                                            ID="btnInquiry"
                                            runat="server"
                                            CommandName="Inquiry"
                                            CommandArgument='<%# Eval("ID_DETAIL") %>'
                                            CssClass="btn btn-xs btn-info">
                                            Inquiry
                                        </asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                               <button type="button" 
                                                        class="btn-delete btn btn-xs btn-danger" 
                                                        data-id='<%# Eval("ID_DETAIL") %>' 
                                                        title="Hapus">
                                                    <i class="fa fa-trash"></i>
                                                </button>
                                          <%--  <asp:LinkButton 
                                                ID="btnDelete" 
                                                runat="server" 
                                                CommandName="Delete" 
                                                CommandArgument='<%# Eval("ID_DETAIL") %>' 
                                                CssClass="btn-delete"
                                                ToolTip="Hapus"
                                                OnClientClick="return confirmDelete(this);">
                                                <i class="fa fa-trash"></i>
                                            </asp:LinkButton>--%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           <%-- <asp:LinkButton 
                                                ID="btnInquiryNew"
                                                runat="server"
                                                CommandName="InquiryNew"
                                                CssClass="btn btn-xs btn-info me-1"
                                                ToolTip="Inquiry">
                                                <i class="fa fa-search"></i>
                                            </asp:LinkButton>--%>
                                            <asp:LinkButton 
                                                runat="server" 
                                                CommandName="AddNew" 
                                                CssClass="btn btn-xs btn-success"
                                                ToolTip="Tambah data">
                                                <i class="fa fa-plus"></i>
                                            </asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Aksi">
                <ItemTemplate>
                    <div class="dropdown">
                        <button class="btn btn-sm btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown">
                            Aksi
                        </button>
                        <ul class="dropdown-menu">
                            <li>
                                <asp:LinkButton ID="btnExportWord" runat="server" 
                                    CommandName="ExportWord" CommandArgument='<%# Eval("NO_IOM") %>' 
                                    CssClass="dropdown-item">
                                    <i class="fa fa-file-word text-primary"></i> Word
                                </asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnExportPdf" runat="server" 
                                    CommandName="ExportPdf" CommandArgument='<%# Eval("NO_IOM") %>' 
                                    CssClass="dropdown-item">
                                    <i class="fa fa-file-pdf text-danger"></i> PDF
                                </asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnExportExcel" runat="server" 
                                    CommandName="ExportExcel" CommandArgument='<%# Eval("NO_IOM") %>' 
                                    CssClass="dropdown-item">
                                    <i class="fa fa-file-excel text-success"></i> Excel
                                </asp:LinkButton>
                            </li>
                            <li><hr class="dropdown-divider" /></li>
                            <li>
                                <asp:LinkButton 
                                    ID="btnVerification" runat="server" 
                                    CommandName="Verification" CommandArgument='<%# Eval("NO_IOM") %>'
                                    CssClass="dropdown-item text-success"
                                     OnClientClick="return confirmVerification(this);">
                                    <i class="fa fa-check-circle"></i> Verify
                                </asp:LinkButton>
                            </li>
                            <li><hr class="dropdown-divider" /></li>
                            <li>
                                <asp:LinkButton 
                                    ID="btnDeleteHeader" runat="server" 
                                    CommandName="DeleteHeader" CommandArgument='<%# Eval("NO_IOM") %>'
                                    CssClass="dropdown-item btn-delete-header">
                                    <i class="fa fa-trash text-danger"></i> Hapus
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>


