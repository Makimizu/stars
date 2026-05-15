<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationFHP.aspx.cs" Inherits="LIFE.Form_App.ApplicationFHP" %>

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
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 130px;">CLAIM RATIO</td>
                <td>
                    <asp:DropDownList ID="DDL_CR" runat="server" CssClass="ASPDropDownList">
                        <asp:ListItem Value="-1">ALL RATIO</asp:ListItem>
                        <asp:ListItem Value="0">&gt; 0</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>FHP MEMBER NAME</td>
                <td>
                    <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="200"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" Width="100" OnClick="BT_SEARCH_Click" OnClientClick="ShowProgress()"/></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label></td>
            </tr>
        </table>
        <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" GridLines="None" CellPadding="4" ForeColor="#333333" AllowPaging="True" PageSize="20" OnItemCommand="DGR_ItemCommand" OnPageIndexChanged="DGR_PageIndexChanged">
            <HeaderStyle BackColor="#990000" ForeColor="White" VerticalAlign="Top" />
            <ItemStyle Wrap="True" VerticalAlign="Top" Font-Size="X-Small" BackColor="#FFFBD6" ForeColor="#333333" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="SQL_REJECT" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                <asp:BoundColumn DataField="FULLNAME" HeaderText="MEMBER INFO"></asp:BoundColumn>
                <asp:BoundColumn DataField="FHP_PLAN" HeaderText="FHP INFO"></asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_HISTORY"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle Font-Size="XX-Small" Width="40" />
                    <ItemTemplate>
                        <asp:Button ID="BT_REJECT" runat="server" Text="REJECT" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Reject" OnClientClick="ShowProgress()"/>
                        <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" TextMode="MultiLine" MaxLength="255" Width="300" Height="40" placeholder="Type reject remark here .."></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />
        </asp:DataGrid>
    </form>
</body>
</html>
