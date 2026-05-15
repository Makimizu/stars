<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BebanClaimIndividualRisk.aspx.cs" Inherits="FINANCE.Form_AutoReconcile.BebanClaimIndividualRisk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />
    <script>

        function getFilter() {

            getValidateData();
        }

        function getDownload() {

            getValidateData();
        }

        function getValidateData() {

            var sDateVal = document.getElementById("TXT_STARTDATE").value;
            var eDateVal = document.getElementById("TXT_ENDDATE").value;

            var sDate = document.getElementById("TXT_STARTDATE").value.split(/\//);
            var startDate = [sDate[1], sDate[0], sDate[2]].join('/');

            var eDate = document.getElementById("TXT_ENDDATE").value.split(/\//);
            var endDate = [eDate[1], eDate[0], eDate[2]].join('/');


            if (sDateVal != '' && eDateVal != '') {

                var start = new Date(startDate);
                var end = new Date(endDate);

                var diffDate = (end - start) / (1000 * 60 * 60 * 24);
                var days = Math.round(diffDate) + 1;

                if (parseInt(days) <= 0) {

                    alert('Start Date cannot be greater than End Date!');
                }

                document.getElementById("hdnStartDate").value = startDate;
                document.getElementById("hdnEndDate").value = endDate;

            }
            else if (sDateVal == '' && eDateVal == '') {

                alert('Start Date and End Date cannot be empty!');

                document.getElementById("hdnStartDate").value = '';
                document.getElementById("hdnEndDate").value = '';

            }
            else if (sDateVal == '' && eDateVal != '') {

                alert('Start Date cannot be empty!');

                document.getElementById("hdnStartDate").value = '';
                document.getElementById("hdnEndDate").value = endDate;

            }
            else if (sDateVal != '' && eDateVal == '') {

                alert('End Date cannot be empty!');

                document.getElementById("hdnStartDate").value = startDate;
                document.getElementById("hdnEndDate").value = '';

            }
            else {

                startDate = '';
                endDate = '';

                document.getElementById("hdnStartDate").value = startDate;
                document.getElementById("hdnEndDate").value = endDate;
            }

            // loading 
            var state = document.readyState;
            if (state == 'interactive') {
                ShowProgress();
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('DV_LOADING').style.visibility = "hidden";
                }, 1000);
            }


        }

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

    </script>

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

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <div id="DV_LOADING" class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>

        <asp:Label ID="LB_APP" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>

        <asp:HiddenField ID="hdnStartDate" runat="server" />
        <asp:HiddenField ID="hdnEndDate" runat="server" />
         <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
               <tr id="TR_STL1" runat="server">
                <td>
                    <table style="border-spacing: 0px;">

                        <tr>
                            <td>
                                <asp:Label ID="LBL_STARTDATE" runat="server" Text="START DATE" CssClass="ASPLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;<asp:Label ID="LBL_ENDDATE" runat="server" Text="END DATE" CssClass="ASPLabel"></asp:Label>
                                <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" OnClientClick="getFilter();" Text="SEARCH" />
                                <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" OnClick="BT_DOWNLOAD_Click" OnClientClick="getDownload();" Text="DOWNLOAD" />                        
                             </td>
                        </tr>
                    </table>
                </td>
            </tr>
             <tr id="TR_STL2" runat="server">
                <td>
                    
                    <br />
                    <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                    <br />

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid" 
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False">
                        <ItemStyle Wrap="False" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>

                            <asp:BoundColumn DataField="NO" HeaderText="NO.">
                                 <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="AR_DATE" HeaderText="DATE" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCNO" HeaderText="DOC NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="JOURNAL_TYPE" HeaderText="JOURNAL TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="JOURNAL_TYPE_DESCR" HeaderText="JOURNAL TYPE DESCR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE_DESCR" HeaderText="INVOICE TYPE DESCR"></asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="AMOUNT_JD" HeaderText="AMOUNT JOURNAL DETAIL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>

                             <asp:BoundColumn DataField="AMOUNT_REQPAY" HeaderText="AMOUNT PAYMENT REQUEST">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="AMOUNT_CLAIM_LIFE" HeaderText="AMOUNT CLAIM LIFE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="AMOUNT_CLAIM_RISK" HeaderText="AMOUNT CLAIM RISK">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="ID_JD" HeaderText="ID JOURNAL DETAIL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_REQPAY" HeaderText="ID REQUEST PAYMENT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_CLAIMLIFE" HeaderText="ID CLAIM LIFE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_CLAIMRISK" HeaderText="ID CLAIM RISK"></asp:BoundColumn>

                            <asp:BoundColumn DataField="CODE" HeaderText="CODE"></asp:BoundColumn>

                            <asp:BoundColumn DataField="REMARK" HeaderText="REMARK">
                                    <ItemStyle Font-Bold="True" />
                            </asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="DETAIL SELISIH" ItemStyle-Width="300px" >
                                <ItemTemplate>

                                    <asp:LinkButton ID="LB_DETAIL" runat="server" CssClass="ASPLabel" Text="View" />
                                     
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>


                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        function showLoading() {
            document.getElementById("loading").style.display = "block";
        }

        function hideLoading() {
            document.getElementById("loading").style.display = "none";
        }

        // Menyembunyikan loading setelah halaman selesai dimuat
        window.onload = function () {
            hideLoading();
        };
    </script>
</body>
</html>
