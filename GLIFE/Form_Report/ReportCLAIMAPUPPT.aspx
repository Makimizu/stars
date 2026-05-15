<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportCLAIMAPUPPT.aspx.cs" Inherits="GLIFE.Form_Report.ReportCLAIMAPUPPT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    
    <style type="text/css">
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">APPLICATION NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_REGNO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>MAIN INSURED</td>
                                        <td>
                                            <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>POLICY STATUS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PRODUCT" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT GROUP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PRODUCT_GROUP" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CLAIM DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE1">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_STARTDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" Width="100px" OnClick="BT_SEARCH_Click" OnClientClick="ShowProgress()" />
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
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="NO POLIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PEMEGANG POLIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REGNO" HeaderText="REGNO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="NAMA PESERTA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT" HeaderText="PRODUK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GENDER" HeaderText="GENDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAIM_DATE" HeaderText="TGL. TRANSAKSI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUMINS" HeaderText="SUM INSURED"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INCURRED_AMOUNT" HeaderText="INCURRED AMOUNT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOBE_PAID" HeaderText="TO BE PAID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FLAG" HeaderText="STATUS BLACKLIST"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRANSACTION_TYPE" HeaderText="JENIS TRANSAKSI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT" HeaderText="AGENT"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="BT_EXCEL" runat="server" CssClass="ASPButton" Text="EXPORT EXCEL" Visible="false" OnClick="BT_EXCEL_Click" /></td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                modal.attr('id', 'progressModal'); // Add ID for easy removal
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

        $(document).on("click", '#<%= BT_EXCEL.ClientID %>', function () {
            setTimeout(function () {
                var el = document.getElementsByClassName('loading')[0]

                if (el) {
                    el.style.display = 'none'
                }

                var progress = document.getElementById('progressModal')

                if (progress) {
                    progress.remove();
                }

                console.log(progress)
            }, 1000);

            
        });
    </script>
</body>
</html>
