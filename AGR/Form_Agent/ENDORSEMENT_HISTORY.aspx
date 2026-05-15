<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ENDORSEMENT_HISTORY.aspx.cs" Inherits="AGR.Form_Agent.ENDORSEMENT_HISTORY" %>
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
    </script>
    <style type="text/css">
        [aria-current="page"] {
            pointer-events: none;
            cursor: default;
            text-decoration: none;
            color: black;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.6;
            filter: alpha(opacity=80);
            -moz-opacity: 0.6;
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

        .remun-list {
            margin-top: 5px;
        }

        .remun-list label {
            display: block;
            margin: 3px 0;
            cursor: pointer;
        }

        .summary-wrapper {
            max-height: 110px;          /* ±4 baris */
            overflow-y: auto;
            border: 1px solid #4A3C8C;
            margin-top: 6px;
        }

        .summary-table thead th {
            background-color: #4A3C8C;
            color: white;
            font-weight: bold;
            position: sticky;
            top: 0;
            z-index: 2;
        }

        .summary-table td,
        .summary-table th {
            font-size: xx-small;
            padding: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="HF_SELECTED_REMUN" runat="server" />
        <asp:HiddenField ID="HF_SELECTED_POLICY" runat="server" />
        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
         <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label runat="server" ID="LB_TITLE"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>AGENT CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_AGENTCODE" runat="server" CssClass="ASPTextBox" Width="300"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="300"></asp:TextBox><asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList" Enabled="False" Visible="false"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>REMARK</td>
                            <td>
                                <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" Width="100" OnClick="BT_SEARCH_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                        <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="XX-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="AGENT CODE">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COLOR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" HeaderText="REMARK">
                                <ItemStyle Wrap="true" Font-Bold="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#006600" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REQUESTBY" HeaderText="REQUEST BY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="APPROVEBY" HeaderText="APPROVE BY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
                    </asp:DataGrid>


                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server"
                BackColor="White"
                Width="65%"
                Height="520px"
                Style="z-index:111; position:fixed; left:120px; top:30px;
                       border:outset 2px gray; padding:8px; display:none;">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td class="TDBGColor">
                            <asp:Label ID="LB_UNHOLD_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                            <asp:Label ID="LB_AGENTCODE" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td width="20px">
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr>    
                    <%--<tr>
                        <td colspan="2">
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2">
                            <b>REMUN TYPE</b>
                            <div class="remun-list">

                                <label>
                                    <input type="checkbox" id="cbRemunAll" onclick="toggleAllRemun(this)" />
                                    <b>Select All Remun Type</b>
                                </label>

                                <label>
                                    <input type="checkbox" class="cb-remun" data-remun="1"
                                           onclick="onRemunReleaseClick(this)" />
                                    BASIC COMMISSION &amp; OVERRIDING
                                </label>

                                <label>
                                    <input type="checkbox" class="cb-remun" data-remun="2"
                                           onclick="onRemunReleaseClick(this)" />
                                    ALLOWANCE
                                </label>

                                <label>
                                    <input type="checkbox" class="cb-remun" data-remun="2a"
                                           onclick="onRemunReleaseClick(this)" />
                                    BUSINESS ALLOWANCE
                                </label>

                                <label>
                                    <input type="checkbox" class="cb-remun" data-remun="2b"
                                           onclick="onRemunReleaseClick(this)" />
                                    BONUS ROYALTY
                                </label>

                                <label>
                                    <input type="checkbox" class="cb-remun" data-remun="2c"
                                           onclick="onRemunReleaseClick(this)" />
                                    BONUS RECRUITMENT
                                </label>

                                <label>
                                    <input type="checkbox" class="cb-remun" data-remun="2d"
                                           onclick="onRemunReleaseClick(this)" />
                                    BONUS PROMOTION
                                </label>

                            </div>
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            <asp:CheckBox ID="CBAGENT" runat="server" Text="Rekening YBS" onclick="toggleCheckbox(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="CBCOMPANY" runat="server" Text="Rekening Takaful" onclick="toggleCheckbox(this);" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2">
                            <div style="max-height:110px; overflow-y:auto; border:1px solid #ccc;">
                                <table id="tblPolicy" class="ASPDatagrid" style="width:100%">
                                    <thead>
                                        <tr>
                                            <th style="width:40px;text-align:center">
                                                <input type="checkbox" id="chkSelectAllPolicy" />
                                            </th>
                                            <th>POLICY NO</th>
                                            <th>INSURED NAME</th>
                                            <th style="text-align:right">AMOUNT</th>
                                            <th>START DATE</th>
                                            <th>END DATE</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div id="policyPager" style="margin-top:5px;text-align:center;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="CBAGENT" runat="server" Text="Rekening YBS" onclick="toggleRekening(this, 'YBS');" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="CBCOMPANY" runat="server" Text="Rekening Takaful" onclick="toggleRekening(this, 'TAKAFUL');" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="BT_PROCESS"
                            runat="server"
                            Text="Process"
                            CssClass="ASPButton"
                            OnClientClick="return beforeProcessRelease();" />

                            &nbsp;

                            <asp:Button ID="BT_CANCEL" runat="server"
                                Text="Cancel"
                                CssClass="ASPButton"
                                OnClick="BT_CANCEL_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <center>
                                <div class="summary-wrapper">
                                    <table id="tblSummary" class="ASPDatagrid summary-table" style="width:100%">
                                        <thead>
                                            <tr>
                                                <th>START DATE</th>
                                                <th>END DATE</th>
                                                <th style="text-align:right">AMOUNT</th>
                                                <th>HOLD START DATE</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td colspan="4" align="center">No data</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <%--<asp:DataGrid ID="DGR_HOLD" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="30"
                                    GridLines="Horizontal" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%">
                                    <itemstyle wrap="False" verticalalign="Top" backcolor="#E7E7FF" forecolor="#4A3C8C" />
                                    <selecteditemstyle backcolor="#738A9C" forecolor="#F7F7F7" font-bold="True" />
                                    <alternatingitemstyle backcolor="#F7F7F7" verticalalign="Top" />
                                    <itemstyle backcolor="#EEEEEE" forecolor="Black" />
                                    <headerstyle backcolor="#4A3C8C" font-bold="True" forecolor="#F7F7F7" />
                                    <%--<columns>
                                        <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="HOLD_START_DATE" HeaderText="HOLD START DATE"></asp:BoundColumn>
                                    </columns>--%>
                                 <%--   <footerstyle backcolor="#B5C7DE" forecolor="#4A3C8C" />
                                    <pagerstyle backcolor="#E7E7FF" forecolor="#4A3C8C" horizontalalign="Right" mode="NumericPages" />
                                </asp:DataGrid>--%>
                            </center>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <script>
            var currentAgentCode = "";
            var currentRemark = "";
            var selectedRemunTypes = [];

            var policyData = [];          // SEMUA DATA dari server
            var selectedPolicies = {};   // { POLICY_NO : true }
            var pageSize = 50;
            var currentPage = 1;

            window.onload = function () {
                var checks = document.getElementsByClassName('cb-remun');
                for (var i = 0; i < checks.length; i++) {
                    checks[i].checked = false;
                    checks[i].disabled = false;   // ✅ ENABLE
                }

                var cbAll = document.getElementById('cbRemunAll');
                if (cbAll) {
                    cbAll.checked = false;
                    cbAll.disabled = false;      // ✅ ENABLE
                }
            };



            function openPopup(agentCode) {
                if (!agentCode) {
                    alert("Agent belum dipilih!");
                    return;
                }

                currentAgentCode = agentCode;
                selectedRemunTypes = [];

                var checks = document.getElementsByClassName('cb-remun');
                for (var i = 0; i < checks.length; i++) {
                    checks[i].checked = false;
                    checks[i].disabled = false;
                }

                var cbAll = document.getElementById('cbRemunAll');
                if (cbAll) {
                    cbAll.checked = false;
                    cbAll.disabled = false;
                }

                document.getElementById('pnlpopup').style.display = 'block';
            }


            function toggleAllRemun(cb) {
                if (!currentAgentCode) {
                    alert("Agent belum dipilih!");
                    cb.checked = false;
                    return;
                }

                selectedRemunTypes = [];
                var checks = document.getElementsByClassName('cb-remun');

                for (var i = 0; i < checks.length; i++) {
                    checks[i].checked = cb.checked;

                    var remun = checks[i].getAttribute('data-remun');
                    if (cb.checked && remun) {
                        selectedRemunTypes.push(remun);
                    }
                }

                loadPolicyRelease();

                refreshHoldSummary();
            }

            function onRemunReleaseClick(cb) {
                if (!currentAgentCode) {
                    alert("Agent belum dipilih!");
                    cb.checked = false;
                    return;
                }

                var remun = cb.getAttribute('data-remun');
                if (!remun) return;

                if (cb.checked) {
                    if (selectedRemunTypes.indexOf(remun) === -1) {
                        selectedRemunTypes.push(remun);
                    }
                } else {
                    selectedRemunTypes = selectedRemunTypes.filter(x => x !== remun);

                    // uncheck Select All
                    var cbAll = document.getElementById('cbRemunAll');
                    if (cbAll) cbAll.checked = false;
                }

                loadPolicyRelease();

                refreshHoldSummary();
            }

            function loadPolicyRelease() {
                if (!currentAgentCode) return;

                var remunTypesToSend = selectedRemunTypes.length > 0 ? selectedRemunTypes : ['0'];

                $.ajax({
                    type: "POST",
                    url: "ENDORSEMENT_HISTORY.aspx/GetReleasePolicy",
                    data: JSON.stringify({
                        agentCode: currentAgentCode,
                        remunTypes: remunTypesToSend
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        policyData = res.d || [];
                        currentPage = 1;
                        renderPolicyPage();
                        renderPager();
                    }
                });
            }

            function renderPolicyPage() {
                var tbody = "";
                var start = (currentPage - 1) * pageSize;
                var end = Math.min(start + pageSize, policyData.length);

                for (var i = start; i < end; i++) {
                    var p = policyData[i];

                    var isChecked = selectedPolicies[p.PolicyNo] ? "checked" : "";

                    tbody += `
                    <tr>
                        <td align="center">
                            <input type="checkbox"
                                   class="chkPolicy"
                                   data-policy="${p.PolicyNo}"
                                   data-start="${p.StartDate}"
                                   data-end="${p.EndDate || ''}"
                                   data-remun="${p.RemunType}"
                                   ${isChecked} />
                        </td>
                        <td>${p.PolicyNo}</td>
                        <td>${p.InsuredName}</td>
                        <td align="right">${Number(p.Amount).toLocaleString()}</td>
                        <td>${p.StartDate}</td>
                        <td>${p.EndDate || ''}</td>
                    </tr>`;
                            }

                $("#tblPolicy tbody").html(tbody);

                // ✅ Sinkronisasi checkbox header (Select All Policy)
                $("#chkSelectAllPolicy").prop(
                    "checked",
                    policyData.length > 0 &&
                    Object.keys(selectedPolicies).length === policyData.length
                );
            }

            function renderPager() {
                var totalPage = Math.ceil(policyData.length / pageSize);

                if (totalPage <= 1) {
                    $("#policyPager").html("");
                    return;
                }

                var html = "";
                for (var i = 1; i <= totalPage; i++) {
                    html += `<a href="#" onclick="gotoPage(${i});return false;"
                 style="margin:3px; ${i === currentPage ? 'font-weight:bold' : ''}">
                 ${i}</a>`;
                }
                $("#policyPager").html(html);
            }

            function gotoPage(p) {
                currentPage = p;
                renderPolicyPage();
            }

            function syncSelectAllPolicyCheckbox() {
                var totalPolicy = policyData.length;
                var totalChecked = Object.keys(selectedPolicies).length;

                $("#chkSelectAllPolicy").prop(
                    "checked",
                    totalPolicy > 0 && totalChecked === totalPolicy
                );
            }

            $(document).on("change", ".chkPolicy", function () {
                var policyNo = $(this).data("policy");

                if (this.checked) {
                    selectedPolicies[policyNo] = {
                        policyNo: policyNo,
                        startDate: $(this).data("start"),
                        endDate: $(this).data("end"),
                        remunType: $(this).data("remun")
                    };
                } else {
                    delete selectedPolicies[policyNo];
                }

                $("#chkSelectAllPolicy").prop(
                    "checked",
                    Object.keys(selectedPolicies).length === policyData.length
                    && policyData.length > 0
                );

                refreshHoldSummary();
            });

            $(document).on("click", "#chkSelectAllPolicy", function (e) {

                var checked = this.checked;

                if (checked) {
                    // SELECT ALL GLOBAL
                    for (var i = 0; i < policyData.length; i++) {
                        selectedPolicies[policyData[i].PolicyNo] = true;
                    }
                } else {
                    // UNSELECT ALL GLOBAL
                    selectedPolicies = {};
                }

                // UPDATE CHECKBOX YANG TERLIHAT SAJA
                $(".chkPolicy").each(function () {
                    $(this).prop("checked", checked);
                });

                refreshHoldSummary();
            });

            function getSelectedPolicyNos() {
                return Object.keys(selectedPolicies);
            }

            function beforeProcessRelease() {

                var selected = [];

                for (var key in selectedPolicies) {
                    if (!selectedPolicies.hasOwnProperty(key)) continue;

                    var p = policyData.find(x => x.PolicyNo === key);
                    if (!p) continue;

                    selected.push({
                        PolicyNo: p.PolicyNo,
                        StartDate: p.StartDate,
                        EndDate: p.EndDate,
                        RemunType: p.RemunType
                    });
                }

                //if (selected.length === 0) {
                //    alert("Pilih minimal 1 policy terlebih dahulu");
                //    return false; // ❌ stop postback
                //}

                $("#<%= HF_SELECTED_POLICY.ClientID %>").val(
                    JSON.stringify(selected)
                );

                return true; // ✅ lanjut ke BT_PROCESS_Click
            }

            function refreshHoldSummary() {

                var policyNos = Object.keys(selectedPolicies);

                var useSpecialQuery =
                    currentRemark &&
                    (
                        currentRemark.indexOf("NPWP Pending") >= 0 ||
                        currentRemark.indexOf("TERMINATE") >= 0 ||
                        currentRemark.indexOf("Licence Expired") >= 0
                    );

                $.ajax({
                    type: "POST",
                    url: "ENDORSEMENT_HISTORY.aspx/GetHoldSummary",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        agentCode: currentAgentCode,
                        policyNos: policyNos,
                        remunTypes: selectedRemunTypes,
                        useSpecialQuery: useSpecialQuery
                    }),
                    success: function (res) {
                        renderHoldSummary(res.d || []);
                    }
                });
            }

            function renderHoldSummary(data) {

                var html = "";

                if (data.length === 0) {
                    html = "<tr><td colspan='4' align='center'>No data</td></tr>";
                } else {
                    data.forEach(function (r) {
                        html += `
                        <tr>
                            <td>${r.StartDate}</td>
                            <td>${r.EndDate}</td>
                            <td align="right">${Number(r.Amount).toLocaleString()}</td>
                            <td>${r.HoldStartDate}</td>
                        </tr>
                    `;
                    });
                }

                $("#tblSummary tbody").html(html);
            }

            function toggleRekening(cb, type) {

                var cbYbs = document.getElementById("<%= CBAGENT.ClientID %>");
                var cbTakaful = document.getElementById("<%= CBCOMPANY.ClientID %>");

                if (type === 'YBS' && cb.checked) {
                    cbTakaful.checked = false;
                }

                if (type === 'TAKAFUL' && cb.checked) {
                    cbYbs.checked = false;
                }
            }
        </script>
    </form>
</body>
</html>