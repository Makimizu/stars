<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataProduction.aspx.cs" Inherits="LQ.Form_Data.DataProduction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 200px;" alt="" />
        </div>
        <asp:Label ID="LB_YEAR" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_MONTH" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                        <tr>
                            <td style="width: 150px;">PRODUCT GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCTGROUP" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>PRODUCT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_PRODUCTNAME" runat="server" CssClass="ASPTextBox" Width="300"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>TRANSACTION TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TRANSTYPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>YEAR</td>
                            <td>
                                <asp:TextBox ID="TXT_YEARFROM" runat="server" Width="30" CssClass="ASPTextBox"></asp:TextBox>&nbsp;-&nbsp;
                                <asp:TextBox ID="TXT_YEARTO" runat="server" Width="30" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value=" and isnull(a.PREMIUM, 0) &gt;= 0">PRODUCTION</asp:ListItem>
                                    <asp:ListItem Value=" and isnull(a.PREMIUM, 0) &lt; 0">CLAWBACK</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" Width="100" OnClick="BT_SEARCH_Click" OnClientClick="ShowProgress();" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" >
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Font-Size="X-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <PagerStyle BackColor="#666666" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
