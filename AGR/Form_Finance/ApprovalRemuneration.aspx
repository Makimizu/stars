<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalRemuneration.aspx.cs" Inherits="AGR.Form_Finance.ApprovalRemuneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" type="text/css" rel="stylesheet" />
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
        <asp:Label ID="LB_MARKET_SEGMENT" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server" CssClass="ASPLabel" Font-Size="X-Small" Font-Names="Verdana" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">APPROVAL STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_STAT" runat="server" Font-Size="XX-Small" AutoPostBack="True" OnSelectedIndexChanged="DDL_STAT_SelectedIndexChanged">
                                    <asp:ListItem Value="0">PENDING</asp:ListItem>
                                    <asp:ListItem Value="1">APPROVED</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="TR_YEAR" runat="server" visible="false">
                            <td>YEAR</td>
                            <td>
                                <asp:DropDownList ID="DDL_YEAR" runat="server" Font-Size="XX-Small" AutoPostBack="True" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
            BorderWidth="1px" CellPadding="3" PageSize="30"
            GridLines="Vertical" CssClass="ASPDatagrid"
            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%">
            <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
            <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
            <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:BoundColumn DataField="MARKET_SEGMENT" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="MIN_AMOUNT" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="START_DATE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="END_DATE" ItemStyle-HorizontalAlign="Center" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="REMUN_PERIOD" HeaderText="REMUNERATION PERIOD" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TOTAL_AMOUNT" HeaderText="TOTAL AMOUNT" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVE1" HeaderText="APPROVAL #1"></asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVE2" HeaderText="APPROVAL #2"></asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVE3" HeaderText="APPROVAL #3"></asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVE4" HeaderText="APPROVAL #4"></asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVE5" HeaderText="APPROVAL #5"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="130" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" CommandName="Approve" BackColor="Green" ForeColor="White" Text="APPROVE" />
                        <asp:Button ID="BT_DETAIL" runat="server" CssClass="ASPButton" CommandName="Detail" BackColor="Blue" ForeColor="White" Text="DETAIL" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
        </asp:DataGrid>




        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="200px" Width="60%" Style="z-index: 111; background-color: White; position: fixed; left: 120px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td class="TDBGColor">
                            <asp:Label ID="LB_APPROVAL_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <center>
                                <asp:DataGrid ID="DGR_APPROVAL" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="30"
                                    GridLines="Horizontal" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_APPROVAL_ItemCommand" Width="90%">
                                    <itemstyle wrap="False" verticalalign="Top" backcolor="#E7E7FF" forecolor="#4A3C8C" />
                                    <selecteditemstyle backcolor="#738A9C" forecolor="#F7F7F7" font-bold="True" />
                                    <alternatingitemstyle backcolor="#F7F7F7" verticalalign="Top" />
                                    <itemstyle backcolor="#EEEEEE" forecolor="Black" />
                                    <headerstyle backcolor="#4A3C8C" font-bold="True" forecolor="#F7F7F7" />
                                    <columns>
                                        <asp:BoundColumn DataField="MARKET_SEGMENT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MIN_AMOUNT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="START_DATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="END_DATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ENABLE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="LEVEL"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="APPROVAL" HeaderText="APPROVE BY"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <itemstyle width="100" horizontalalign="Right" />
                                            <itemtemplate>
                                                <asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" CommandName="Approve" BackColor="Green" ForeColor="White" Text="APPROVE" Width="100%" />
                                            </itemtemplate>
                                        </asp:TemplateColumn>
                                    </columns>
                                    <footerstyle backcolor="#B5C7DE" forecolor="#4A3C8C" />
                                    <pagerstyle backcolor="#E7E7FF" forecolor="#4A3C8C" horizontalalign="Right" mode="NumericPages" />
                                </asp:DataGrid>
                            </center>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
