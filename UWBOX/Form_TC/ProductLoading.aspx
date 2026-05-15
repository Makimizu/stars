<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductLoading.aspx.cs" Inherits="UWBOX.Form_TC.ProductLoading" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../Standard/image/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td style="width: 130px;">TERM & CONDITION</td>
                <td>
                    <asp:DropDownList ID="DDL_TC" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_TC_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
        </table>
        <table id="TBL_DATA" runat="server" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:Button ID="BT_LOADING" runat="server" CssClass="ASPButton" Text="LOADING" Width="100%" OnClick="BT_LOADING_Click" />
                    <asp:Button ID="BT_CHARGES" runat="server" CssClass="ASPButton" Text="CHARGES" Width="100%" OnClick="BT_CHARGES_Click" />
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div id="DV_LOADING" runat="server" style="width: 100%;">
                        <table id="TBL_PERIODIC" runat="server" style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 80px;">
                                                <asp:Button ID="BT_XLS" runat="server" Text="DOWNLOAD XLS" CssClass="ASPButton" BackColor="Green" ForeColor="White" OnClick="BT_XLS_Click" Width="120px" OnClientClick="ShowProgress();" /></td>
                                            <td>
                                                <asp:DropDownList ID="DDL_PERIODIC" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" BackColor="Blue" ForeColor="White" Text="UPLOAD XLS" OnClick="BT_UPLOAD_Click" Width="120px" OnClientClick="ShowProgress();" />
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="FU" runat="server" CssClass="ASPTextBox" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <iframe id="IFLOADING" runat="server" style="width: 98%; height: 70vh;"></iframe>
                    </div>
                    <div id="DV_CHARGES" runat="server" visible="false" style="width: 100%;">
                        <table id="Table1" runat="server" style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 80px;">
                                    <asp:Button ID="BT_CHARGE_DOWNLOAD" runat="server" Text="DOWNLOAD XLS" CssClass="ASPButton" BackColor="Green" ForeColor="White" OnClick="BT_CHARGE_DOWNLOAD_Click" Width="120px" OnClientClick="ShowProgress();" /></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BT_CHARGE_UPLOAD" runat="server" CssClass="ASPButton" BackColor="Blue" ForeColor="White" Text="UPLOAD XLS" OnClick="BT_CHARGE_UPLOAD_Click" Width="120px" OnClientClick="ShowProgress();" />
                                </td>
                                <td>
                                    <asp:FileUpload ID="FU_CHG" runat="server" CssClass="ASPTextBox" />
                                </td>
                            </tr>
                        </table>
                        <iframe id="IFCHARGE" runat="server" style="width: 98%; height: 70vh;" name="I1"></iframe>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
