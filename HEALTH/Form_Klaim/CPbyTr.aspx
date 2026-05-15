<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CPbyTr.aspx.cs" Inherits="HEALTH.Form_Klaim.CPbyTr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.1.js"></script>
    <script src="../Scripts/sweetalert2.all.min.js"></script>
    <script type="text/javascript">

        function showLoading() {
            Swal.fire({
                title: 'Please wait...',
                html: '<div class="swal-loading"></div>',
                allowOutsideClick: false,
                showConfirmButton: false,
                didOpen: () => {
                    Swal.showLoading(); // Show loading animation
                }
            });
        }

        function hideLoading() {
            Swal.close(); // Close the Swal popup
        }

        function sweetConfirmation()
        {
            LB_TRACK = document.getElementById("LB_TRACK");
            var sMsg = "";
            if (LB_TRACK.innerHTML == "1")
            {
                sMsg = "verifikasi";
            }
            else if (LB_TRACK.innerHTML == "2")
            {
                sMsg = "approve";
            }

            let timerInterval;

            Swal.fire({
                title: "Apakah anda yakin " + sMsg + " data klaim?",
                type: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve()
                        }, 1000)
                    })
                }
            }).then(function (result) {
                if (result.value == true) {
                    document.getElementById("buttonHide").click();
                }
            })
        
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" Width="200px" Placeholder="CARI KODE PROVIDER..."></asp:TextBox>
                    <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="200px" Placeholder="CARI NAMA PROVIDER..."></asp:TextBox>
                    <%--    <asp:DropDownList ID="DDL_MONTH" runat="server" CssClass="ASPDropdown">
                        <asp:ListItem Text="-- Select Month --" Value="" />
                        <asp:ListItem Text="January" Value="1" />
                        <asp:ListItem Text="February" Value="2" />
                        <asp:ListItem Text="March" Value="3" />
                        <asp:ListItem Text="April" Value="4" />
                        <asp:ListItem Text="May" Value="5" />
                        <asp:ListItem Text="June" Value="6" />
                        <asp:ListItem Text="July" Value="7" />
                        <asp:ListItem Text="August" Value="8" />
                        <asp:ListItem Text="September" Value="9" />
                        <asp:ListItem Text="October" Value="10" />
                        <asp:ListItem Text="November" Value="11" />
                        <asp:ListItem Text="December" Value="12" />
                    </asp:DropDownList>

                    <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropdown">
                        <asp:ListItem Text="-- Select Year --" Value="" />
                    </asp:DropDownList>--%>
                    <asp:Button ID="BT_FILTER" runat="server" CssClass="ASPButton" Text="FILTER" OnClick="BT_FILTER_Click" OnClientClick="showLoading();" />
                    <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD EXCEL" OnClick="BT_DOWNLOAD_Click" OnClientClick="showLoading();" />
                    
                    <div class="">
                    <asp:DataGrid ID="DGR_REPORT1" runat="server" AutoGenerateColumns="False" AllowSorting="True" 
                        AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_REPORT1_PageIndexChanged"
                        OnSortCommand="DGR_REPORT1_SortCommand" CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="Both">
    
                        <Columns>
                            <asp:BoundColumn DataField="KODE_PROVIDER" HeaderText="Provider" SortExpression="KODE_PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA_PROVIDER" HeaderText="NAMA_PROVIDER" SortExpression="NAMA_PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ALAMAT" HeaderText="ALAMAT" SortExpression="ALAMAT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KOTA" HeaderText="KOTA" SortExpression="KOTA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="JENIS_TARIF" HeaderText="JENIS TARIF" SortExpression="JENIS_TARIF"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUB_TARIF" HeaderText="SUB TARIF" SortExpression="SUB_TARIF"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Kelas_3" HeaderText="Kelas 3" SortExpression="Kelas_3"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Kelas_2" HeaderText="Kelas 2" SortExpression="Kelas_2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Kelas_1" HeaderText="Kelas_1" SortExpression="Kelas_1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UTAMA" HeaderText="UTAMA" SortExpression="UTAMA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VIP" HeaderText="VIP" SortExpression="VIP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUPER_VIP" HeaderText="SUPER VIP" SortExpression="SUPER_VIP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VVIP" HeaderText="VVIP" SortExpression="VVIP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ICU" HeaderText="ICU" SortExpression="ICU"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ISOLASI" HeaderText="ISOLASI" SortExpression="ISOLASI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MULAI_BERLAKU" HeaderText="MULAI BERLAKU" SortExpression="MULAI_BERLAKU"></asp:BoundColumn>
                       </Columns>

                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                        </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
