<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CPbyRs.aspx.cs" Inherits="HEALTH.Form_Klaim.CPbyRs" %>

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
                    <asp:TextBox ID="TXT_SEARCH" runat="server" CssClass="ASPTextBox" Width="200px" Placeholder="CARI PROVIDER..."></asp:TextBox>
                    <asp:DropDownList ID="DDL_MONTH" runat="server" CssClass="ASPDropdown">
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
                    </asp:DropDownList>
                    <asp:Button ID="BT_FILTER" runat="server" CssClass="ASPButton" Text="FILTER" OnClick="BT_FILTER_Click" OnClientClick="showLoading();" />
                    <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD EXCEL" OnClick="BT_DOWNLOAD_Click" OnClientClick="showLoading();" />
                    
                    <div class="">
                    <asp:DataGrid ID="DGR_REPORT" runat="server" AutoGenerateColumns="False" AllowSorting="True" 
                        AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_REPORT_PageIndexChanged"
                        OnSortCommand="DGR_REPORT_SortCommand" CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="Both">
    
                        <Columns>
                            <asp:BoundColumn DataField="PROVIDER" HeaderText="Provider" SortExpression="PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MONTH_NAME" HeaderText="MONTH" SortExpression="MONTH"></asp:BoundColumn>
                            <asp:BoundColumn DataField="YEAR" HeaderText="YEAR" SortExpression="YEAR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL_CP" HeaderText="Total Claim CP" SortExpression="TOTAL_CLAIMS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CP_COUNT" HeaderText="CP Count" SortExpression="CP_COUNT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CP_NOT_FITS_COUNT" HeaderText="CP Not Fits Count" SortExpression="NON_CP_COUNT"></asp:BoundColumn>
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
