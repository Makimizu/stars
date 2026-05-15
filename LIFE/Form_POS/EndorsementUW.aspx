<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementUW.aspx.cs" Inherits="LIFE.Form_POS.EndorsementUW" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script>
        function resizeIframe(obj) {
            obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
        }
    </script>
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

        document.onload = function () {
            var state = document.readyState
            if (state == 'interactive') {
                ShowProgress();
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('DV_LOADING').style.visibility = "hidden";
                }, 1000);
            }
        }

        function hourglass() {
            document.body.style.cursor = "wait";
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
        <div id="DV_LOADING" class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:Button ID="BT_SUBMISSION" runat="server" CssClass="ASPButton" Text="SUBMISSION INFO" Width="100%" OnClick="BT_SUBMISSION_Click" />
                    <asp:Button ID="BT_OTHERPOLICY" runat="server" CssClass="ASPButton" Text="OTHER POLICY" Width="100%" OnClick="BT_OTHERPOLICY_Click" />
                    <asp:Button ID="BT_CLAIM" runat="server" CssClass="ASPButton" Text="CLAIM HISTORY" Width="100%" OnClick="BT_CLAIM_Click" />
                    <asp:Button ID="BT_DISEASE" runat="server" CssClass="ASPButton" Text="DISEASE HISTORY" Width="100%" OnClick="BT_DISEASE_Click" />
                    <asp:Button ID="BT_MEDICALDOC" runat="server" CssClass="ASPButton" Text="MEDICAL DOCUMENT" Width="100%" OnClick="BT_MEDICALDOC_Click" />
                    <asp:Button ID="BT_EXCLUSION" runat="server" CssClass="ASPButton" Text="DIAGNOSE EXCLUSION" Width="100%" OnClick="BT_EXCLUSION_Click" />
                    <asp:Button ID="BT_EXTRAPREMIUM" runat="server" CssClass="ASPButton" Text="EXTRA PREMIUM" Width="100%" OnClick="BT_EXTRAPREMIUM_Click" BackColor="Red" ForeColor="White" Font-Bold="true" />
                    <asp:Button ID="BT_PAYMENT" runat="server" CssClass="ASPButton" Text="PAYMENT STATUS" Width="100%" OnClick="BT_PAYMENT_Click" />
                    <asp:DataGrid ID="DGR_QUOTATION" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" ShowHeader="False" OnItemCommand="DGR_QUOTATION_ItemCommand">
                        <HeaderStyle Font-Size="XX-Small" />
                        <ItemStyle VerticalAlign="Top" Font-Size="XX-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_QUOTATION" runat="server" CssClass="ASPButton" Width="100%" CommandName="Quotation" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <br />
                    <br />


                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
                        </tr>
                    </table>

                    <table id="TBL_SUBMISSION" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr style="vertical-align: top;">
                                        <td style="width: 30%;">
                                            <asp:DataGrid ID="DGR_UW_INFO" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="95%">
                                                <ItemStyle VerticalAlign="Top" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="DESCR">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="VAL">
                                                        <ItemStyle ForeColor="Blue" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                        <td style="width: 70%;">
                                            <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="95%">
                                                <ItemStyle VerticalAlign="Top" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR">
                                                        <ItemStyle Width="300px" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="150px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            <asp:Button ID="BT_ITEM_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_ITEM_SAVE_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:DataGrid ID="DGR_BLACKLIST" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="95%">
                                                <ItemStyle VerticalAlign="Top" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="DESCR">
                                                        <ItemStyle Width="100px" Font-Bold="true" ForeColor="Red" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_OTHERPOLICY" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td class="TDBGColor" style="width: 50%;">SUM INSURED ACCUMULATION</td>
                                        <td class="TDBGColor" style="width: 50%;">OTHER INSURANCE POLICY</td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                            <asp:DataGrid ID="DGR_OTHERPOLICY" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" ForeColor="#333333" OnItemDataBound="DGR_OTHERPOLICY_ItemDataBound" ShowFooter="True">
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Font-Size="XX-Small" />
                                                <ItemStyle BackColor="#E3EAEB" VerticalAlign="Top" Font-Size="XX-Small" />
                                                <EditItemStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="Gainsboro" ForeColor="Black" Font-Size="XX-Small" Font-Bold="true" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="REGNO" HeaderText="POLICY NO"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PRODUCT_NAME" HeaderText="PRODUCT NAME"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PERIOD" HeaderText="PERIOD"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SUMINS" HeaderText="SUM INSURED">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            <br />
                                            <br />
                                        </td>
                                        <td>
                                            <asp:DataGrid ID="DGR_OTHERINS" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" ForeColor="#333333">
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Font-Size="XX-Small" />
                                                <ItemStyle BackColor="#E3EAEB" VerticalAlign="Top" Font-Size="XX-Small" />
                                                <EditItemStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="INSURANCE NAME"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDBGColor" style="width: 50%;">BENEFIT ACCUMULATION</td>
                                        <td></td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                            <asp:DataGrid ID="DGR_BENEFIT" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" ForeColor="#333333" ShowHeader="False">
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Font-Size="XX-Small" />
                                                <ItemStyle BackColor="#E3EAEB" VerticalAlign="Top" Font-Size="XX-Small" />
                                                <EditItemStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="BENEFIT_DESCR"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ACC_SUMINS">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_CLAIM" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_CLAIM" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Names="Tahoma" Font-Size="XX-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" ForeColor="#333333">
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Font-Size="XX-Small" VerticalAlign="Top" />
                                    <ItemStyle BackColor="#E3EAEB" VerticalAlign="Top" Font-Size="XX-Small" />
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CLAIMNO" HeaderText="CLAIM NO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CLAIM_DATE" HeaderText="OCCURED DATE">
                                            <HeaderStyle Width="60" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TOTAL" HeaderText="AMOUNT">
                                            <HeaderStyle HorizontalAlign="Right" Width="80" />
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CLAIM_TYPE_DESCR" HeaderText="BENEFIT">
                                            <ItemStyle ForeColor="Green" Wrap="true" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DIAGNOSIS" HeaderText="DIAGNOSIS">
                                            <ItemStyle ForeColor="Black" Wrap="true" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CLAIM_STATUS" HeaderText="STATUS"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_DISEASE" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_DISEASE" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" ForeColor="#333333">
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Font-Size="XX-Small" />
                                    <ItemStyle BackColor="#E3EAEB" VerticalAlign="Top" Font-Size="XX-Small" />
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CASES" HeaderText="CASES">
                                            <ItemStyle Width="50%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="REMARK" HeaderText="REMARK"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_MEDICAL_HISTORY" runat="server" Text="FAMILY MEDICAL HISTORY"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_MEDICAL_HISTORY" runat="server" AutoGenerateColumns="False" CellPadding="3" Font-Names="Tahoma" Font-Size="8pt" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" CssClass="ASPDatagrid" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" VerticalAlign="top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <ItemStyle BackColor="#E7E7FF" VerticalAlign="Top" ForeColor="#4A3C8C" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RELATION_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RELATION" HeaderText="RELATION"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ALIVE" HeaderText="STATUS"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AGE" HeaderText="AGE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="HEALTH_CONDITION" HeaderText="HEALTH<br/>CONDITION"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AGE_AT_DEATH" HeaderText="AGE AT<br/>DEATH"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="YEAR_OF_DEATH" HeaderText="YEAR OF<br/>DEATH"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CAUSE_OF_DEATH" HeaderText="CAUSE OF<br/>DEATH"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>


                    <table id="TBL_EXTRAPREMIUM" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" CssClass="ASPDatagrid" Width="100%" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="Green" Font-Bold="true" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TC_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BENEFIT_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TC_NAME" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BENEFIT_DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MEMBER_NAME" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="END_DATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SUMINS" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EP_RATE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EM_RATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PREMIUM_AMOUNT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EP_AMOUNT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EM_AMOUNT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EP_REMARK" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EM_REMARK" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EP_GROSSUP_FACTOR" Visible="False"></asp:BoundColumn>


                                        <asp:TemplateColumn HeaderText="ITEM">
                                            <ItemStyle Width="250" />
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr>
                                                        <td style="width: 80px;">BENEFIT</td>
                                                        <td>
                                                            <asp:Label ID="LB_BENEFIT" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>SUM INSURED</td>
                                                        <td>
                                                            <asp:Label ID="LB_SUMINS" runat="server" ForeColor="Blue"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>INSURED NAME</td>
                                                        <td>
                                                            <asp:Label ID="LB_INSURED" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>END DATE</td>
                                                        <td>
                                                            <asp:Label ID="LB_ENDDATE" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="PREMIUM">
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                                    <tr>
                                                        <td style="width: 30px;">BASIC</td>
                                                        <td style="width: 20px; text-align: center;">:</td>
                                                        <td style="text-align: right; width: 150px;">
                                                            <asp:Label ID="LB_SUMINS1" runat="server"></asp:Label>
                                                            &nbsp;x&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_RATE" runat="server" Width="30px" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30px; text-align: right;">&permil;&nbsp;=&nbsp;</td>
                                                        <td style="width: 60px;">
                                                            <asp:Label ID="LB_PREMIUM1" runat="server" ForeColor="Green"></asp:Label></td>
                                                        <td style="width: 400px;"></td>
                                                    </tr>
                                                    <tr style="vertical-align: top;">
                                                        <td>EP</td>
                                                        <td style="width: 20px; text-align: center;">:</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="LB_SUMINS2" runat="server"></asp:Label>
                                                            &nbsp;x&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_EP_RATE" runat="server" Width="30px" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30px; text-align: right;">&permil;&nbsp;=&nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="LB_PREMIUM2" runat="server" ForeColor="Green"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_EP_REMARK" runat="server" Width="100%" CssClass="ASPTextBox" MaxLength="255" BackColor="Gainsboro" placeholder="Extra Premium Remark ..."></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>EM</td>
                                                        <td style="width: 20px; text-align: center;">:</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="LB_BASICPREMIUM" runat="server"></asp:Label>
                                                            &nbsp;x&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_EM_RATE" runat="server" Width="30px" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30px; text-align: right;">%&nbsp;=&nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="LB_PREMIUM3" runat="server" ForeColor="Green"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_EM_REMARK" runat="server" Width="100%" CssClass="ASPTextBox" MaxLength="255" BackColor="Gainsboro" placeholder="Extra Mortality Remark ..."></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="TOTAL_AMOUNT" HeaderText="TOTAL AMOUNT">
                                            <HeaderStyle HorizontalAlign="Right" Width="80" />
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Green" Font-Bold="true" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE PREMIUM" Width="150px" OnClick="BT_SAVE_Click" OnClientClick="ShowProgress();" />
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_MEDICALDOC" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <iframe style="width: 99%; height: 90vh; overflow: auto;" src="../Form_App/ApplicationDocument.aspx?ID=<%=Request.QueryString["REGNO"]%>"></iframe>
                            </td>
                        </tr>
                    </table>


                    <table id="TBL_EXCLUSION" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%;" class="TDBGColor">AVAILABLE ITEMS</td>
                            <td style="width: 50%;" class="TDBGColor">EXCLUDED ITEMS</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:TextBox ID="TXT_ICD" runat="server" CssClass="ASPTextBox" placeholder="Search Item .." AutoPostBack="true" Width="90%" OnTextChanged="TXT_ICD_TextChanged"></asp:TextBox>
                                <asp:DataGrid ID="DGR_ICD_AVAIL" runat="server" AutoGenerateColumns="False" CellPadding="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" ForeColor="#333333" ShowHeader="False" OnItemCommand="DGR_ICD_AVAIL_ItemCommand">
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Font-Size="XX-Small" />
                                    <ItemStyle BackColor="#E3EAEB" VerticalAlign="Top" Font-Size="XX-Small" />
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="50" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                            <td>
                                <asp:DataGrid ID="DGR_ICD_EXCLUDED" runat="server" AutoGenerateColumns="False" CellPadding="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="100%" ForeColor="#333333" ShowHeader="False" OnItemCommand="DGR_ICD_EXCLUDED_ItemCommand">
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" Font-Size="XX-Small" />
                                    <ItemStyle BackColor="#FFFBD6" VerticalAlign="Top" Font-Size="XX-Small" ForeColor="#333333" />
                                    <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="50" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LB_DELETE" runat="server" CommandName="Delete" ForeColor="Red"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="200" />
                                            <ItemTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="TDBGColor">APPLIED TO BENEFIT :</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DataGrid ID="DGR_ICD_BENEFIT" runat="server" AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" Font-Names="Tahoma" Font-Size="XX-Small" GridLines="None" ItemStyle-Wrap="true" Width="100%" ForeColor="#333333" ShowHeader="False">
                                                                <ItemStyle VerticalAlign="Top" Font-Size="XX-Small" />
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="BENEFIT_CODE" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="BENEFIT_DESCR">
                                                                        <ItemStyle ForeColor="Blue" />
                                                                    </asp:BoundColumn>
                                                                    <asp:TemplateColumn>
                                                                        <ItemStyle Width="30" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CB" runat="server" OnCheckedChanged="CB_CheckedChanged" AutoPostBack="true" CssClass="ASPTextBox" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <br />
                                <br />
                                <div id="DV_ICD_ENTRY" runat="server" visible="false">
                                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                        <tr>
                                            <td style="width: 50%;" class="TDBGColor">OTHER EXCLUSION</td>
                                        </tr>
                                    </table>
                                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small; background-color: lightyellow;">
                                        <tr style="vertical-align: top;">
                                            <td style="width: 130px;">EXCLUDED JOB TYPE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_JOB" runat="server" CssClass="ASPTextBox" Width="99%" TextMode="MultiLine" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>EXCLUDED HOBBY</td>
                                            <td>
                                                <asp:TextBox ID="TXT_HOBBY" runat="server" CssClass="ASPTextBox" Width="99%" TextMode="MultiLine" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_SAVE_OTHER" runat="server" CssClass="ASPButton" Width="100" Text="SAVE" OnClick="BT_SAVE_OTHER_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>

                    <table id="TBL_PAYMENT" runat="server" visible="false" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr style="vertical-align: top;">
                            <td style="width: 30%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">PREMIUM</td>
                                        <td>
                                            <asp:Label ID="LB_PREMIUM" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY CHARGE</td>
                                        <td>
                                            <asp:Label ID="LB_POLICY_CHG" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>STAMP CHARGE</td>
                                        <td>
                                            <asp:Label ID="LB_STAMP_CHG" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-top-style: ridge;">TOTAL</td>
                                        <td style="border-top-style: ridge;">
                                            <asp:Label ID="LB_TOTAL_CHG" runat="server" ForeColor="Blue" Style="font-weight: 700"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 70%;">
                                <table id="TBL_FIRST_PAYMENT" runat="server" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 130px;">VIRTUAL ACC NO.</td>
                                        <td>
                                            <asp:Label ID="LB_VACCNO" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BANK</td>
                                        <td>
                                            <asp:Label ID="LB_BANK" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PAYMENT AMOUNT</td>
                                        <td>
                                            <asp:Label ID="LB_PAYMENT_AMOUNT" runat="server" ForeColor="Green"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PAYMENT DATE</td>
                                        <td>
                                            <asp:Label ID="LB_PAYMENT_DATE" runat="server" ForeColor="Green"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>PAYMENT NOTE</td>
                                        <td>
                                            <asp:Label ID="LB_PAYMENT_NOTE" runat="server" ForeColor="Green"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_QUOTATION" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <iframe id="IF_QUOTATION" runat="server" style="width: 99%; height: 90vh; overflow: auto; background-color: white;"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
