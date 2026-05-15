<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_ApprovalList.aspx.cs" Inherits="HEALTH.Form_Member.GPA_ApprovalList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.blockUI.js"></script>
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
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;">
            <tr>
                <td class="auto-style1">
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 150px;">BATCH ID</td>
                            <td>
                                <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PERUSAHAAN</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NO POLIS</td>
                            <td>
                                <asp:TextBox ID="TXT_POLICY_NO" runat="server" CssClass="ASPTextBox" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NO DOKUMEN</td>
                            <td>
                                <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TIPE ENDORSEMENT</td>
                            <td>
                                <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL PROSES</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="80px" style="text-align:center;"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                        </ajaxToolkit:CalendarExtender>
                                        &nbsp;-
                                        <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="80px" style="text-align:center;"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="CARI" OnClick="BT_SEARCH_Click" />
                                <asp:Label ID="LB_ERROR" runat="server" EnableViewState="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Green" OnClick="BT_APPROVE_Click" Text="APPROVE" Visible="False" />
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="20" OnPageIndexChanged="DGR_PageIndexChanged" OnItemCommand="DGR_ItemCommand"
                        AllowPaging="True" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="LB_ALL" runat="server" CommandName="All" CssClass="ASPLabel" Font-Bold="True" ForeColor="White">ALL</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr style="vertical-align: top;">
                                            <td>
                                                <asp:CheckBox ID="CB" runat="server" /></td>
                                            <td>
                                                <asp:Button ID="BT_SELECT" runat="server" CssClass="ASPButton" Font-Bold="True" Text="PREVIEW" CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="BATCH_ID" HeaderText="BATCH ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_ENDORS" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_ENDORS_DESCR" HeaderText="TIPE ENDORSEMENT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCNO" HeaderText="DOCUMENT NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOC_SOURCE_DESCR" HeaderText="SUMBER DOK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLIS NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VER_END" HeaderText="TGL VERIFY" Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_PERIOD_ID" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PROSES">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>BATCH ID</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_BATCH_ID" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>TIPE</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_TIPE" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>TGL VERIFIKASI</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_TGLVER" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DOKUMEN">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>NO DOK</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_DOCNO" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>SUMBER DOK</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_DOCSRC" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="POLIS">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>NO POLIS</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_POLNO" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>PERUSAHAAN</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_GOTO" runat="server" CssClass="ASPButton" Font-Bold="True" Text="GOTO :" BackColor="Red" CommandName="GOTO" />
                                                <asp:DropDownList ID="DDL_GOTO" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" Height="35px" Width="179px" TextMode="MultiLine" MaxLength="1000"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                            Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
