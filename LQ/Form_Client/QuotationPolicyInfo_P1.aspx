<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationPolicyInfo_P1.aspx.cs" Inherits="LQ.Form_Client.QuotationPolicyInfo_P1" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
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
        <div class="loading" align="center">
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_UNITIZE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr style="vertical-align: top;">
                <td style="width: 250px;">
                    <asp:Button ID="BT_FORM" runat="server" CssClass="ASPButton" Text="SPAJ" Width="100%" OnClick="BT_FORM_Click" />
                    <asp:Button ID="BT_BENEFIT" runat="server" CssClass="ASPButton" Text="MANFAAT" Width="100%" OnClick="BT_BENEFIT_Click" />
                    <asp:Button ID="BT_IRG" runat="server" CssClass="ASPButton" Text="IRREGULER PLAN" Width="100%" OnClick="BT_IRG_Click" />
                    <asp:Button ID="BT_FUND" runat="server" CssClass="ASPButton" Text="FUND" Width="100%" OnClick="BT_FUND_Click" />
                    <asp:Button ID="BT_ORGBEN" runat="server" CssClass="ASPButton" Text="WAKAF" Width="100%" OnClick="BT_ORGBEN_Click" />
                    <asp:Button ID="BT_BENCYCLE" runat="server" CssClass="ASPButton" Text="RENCANA TAHAPAN" Width="100%" OnClick="BT_BENCYCLE_Click" />
                    <asp:Button ID="BT_PAYMENT" runat="server" CssClass="ASPButton" Text="STATUS PEMBAYARAN" Width="100%" OnClick="BT_PAYMENT_Click" />
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table id="TBL_FORM" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 300px;">NOMOR SPAJ</td>
                                        <td>
                                            <asp:TextBox ID="TXT_FORMNO" runat="server" CssClass="ASPTextBox" BackColor="Yellow" Font-Bold="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TANGGAL MULAI</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                                <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="98%">
                                    <ItemStyle VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR">
                                            <ItemStyle Width="300px" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False" AutoPostBack="true" OnSelectedIndexChanged="DDL_REFF_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBoxUPPER" Visible="False" Width="150px"></asp:TextBox>
                                                <asp:TextBox ID="TXT_VALDATE" runat="server" CssClass="ASPTextBox" Visible="False" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_VALDATE">
                                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_FOP" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" GridLines="None" ShowHeader="False">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" Font-Size="X-Small" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TC_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TC_DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FOP" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="POLICY_PERIOD" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PAYMENT_PERIOD" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PREMIUM" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TOPUP" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr style="vertical-align: top;">
                                                        <td>
                                                            <table style="border-spacing: 0px; width: 100%;">
                                                                <tr>
                                                                    <td style="width: 300px;">CARA BAYAR</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DDL_FOP" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>PERIODE PEMBAYARAN</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DDL_PAYMENT_PERIOD" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>PERIODE POLIS</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DDL_POLICY_PERIOD" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>PREMI REGULER</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TXT_PREMIUM" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox></td>
                                                                </tr>
                                                                <tr id="TR_TOPUP_REGULER" runat="server">
                                                                    <td>TOPUP REGULER</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TXT_TOPUP" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <asp:Button ID="BT_SAVE_FOP" runat="server" CssClass="ASPButton" Text="SAVE PERIODE &amp; PREMI" Width="150px" OnClick="BT_SAVE_FOP_Click" OnClientClick="ShowProgress()" />
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_BENEFIT" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <iframe id="IF_BENEFIT" runat="server" style="width: 98%; border-style: none;"></iframe>
                                <%--<asp:DataGrid ID="DGR_BENEFIT" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" GridLines="Horizontal" ShowHeader="False" BorderColor="#CCCCCC">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" Font-Size="X-Small" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TC_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BENEFIT_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BENEFIT_DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SUMINS" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="END_DATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BASIC" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SUMINS_MIN" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SUMINS_MAX" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PAYOR_STAT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PAYOR_MAXAGE_SQL" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 30%;">
                                                            <table style="border-spacing: 0px; width: 100%;">
                                                                <tr>
                                                                    <td style="width: 80px;">
                                                                        <asp:Label ID="LB_BASIC" runat="server">BASIC</asp:Label>&nbsp;BENEFIT</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="CB_BENEFIT" runat="server" />&nbsp;&nbsp;
                                                                                    <asp:Label ID="LB_BENEFIT_DESCR" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 70%;">
                                                            <table style="border-spacing: 0px; width: 100%;">
                                                                <tr id="TR_ENDDATE" runat="server">
                                                                    <td style="width: 150px;">TANGGAL AKHIR</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 150px;">PENERIMA MANFAAT</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DDL_INSUREDPERSON" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                                                </tr>
                                                                <tr id="TR_SUMINS" runat="server">
                                                                    <td>UP</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TXT_SUMINS" runat="server" CssClass="ASPTextBox" Width="100"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="TR_SUMINS_LIMIT" runat="server">
                                                                    <td>BATAS UP</td>
                                                                    <td>
                                                                        <asp:Label ID="LB_SUMINS_MIN" runat="server" ForeColor="Red"></asp:Label>&nbsp;-&nbsp;
                                                                                    <asp:Label ID="LB_SUMINS_MAX" runat="server" ForeColor="Red"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="TR_MAXAGE" runat="server">
                                                                    <td>USIA MAX PAYOR</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DDL_MAXAGE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>&nbsp;TAHUN
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <br />
                                <asp:DataGrid ID="DGR_BENEFIT_HEALTHPLAN" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="2" ForeColor="#333333" GridLines="None">
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PLAN_VALUE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_PLAN" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MEMBER_NAME" HeaderText="PENERIMA MANFAAT"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AGE" HeaderText="USIA MASUK"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BENEFIT_ID" HeaderText="PLAN">
                                            <HeaderStyle Width="20" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_PLAN" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="ANNUAL_PREMIUM" HeaderText="ANNUAL<BR>PREMIUM">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FACTOR" HeaderText="FACTOR">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="REGULER_PREMIUM" HeaderText="REGULER<BR>PREMIUM">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <asp:Button ID="BT_SAVE_BENEFIT" runat="server" CssClass="ASPButton" Text="SAVE BENEFIT" Width="150px" OnClick="BT_SAVE_BENEFIT_Click" />--%>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_IRG" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_IRREGULER" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="YEARSEQ" HeaderText="#YEAR"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="THEDATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TOPUP_IR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="WDW_IR" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="TOP UP IRREGULER">
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_TIR" runat="server" Width="100" CssClass="ASPTextBoxNumber" BackColor="Cyan"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="WITHDRAWAL" Visible="false">
                                            <HeaderStyle HorizontalAlign="Right" Width="100" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_WDW" runat="server" Width="100" CssClass="ASPTextBoxNumber" BackColor="LightPink"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                                <asp:Button ID="BT_IRREGULER" runat="server" CssClass="ASPButton" Text="SAVE IRREGULER" Width="150px" OnClick="BT_IRREGULER_Click" OnClientClick="ShowProgress()" />
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_FUND" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_FUND" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="None" ItemStyle-Wrap="true" ShowHeader="False" Width="100%">
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                                    <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="True" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TC_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FUND_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PCT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TC_DESCR"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FUND_DESCR"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_PCT_FUND" runat="server" CssClass="ASPDropDownList">
                                                </asp:DropDownList>
                                                &nbsp;%
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                                <asp:Button ID="BT_SAVE_FUND" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_FUND_Click" Text="SAVE FUND" Width="150px" OnClientClick="ShowProgress()" />
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_ORGBEN" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">ORGANISASI</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ORG" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">NO. SERTIFIKAT</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CERNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NO. REKENING</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NAMA REKENING</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BANK</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TXT_ACCBANK" runat="server" CssClass="ASPTextBox" AutoPostBack="true" Width="30%" OnTextChanged="TXT_ACCBANK_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% MANFAAT RESIKO</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PCTCLM" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% MANFAAT INVESTASI</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PCTINV" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_ORG" runat="server" CssClass="ASPButton" OnClick="BT_ORG_Click" Text="SAVE WAKAF" Width="150px" OnClientClick="ShowProgress()" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_BENCYCLE" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_BENEFIT_CYCLE" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DEATH" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TOBEPAID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SEQ" HeaderText="#YEAR"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="THEDATE" HeaderText="DATE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PCT_VAL" HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT_VAL" HeaderText="AMOUNT">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="TOBE PAID">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_PAYMENT" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
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
                                    <tr>
                                        <td style="width: 130px;">PREMIUM</td>
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
                                        <td>TOTAL</td>
                                        <td>
                                            <asp:Label ID="LB_TOTAL_CHG" runat="server" ForeColor="Blue" Style="font-weight: 700"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>PAYMENT NOTE</td>
                                        <td>
                                            <asp:Label ID="LB_PAYMENT_NOTE" runat="server" ForeColor="Green"></asp:Label></td>
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
