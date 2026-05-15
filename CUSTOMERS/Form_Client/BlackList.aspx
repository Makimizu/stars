<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlackList.aspx.cs" Inherits="CUSTOMERS.Form_Client.BlackList" %>

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
        .fixed-grid th:first-child,
        .fixed-grid td:first-child {
            position: sticky;
            left: 0;
            z-index: 10;
            background-color: #fff; /* solid background */
        }

        .fixed-grid tr:nth-child(even) td:first-child {
            background-color: #E3EAEB;
        }

        .fixed-grid tr:nth-child(odd) td:first-child {
    
            background-color: White;
        }
        .fixed-grid tr:nth-child(1) td:first-child {
            background-color: #1C5E55;
        }
		.fixed-grid tr:last-child td:first-child {
            font-color: black;
        }
        .fixed-grid th {
            top: 0;
            position: sticky;
            z-index: 12;
        }
    </style>
</head>
<body>
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

    function setGridUploadWidth() {
        var screenWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
        screenWidth = screenWidth - 18;
        console.log("Screen width: " + screenWidth);

        document.getElementById('<%= LB_DGRUPLOAD.ClientID %>').style.maxWidth = screenWidth + 'px';
    }
</script>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 33%;" valign="top">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">FULLNAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>GENDER</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_GENDER" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="M">MALE</asp:ListItem>
                                                <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DOB</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DOBSTART" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" TargetControlID="TXT_DOBSTART" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                                <asp:TextBox ID="TXT_DOBTO" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="TXT_REGDATETO_CalendarExtender" runat="server" TargetControlID="TXT_DOBTO" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ID NUMBER</td>
                                        <td>
                                            <asp:TextBox ID="TXT_IDNO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%;" valign="top">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">CITY</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CITY" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>JOB</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_JOB" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PROVINCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PROVINCE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Width="100px" OnClick="BT_SEARCH_Click" Text="SEARCH" />
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
                    <asp:Label ID="LB_INFO" runat="server" ForeColor="Blue"></asp:Label>
                    <asp:Label ID="LB_ERR" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True" EnableViewState="false" ForeColor="Blue"></asp:Label>
                    <asp:Label ID="LB_DGR" runat="server" style="display:block;overflow:auto;">
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="40"
                        OnPageIndexChanged="DGR_PageIndexChanged"
                        AllowPaging="True" AutoGenerateColumns="False" CssClass="ASPDatagrid fixed-grid" GridLines="None" ForeColor="#333333" Width="100%" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" BorderColor="Black" BorderWidth="0" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="FULLNAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ALIAS" HeaderText="ALIAS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GENDER" HeaderText="GENDER">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB" DataFormatString="{0:dd MMM yyyy}">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="POB" HeaderText="POB"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MOTHER_NAME" HeaderText="MOTHER NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="JOB" HeaderText="JOB"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RELIGION" HeaderText="RELIGION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MARITAL_STATUS" HeaderText="MARITAL STATUS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_TYPE" HeaderText="ID TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_NO" HeaderText="ID NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TAX_NO" HeaderText="TAX NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CITIZENSHIP" HeaderText="CITIZENSHIP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EDUCATION" HeaderText="EDUCATION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PHONE_1" HeaderText="PHONE 1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PHONE_2" HeaderText="PHONE 2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ADDRESS_1" HeaderText="ADDRESS 1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ADDRESS_2" HeaderText="ADDRESS 2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CITY" HeaderText="CITY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVINCE" HeaderText="PROVINCE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COUNTRY" HeaderText="COUNTRY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ZIP_CODE" HeaderText="ZIP CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FLAG" HeaderText="FLAG"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SOURCE" HeaderText="SOURCE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" HeaderText="REMARK"></asp:BoundColumn>
                            
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Text="X" ForeColor="White" BackColor="Red" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                            Wrap="False" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                    </asp:Label>
                    
                    <asp:Label ID="LB_DGRUPLOAD" runat="server" style="height:80vh;display:block;overflow:auto;">
                    <asp:DataGrid ID="DGRUPLOAD" runat="server" CellPadding="4" 
                        OnPageIndexChanged="DGRUPLOAD_PageIndexChanged"
                        AutoGenerateColumns="False" CssClass="ASPDatagrid fixed-grid" GridLines="None" ForeColor="#333333" Width="100%">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" BorderColor="Black" BorderWidth="0" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ALIAS" HeaderText="ALIAS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GENDER" HeaderText="GENDER">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB" DataFormatString="{0:dd MMM yyyy}">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="POB" HeaderText="POB"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MOTHER_NAME" HeaderText="MOTHER NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="JOB" HeaderText="JOB"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RELIGION" HeaderText="RELIGION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MARITAL_STATUS" HeaderText="MARITAL STATUS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_TYPE" HeaderText="ID TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_NO" HeaderText="ID NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TAX_NO" HeaderText="TAX NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CITIZENSHIP" HeaderText="CITIZENSHIP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EDUCATION" HeaderText="EDUCATION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PHONE_1" HeaderText="PHONE 1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PHONE_2" HeaderText="PHONE 2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ADDRESS_1" HeaderText="ADDRESS 1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ADDRESS_2" HeaderText="ADDRESS 2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CITY" HeaderText="CITY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVINCE" HeaderText="PROVINCE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COUNTRY" HeaderText="COUNTRY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ZIP_CODE" HeaderText="ZIP CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FLAG" HeaderText="FLAG"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SOURCE" HeaderText="SOURCE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" HeaderText="REMARK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UPLOAD_STATUS" HeaderText="STATUS"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                            Wrap="False" HorizontalAlign="Left" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <%--<PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />--%>
                    </asp:DataGrid>
                    </asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
