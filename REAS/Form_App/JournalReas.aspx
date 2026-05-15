<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JournalReas.aspx.cs" Inherits="REAS.Form_App.JournalReas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">7
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
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

    function openModalViewDetailMemo(data) {
        // Menampilkan modal
        document.getElementById('modalViewDetail').style.display = 'block';
        // Menyiapkan konten tabel untuk modal
        var modalContent = "<table class='table'><thead><tr>" +
            "<th>MEMO TYPE</th>" +
            "<th>ID</th>" +
            "<th>NO POLIS</th>" +
            "<th>TGL AWAL POLIS</th>" +
            "<th>TGL AKHIR POLIS</th>" +
            "</tr></thead><tbody>";

        // Looping untuk mengisi data dalam tabel
        data.forEach(function (item) {
            modalContent += "<tr>" +
                "<td>" + item.MEMO_TYPE + "</td>" +
                "<td>" + item.ID + "</td>" +
                "<td>" + item.NO_POLIS + "</td>" +
                "<td>" + formatDate(item.TGL_MULAI_POLIS) + "</td>" +
                "<td>" + formatDate(item.TGL_AKHIR_POLIS) + "</td>" +
                "</tr>";
        });

        modalContent += "</tbody></table>";

        // Ambil nilai dari elemen input hidden
        var noMemoValue = document.getElementById("hiddenNoMemoView").value;

        // Mengubah konten modal dengan tabel yang telah disusun
        document.getElementById("viewDetailContent").innerHTML = modalContent;
        document.getElementById("viewModalLabel").innerHTML = "NO MEMO : " + noMemoValue;


        // Menampilkan modal
        $('#modalViewDetail').modal('show');
    }

    function closeModalStatus() {
        document.getElementById('modalStatus').style.display = 'none';
        document.getElementById('modalViewDetail').style.display = 'none';
        document.getElementById('modalUpdateDoc').style.display = 'none';

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

 
        .modal-content {
            position: relative;
            margin: 5% auto;
            background-color: #fff;
            padding: 20px;
            width: 80%;
            max-width: 800px; /* Ukuran lebar maksimal */
            max-height: 80vh; /* Ukuran maksimal tinggi modal */
            overflow-y: auto; /* Scroll vertikal jika konten melebihi tinggi modal */
        }

        /* Styling untuk tabel utama */
        .form-table {
            width: 100%;
            display: flex;
            justify-content: space-between; /* Membuat kolom sejajar */
            margin-top: 20px;
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
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

     <%--   <div id="modalViewDetail" class="modal-view">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="viewModalLabel">NO MEMO : </h5>
                </div>
                 <div class="modal-buttons">
                    <asp:Button ID="btnDownloadDetail" runat="server" CssClass="btn-process" Text="Download File" OnClick="btnDownloadDetail_Click" />
                    <asp:Button ID="btnCloseView" runat="server" CssClass="btn-cancel" Text="Close" OnClick="btnCloseView_Click" />
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hiddenNoMemoView" runat="server" />
                    <asp:TextBox ID="txtNoMemoView" runat="server" ReadOnly="true" CssClass="form-control" Visible="false"/>
                    <div id="viewDetailContent">Proses sedang berlangsung...</div>
                </div>
        
            </div>
        </div>--%>

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
                <div style="margin-bottom: 20px">
                    <h3 id="modalTitle" class="modal-title">UPLOAD DRAFT JOURNAL</h3>
                    <p>** <b>Mohon di perhatikan</b>. Data yang di upload akan di jadikan Draft Journal.</p>
                </div>

               <table class="form-table">
                <tr>
                    <%--Kolom 1--%>
                    <td class="form-column">
                        <table class="form-sub-table">
                            <tr>
                                <td style="width: 100px;">REAS NAME</td>
                                <td>
                                     <asp:DropDownList 
                                        ID="DDL_REAS_SUB" 
                                        runat="server" 
                                        AutoPostBack="False" 
                                        CssClass="ASPDropDownList" 
                                        OnSelectedIndexChanged="DDL_REAS_SUB_SelectedIndexChanged"  
                                        Width="200px">
                                    </asp:DropDownList>   
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px;">TYPE</td>
                                <td>
                                    <asp:DropDownList ID="DDL_TYPE" runat="server" AutoPostBack="false" CssClass="ASPDropDownList" 
                                        OnSelectedIndexChanged="DDL_TYPE_SelectedIndexChanged"  Width="200px">
                                        <asp:ListItem Value="" ></asp:ListItem>
                                        <asp:ListItem Value="CRH">CONTRIBUTION HEALTH</asp:ListItem>
                                        <asp:ListItem Value="CRI">CONTRIBUTION INDIVIDUAL</asp:ListItem>
                                        <asp:ListItem Value="CLI">CLAIM INDIVIDUAL</asp:ListItem>
                                        <asp:ListItem Value="CLH">CLAIM HEALTH</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px;">REMARKS</td>
                                <td>
                                    <asp:TextBox ID="TXT_REMARKS" runat="server" CssClass="ASPTextBox" Width="250px" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                 <td>TEMPLATE FILE</td>
                                 <td>
                                     <asp:LinkButton ID="btnDownloadTemplate" runat="server" Text="DOWNLOAD" OnClick="btnDownloadTemplate_Click"></asp:LinkButton>
                                  </td>
                             </tr>
                            <tr>
                                <td>UPLOAD FILE*</td>
                                <td>
                                    <asp:FileUpload ID="TXT_FILE_UPLOAD" runat="server" AllowMultiple="true" class="ASPTextBox"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="max-height: 150px; overflow-y: auto; border: 0px solid #ccc; padding: 10px; background-color: #f9f9f9;">
                                        <asp:Label ID="LB_ERR" runat="server" CssClass="error-label" Font-Bold="True" Style="font-size: x-small; width: 100%;" TextMode="MultiLine"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>

                <!-- Tombol-Tombol untuk Process, Cancel -->
                <div class="modal-buttons">
                    <asp:Button ID="btnUploadDraft" runat="server" CssClass="btn-process" Text="Process" OnClick="btnUploadDraft_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn-cancel" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>

        <div id="loading" style="display:none; position:fixed; top:50%; left:50%; transform:translate(-50%, -50%); z-index:9999; background-color: rgba(255, 255, 255, 0.8); padding: 20px; border-radius: 10px; text-align: center;">
            <img src="../Content/loading.gif" alt="Loading..." />
            <p>Loading...</p>
        </div>
        <input type="file" id="fileInput" runat="server" style="display:none;"  onchange="handleFileSelect(event)" />


        <div>
        </div>    <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="margin-top:2px">
                <td class="TDBGColor">
                    <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="XX-Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">REAS NAME</td>
                                        <td>
                                            <%--<asp:TextBox ID="TXT_REASNAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>--%>
                                            <asp:DropDownList 
                                                ID="DDL_REAS" 
                                                runat="server" 
                                                AutoPostBack="false" 
                                                CssClass="ASPDropDownList" 
                                                OnSelectedIndexChanged="DDL_REAS_SelectedIndexChanged"  
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">NO JOURNAL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NO_SURAT" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <%--<tr>
                                        <td style="width: 100px;"></td>
                                        <td>
                                            <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                            <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD" OnClick="BT_DOWNLOAD_Click" Width="100px" />
                                            
                                        </td>
                                    </tr>--%>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">START DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                          <td style="width: 100px;">END DATE</td>
                                        <td>
                         
                                            <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodEnd" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                            </ajaxToolkit:CalendarExtender></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" OnClientClick="showLoading();"/>
                                            <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD" OnClick="BT_DOWNLOAD_Click" Width="100px" BackColor="Aqua"/>
            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_JOURNAL" runat="server" CssClass="ASPButton" Text="ADD DRAFT JOURNAL" BackColor="Yellow" OnClick="BT_JOURNAL_Click" Width="125px" OnClientClick="showLoading();"/>
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
                    <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
                                Font-Size="11px"
                                CssClass="ASPDatagrid" OnPageIndexChanged="DGR_PageIndexChanged">
                   
                           <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" Wrap="false"/>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Left" Mode="NumericPages" />

                    <Columns>
                        <asp:BoundColumn DataField="NO" HeaderText="NO" />
                        <asp:BoundColumn DataField="JOURNAL_STATUS" HeaderText="STATUS" />
                        <asp:BoundColumn DataField="JOURNAL_NO" HeaderText="JOURNAL NO" FooterStyle-Font-Bold="true">
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="JOURNAL_DATE" HeaderText="JOURNAL DATE" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundColumn DataField="JOURNAL_TYPE" HeaderText="JOURNAL TYPE" />
                        <asp:BoundColumn DataField="REAS_NAME" HeaderText="REAS NAME" />
                        <asp:BoundColumn DataField="REAS_TYPE" HeaderText="REAS TYPE" />
                        <asp:BoundColumn DataField="AMOUNT" HeaderText="TOTAL AMOUNT" />

                        <asp:BoundColumn DataField="REMARKS" HeaderText="REMARKS" />
                        <asp:TemplateColumn HeaderText="ACTIONS">
                            <ItemTemplate>
                                <asp:Button ID="btnAction" runat="server" Text="APPROVE" OnCommand="btnAction_Command" CommandName="Approve" CommandArgument='<%# Eval("JOURNAL_NO") %>' Width="70px" Font-Size="10px" Enabled='<%# Eval("JOURNAL_STATUS").ToString() == "NEW" %>'/>
                                <asp:Button ID="btnReject" runat="server" Text="REJECT" OnCommand="btnReject_Command" CommandName="Reject" CommandArgument='<%# Eval("JOURNAL_NO") %>' Width="70px" Font-Size="10px" Enabled='<%# Eval("JOURNAL_STATUS").ToString() == "NEW" %>'/>
                                <asp:Button ID="btnView" runat="server" Text="VIEW" OnCommand="btnView_Command" CommandName="View" CommandArgument='<%# Eval("JOURNAL_NO") %>' Width="70px" Font-Size="10px" Visible="false"/>
                            </ItemTemplate>
   
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                </td>
            </tr>
        </table>
       
    </form>

</body>
</html>
