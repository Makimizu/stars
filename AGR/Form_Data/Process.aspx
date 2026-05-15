<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Process.aspx.cs" Inherits="AGR.Form_Data.Process" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
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
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:DataGrid ID="DGR_CD" runat="server" PageSize="20" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" Width="100%" ShowHeader="False" OnItemCommand="DGR_CD_ItemCommand" GridLines="None" CellPadding="0">
                        <ItemStyle Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_CD" runat="server" Font-Size="X-Small" Width="100%" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle VerticalAlign="Top" />
                        <ItemStyle BackColor="#EFF3FB" />
                    </asp:DataGrid>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:DataGrid ID="DGR_TO_APPROVAL" runat="server" PageSize="20" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" Width="100%" OnItemCommand="DGR_TO_APPROVAL_ItemCommand" GridLines="None" CellPadding="0">
                        <ItemStyle Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="SQL_EXEC" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PROCESS TO APPROVAL">
                                <ItemTemplate>
                                    <asp:Button ID="BT_TO_APPROVAL" runat="server" Font-Size="X-Small" Width="100%" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle VerticalAlign="Top" BackColor="Navy" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle BackColor="#EFF3FB" />
                    </asp:DataGrid>
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="LB_CD" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 130px;">YEAR</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>MONTH</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MONTH" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_MONTH_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_PERIOD" runat="server" CellPadding="4" PageSize="20"
                                    GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%">
                                    <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CD" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REMUN_TYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PAST" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ENABLE_PROC" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ENABLE_DEL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ENABLE_FIN" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REMUN_DESCR" HeaderText="REMUN TYPE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PROD_RECORDS" HeaderText="#PRODUCTION&lt;BR&gt;RECORDS">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PROCESSED" HeaderText="#PROCESSED&lt;BR&gt;RECORDS">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FINANCE" HeaderText="#FINANCE&lt;BR&gt;RECORDS">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="100" />
                                            <ItemTemplate>
                                                <asp:Button ID="BT_PROCESS" runat="server" Text="PROCESS" CssClass="ASPButton" CommandName="Process" BackColor="Blue" ForeColor="White" Width="100%" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="100" />
                                            <ItemTemplate>
                                                <asp:Button ID="BT_DEL" runat="server" Text="UNPROCESS" CssClass="ASPButton" CommandName="Unprocess" BackColor="Red" ForeColor="White" Width="100%" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="100" />
                                            <ItemTemplate>
                                                <asp:Button ID="BT_FINANCE" runat="server" Text="SEND to FINANCE" CssClass="ASPButton" CommandName="Finance" BackColor="Green" ForeColor="White" Width="100%" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Top" />
                                    <ItemStyle BackColor="#EFF3FB" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
