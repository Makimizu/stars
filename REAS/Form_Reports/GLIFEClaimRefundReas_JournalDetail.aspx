<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GLIFEClaimRefundReas_JournalDetail.aspx.cs" Inherits="REAS.Form_Reports.GLIFEClaimRefundReas_JournalDetail" %>
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

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

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
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
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
                                            <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD" OnClick="BT_DOWNLOAD_Click" Width="100px" BackColor="Aqua"  OnClientClick="showLoading();"/>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_JOURNAL" runat="server" CssClass="ASPButton" Text="DRAFT JOURNAL" BackColor="Yellow" OnClick="BT_JOURNAL_Click" Width="100px" OnClientClick="showLoading();" Visible="false"/>
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
                    <br />
                    <asp:Label ID="LB_RESULT2" runat="server" Font-Size="11px" Font-Bold="false"></asp:Label>

                   
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
                            <asp:BoundColumn DataField="NOTE" HeaderText="TIPE" />

                            <asp:BoundColumn DataField="PERIOD" HeaderText="PERIOD" />
                            <asp:BoundColumn DataField="JOURNAL_DATE" HeaderText="JOURNAL DATE" DataFormatString="{0:dd MMM yyyy}"/>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO" FooterStyle-Font-Bold="true">
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REAS_NAME" HeaderText="REAS NAME" />
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION" />

                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT CLAIM/REFUND" >
                                 <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="T00" HeaderText="T00" />
                            <asp:BoundColumn DataField="T01" HeaderText="T01" />
                            <asp:BoundColumn DataField="T02" HeaderText="T02" />
                            <asp:BoundColumn DataField="T03" HeaderText="T03" />
                            <asp:BoundColumn DataField="T04" HeaderText="T04" />
                            <asp:BoundColumn DataField="T05" HeaderText="T05" />

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
    </script>
</body>
</html>
