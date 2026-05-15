<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExternalRevenue.aspx.cs" Inherits="AGR.Form_Data.ExternalRevenue" %>

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
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td>DIVISION IN CHARGE</td>
                <td>
                    <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged" Width="100px" onchange="ShowProgress();"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 130px;">YEAR</td>
                <td>
                    <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged" onchange="ShowProgress();"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>PERIOD</td>
                <td>
                    <asp:DropDownList ID="DDL_PERIOD" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_PERIOD_SelectedIndexChanged" onchange="ShowProgress();"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>AGENT LEVEL</td>
                <td>
                    <asp:DropDownList ID="DDL_LEVEL" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_LEVEL_SelectedIndexChanged" onchange="ShowProgress();"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>AGENT NAME</td>
                <td>
                    <asp:TextBox ID="TXT_AGENTNAME" runat="server" AutoPostBack="true" CssClass="ASPTextBox" Width="200" OnTextChanged="TXT_AGENTNAME_TextChanged" onchange="ShowProgress();"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="width: 100%; height: 300px; overflow: auto;">
            <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%">
                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <AlternatingItemStyle BackColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="AGENT_CODE" HeaderText="AGENT CODE">
                        <HeaderStyle Width="130px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME">
                        <HeaderStyle Width="200px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL">
                        <HeaderStyle Width="200px" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="AMOUNT">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox>
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
        </div>
        <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
    </form>
</body>
</html>
