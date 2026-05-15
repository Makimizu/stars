<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimPending004.aspx.cs" Inherits="LIFE.Form_Claim.ClaimPending004" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_READONLY" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TBL_REMARK" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 120px;">
                                <asp:Button ID="BT_EMAIL" runat="server" CssClass="ASPButton" Text="SEND EMAIL TO" Width="100%" OnClick="BT_EMAIL_Click" />
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:Button ID="BT_REMARK" runat="server" CssClass="ASPButton" Text="SET REMARK" Width="100%" OnClick="BT_REMARK_Click" />
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" Width="95%" Height="60px" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BT_ARCHIEVE" runat="server" Text="ARCHIEVE" CssClass="ASPButton" Width="100%" BackColor="Green" ForeColor="White" OnClick="BT_ARCHIEVE_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="300px" Width="95%" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 60px; border: outset 2px gray; padding: 5px; display: none; overflow: auto;">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">ARCHIEVE
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
                            <iframe id="ifClaim" runat="server" src=""  width="100%" style="height:auto;"></iframe>
                            </center>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

