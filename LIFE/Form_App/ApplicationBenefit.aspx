<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationBenefit.aspx.cs" Inherits="LIFE.Form_App.ApplicationBenefit" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
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
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td id="TD_MEMBERLIST" runat="server" style="width: 250px;">
                    <asp:DataGrid ID="DGR_INSUREDLIST" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" GridLines="None" CellPadding="4" ForeColor="#333333" OnItemCommand="DGR_INSUREDLIST_ItemCommand">
                        <HeaderStyle BackColor="#990000" ForeColor="White" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" Font-Size="X-Small" BackColor="#FFFBD6" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MEMBER_TYPE_DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_AGE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GENDER" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="INSURED PERSON">
                                <ItemStyle Font-Size="XX-Small" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_MEMBER" runat="server" CommandName="Select"></asp:LinkButton><br />
                                    <asp:Label ID="LB_MEMBER" runat="server" ForeColor="Gray" Font-Italic="true" Font-Size="XX-Small"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small">
                        <tr>
                            <td style="width: 130px;">INSURED NAME</td>
                            <td>
                                <asp:Label ID="LB_MEMBERNAME" runat="server" Font-Bold="true"></asp:Label>
                                <asp:Label ID="LB_MEMBERID" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>MEMBER TYPE</td>
                            <td>
                                <asp:Label ID="LB_MEMBERTYPE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>GENDER - START AGE</td>
                            <td>
                                <asp:Label ID="LB_MEMBERAGE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">BENEFIT - LIFE</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_BENEFIT" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" GridLines="Horizontal" ShowHeader="False" BorderColor="#CCCCCC">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" Font-Size="XX-Small" />
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
                                        <asp:BoundColumn DataField="MUSTHAVE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PREMIUM" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 30%;">
                                                            <table style="border-spacing: 0px; width: 100%;">
                                                                <tr>
                                                                    <td style="width: 30px;">
                                                                        <asp:CheckBox ID="CB_BENEFIT" runat="server" /></td>
                                                                    <td>
                                                                        <asp:Label ID="LB_BENEFIT_DESCR" runat="server"></asp:Label><br />
                                                                        <span style="font-style: italic; color: gray; font-size: 6pt;">
                                                                            <asp:Label ID="LB_BASIC" runat="server">BASIC</asp:Label>&nbsp;BENEFIT</span>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 50%;">
                                                            <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                                                <tr id="TR_ENDDATE" runat="server">
                                                                    <td style="width: 130px;">END DATE</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    </td>
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
                                                                <tr id="TR_RATE" runat="server">
                                                                    <td>RATE</td>
                                                                    <td style="width: 20%;">
                                                                        <asp:Label ID="LB_RATE" runat="server" ForeColor="Blue"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="TR_PREMIUM" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="LB_PREMIUM_INFO" runat="server" Text="PREMIUM"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 20%;">
                                                                        <asp:Label ID="LB_PREMIUM" runat="server" ForeColor="Blue"></asp:Label>
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
                            </td>
                        </tr>
                    </table>

                    <table style="border-spacing: 0px; width: 100%;" id="TBL_PAYOR" runat="server">
                        <tr>
                            <td class="TDBGColor">BENEFIT - PAYOR</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_PAYOR" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" GridLines="Horizontal" ShowHeader="False" BorderColor="#CCCCCC">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" Font-Size="XX-Small" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TC_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BENEFIT_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BENEFIT_DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BASIC" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PAYOR_MAXAGE_SQL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TENOR" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 30%;">
                                                            <table style="border-spacing: 0px; width: 100%;">
                                                                <tr>
                                                                    <td style="width: 30px;">
                                                                        <asp:CheckBox ID="CB_BENEFIT" runat="server" /></td>
                                                                    <td>
                                                                        <asp:Label ID="LB_BENEFIT_DESCR" runat="server"></asp:Label><br />
                                                                        <span style="font-style: italic; color: gray; font-size: 6pt;">
                                                                            <asp:Label ID="LB_BASIC" runat="server">BASIC</asp:Label>&nbsp;BENEFIT</span>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 70%;">
                                                            <table style="border-spacing: 0px; width: 100%;">
                                                                <tr>
                                                                    <td style="width: 130px;">USIA MAX PAYOR</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DDL_MAXAGE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>&nbsp;<asp:Label ID="LB_MAXAGE" runat="server" Text="TAHUN"></asp:Label>
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
                            </td>
                        </tr>
                    </table>

                    <table style="border-spacing: 0px; width: 100%;" id="TBL_HEALTH" runat="server">
                        <tr>
                            <td class="TDBGColor">BENEFIT - HEALTH</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_BENEFIT_HEALTHPLAN" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="2" ForeColor="#333333" GridLines="None">
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" Font-Size="Xx-Small" />
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Xx-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PLAN_VALUE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_PLAN" Visible="False"></asp:BoundColumn>
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
                            </td>
                        </tr>
                    </table>

                    <br />
                    <asp:Button ID="BT_SAVE_BENEFIT" runat="server" CssClass="ASPButton" Text="SAVE BENEFIT" Width="150px" OnClick="BT_SAVE_BENEFIT_Click" OnClientClick="ShowProgress()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
