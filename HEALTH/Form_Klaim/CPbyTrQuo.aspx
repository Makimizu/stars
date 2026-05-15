<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CPbyTrQuo.aspx.cs" Inherits="HEALTH.Form_Klaim.CPbyTrQuo" %>

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
               
                    <asp:Button ID="BT_FILTER" runat="server" CssClass="ASPButton" Text="FILTER" OnClick="BT_FILTER_Click" OnClientClick="showLoading();" />
                    <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD EXCEL" OnClick="BT_DOWNLOAD_Click" OnClientClick="showLoading();" />
                    
                    <div class="">
                    <asp:DataGrid ID="DGR_REPORT1" runat="server" AutoGenerateColumns="False" AllowSorting="True" 
                        AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_REPORT1_PageIndexChanged"
                        OnSortCommand="DGR_REPORT1_SortCommand" CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="Both">
    
                        <Columns>
                            <asp:BoundColumn DataField="KODE_PROVIDER" HeaderText="KODE PROVIDER" SortExpression="KODE_PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVIDER" HeaderText="NAMA PROVIDER" SortExpression="PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CODE" HeaderText="ICDX" SortExpression="ICDX"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DIAGNOSE" HeaderText="DIAGNOSE" SortExpression="DIAGNOSE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAMA_RAWAT" HeaderText="LAMA RAWAT" SortExpression="LAMA_RAWAT"></asp:BoundColumn> 
                            <asp:BoundColumn DataField="KELAS_KAMAR" HeaderText="KELAS KAMAR" SortExpression="KELAS_KAMAR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BIAYA_KAMAR" HeaderText="BIAYA KAMAR" SortExpression="BIAYA_KAMAR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL_BIAYA_CP" HeaderText="TOTAL BIAYA CP" SortExpression="TOTAL_BIAYA_CP"></asp:BoundColumn>
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
