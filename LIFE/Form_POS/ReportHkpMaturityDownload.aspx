<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportHkpMaturityDownload.aspx.cs" Inherits="LIFE.Form_POS.ReportHkpMaturityDownload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</head>

<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server"></ajaxToolkit:ToolkitScriptManager>

        <table>
            <tr>
                <td>Tanggal Habis Kontrak</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtStartDate" runat="server" Width="120px" CssClass="ASPTextBox"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate"></ajaxToolkit:CalendarExtender>
                            &nbsp;s/d&nbsp;
                            <asp:TextBox ID="txtEndDate" runat="server" Width="120px" CssClass="ASPTextBox"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceEndDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate"></ajaxToolkit:CalendarExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BT_SUBMIT" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>Unitize Non Unitize</td>
                <td>
                    <asp:DropDownList ID="ddlUnitize" runat="server" Width="150px" CssClass="ASPDropDownList">
                        <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                        <asp:ListItem Text="Unitize" Value="Unitize"></asp:ListItem>
                        <asp:ListItem Text="Non Unitize" Value="NonUnitize"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Currency</td>
                <td>
                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="150px" CssClass="ASPDropDownList">
                        <asp:ListItem Text="IDR - RUPIAH" Value="IDR"></asp:ListItem>
                        <asp:ListItem Text="USD - DOLLAR" Value="USD"></asp:ListItem>
                        <asp:ListItem Text="EUR - EURO" Value="EUR"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Status Rekening</td>
                <td>
                    <asp:DropDownList ID="ddlStatusRekening" runat="server" Width="150px" CssClass="ASPDropDownList">
                        <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                        <asp:ListItem Text="REKENING PENAMPUNG" Value="REKENING PENAMPUNG"></asp:ListItem>
                        <asp:ListItem Text="REKENING TABARRU" Value="REKENING TABARRU"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Status Pencairan</td>
                <td>
                    <asp:DropDownList ID="ddlStatusPencairan" runat="server" Width="150px" CssClass="ASPDropDownList">
                        <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                        <asp:ListItem Text="PAID" Value="PAID"></asp:ListItem>
                        <asp:ListItem Text="UNPAID" Value="UNPAID"></asp:ListItem>
                        <asp:ListItem Text="RETUR" Value="RETUR"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SUBMIT" runat="server" CssClass="ASPButton"
                        Text="DOWNLOAD" OnClick="BT_SUBMIT_Click"
                        OnClientClick="showSpin();" />
                </td>
            </tr>
        </table>
    </form>

<script type="text/javascript">
    function showSpin() {
        Swal.fire({
            title: 'Sedang memproses...',
            text: 'Mohon tunggu, file sedang dibuat',
            allowOutsideClick: false,
            didOpen: () => { Swal.showLoading(); }
        });

        // cek cookie tiap 1 detik
        var interval = setInterval(function () {
            if (document.cookie.indexOf("downloadFinished=true") !== -1) {
                clearInterval(interval);
                Swal.close();

                // hapus cookie supaya tidak kebaca lagi
                document.cookie = "downloadFinished=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";

                Swal.fire("Selesai!", "File sudah berhasil didownload", "success");
            }
        }, 1000);
    }
</script>
</body>
</html>
