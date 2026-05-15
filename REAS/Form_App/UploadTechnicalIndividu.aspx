<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadTechnicalIndividu.aspx.cs" Inherits="REAS.Form_App.UploadTechnicalIndividu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });

        function hourglass() {
            document.body.style.cursor = "wait";
        }

        // Function to show the modal
        function showModal(message) {
            var modal = document.getElementById("messageModal");
            var modalMessage = document.getElementById("modalMessage");
            modalMessage.textContent = message; // Set the message text
            modal.style.display = "block"; // Show the modal
        }

        // Close the modal when the user clicks on the close button
        window.onload = function () {
            var closeBtn = document.getElementsByClassName("close")[0];
            closeBtn.onclick = function () {
                document.getElementById("messageModal").style.display = "none";
            }

            // Close the modal if the user clicks anywhere outside the modal
            window.onclick = function (event) {
                var modal = document.getElementById("messageModal");
                if (event.target == modal) {
                    modal.style.display = "none";
                }
            }
        };

        function openModal() {
            document.getElementById("modal").style.display = "block";
        }

        function closeModal() {
            document.getElementById("modal").style.display = "none";
        }

        function downloadData() {
            // Mengambil data dari GridView dan menyiapkannya untuk diunduh
            // Anda dapat menambahkan logika untuk mengunduh data grid dalam format tertentu (misalnya CSV, Excel, dll).
            alert("Download data...");
        }

        function processData() {
            // Mengambil data dari GridView dan memprosesnya (misalnya menyimpan ke database)
            // Anda bisa menambahkan logika untuk menyimpan data ke database atau melakukan tindakan lainnya.
            alert("Proses data...");
        }

        function closeModal() {
            document.getElementById("modal").style.display = "none";
        }

        function openModalStatus(message) {
            document.getElementById('modalStatus').style.display = 'block';
            document.getElementById('statusMessage').innerText = message;
        }

        // Fungsi untuk menutup modal status
        function closeModalStatus() {
            document.getElementById('modalStatus').style.display = 'none';
        }
    </script>
    <style type="text/css">
        .error-label {
            width: 100%;
            word-wrap: break-word; /* Memastikan teks tidak meluber */
        }
        .modal-status {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.6); /* Transparan dengan latar belakang gelap */
            display: none; /* Disembunyikan secara default */
            z-index: 1000; /* Memastikan modal tampil di atas */
            padding-top: 100px;
            text-align: center;
        }

        .modal-status .modal-content {
            background-color: #fff;
            border: 1px solid #888;
            width: 50%;
            margin: 0 auto;
            padding: 20px;
            border-radius: 10px;
            text-align: center;
        }

        .modal-status .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

        .modal-status .close:hover,
        .modal-status .close:focus {
            color: black;
            text-decoration: none;
            cursor: pointer;
        }

        .modal-status #statusMessage {
            font-size: 18px;
            color: green;
            margin-top: 10px;
        }
        /* Modal */
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            display: none;
            z-index: 9999;
        }

        /* Konten Modal */
        .modal-content {
            position: relative;
            margin: 5% auto;
            padding: 20px;
            background-color: white;
            border-radius: 10px;
            width: 80%;
            max-width: 900px;
        }

        /* Gaya untuk LBL_PROCESS */
        .lbl-process {
            font-size: 15px;
            margin-bottom: 50px; /* Memberikan jarak ke bawah */
            width: 100%;
        }

        /* Gaya untuk Grid View */
        .modal-grid {
            max-height: 300px; /* Maksimal tinggi untuk grid */
            overflow-y: auto;  /* Aktifkan scrollbar vertikal */
            margin-bottom: 20px;
            margin-top: 20px;
        }

        /* Tombol-Tombol di Bawah Grid */
        .modal-buttons {
            text-align: right;
            margin-top: 5px;
        }

        .modal-buttons button {
            padding: 10px 15px;
            font-size: 14px;
            margin: 0 5px;
            border: none;
            border-radius: 2.8px;
            cursor: pointer;
        }

        .modal-buttons .btn-download {
            background-color: #4CAF50; /* Hijau */
            color: white;
        }

        .modal-buttons .btn-process {
            background-color: #008CBA; /* Biru */
            color: white;
        }

        .modal-buttons .btn-cancel {
            background-color: #f44336; /* Merah */
            color: white;
        }

        /* Tabel GridView */
        .table {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid #ddd;
        }

        .table th, .table td {
            padding: 10px;
            text-align: left;
            border: 1px solid #ddd;
        }

        .table th {
            background-color: #f2f2f2;
        }

        /* Tombol Tutup */
        .close {
            position: absolute;
            top: 4px;
            right: 20px;
            font-size: 30px;
            font-weight: bold;
            cursor: pointer;
        }

   
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <!-- Modal for Success or Error -->
        <div id="modalStatus" class="modal-status">
            <div class="modal-content">
                <h2>PROCESS STATUS</h2>
                <p id="statusMessage">Proses sedang berlangsung...</p>

                <!-- Tombol Close di Bawah Kanan -->
                <button class="close-btn" onclick="closeModalStatus()">OK</button>
            </div>
        </div>

        <div id="modal" class="modal" style="display:none;">
            <div class="modal-content">
                <span class="close" onclick="closeModal()">&times;</span>

                <!-- Error/Sukses Label -->
                <div style="margin-bottom: 20px">
                    <asp:Label ID="LBL_PROCESS" CssClass="lbl-process" runat="server" Font-Bold="True" TextMode="MultiLine"></asp:Label>
                    <asp:Label ID="LBL_PATH_FILE" runat="server" Visible="false"></asp:Label>
                </div>

                <!-- Grid untuk Menampilkan Data Upload -->
                <div>
                    <asp:GridView ID="GV_UploadedData" runat="server" AutoGenerateColumns="False" CssClass="table" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" CellSpacing="2" Style="width: 100%">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                            <asp:BoundField DataField="MEMOID" HeaderText="MEMO ID" SortExpression="MEMO ID" />
                            <asp:BoundField DataField="POLICY_NO" HeaderText="POLICY NO / REGNO" SortExpression="POLICY NO / REGNO" />
                            <asp:BoundField DataField="NOTE" HeaderText="NOTE" SortExpression="NOTE" />
                        </Columns>
                    </asp:GridView>
                </div>

                <!-- Tombol-Tombol untuk Download, Process, Cancel -->
                <div class="modal-buttons">
                    <asp:Button ID="btnDownload" runat="server" CssClass="btn-download" Text="Download" OnClick="btnDownloadList_Click" />
                    <asp:Button ID="btnProcess" runat="server" CssClass="btn-process" Text="Process" OnClick="btnProcess_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn-cancel" Text="Cancel" OnClick="btnCancel_Click" />
                    <%--<button class="btn-download" onclick="downloadData()" >Download</button>
                    <button class="btn-process" onclick="processData()">Process</button>
                    <button class="btn-cancel" onclick="closeModal()">Cancel</button>--%>
                </div>
            </div>
        </div>
        <div id="loading" style="display:none; position:fixed; top:50%; left:50%; transform:translate(-50%, -50%); z-index:9999; background-color: rgba(255, 255, 255, 0.8); padding: 20px; border-radius: 10px; text-align: center;">
            <img src="../Content/loading.gif" alt="Loading..." style="border: 0; width: 100px;" />
            <p>Loading...</p>
        </div>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 2px; width: 100%; font-size: 12px">
            <tr style="margin-top:2px">
                <td class="TDBGColor">
                    <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="XX-Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 6px;">
                        <tr>
                            <td style="width: 150px;">UPLOAD DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_UPDATEDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" Enabled="false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceUpdateDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_UPDATEDATE" >
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>TYPE <b>*</b></td>
                            <td>
                                <asp:DropDownList ID="DDL_TYPE" runat="server" AutoPostBack="false" CssClass="ASPDropDownList" 
                                    OnSelectedIndexChanged="DDL_TYPE_SelectedIndexChanged"  Width="300px" Font-Size="12px">
                                    <asp:ListItem Value="" ></asp:ListItem>
                                    <asp:ListItem Value="contribution_individual">CONTRIBUTION</asp:ListItem>
                                    <asp:ListItem Value="claim_individual">CLAIM</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="LBL_TYPE" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>TEMPLATE FILE</td>
                            <td>
                                <asp:LinkButton ID="LBT_TEMPLATE" runat="server" Text="DOWNLOAD" OnClick="btnDownload_Click" Font-Size="12px"></asp:LinkButton>
                                </td>
                        </tr>
                        <tr>
                            <td>UPLOAD FILE</td>
                            <td>
                                <asp:FileUpload ID="TXT_FILE_UPLOAD" runat="server" class="ASPTextBox" Font-Size="12px"/>
                        </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">DESCRIPTION</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCRIPTION" runat="server" CssClass="ASPTextBox" Width="200px" TextMode="MultiLine" 
                                    Height="50px" EnableTheming="false" Font-Size="12px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" OnClick="BT_UPLOAD_Click" Text="SUBMIT DOCUMENT" Font-Size="12px"/>
                            </td>
                        </tr>
                        <tr>
                           <td colspan="2">
                                <div style="max-height: 150px; overflow-y: auto; border: 1px solid #ccc; padding: 10px; background-color: #f9f9f9;">
                                    <asp:Label ID="LB_ERR" runat="server" CssClass="error-label" Font-Bold="True" Style="font-size: 12px; width: 100%;" TextMode="MultiLine"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>