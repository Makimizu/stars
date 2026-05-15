<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checker_Zakat.aspx.cs" Inherits="LIFE.Form_POS.Checker_Zakat" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Checker Zakat</title>
    <link href="../include/css/Maker_Zakat.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" />
    <link href="../standard/CommonStyle.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#datepickerTanggalNAV').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            });
        });

        function formatCurrencyNoDecimal(el) {
            if (!el) return;
            let v = el.value.replace(/\D/g, '');
            if (v === '') { el.value = ''; return; }
            el.value = v.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        }

        function applyNoDecimalFormat(id) {
            var el = document.getElementById(id);
            if (!el) return;
            el.addEventListener('input', function () { formatCurrencyNoDecimal(el); });
            el.addEventListener('blur', function () { formatCurrencyNoDecimal(el); });
        }
        function allowOnlyInt(el) { el.value = el.value.replace(/\D/g, ''); }
    </script>
</head>
<body>
<form id="form1" runat="server">
    <table style="width:100%" hidden>
        <tr>
            <td><b>Periode Zakat</b></td>
            <td><asp:DropDownList 
                    ID="ddlPeriode" 
                    runat="server" 
                    CssClass="form-select form-select-sm"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlPeriode_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:18%"><b>Tanggal NAV</b></td>
                <td style="width:32%">
                <div class="input-group date" id="datepickerTanggalNAV">
                        <asp:TextBox ID="txtTanggalNAV" runat="server" CssClass="form-control" placeholder="" />
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
            <td><asp:TextBox ID="txtTotalBayar" runat="server" CssClass="form-control form-control-sm" /></td>
            <td><b>Total Zakat</b></td>
            <td><asp:TextBox ID="txtTotalZakat" runat="server" CssClass="form-control form-control-sm" /></td>
        </tr>
        <tr>
            <td colspan="4" class="pt-2">
                <asp:Button ID="btnSaveHeader" runat="server" Text="Simpan Header" CssClass="btn btn-primary btn-sm" OnClick="btnSaveHeader_Click" />
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
            <asp:BoundField DataField="STATUS" HeaderText="Status" />

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
                            ShowFooter="false"
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
                                        <%# Eval("NO_REKENING") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewNoRek" runat="server" CssClass="input-sm" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nama Rekening">
                                    <ItemTemplate>
                                        <%# Eval("NAMA_REKENING") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewNamaRek" runat="server" CssClass="input-sm" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate>
                                        <%# Eval("BANK_NAME") %>
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
                                        <asp:TextBox ID="txtNewJumlah" runat="server" CssClass="input-sm" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inquiry" Visible="false">
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
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <button type="button" 
                                                style="display:none;"
                                                class="btn-delete btn btn-xs btn-danger" 
                                                data-id='<%# Eval("ID_DETAIL") %>' 
                                                title="Hapus">
                                            <i class="fa fa-trash"></i>
                                        </button>
                                    </ItemTemplate>
                                    <FooterTemplate>
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
                    <asp:LinkButton ID="btnEditHeader" runat="server"
                        CommandName="EditHeader" CommandArgument='<%# Eval("NO_IOM") %>'
                        CssClass="dropdown-item text-warning" Visible="false">
                        <i class="fa fa-edit"></i> Edit Header
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="btnApprove" runat="server"
                        CssClass="dropdown-item text-success"
                        OnClientClick="return confirmApprove(this);"
                        CommandName="Approve" CommandArgument='<%# Eval("NO_IOM") %>'>
                        <i class="fa fa-check-circle"></i> Approve
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="btnBackToMaker" runat="server"
                        CssClass="dropdown-item text-secondary"
                        CommandName="BackToMaker" CommandArgument='<%# Eval("NO_IOM") %>'>
                        <i class="fa fa-undo"></i> Back to Maker
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="btnReject" runat="server"
                        CommandName="Reject" CommandArgument='<%# Eval("NO_IOM") %>'
                        CssClass="dropdown-item text-danger">
                        <i class="fa fa-ban"></i> Reject
                    </asp:LinkButton>
                </li>
                <%--<li><hr class="dropdown-divider" /></li>
                <li>
                    <asp:LinkButton 
                        ID="btnDeleteHeader" runat="server" 
                        CommandName="DeleteHeader" CommandArgument='<%# Eval("NO_IOM") %>'
                        CssClass="dropdown-item btn-delete-header text-danger">
                        <i class="fa fa-trash"></i> Hapus
                    </asp:LinkButton>
                </li>--%>
            </ul>
        </div>
    </ItemTemplate>
</asp:TemplateField>


        </Columns>
    </asp:GridView>
</form>
</body>
</html>