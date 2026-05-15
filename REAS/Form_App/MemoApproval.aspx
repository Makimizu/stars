<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemoApproval.aspx.cs" Inherits="REAS.Form_App.MemoApproval" %>
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

    .modal {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 9999;
    background-color: white;
    border: 1px solid #333;
    padding: 20px;
    display: none;
    max-width: 80%;
    max-height: 80%;
    overflow: auto;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
    <div id="modal" class="modal" style="display:none;">
    <div class="modal-content">
        <span class="close" onclick="closeModal()">&times;</span>

        <!-- Error/Sukses Label -->
        <div style="margin-bottom: 20px">
            <asp:Label ID="LBL_PROCESS" CssClass="lbl-process" runat="server" Font-Bold="True" TextMode="MultiLine"></asp:Label>
            <asp:Label ID="LBL_PATH_FILE" runat="server" Visible="false"></asp:Label>
        </div>

        <!-- Tombol-Tombol untuk Download, Process, Cancel -->
        <div class="modal-buttons">
            <asp:Button ID="btnDownload" runat="server" CssClass="btn-download" Text="Download" OnClick="btnDownload_Click" />
        </div>

        <!-- Grid untuk Menampilkan Data Upload -->
        <div>
            <asp:GridView ID="GV_UploadedData" runat="server" AutoGenerateColumns="False" CssClass="table" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" CellSpacing="2" Style="width: 100%">
                <Columns>
                    <asp:BoundField DataField="MEMO_TYPE" HeaderText="TYPE" SortExpression="TYPE" />
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                    <asp:BoundField DataField="NO_POLIS" HeaderText="NO POLIS" SortExpression="NO POLIS" />
                </Columns>
            </asp:GridView>
        </div>

        
    </div>
</div>
        <div id="loading" style="display:none; position:fixed; top:50%; left:50%; transform:translate(-50%, -50%); z-index:9999; background-color: rgba(255, 255, 255, 0.8); padding: 20px; border-radius: 10px; text-align: center;">
            <img src="../Content/loading.gif" alt="Loading..." style="border: 0; width: 100px;" />
            <p>Loading...</p>
        </div>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">REAS NAME</td>
                                        <td>
                                             <asp:DropDownList 
                                                ID="DDL_REAS" 
                                                runat="server" 
                                                AutoPostBack="false" 
                                                CssClass="ASPDropDownList" 
                                                OnSelectedIndexChanged="DDL_REAS_SelectedIndexChanged"  
                                                Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">NO MEMO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NO_MEMO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
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
                                        <td>STATUS MEMO</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS" runat="server" AutoPostBack="true" CssClass="ASPDropDownList" 
                                                OnSelectedIndexChanged="DDL_STATUS_SelectedIndexChanged"  Width="200px">
                                                <asp:ListItem Value="" ></asp:ListItem>
                                                <asp:ListItem Value="0">ON PROGRESS</asp:ListItem>
                                                <asp:ListItem Value="1">APPROVED</asp:ListItem>
                                                <asp:ListItem Value="2">CANCELED</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="LBL_STATUS" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">AR DATE</td>
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
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" OnClientClick="showLoading();"/>
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
                    <asp:Label ID="LB_DOWNLOAD" runat="server"></asp:Label>
                    <asp:Label ID="LB_RESULT" runat="server" Font-Size="11px" Font-Bold="false"></asp:Label>
                   
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical"
                        Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" 
                        Font-Size="11px"
                        PageSize="200" >
                        
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" Wrap="false"  Font-Size="13px"/>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />

                        <Columns>
                            <asp:TemplateColumn HeaderText="">
                            <ItemTemplate>
                                <asp:Button ID="btnAction" runat="server" Text="APPROVE" OnCommand="btnAction_Command" CommandName="Approved" CommandArgument='<%# Eval("NO_MEMO") %>' Width="70px" Font-Size="10px" Enabled='<%# Eval("IS_STATUS_MEMO").ToString() == "ON PROGRESS" %>'/>
                                <asp:Button ID="btnReject" runat="server" Text="REJECT" OnCommand="btnReject_Command" CommandName="Canceled" CommandArgument='<%# Eval("NO_MEMO") %>' Width="70px" Font-Size="10px" Enabled='<%# Eval("IS_STATUS_MEMO").ToString() == "ON PROGRESS" %>'/>
                                <asp:Button ID="btnDownload" runat="server" Text="DOWNLOAD" OnCommand="btnDownload_Command" CommandName="Download" CommandArgument='<%# Eval("NO_MEMO") %>' Width="70px" Font-Size="10px"  Enabled='<%# Eval("IS_STATUS_MEMO").ToString() == "APPROVED" %>'/>
                            
                            </ItemTemplate>
                           
                        </asp:TemplateColumn>
                            <asp:BoundColumn DataField="MEMO_DATE" HeaderText="MEMO DATE" DataFormatString="{0:dd MMM yyyy}"  />
                            <asp:BoundColumn DataField="NO_MEMO" HeaderText="NO MEMO">
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="IS_STATUS_MEMO" HeaderText="STATUS MEMO"/>
                            <asp:BoundColumn DataField="REAS_NAME" HeaderText="REAS NAME" />
                            <asp:BoundColumn DataField="MEMO_TYPE" HeaderText="MEMO TYPE" />
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT" />
                            <asp:BoundColumn DataField="ACTUAL_AMOUNT" HeaderText="ACTUAL AMOUNT" />
                            <asp:TemplateColumn HeaderText="VIEW DETAIL">
                            <ItemTemplate>
                                <asp:Button ID="btnView" runat="server" Text="VIEW" OnCommand="btnView_Command" CommandName="View" CommandArgument='<%# Eval("NO_MEMO") %>' Width="70px" Font-Size="10px" />
                            </ItemTemplate>
                           
                        </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <div style="margin-top:6px;text-align:left;">
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



            <tr>
<%--                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" OnClientClick="showLoading();"/>
                </td>--%>
                <td>
                    <asp:DataGrid ID="DGDetailMemo" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical"
                        Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" Visible="true"
                        Font-Size="11px" PageSize="200">

                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" Wrap="false" Font-Size="13px"/>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />

                        <Columns>
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

                        <%--<EmptyDataTemplate>
                            <table style="width:100%; border:1px solid #4A3C8C;">
                                <tr style="background-color:#4A3C8C; color:#fff;">
                                    <th>MEMO DATE</th>
                                    <th>NO MEMO</th>
                                    <th>STATUS MEMO</th>
                                    <th>REAS NAME</th>
                                    <th>MEMO TYPE</th>
                                    <th>AMOUNT</th>
                                    <th>ACTUAL AMOUNT</th>
                                    <th>VIEW DETAIL</th>
                                </tr>
                                <tr>
                                    <td colspan="8" style="text-align:center; padding:10px;">No data available</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>--%>

                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        function openModal() {
            document.getElementById("modal").style.display = "block";
        }

        function closeModal() {
            document.getElementById("modal").style.display = "none";
        }

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

        function openModalWithData(noMemo) {
            // Tampilkan modal
            document.getElementById("modal").style.display = "block";

            // Opsional: isi GridView/GV_UploadedData dengan AJAX / JSON
            // Misal: panggil WebMethod atau API untuk ambil data berdasarkan noMemo
        }
    </script>
</body>
</html>
