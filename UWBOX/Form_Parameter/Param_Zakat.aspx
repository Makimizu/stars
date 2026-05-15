<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Param_Zakat.aspx.cs" Inherits="UWBOX.Form_Parameter.Param_Zakat" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0,
Culture=neutral, PublicKeyToken=89845dcd8080cc91"
Namespace="Microsoft.Reporting.WebForms"
TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Data Zakat</title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        body {
            margin: 30px;
        }
    </style>

    <script type="text/javascript">
        function formatCurrency(input) {
            let value = input.value.replace(/[^,\d]/g, "");
            let parts = value.split(",");
            let sisa = parts[0].length % 3;
            let rupiah = parts[0].substr(0, sisa);
            let ribuan = parts[0].substr(sisa).match(/\d{3}/g);

            if (ribuan) {
                let separator = sisa ? "." : "";
                rupiah += separator + ribuan.join(".");
            }
            rupiah = parts[1] !== undefined ? rupiah + "," + parts[1] : rupiah;
            input.value = rupiah;
        }


        function openReport(url) {
            window.open(url, '_blank');
        }

        function confirmSend() {
            Swal.fire({
                title: 'Kirim Data?',
                text: "Setelah dikirim (SENT), data tidak dapat diedit lagi!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ya, Kirim!',
                cancelButtonText: 'Batal'
            }).then((result) => {
                if (result.isConfirmed) {

                    // Tampilkan loading
                    Swal.fire({
                        title: 'Mengirim data...',
                        html: 'Harap tunggu sebentar ⏳',
                        allowOutsideClick: false,
                        didOpen: () => Swal.showLoading()
                    });

                    // Trigger postback ASP.NET
                    __doPostBack('<%= btnSend.UniqueID %>', '');
                    }
                });

            // Hindari postback default
            return false;
        }


        function showLoading() {
            Swal.fire({
                title: 'Menyimpan data...',
                html: 'Mohon tunggu sebentar ya 🕐',
                allowOutsideClick: false,
                didOpen: () => {
                    Swal.showLoading();
                }
            });
        }

        function closeLoading() {
            Swal.close();
        }

        function startDownload() {
            Swal.fire({
                title: 'Menyiapkan file...',
                html: 'Harap tunggu ⏳',
                allowOutsideClick: false,
                didOpen: () => Swal.showLoading()
            });

            // Hapus cookie lama kalau ada
            document.cookie = "fileDownload=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";

            // Mulai polling cek cookie
            var checkDownload = setInterval(function () {
                if (document.cookie.indexOf("fileDownload=true") !== -1) {

                    clearInterval(checkDownload);

                    Swal.close();

                    // hapus cookie lagi
                    document.cookie = "fileDownload=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                }
            }, 500);
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <h2>Form Input Data Zakat</h2>
        <table style="border-spacing: 0px; width: 100%; font-size: small;">
            <tr>
                <td style="width: 150px;">Tahun Zakat</td>
                <td>
                    <asp:DropDownList ID="ddlTahunZakat" runat="server" CssClass="ASPDropDownList" Style="font-size:12px;"/>
                </td>
            </tr>
            <tr>
                <td>Nishob Zakat</td>
                <td>
                    <asp:TextBox ID="txtNishobZakat" runat="server" CssClass="ASPTextBox" onkeyup="formatCurrency(this)" Style="font-size:12px;" />
                </td>
            </tr>
            <tr>
                <td>Tarif Zakat (%)</td>
                <td>
                    <asp:TextBox ID="txtTarifZakat" runat="server" CssClass="ASPTextBox" Style="font-size:12px;"/>
                </td>
            </tr>
            <tr>
                <td>Kabisat</td>
                <td>
                    <asp:CheckBox ID="chkKabisat" runat="server" Style="font-size:12px;" />
                </td>
            </tr>
            <tr>
                <td>Biaya Zakat (%)</td>
                <td>
                    <asp:TextBox ID="txtBiayaZakatPersen" runat="server" CssClass="ASPTextBox" Style="font-size:12px;"/>
                </td>
            </tr>
            <tr>
                <td>Biaya Zakat Maksimum (Nominal)</td>
                <td>
                    <asp:TextBox ID="txtBiayaZakatNominal" runat="server" CssClass="ASPTextBox" onkeyup="formatCurrency(this)" Style="font-size:12px;"/>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="ASPButton" OnClientClick="showLoading();" OnClick="btnSave_Click" Style="font-size:12px;"/>
                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="ASPButton" OnClientClick="showLoading();" OnClick="btnView_Click" Style="font-size:12px;"/>
                    <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="ASPButton" OnClientClick="return confirmSend();" OnClick="btnSend_Click" Style="font-size:12px;"/>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <asp:GridView ID="GridViewZakat" runat="server" CssClass="ASPDatagrid"
                        AutoGenerateColumns="False" Width="100%"
                        AutoGenerateSelectButton="True"
                        OnSelectedIndexChanged="GridViewZakat_SelectedIndexChanged"
                        OnRowDataBound="GridViewZakat_RowDataBound"
                        Style="font-size:12px;">
                        <Columns>
                            <asp:BoundField DataField="TAHUN_ZAKAT" HeaderText="Tahun Zakat" />
                            <asp:BoundField DataField="NISHOB_ZAKAT" HeaderText="Nishob Zakat" DataFormatString="{0:#,0}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TARIF_ZAKAT" HeaderText="Tarif Zakat (%)" DataFormatString="{0:0.####}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="KABISAT" HeaderText="Kabisat" />
                            <asp:BoundField DataField="BIAYA_PERSEN" HeaderText="Biaya Zakat (%)"/>
                            <asp:BoundField DataField="BIAYA_NOMINAL" HeaderText="Biaya Zakat (Nominal)" DataFormatString="{0:#,0}" />
                            <asp:BoundField DataField="STATUS" HeaderText="Status" />
                            <asp:BoundField DataField="CREATEBY" HeaderText="Create By" />
                            <asp:BoundField DataField="CREATED_AT" HeaderText="Create Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                            <asp:BoundField DataField="LASTCHANGEBY" HeaderText="Last Change By" />

                            <asp:TemplateField HeaderText="Last Change Date">
                                <ItemTemplate>
                                    <%# (Eval("LASTCHANGEDATE") == DBNull.Value || Eval("LASTCHANGEDATE") == null) 
                                        ? "✅" 
                                        : "✏️ " + Convert.ToDateTime(Eval("LASTCHANGEDATE")).ToString("yyyy-MM-dd HH:mm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="ASPButton" Visible="false" Style="font-size:12px;" 
                        OnClientClick="startDownload();" OnClick="btnDownload_Click"/>
                    <%--<asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="ASPButton" Visible="false" Style="font-size:12px;" 
                        OnClick="btnDownload_Click"/>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvZakatNishob" runat="server" CssClass="ASPDatagrid"
                        AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="50"
                        Style="font-size: 12px;" OnPageIndexChanging="gvZakatNishob_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="REGNO" HeaderText="REGNO" />
                            <asp:BoundField DataField="MEMBER_ID" HeaderText="Member ID" />
                            <asp:BoundField DataField="FULL_NAME" HeaderText="Full Name" />
                            <asp:BoundField DataField="POLICY_NUMBER" HeaderText="Policy No." />
                            <asp:BoundField DataField="CODE" HeaderText="Product Code" />
                            <asp:BoundField DataField="FUND_CODE" HeaderText="Fund Code" />
                            <asp:BoundField DataField="FUND_NAME" HeaderText="Fund Name" />
                            <asp:BoundField DataField="POLICY_STATUS" HeaderText="Policy Status" />
                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" />
                            <asp:BoundField DataField="WEALTH_PROPORTION" HeaderText="Wealth Proportion" />
                            <asp:BoundField DataField="ZAKAT_PROPORTION" HeaderText="Zakat Proportion" />
                            <asp:BoundField DataField="CHARGE" HeaderText="Charge" />
                            <asp:BoundField DataField="TOTAL_ZAKAT" HeaderText="Total Zakat" />
                            <asp:BoundField DataField="NISHAB" HeaderText="Nishob/Tidak" />
                            <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                            <asp:BoundField DataField="NAV_DATE" HeaderText="NAV Date" />
                            <asp:BoundField DataField="NAV_VALUE" HeaderText="NAV Value" />
                            <asp:BoundField DataField="STATUS" HeaderText="Status" />
                            <asp:BoundField DataField="TREASURY_MEMO_NUMBER" HeaderText="Rekap ID" />
                            <asp:BoundField DataField="PAYMENT_DATE" HeaderText="Payment Date" />
                            <asp:BoundField DataField="PAYMENT_STATUS" HeaderText="Payment Status" />
                            <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                            <asp:BoundField DataField="MOBILE_NUMBER" HeaderText="Mobile No." />
                            <asp:BoundField DataField="EMAIL" HeaderText="E-mail" />
                            <asp:BoundField DataField="DELIVERY_DATE" HeaderText="Delivery Date" />
                            <asp:BoundField DataField="DELIVERY_STATUS" HeaderText="Deliv. Status" />
                            <asp:BoundField DataField="DELIVERY_MEDIA" HeaderText="Deliv. Media" />
                        </Columns>
                    </asp:GridView>
                    <%--<asp:Panel ID="pnlReport" runat="server" Visible="false">--%>
                        <%--<hr />
                        <h3>Report Zakat</h3>

                        <rsweb:ReportViewer ID="RV"
                            runat="server"
                            Width="100%"
                            Height="800px"
                            AsyncRendering="False"
                            SizeToReportContent="True">
                        </rsweb:ReportViewer>--%>
                        <%--<iframe id="frameReport"
                            runat="server"
                            width="100%"
                            height="800px"
                            frameborder="0">
                        </iframe>--%>

                    <%--</asp:Panel>--%>
                </td>
            </tr>
        </table>
    </form>
    <%--<iframe id="downloadFrame" name="downloadFrame" style="display:none;"></iframe>--%>
</body>
</html>

