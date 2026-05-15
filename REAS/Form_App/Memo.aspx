<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Memo.aspx.cs" Inherits="REAS.Form_App.Memo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />

    <style type="text/css">
        .pager-btn, .pager-link {
            display: inline-block;
            margin: 1px;
            padding: 4px 9px;
            border: 1px solid #4A3C8C;
            border-radius: 4px;
            background-color: #f8f8f8;
            color: #4A3C8C;
            text-decoration: none;
            cursor: pointer;
        }

            .pager-btn:hover, .pager-link:hover {
                background-color: #4A3C8C;
                color: #fff;
            }

            .pager-link.active {
                background-color: #4A3C8C;
                color: white;
                font-weight: bold;
            }

        /* PANEL CONTAINER */
        .panel-container {
            width: 95%;
            margin-top: 20px;
            background: #ffffff;
            border-radius: 10px;
            border: 1px solid #d0d0e0;
            box-shadow: 0 4px 10px rgba(0,0,0,0.08);
            animation: fadeIn 0.3s ease-in-out;
            padding-bottom: 20px;
        }

        /* PANEL HEADER */
        .panel-header {
            background: #4A3C8C;
            color: white;
            padding: 12px 18px;
            border-radius: 10px 10px 0 0;
            font-weight: bold;
            font-size: 16px;
            letter-spacing: 0.5px;
            border-bottom: 3px solid #352c6b;
        }

        /* PANEL BODY */
        .panel-body {
            padding: 30px;
        }

        /* ANIMATION */
        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(-8px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .dropdown-wrapper {
            position: relative;
            width: 300px;
        }

        .dropdown-display {
            padding: 6px 10px;
            border: 1px solid #888;
            background: #fff;
            border-radius: 4px;
            cursor: pointer;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

            .dropdown-display .arrow {
                font-size: 12px;
                color: #555;
            }

        .dropdown-content {
            display: none;
            position: absolute;
            width: 300px;
            background: white;
            border: 1px solid #888;
            border-radius: 5px;
            padding: 5px;
            margin-top: 3px;
            z-index: 2000;
            box-shadow: 0px 4px 8px rgba(0,0,0,0.15);
        }

        .multi-dropdown-type .dropdown-display,
        .multi-dropdown-type .dropdown-content {
            width: 300px !important;
        }


        .custom-listbox {
            border: none;
            width: 100%;
            outline: none;
        }

        .modal-status {
            display: none;
            position: fixed;
            z-index: 9999;
            padding-top: 120px;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0,0.4);
        }

        .modal-content-status {
            background-color: #fff;
            margin: auto;
            padding: 20px 30px;
            border-radius: 8px;
            width: 380px;
            text-align: center;
            font-family: Arial;
            box-shadow: 0 0 10px rgba(0,0,0,0.3);
            animation: fadeIn 0.4s;
        }

        .close-status {
            color: #aaa;
            float: right;
            font-size: 22px;
            font-weight: bold;
            cursor: pointer;
        }

            .close-status:hover {
                color: #000;
            }

        /* ====== ANIMASI FADE IN (DIGUNAKAN PANEL & MODAL) ====== */
        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(-15px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* PANEL */
        .panel-container {
            animation: fadeIn 0.3s ease-in-out;
        }

        /* MODAL BACKDROP */
        .modal-status {
            display: none;
            position: fixed;
            z-index: 9999;
            padding-top: 120px;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0,0.4);
        }

        /* MODAL BODY */
        .modal-content-status {
            background-color: #fff;
            margin: auto;
            padding: 20px 30px;
            border-radius: 8px;
            width: 380px;
            text-align: center;
            font-family: Arial;
            box-shadow: 0 0 10px rgba(0,0,0,0.3);
            animation: fadeIn 0.4s ease-in-out;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="modalStatus" class="modal-status">
            <div class="modal-content-status">
                <span class="close-status" onclick="closeModalStatus()">&times;</span>
                <h3 id="modalMessageStatus">Status</h3>
            </div>
        </div>
        <div id="loading" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 9999; background-color: rgba(255, 255, 255, 0.8); padding: 20px; border-radius: 10px; text-align: center;">
            <img src="../Content/loading.gif" alt="Loading..." style="border: 0; width: 100px;" />
            <p>Loading...</p>
        </div>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 2px; width: 100%; font-size: 12px">
            <!-- Row 1: Filter Section -->
            <tr style="margin-top: 2px;">
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
                                           <div class="dropdown-wrapper">
                                                <div class="dropdown-display" onclick="toggleDDLReas()">
                                                    <span id="ddlReasText">Select REAS NAME</span>
                                                    <span class="arrow">▼</span>
                                                </div>

                                                <div id="ddlReasPanel" class="dropdown-content">
                                                    <asp:ListBox
                                                        ID="DDL_REAS"
                                                        runat="server"
                                                        CssClass="custom-listbox"
                                                        SelectionMode="Multiple"
                                                        Width="300px"
                                                        Height="160px"
                                                        onchange="updateDDLReasText()">
                                                    </asp:ListBox>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">REAS TYPE</td>

                                        <td>
                                            <div class="dropdown-wrapper">
                                                <div class="dropdown-display" onclick="toggleDDLType()">
                                                    <span id="ddlTypeText">Select REAS TYPE</span>
                                                    <span class="arrow">▼</span>
                                                </div>

                                                <div id="ddlTypePanel" class="dropdown-content">
                                                    <asp:ListBox ID="DDL_TYPE"
                                                        runat="server"
                                                        CssClass="custom-listbox"
                                                        SelectionMode="Multiple"
                                                        Width="300px"
                                                        Height="160px"
                                                        onchange="updateDDLTypeText()">
                                                        <asp:ListItem Value="all">ALL REAS TYPE</asp:ListItem>
                                                        <asp:ListItem Value="contribution_health">CONTRIBUTION HEALTH</asp:ListItem>
                                                        <asp:ListItem Value="contribution_individual">CONTRIBUTION INDIVIDUAL</asp:ListItem>
                                                        <asp:ListItem Value="claim_health">CLAIM HEALTH</asp:ListItem>
                                                        <asp:ListItem Value="claim_individual">CLAIM INDIVIDUAL</asp:ListItem>
                                                        <asp:ListItem Value="claim_glife">CLAIM GLIFE</asp:ListItem>
                                                        <asp:ListItem Value="refund">REFUND GLIFE</asp:ListItem>
                                                        <asp:ListItem Value="contribution_gtlr">CONTRIBUTION GTLR GLIFE</asp:ListItem>
                                                        <asp:ListItem Value="contribution_non_gtlr">CONTRIBUTION NON GTLR GLIFE</asp:ListItem>
                                                        <asp:ListItem Value="contribution_renewal">CONTRIBUTION RENEWAL GLIFE</asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                            </div>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">PERIOD</td>
                                        <td>

                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>

                                            <asp:Label ID="Label1" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="XX-Small">-</asp:Label>

                                            <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodEnd" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                            </ajaxToolkit:CalendarExtender>

                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" OnClientClick="showLoading();" />
                                            <asp:Button ID="BT_CREATE" runat="server" CssClass="ASPButton" Text="CREATE MEMO" OnClick="BT_CREATE_Click" Width="100px" BackColor="Green" />

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
                    <br />
                    <!-- ===== RESULT STRING TABLE ===== -->
                    <br />

                    <table id="tblStringResult" runat="server"
                        style="width: 400px; border-collapse: collapse; font-size: 12px;"
                        border="1" visible="false">

                        <tr style="background-color: #4A3C8C; color: white;">
                            <td style="padding: 6px; font-weight: bold;">TRANSAKSI
                            </td>
                            <td style="padding: 6px; font-weight: bold;">NOMINAL
                            </td>
                        </tr>

                        <tr>
                            <td style="padding: 6px;">Total Amount Settlement Tabbaru</td>
                            <td style="padding: 6px;">
                                <asp:Label ID="LBL_STR_TOTAL_TBR" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td style="padding: 6px;">Total Amount Settlement Ujroh</td>
                            <td style="padding: 6px;">
                                <asp:Label ID="LBL_STR_TOTAL_UJR" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td style="padding: 6px;">Total Amount Settlement Contribution</td>
                            <td style="padding: 6px;">
                                <asp:Label ID="LBL_STR_TOTAL_CTRB" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td style="padding: 6px;">Total Amount Settlement Claim / Refund</td>
                            <td style="padding: 6px;">
                                <asp:Label ID="LBL_STR_TOTAL_CLMRF" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td style="padding: 6px;">Total Amount Settlement Net Off</td>
                            <td style="padding: 6px;">
                                <asp:Label ID="LBL_STR_TOTAL_NETOFF" runat="server" />
                            </td>
                        </tr>

                    </table>

                    <br />
                    <!-- ===== END STRING TABLE ===== -->

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical"
                        Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False"
                        Font-Size="11px"
                        PageSize="200">

                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" Wrap="false" Font-Size="13px" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="ACTIONS" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="btnAction" runat="server" Text="ACTION" CommandName="ActionCommand" CommandArgument='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TYPE" HeaderText="TYPE" />
                            <asp:BoundColumn DataField="ID" HeaderText="ID" FooterStyle-Font-Bold="true">
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS" HeaderText="STATUS" />
                            <asp:BoundColumn DataField="REAS_NAME" HeaderText="REAS NAME" />
                            <asp:BoundColumn DataField="NAMA_PESERTA" HeaderText="NAMA PESERTA" />
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO" />
                            <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT" HeaderText="NO SURAT SETTLEMENT" />
                            <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_CONTRIBUTION" HeaderText="AMOUNT SETTLEMENT CONTRIBUTION" />
                            <asp:BoundColumn DataField="AMOUNT_TABBARU" HeaderText="AMOUNT TABBARU" />
                            <asp:BoundColumn DataField="AMOUNT_UJROH" HeaderText="AMOUNT UJROH" />
                            <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_CLAIM_REFUND" HeaderText="AMOUNT SETTLEMENT CLAIM/REFUND" />
                            <asp:BoundColumn DataField="TOTAL_NETOFF" HeaderText="TOTAL NETOFF" />

                            <asp:BoundColumn DataField="UPDATE_DATE_SETTLEMENT" HeaderText="UPDATE DATE SETTLEMENT" DataFormatString="{0:dd MMM yyyy}" />
                        </Columns>
                    </asp:DataGrid>
                    <div style="margin-top: 6px; text-align: left;">
                        <asp:LinkButton ID="lnkPrev" runat="server" Text="◀ Prev" OnClick="lnkPrev_Click" CssClass="pager-btn" />
                        <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server"
                                    CommandName="Page"
                                    CommandArgument='<%# Eval("PageNumber") %>'
                                    Text='<%# Eval("PageNumber") %>'
                                    CssClass="pager-link" />
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:LinkButton ID="lnkNext" runat="server" Text="Next ▶" OnClick="lnkNext_Click" CssClass="pager-btn" />
                    </div>
                </td>
            </tr>

            <!-- Row 2: Input Form (Hidden by Default) -->
            <tr>
                <td>
                    <div id="divPanel" runat="server" style="display: none;" class="panel-container">
                        <div class="panel-header">CREATE MEMO</div>
                        <table class="form-table">
                            <tr>
                                <!-- Column 1 -->
                                <td class="form-column">
                                    <asp:UpdatePanel ID="UPDate" runat="server">
                                        <ContentTemplate>
                                            <table class="form-sub-table">
                                                <tr>
                                                    <td style="width: 90px;">MEMO DATE</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_UPDATEDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" AutoPostBack="true" OnTextChanged="TXT_UPDATEDATE_TextChanged"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="ceUpdateDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_UPDATEDATE"></ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 90px;">HIJRIYAH DATE</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_HIJRIYAH" runat="server" CssClass="ASPTextBox" Width="200px" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>MEMO TYPE</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_MEMO" runat="server" AutoPostBack="False" CssClass="ASPDropDownList" Width="200px">
                                                            <asp:ListItem Value=""></asp:ListItem>
                                                            <asp:ListItem Value="IM">INTERNAL MEMO</asp:ListItem>
                                                            <asp:ListItem Value="EM">EXTERNAL MEMO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;">PERIHAL</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_PERIHAL" runat="server" CssClass="ASPTextBox" Width="350px" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;">REMARKS</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_REMARKS" runat="server" CssClass="ASPTextBox" Width="350px" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 30px;"></td>
                                <!-- Column 2 -->
                                <td class="form-column">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table class="form-sub-table">
                                                <tr>
                                                    <td style="width: 100px;">AMOUNT</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBox" Width="200px" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;">ACTUAL AMOUNT</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_ACTUAL_AMOUNT" runat="server" CssClass="ASPTextBox" Width="200px" AutoPostBack="true" OnTextChanged="TXT_ACTUAL_AMOUNT_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;">AMOUNT SPELL</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_AMOUNT_SPELL" runat="server" CssClass="ASPTextBox" Width="350px" TextMode="MultiLine" Rows="2" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;">REFERENCE</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_REFERENCE" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>UPLOAD FILE*</td>
                                                    <td>
                                                        <asp:FileUpload ID="TXT_FILE_UPLOAD" runat="server" AllowMultiple="true" class="ASPTextBox" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <!-- Process and Cancel Buttons -->
                        <div class="modal-buttons">
                            <asp:Button ID="BT_CREATE_MEMO" runat="server" CssClass="btn-process" Text="Process" OnClick="BT_CREATE_MEMO_Click" />
                            <asp:Button ID="BT_CANCEL_MEMO" runat="server" CssClass="btn-cancel" Text="Cancel" OnClick="BT_CANCEL_MEMO_Click" />
                        </div>
                    </div>
                </td>
            </tr>

        </table>
    </form>
    <script type="text/javascript">
        function showLoading() {
            document.getElementById("loading").style.display = "block";

            // Tambahkan ini: Sembunyikan loading setelah 3 detik
            // Waktu ini adalah perkiraan; sesuaikan berdasarkan rata-rata waktu respons server Anda.
            setTimeout(function () {
                hideLoading();
            }, 20000); // 3000 ms = 3 detik
        }

        function hideLoading() {
            var loadingElement = document.getElementById("loading");
            if (loadingElement) {
                loadingElement.style.display = "none";
            }
        }

        // Biarkan window.onload tetap ada untuk menyembunyikan loading saat halaman dimuat pertama kali
        window.onload = function () {
            hideLoading();
        };


        function toggleDDLType() {
            var panel = document.getElementById("ddlTypePanel");
            panel.style.display = (panel.style.display === "block") ? "none" : "block";
        }

        document.addEventListener("click", function (e) {
            document.querySelectorAll(".dropdown-wrapper").forEach(function (wrapper) {
                if (!wrapper.contains(e.target)) {
                    var panel = wrapper.querySelector(".dropdown-content");
                    if (panel) panel.style.display = "none";
                }
            });
        });

        function updateDDLTypeText() {
            var lb = document.getElementById("<%= DDL_TYPE.ClientID %>");
            var label = document.getElementById("ddlTypeText");

            var selected = [];
            for (var i = 0; i < lb.options.length; i++) {
                if (lb.options[i].selected)
                    selected.push(lb.options[i].text);
            }
            label.textContent = selected.length > 0 ? selected.join(", ") : "Select REAS TYPE";
        }

        function toggleDDLReas() {
            var panel = document.getElementById("ddlReasPanel");
            panel.style.display = (panel.style.display === "block") ? "none" : "block";
        }

        function updateDDLReasText() {
            var lb = document.getElementById("<%= DDL_REAS.ClientID %>");
            var label = document.getElementById("ddlReasText");

            var selected = [];
            for (var i = 0; i < lb.options.length; i++) {
                if (lb.options[i].selected)
                    selected.push(lb.options[i].text);
            }

            label.textContent = selected.length > 0
                ? selected.join(", ")
                : "Select REAS NAME";
        }

        function openModalStatus(message) {
            alert("MODAL FUNCTION DIPANGGIL!\nPesan: " + message);
            document.getElementById("modalMessageStatus").innerHTML = message;
            document.getElementById("modalStatus").style.display = "block";
        }

        function closeModalStatus() {
            document.getElementById("modalStatus").style.display = "none";
        }

        window.onclick = function (event) {
            var modal = document.getElementById("modalStatus");
            if (event.target === modal) {
                modal.style.display = "none";
            }
        }



    </script>
</body>
</html>
