<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_SCREENING.aspx.cs" Inherits="AGR.Form_Agent.AGENT_SCREENING" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <style>
        .hidden {
            display: none;
        }
        .min-width-60 {
            min-width: 60px;
        }
        .min-width-100 {
            min-width: 100px;
        }
        .fixed-grid th:first-child,
        .fixed-grid td:first-child {
            position: sticky;
            left: 0;
            z-index: 10;
            background-color: #fff; /* solid background */
        }
        
        .fixed-grid tr:nth-child(even) td:first-child {
            background-color: #EEEEEE;
        }

        .fixed-grid tr:nth-child(odd) td:first-child {
            
            background-color: White;
        }
        .fixed-grid tr:nth-child(1) td:first-child {
            background-color: #507CD1;
        }
        .fixed-grid tr:last-child td:first-child {
            background-color: #507CD1;
        }
        .fixed-grid th {
            top: 0;
            position: sticky;
            z-index: 12;
        }
    </style>
</head>
<body style="width: 100%;">
<script type="text/javascript">
    var dotInterval;

    function startDotLoading() {
        var label = document.getElementById('<%= LB_LOADING.ClientID %>');
        var dots = 0;

        dotInterval = setInterval(function () {
            dots = (dots + 1) % 4; // 0 to 3 dots
            label.innerText = 'Proses upload sedang berlangsung' + '.'.repeat(dots);
        }, 500);
    }

    function showLoading() {
        var label = document.getElementById('<%= LB_LOADING.ClientID %>');
        if (label) {
            label.classList.remove('hidden');
        }
    }
    function hideLoading() {
        var label = document.getElementById('<%= LB_LOADING.ClientID %>');
        if (label) {
            label.classList.add('hidden');
        }
    }

    function setGridWidth() {
        var screenWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
        screenWidth = screenWidth - 18;
        console.log("Screen width: " + screenWidth);

        document.getElementById('<%= LB_DGR.ClientID %>').style.maxWidth = screenWidth + 'px';
    }
    
</script>
    <form id="form1" runat="server">
        <%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">AGENT NAME</td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">DOB RANGE</td>
                                        <td style="width: 1%;">
                                            <asp:TextBox ID="TXT_DATE_START" runat="server" Width="80px" Style="text-align: center;" Font-Size="XX-Small"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE_START">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_DATE_END" runat="server" Width="80px" Style="text-align: center;" Font-Size="XX-Small"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE_END">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">FLAG</td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="DDL_FLAG" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="grey">GREY</asp:ListItem>
                                                <asp:ListItem Value="black">BLACK</asp:ListItem>
                                                <asp:ListItem Value="clear">CLEAR</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BT_SHOW" runat="server" CssClass="ASPButton" Text="TAMPIL SEMUA" OnClick="BT_SHOW_Click" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                           
                            <td style="width:33%" valign="top">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">DOWNLOAD TEMPLATE</td>
                                        <td>
                                            <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" Font-Bold="True" ForeColor="White" BackColor="Green" Visible="True" OnClick="BT_XLS_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>UPLOAD EXCEL FILE(.XLS, .CSV, .XLSX)</td>
                                        <td>
                                            <asp:FileUpload ID="FU" runat="server" CssClass="ASPTextBox" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" Width="100px" OnClick="BT_UPLOAD_Click" OnClientClick="showLoading();startDotLoading();" Text="Upload" EnableViewState="true" />
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
                    <asp:Label ID="LB_LOADING" runat="server" CssClass="hidden" Font-Size="Large"></asp:Label>
                    
                    <asp:Label ID="LB_ERR" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True" EnableViewState="false" ForeColor="Blue"></asp:Label>
                    <asp:Label ID="LB_DGR" runat="server" style="display:block;overflow:auto;">
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="2" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid fixed-grid"
                            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Xx-Small" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundColumn DataField="AGENTNAME" HeaderText="AGENT NAME" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DOB" HeaderText="DoB" ItemStyle-CssClass="min-width-60"  DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                                <asp:BoundColumn DataField="POB" HeaderText="PoB" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="GENDER" HeaderText="GENDER" ItemStyle-CssClass="min-width-60"></asp:BoundColumn>
                                <asp:BoundColumn DataField="JOB" HeaderText="JOB" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RELIGION" HeaderText="RELIGION" ItemStyle-CssClass="min-width-60"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MARITALSTATUS" HeaderText="MARITAL STATUS" ItemStyle-CssClass="min-width-60"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IDTYPE" HeaderText="ID TYPE" ItemStyle-CssClass="min-width-60"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IDNO" HeaderText="ID NO" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TAXNO" HeaderText="TAX NO" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CITIZENSHIP" HeaderText="CITIZENSHIP" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PHONE" HeaderText="PHONE" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ADDRESS" HeaderText="ADDRESS" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CITY" HeaderText="CITY" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROVINCE" HeaderText="PROVINCE" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COUNTRY" HeaderText="COUNTRY" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ZIPCODE" HeaderText="ZIPCODE" ItemStyle-CssClass="min-width-60"></asp:BoundColumn>
                                <asp:BoundColumn DataField="EDUCATION" HeaderText="EDUCATION" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FLAG" HeaderText="FLAG" ItemStyle-CssClass="min-width-60"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SOURCE" HeaderText="SOURCE" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                                <asp:BoundColumn DataField="REMARK" HeaderText="REMARK" ItemStyle-CssClass="min-width-100"></asp:BoundColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#2461BF" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:Label>
                    
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="300px" ProcessingMode="Local" />
                </td>
            </tr>
            <tr>
                <td>
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                
                                <tr>
                                    <td style="width: 150px;"><asp:Label ID="LB_EXPORT" runat="server" Visible="false">EXPORT AS</asp:Label></td>
                                    <td style="width: 1%;">
                                        <asp:DropDownList ID="DDL_FILETYPE" runat="server" CssClass="ASPDropDownList" Visible="false">
                                            
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD" OnClick="BT_DOWNLOAD_Click" Width="100px" Visible="false"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
       
                    </tr>
                </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
