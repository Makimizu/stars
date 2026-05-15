<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurplusUwDetail.aspx.cs" Inherits="UWBOX.Form_Tools.SurplusUwDetail" %>

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
        <asp:Label ID="LB_BATCHID" runat="server" Visible="False"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 90px;">SURPLUS DATE</td>
                <td>
                    <asp:DropDownList ID="DDL_DATE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>UPLOAD FILE</td>
                <td>
                    <asp:FileUpload ID="FU" runat="server" CssClass="ASPTextBox" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>DOWNLOAD</td>
                <td>
                    
                    <button onclick="window.open('UPLOAD_UW_SURPLUS.xls');" style="background-color:green;color:white;font-size:xx-small;">XLS</button> &nbsp;
                    <asp:Button ID="BT_UPLOAD" runat="server" Text="UPLOAD" Width="100" OnClick="BT_UPLOAD_Click" OnClientClick="ShowProgress()" Font-Size="XX-Small"/>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <asp:DataGrid ID="DGR_BATCH" runat="server" CssClass="ASPDatagrid" GridLines="None" Font-Size="XX-Small" AutoGenerateColumns="False" ShowHeader="False" Width="100%" OnItemCommand="DGR_BATCH_ItemCommand">
            <ItemStyle Wrap="False" HorizontalAlign="Center" />
            <Columns>
                <asp:BoundColumn DataField="THEDATE" Visible="false"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="BT_SHOW" runat="server" CommandName="Show" Width="100%" Font-Bold="true" Font-Size="XX-Small" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="30px" />
                    <ItemTemplate>
                        <asp:Button ID="BT_DEL" runat="server" Text="X" BackColor="Red" ForeColor="White" CommandName="Delete" Width="100%" Font-Bold="true" Font-Size="XX-Small" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle Wrap="False"
                HorizontalAlign="Center" />
            <PagerStyle PageButtonCount="5" />
        </asp:DataGrid>


        <asp:Label ID="LB_ERR" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
