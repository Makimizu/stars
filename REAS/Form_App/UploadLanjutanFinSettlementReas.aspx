<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadLanjutanFinSettlementReas.aspx.cs" Inherits="REAS.Form_App.UploadLanjutanFinSettlementReas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
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
         <div id="loading" style="display:none; position:fixed; top:50%; left:50%; transform:translate(-50%, -50%); z-index:9999; background-color: rgba(255, 255, 255, 0.8); padding: 20px; border-radius: 10px; text-align: center;">
            <img src="../Content/loading.gif" alt="Loading..." style="border: 0; width: 100px;" />
            <p>Loading...</p>
        </div>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 2px; width: 100%;">
            <tr style="margin-top:2px">
                <td class="TDBGColor">
                    <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="XX-Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 6px;">
                        <tr>
                            <td style="width: 150px;">UPLOAD DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_UPDATEDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceUpdateDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_UPDATEDATE">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>TYPE <b>*</b></td>
                            <td>
                                <asp:DropDownList ID="DDL_TYPE" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" 
                                    OnSelectedIndexChanged="DDL_TYPE_SelectedIndexChanged"  Width="200px">
                                    <asp:ListItem Value="" ></asp:ListItem>
                                    <asp:ListItem Value="contribution_reas">CONTRIBUTION</asp:ListItem>
                                    <asp:ListItem Value="claim_reas">CLAIM</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="LBL_TYPE" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>TEMPLATE FILE</td>
                            <td>
                                <asp:LinkButton ID="LBT_TEMPLATE" runat="server" Text="DOWNLOAD" OnClick="btnDownload_Click"></asp:LinkButton>
                        </tr>
                        <tr>
                            <td>UPLOAD FILE</td>
                            <td>
                                <asp:FileUpload ID="TXT_FILE_UPLOAD" runat="server" class="ASPTextBox"/>
                        </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">DESCRIPTION</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCRIPTION" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" OnClick="BT_UPLOAD_Click" Text="SUBMIT DOCUMENT" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                 <asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>