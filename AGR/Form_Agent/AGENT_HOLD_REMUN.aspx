<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_HOLD_REMUN.aspx.cs" Inherits="AGR.Form_Agent.AGENT_HOLD_REMUN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
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



        function ShowAlert(msg) {
            alert(msg);
        }
    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: transparent;
            z-index: 99;
            opacity: 0.9;
            filter: alpha(opacity=90);
            -moz-opacity: 0.9;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: transparent;
            z-index: 999;
        }

        .fa {
            display: inline-block;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: inherit;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        #policyPager {
            text-align: center;
            margin-top: 8px;
        }

        #policyPager .pager-link {
            display: inline-block;
            padding: 3px 7px;
            margin: 0 2px;
            border: 1px solid #bbb;
            cursor: pointer;
            font-size: 11px;
            text-decoration: none;
            color: #333;
        }

        #policyPager .pager-current {
            display: inline-block;
            padding: 3px 7px;
            margin: 0 2px;
            background: #2c7be5;
            color: #fff;
            font-size: 11px;
            font-weight: bold;
        }
    </style>
    <style>
        /* Membuat kolom REMUN TYPE punya lebar fix */
        .remun-cell {
            width: 300px;               /* fix sesuai kebutuhan */
            display: inline-block;
            vertical-align: top;
            white-space: normal;        /* biarkan text wrap tapi tidak menambah width */
        }

        /* Supaya label miring tetap dalam alur tanpa menggeser kolom */
        .remun-label, 
        .policy-count-label {
            display: inline-block;
            max-width: 120px;      /* batasi lebar */
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            margin-left: 5px;
            font-style: italic;
            color: darkblue;
        }

        /* Supaya label remun tidak membuat cell melar */
        .remun-wrapper {
            display: flex;
            flex-wrap: wrap;
            gap: 3px;
            align-items: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager 
            ID="ToolkitScriptManager1" 
            runat="server"
            EnablePageMethods="true">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>

        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">AGENT CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>UPLINER NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_UPLINER" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>DATE</td>
                            <td>
                                <asp:TextBox 
                                    ID="TXT_FILTER_DATE" 
                                    runat="server" 
                                    CssClass="ASPTextBox" 
                                    Width="120px"
                                    placeholder="dd/MM/yyyy">
                                </asp:TextBox>

                                <ajaxToolkit:CalendarExtender 
                                    ID="CE_FILTER_DATE" 
                                    runat="server" 
                                    TargetControlID="TXT_FILTER_DATE"
                                    Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>AGENCY</td>
                            <td>
                                <asp:TextBox ID="TXT_AGENCY" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">DIVISION IN CHARGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>LEVEL</td>
                            <td>
                                <asp:TextBox ID="TXT_CHANNEL" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: left;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_RECORDS" runat="server"></asp:Label></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_SELECTED_AGENT" runat="server" />
        <asp:HiddenField ID="HF_POLICY_DETAIL" runat="server" />
        <asp:HiddenField ID="HF_SELECTED_REMUN_TYPE" runat="server" />
        <asp:HiddenField ID="HF_IS_SELECT_ALL" runat="server" />
        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid"
            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
            <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="XX-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
            <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
            <Columns>
                <asp:TemplateColumn HeaderText="AGENT CODE">
                    <ItemTemplate>
                        <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="60" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FULLNAME" HeaderText="AGENT"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="REMUN TYPE">
                    <ItemStyle Width="300px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="CB_ALL"
                            runat="server"
                            Text=" REMUN TYPE"
                            onclick="toggleAllRemun(this);" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <table style="border-spacing:0px;">
                            <tr>
                                <!-- Checkbox Select All PER AGENT -->
                                <%--<td style="width:20px;">
                                    <input type="checkbox" class="remun-select-all-agent" data-agent='<%# Eval("CODE") %>' onclick="selectAllRemunPerAgent(this)" />
                                </td>--%>

                                <!-- REMUN TYPE VALUE -->
                                <td>
                                    <div class="remun-cell">
                                        <asp:Literal 
                                            ID="LIT_REMUN_TYPE"
                                            runat="server"
                                            Text='<%# ((System.Data.DataRowView)Container.DataItem)["REMUN_TYPE"].ToString() %>'>
                                        </asp:Literal>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Left" Width="450" />
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderTemplate>
                        <table style="border-spacing: 0px; width: 100%">
                            <tr>

                                <td style="padding-left:10px;">
                                    <!-- Tombol Hold Remun -->
                                    <asp:Button ID="BT_A" runat="server"
                                    CssClass="ASPButton"
                                    Text="HOLD REMUN"
                                    BackColor="Red"
                                    ForeColor="White"
                                    CommandName="Approve"
                                    Width="100"
                                    OnClientClick="collectSelectedAgents();" />
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="border-spacing: 0px; width: 100%">
                            <tr style="vertical-align: top;">
                                <td style="width: 30px;">
                                    <%--<asp:CheckBox ID="CB" runat="server" /></td>--%>
                                <td>
                                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                        <tr>
                                            <td style="width: 80px; border-bottom: ridge;">START DATE</td>
                                            <td style="border-bottom: ridge;">
                                                <asp:TextBox ID="TXT_STARTDATE" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small" CssClass="ASPTextBox"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>REASON</td>
                                            <td>
                                                <asp:TextBox ID="TXT_REASON" runat="server" CssClass="ASPTextBox" Width="100%" Height="40" TextMode="MultiLine" MaxLength="1000" placeholder="Reason .." BackColor="#ffe8ff"></asp:TextBox>

                                                <!-- tombol upload document -->
                                                <div style="margin-top:6px;">
                                                    <button type="button" 
                                                            class="ASPButton"
                                                            style="background-color:green; color:white; width:120px;"
                                                            onclick="triggerUploadRow('<%# Eval("CODE") %>')">
                                                        UPLOAD DOCUMENT
                                                    </button>

                                                    <!-- hidden input file per-row -->
                                                    <input type="file"
                                                           id="fileUpload_<%# Eval("CODE") %>"
                                                           style="display:none;"
                                                           onchange="onFileSelected('<%# Eval("CODE") %>')" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
        </asp:DataGrid>
        <div style="background-color: transparent !important; opacity: 0.95;">
            <asp:Panel ID="pnlHold" runat="server" Width="60%"
                Style="height:auto; z-index:2000; background-color:White; position:fixed;
                       left:120px; top:50px; border:outset 2px gray; padding:10px; display:none">

                <table style="width:100%; border-spacing:0px;">
                    <tr>
                        <td class="TDBGColor" style="font-weight:bold; font-size:14px;">
                            POLICY LIST (HOLD)
                        </td>
                        <td width="20px" align="right">
                            <asp:Button ID="BTN_CLOSE_HOLD" runat="server" CssClass="ASPButton"
                                BackColor="Red" ForeColor="White" Font-Bold="True"
                                Text="X"
                                OnClientClick="document.getElementById('pnlHold').style.display='none'; return false;" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" style="padding-top:10px;">
                            <div id="policyScroll" style="
                                max-height: 150px; 
                                overflow-y: auto; 
                                border: 1px solid #ccc;
                            ">
                                <table border="1" cellpadding="5" cellspacing="0" width="100%">
                                    <thead style="background:#f2f2f2; font-weight:bold; text-align:center;">
                                        <tr>
                                            <th rowspan="2" style="width:30px;">
                                                <input type="checkbox" id="chkAll" onclick="toggleAllPolicy(this)">
                                            </th>
                                            <th rowspan="2">POLICY NO</th>
                                            <th rowspan="2">INSURED NAME</th>
                                            <th rowspan="2">AMOUNT</th>
                                            <th colspan="2">PERIODE REMUN</th>
                                        </tr>
                                        <tr>
                                            <th>Start Date</th>
                                            <th>End Date</th>
                                        </tr>
                                    </thead>

                                    <tbody id="policyBody"></tbody>
                                </table>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" style="padding-top:8px; text-align:center;">
                            <div id="policyPager"></div>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" align="right" style="padding-top:12px;">
                            <button type="button" class="ASPButton" onclick="savePolicy()">SAVE</button>
                            &nbsp;
                            <button type="button" class="ASPButton" onclick="closePopup(true)">CANCEL</button>
                        </td>
                    </tr>
                </table>

            </asp:Panel>
        </div>
        <script type="text/javascript">
            var currentRemunCheckbox = null;
            var policyPage = 1;
            var policyPageSize = 50;
            var totalPolicyRows = 0;
            var isPagingPolicy = false;
            var currentAgentCode = null;
            var currentRemunType = null;

            var policyPage = 1;
            var policyPageSize = 50;
            var totalPolicyRows = 0;

            var isPolicyPaging = false;

            var selectedPolicyDetails = [];
            var isSelectAllPolicy = false;
            var selectedPolicyMap = {}; 
            var selectedPolicies = new Set();
            var selectedRemunTypes = [];

            var selectedFiles = {};

            function onFileSelected(agentCode) {
                var input = document.getElementById("fileUpload_" + agentCode);
                if (!input || input.files.length === 0) return;

                var file = input.files[0];

                var reader = new FileReader();
                reader.onload = function () {

                    var base64 = reader.result.split(',')[1]; // buang data:mime;base64,

                    fetch("AGENT_HOLD_REMUN.aspx/UploadTempFile", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json; charset=utf-8"
                        },
                        body: JSON.stringify({
                            agentCode: agentCode,
                            fileName: file.name,
                            contentType: file.type,
                            fileSize: file.size,
                            base64Data: base64
                        })
                    })
                        .then(res => res.json())
                        .then(() => alert("File tersimpan sementara"))
                        .catch(err => alert(err));
                };

                reader.readAsDataURL(file);
            }

            // helper: set policy-count-label sebelah remun checkbox (dipakai ketika save)
            function updateRemunLabel(remunCheckbox, count) {
                if (!remunCheckbox) return;

                var td = remunCheckbox.parentElement;
                var old = td.querySelector(".policy-count-label");

                if (count <= 0) {
                    if (old) old.remove();
                    return;
                }
                if (!old) {
                    var label = document.createElement("span");
                    label.className = "policy-count-label";
                    label.style.fontStyle = "italic";
                    label.style.marginLeft = "8px";
                    label.style.color = "darkblue";
                    label.innerText = count + " Policy Selected";
                    td.appendChild(label);
                } else {
                    old.innerText = count + " Policy Selected";
                }
            }

            // jangan biarkan klik di panel menutup popup
            document.getElementById("pnlHold").addEventListener("click", function (e) {
                e.stopPropagation();
            });

            // bersihkan checkbox policy pada popup
            function clearPolicySelections() {
                selectedPolicies.clear();
                isSelectAllPolicy = false;

                var chkAll = document.getElementById("chkAll");
                if (chkAll) chkAll.checked = false;

                document.querySelectorAll(".policy-check").forEach(cb => cb.checked = false);
            }

            function renderPolicyTable(data) {

                var tbody = document.getElementById("policyBody");
                tbody.innerHTML = "";

                if (!data || data.length === 0) {
                    tbody.innerHTML = "<tr><td colspan='6' align='center'>No data</td></tr>";
                    return;
                }

                data.forEach(function (row) {

                    // Select all masih dihormati
                    var checked = isSelectAllPolicy ? "checked" : "";

                    var trHtml =
                        "<tr " +
                        " data-policy='" + row.PolicyNo + "'" +
                        " data-insured='" + (row.PolicyHolder || "") + "'" +
                        " data-amount='" + row.Amount + "'" +
                        " data-start='" + formatDateForAttr(row.StartDate) + "'" +
                        " data-end='" + formatDateForAttr(row.EndDate) + "'" +
                        ">" +

                        "<td align='center'>" +
                        "<input type='checkbox' class='policy-check' " + checked + ">" +
                        "</td>" +

                        "<td>" + row.PolicyNo + "</td>" +
                        "<td>" + (row.PolicyHolder || "") + "</td>" +
                        "<td align='right'>" + Number(row.Amount).toLocaleString() + "</td>" +
                        "<td align='center'>" + parseDate(row.StartDate) + "</td>" +
                        "<td align='center'>" + parseDate(row.EndDate) + "</td>" +

                        "</tr>";

                    tbody.insertAdjacentHTML("beforeend", trHtml);
                });

                attachPolicyCheckListeners();
                syncCheckAll();
            }

            function formatDate(dateStr) {
                if (!dateStr) return "";
                var d = new Date(dateStr);
                return d.toLocaleDateString("en-GB"); // dd/MM/yyyy
            }

            function formatDateForAttr(dotNetDate) {
                if (!dotNetDate) return "";

                var ticks = parseInt(dotNetDate.replace(/[^0-9]/g, ""));
                var d = new Date(ticks);

                var yyyy = d.getFullYear();
                var mm = String(d.getMonth() + 1).padStart(2, '0');
                var dd = String(d.getDate()).padStart(2, '0');

                return yyyy + "-" + mm + "-" + dd;
            }

            function formatNumber(num) {
                if (!num) return "0";
                return parseFloat(num).toLocaleString("en-US", {
                    minimumFractionDigits: 0,
                    maximumFractionDigits: 2
                });
            }

            // attach listener untuk policy checkboxes
            function attachPolicyCheckListeners() {
                document.querySelectorAll(".policy-check").forEach(function (cb) {
                    cb.onchange = function () {

                        if (isSelectAllPolicy) {
                            // jika select-all aktif, user uncheck satu policy
                            isSelectAllPolicy = false;
                            document.getElementById("chkAll").checked = false;
                        }

                        if (cb.checked) {
                            selectedPolicies.add(cb.value);
                        } else {
                            selectedPolicies.delete(cb.value);
                        }

                        syncCheckAll();
                    };
                });
            }

            function syncCheckAll() {
                var checks = document.querySelectorAll(".policy-check");
                var chkAll = document.getElementById("chkAll");

                if (!chkAll || checks.length === 0) return;

                chkAll.checked = Array.from(checks).every(cb => cb.checked);
            }

            // toggle all policy in popup
            function toggleAllPolicy(source) {

                isSelectAllPolicy = source.checked;

                if (isSelectAllPolicy) {
                    // jika select all → kita anggap semua policy terpilih
                    selectedPolicies.clear();
                } else {
                    // jika batal select all → kosongkan semua
                    selectedPolicies.clear();
                }

                // hanya update checkbox yg tampil (visual saja)
                document.querySelectorAll(".policy-check").forEach(function (cb) {
                    cb.checked = source.checked;
                });
            }

            // save selected policy and update label count next to remun checkbox
            function savePolicy() {

                var selectedPolicyDetails = [];

                // ==========================================
                // MODE MANUAL (BUKAN SELECT ALL)
                // ==========================================
                if (!isSelectAllPolicy) {

                    document.querySelectorAll(".policy-check:checked").forEach(function (cb) {

                        var tr = cb.closest("tr");

                        selectedPolicyDetails.push({
                            AgentCode: currentAgentCode,
                            RemunType: currentRemunType,
                            PolicyNo: tr.dataset.policy,
                            InsuredName: tr.dataset.insured,
                            Amount: parseFloat(tr.dataset.amount),
                            PolicyStartDate: tr.dataset.start,
                            PolicyEndDate: tr.dataset.end
                        });

                    });
                }

                // ==========================================
                // HITUNG TOTAL TERPILIH (FIX BUG)
                // ==========================================
                var totalSelectedCount = 0;

                if (isSelectAllPolicy) {
                    // 🔥 SELECT ALL = TOTAL DATA SERVER
                    totalSelectedCount = totalPolicyRows;
                } else {
                    totalSelectedCount = selectedPolicyDetails.length;
                }

                if (totalSelectedCount === 0) {
                    alert("Pilih minimal satu POLICY");
                    return;
                }

                // ==========================================
                // SIMPAN KE HIDDEN FIELD
                // ==========================================
                var payload = {
                    IsSelectAll: isSelectAllPolicy,
                    TotalRows: totalPolicyRows,
                    AgentCode: currentAgentCode,
                    RemunType: currentRemunType,
                    Items: selectedPolicyDetails
                };

                document.getElementById("<%= HF_POLICY_DETAIL.ClientID %>").value =
                    JSON.stringify(payload);

                document.getElementById("<%= HF_SELECTED_REMUN_TYPE.ClientID %>").value =
                    selectedRemunTypes.join(",");

                // ==========================================
                // UPDATE LABEL REMUN
                // ==========================================
                updateRemunLabel(currentRemunCheckbox, totalSelectedCount);

                // ==========================================
                // TUTUP POPUP
                // ==========================================
                closePopup(false);
            }

            // close popup. if isCancel then rollback remun checkbox
            function closePopup(isCancel) {
                document.getElementById("pnlHold").style.display = "none";

                if (isCancel && currentRemunCheckbox) {
                    currentRemunCheckbox.checked = false;
                    var lbl = currentRemunCheckbox.closest("label").querySelector(".remun-label");
                    if (lbl) lbl.innerHTML = "";
                    updateRemunLabel(currentRemunCheckbox, 0);
                    clearPolicySelections();
                }
                currentRemunCheckbox = null;
            }

            // ===== select all remun types in one agent (DO NOT open popup) =====
            function selectAllRemunPerAgent(cb) {
                var agent = cb.getAttribute("data-agent");
                var check = cb.checked;

                document.getElementById("<%= HF_IS_SELECT_ALL.ClientID %>").value =
                    check ? "1" : "0";

                var items = document.querySelectorAll(
                    'input.remun-check[data-agent="' + agent + '"]'
                );

                items.forEach(function (item) {
                    item.checked = check;

                    var remun = item.dataset.remun;
                    if (check && !selectedRemunTypes.includes(remun)) {
                        selectedRemunTypes.push(remun);
                    }
                });
            }



            function triggerUploadRow(agentCode) {
                var input = document.getElementById("fileUpload_" + agentCode);
                if (input) input.click();
            }

            function onRemunClick(cb) {

                // 🔥 RESET MODE SELECT ALL
                document.getElementById("<%= HF_IS_SELECT_ALL.ClientID %>").value = "0";

                var remun = cb.dataset.remun;

                if (cb.checked) {

                    if (!selectedRemunTypes.includes(remun)) {
                        selectedRemunTypes.push(remun);
                    }

                    currentRemunCheckbox = cb;
                    currentAgentCode = cb.dataset.agent;
                    currentRemunType = remun;

                    clearPolicySelections();
                    totalPolicyRows = 0;
                    policyPage = 1;

                    loadPolicyFromServer(currentAgentCode, remun, 1);

                } else {
                    selectedRemunTypes = selectedRemunTypes.filter(x => x !== remun);
                    updateRemunLabel(cb, 0);
                }
            }

            function parseDate(dotNetDate) {
                if (!dotNetDate) return "";

                var ticks = parseInt(dotNetDate.replace(/[^0-9]/g, ""));
                var d = new Date(ticks);

                // AMBIL TANGGAL LOKAL (BUKAN UTC)
                var yyyy = d.getFullYear();
                var mm = String(d.getMonth() + 1).padStart(2, '0');
                var dd = String(d.getDate()).padStart(2, '0');

                return yyyy + "-" + mm + "-" + dd;
            }

            function loadPolicyFromServer(agentCode, remunType, page) {

                if (!agentCode || !remunType) {
                    console.warn("AgentCode / RemunType kosong");
                    return;
                }

                if (!page) page = 1;

                currentAgentCode = agentCode;
                currentRemunType = remunType;
                policyPage = page;

                isPolicyPaging = true;

                // contoh endpoint — sesuaikan dengan WebMethod kamu
                fetch("AGENT_HOLD_REMUN.aspx/GetPolicyList", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    body: JSON.stringify({
                        agentCode: agentCode,
                        remunType: remunType,
                        filterDate: document.getElementById("<%= TXT_FILTER_DATE.ClientID %>").value,
                        page: page,
                        pageSize: policyPageSize
                    })
                })
                    .then(res => res.json())
                    .then(res => {

                        if (!res || !res.d) {
                            alert("Gagal mengambil data policy");
                            return;
                        }

                        var data = res.d.Data || [];
                        totalPolicyRows = res.d.TotalRows || 0;

                        var totalPages = Math.ceil(totalPolicyRows / policyPageSize);

                        // render table policy
                        renderPolicyTable(data);

                        // render pager (INI YANG SEBELUMNYA HILANG)
                        renderPolicyPager(policyPage, totalPages);

                        // tampilkan popup
                        var pnl = document.getElementById("pnlHold");
                        if (pnl) pnl.style.display = "block";

                    })
                    .catch(err => {
                        console.error(err);
                        alert("Error load policy");
                    })
                    .finally(() => {
                        setTimeout(function () {
                            isPolicyPaging = false;
                        }, 200);
                    });
            }

            function renderPolicyPager(currentPage, totalPages) {

                var pager = document.getElementById("policyPager");
                if (!pager) return;

                pager.innerHTML = "";

                // tidak perlu pager kalau cuma 1 halaman
                if (!totalPages || totalPages <= 1) return;

                // ===== PREV =====
                if (currentPage > 1) {
                    pager.insertAdjacentHTML(
                        "beforeend",
                        "<a href='javascript:void(0)' class='pager-link' " +
                        "onclick='onPolicyPageClick(" + (currentPage - 1) + ")'>&laquo;</a>"
                    );
                }

                // ===== PAGE NUMBERS =====
                for (var i = 1; i <= totalPages; i++) {

                    if (i === currentPage) {
                        pager.insertAdjacentHTML(
                            "beforeend",
                            "<span class='pager-current'>" + i + "</span>"
                        );
                    } else {
                        pager.insertAdjacentHTML(
                            "beforeend",
                            "<a href='javascript:void(0)' class='pager-link' " +
                            "onclick='onPolicyPageClick(" + i + ")'>" + i + "</a>"
                        );
                    }
                }

                // ===== NEXT =====
                if (currentPage < totalPages) {
                    pager.insertAdjacentHTML(
                        "beforeend",
                        "<a href='javascript:void(0)' class='pager-link' " +
                        "onclick='onPolicyPageClick(" + (currentPage + 1) + ")'>&raquo;</a>"
                    );
                }
            }

            function onPolicyPageClick(page) {

                if (!currentAgentCode || !currentRemunType) return;

                isPolicyPaging = true;

                loadPolicyFromServer(
                    currentAgentCode,
                    currentRemunType,
                    page
                );
            }


            function collectSelectedAgents() {

                var agents = new Set();

                document.querySelectorAll(
                    "input.remun-check:checked"
                ).forEach(function (cb) {
                    agents.add(cb.dataset.agent);
                });

                document.getElementById("<%= HF_SELECTED_AGENT.ClientID %>").value =
                    Array.from(agents).join(",");

                document.getElementById("<%= HF_SELECTED_REMUN_TYPE.ClientID %>").value =
                    selectedRemunTypes.join(",");
            }

        </script>


    </form>
</body>
</html>

