<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationPersonOtherInfo.aspx.cs" Inherits="LQ.Form_Client.QuotationPersonOtherInfo" %>

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
        <asp:Label ID="LB_MEMBERID" runat="server" Visible="false"></asp:Label>
        <table id="TBL_FORM" runat="server" style="border-spacing: 0px; width: 100%;font-size: x-small;">
            <!--
            <tr class="TDBGColor">
                <td>
                    <asp:Label ID="LB_FULLNAME" runat="server"></asp:Label>
                </td>
            </tr>
            -->
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%">
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
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE OTHER INFO" Width="200px" OnClick="BT_SAVE_Click" OnClientClick="ShowProgress()" />
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
