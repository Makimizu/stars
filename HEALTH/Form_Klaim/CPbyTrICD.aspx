<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="CPbyTrICD.aspx.cs" Inherits="HEALTH.Form_Klaim.CPbyTrICD" %>

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
                            <asp:BoundColumn DataField="KODE_PROVIDER" HeaderText="PROVIDER" SortExpression="KODE_PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA_PROVIDER" HeaderText="NAMA_PROVIDER" SortExpression="NAMA_PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KODE_ICD" HeaderText="KODE ICD" SortExpression="KODE_ICD"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DIAGNOSA" HeaderText="DIAGNOSA" SortExpression="DIAGNOSA"></asp:BoundColumn>                             
                            <asp:BoundColumn DataField="LOS" HeaderText="LOS" SortExpression="LOS"></asp:BoundColumn>      
                            <asp:BoundColumn DataField="VIP Utama" HeaderText="VIP Utama" SortExpression="VIP Utama"></asp:BoundColumn> 
                            <asp:BoundColumn DataField="VVIP" HeaderText="VVIP" SortExpression="VVIP"></asp:BoundColumn> 
                            <asp:BoundColumn DataField="VIP" HeaderText="VIP" SortExpression="VIP"></asp:BoundColumn>                          
                            <asp:BoundColumn DataField="Kelas 3" HeaderText="Kelas 3" SortExpression="Kelas 3"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Kelas 2" HeaderText="Kelas 2" SortExpression="Kelas 2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Kelas 1" HeaderText="Kelas 1" SortExpression="Kelas 1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CreateDate" HeaderText="Waktu Upload" SortExpression="CreateDate"></asp:BoundColumn>
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
