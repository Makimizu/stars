<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Process.aspx.cs" Inherits="AGR.Form_Tax.Process" %>

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
        <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>
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
                </td>
                <td>
                    <table style="width: 100%; border-spacing: 0px;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" Font-Bold="true" runat="server"></asp:Label>
                                <asp:Label ID="LB_CD" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr id="TR_STL1" runat="server">
                            <td>
                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                    <tr>
                                        <td>YEAR</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_YEAR" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">MONTH</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MONTH" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_MONTH_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TR_STL2" runat="server">
                            <td>
                                <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                                <asp:Button ID="BT_PROCESS" runat="server" CssClass="ASPButton" ForeColor="Blue" OnClick="BT_PROCESS_Click" Text="PROCESS" Width="100px" />
                                <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="40"
                                    GridLines="Vertical" CssClass="ASPDatagrid"
                                    OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%">
                                    <ItemStyle Wrap="False" />
                                    <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="#DCDCDC" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="DOC NO">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LB_REKAPID" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="LB_ALL" runat="server" CommandName="All" CssClass="ASPLabel" ForeColor="White">ALL</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="REKAPID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CNT" HeaderText="#">
                                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="30px" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESTINATION_BANK_DESCR" HeaderText="DESTINATION BANK"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GENERATE" HeaderText="GENERATE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="30" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ENABLE_REFUND" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AUTH" Visible="False"></asp:BoundColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
    </form>
</body>
</html>
