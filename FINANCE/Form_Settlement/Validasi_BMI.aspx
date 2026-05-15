<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validasi_BMI.aspx.cs" Inherits="FINANCE.Form_Settlement.Validasi_BMI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }
    </style>
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.blockUI.js"></script>
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

        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_ACCT_NO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_ACCT_NAME" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="SOURCE_NAME" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="SOURCE_ACC" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="TF_DESC" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="AMOUNT" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="AMOUNTS" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="KODEBANK" runat="server" Visible="false"></asp:Label>

        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>Status :
                                <asp:Label ID="LB_STATUS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                            <td></td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <table id="TB_TRANSFER" runat="server">
                                    <tr>
                                        <td style="width: 100px;">Transfer</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TRANSFER" runat="server" CssClass="ASPDropDownList" AutoPostBack="True"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="BT_PROCESS" runat="server" CssClass="ASPButton" OnClick="BT_PROCESS_Click" Text="show" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <table id="TB_INQUIRY" runat="server" style="border-spacing: 1px; background-color: #CCFFCC; width: 100%; border-color: black;">
                                    <tr>
                                        <td style="width: 100px;">Destination Name</td>
                                        <td>
                                            <asp:Label ID="LB_ACCNAME" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Destination Acc</td>
                                        <td>
                                            <asp:TextBox ID="LB_ACCNO" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: left;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Destination Bank</td>
                                        <td>
                                            <asp:Label ID="LB_ACCBANKDESC" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LB_ACCBANK" runat="server" CssClass="ASPLabel" Font-Bold="True" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LB_SWIFTCODE" runat="server" CssClass="ASPLabel" Font-Bold="True" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_INQUIRY" runat="server" CssClass="ASPButton" OnClick="BT_INQUIRY_Click" Text="INQUIRY" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <table id="TB_INFO" runat="server" style="border-spacing: 1px; background-color: #CCFFCC; width: 100%; border-color: black;">

                                    <tr>
                                        <td style="width: 100px;">Destination Name</td>
                                        <td>
                                            <asp:Label ID="LB_ACCNAME_INFO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="LB_TRANSACTIONID_INFO" runat="server" CssClass="ASPLabel" Font-Bold="True" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Destination Acc</td>
                                        <td>
                                            <asp:Label ID="LB_ACCNO_INFO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Destination Bank</td>
                                        <td>
                                            <asp:Label ID="LB_BANK_INFO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">Source Name</td>
                                        <td>
                                            <asp:Label ID="LB_SRNAME_INFO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Source Acc</td>
                                        <td>
                                            <asp:Label ID="LB_SRACC_INFO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Transfer Amount</td>
                                        <td>
                                            <asp:Label ID="LB_AMOUNT_INFO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Transfer Description</td>
                                        <td>
                                            <asp:Label ID="LB_TFDESC_INFO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="BT_REGISTRATION" runat="server" CssClass="ASPButton" OnClick="BT_REGISTRATION_Click" Text="REGISTRATION" Visible="false" />
                                            <asp:Button ID="BT_VERIFICATION" runat="server" CssClass="ASPButton" OnClick="BT_VERIFICATION_Click" Text="VERIFICATION" Visible="false" />
                                            <asp:Button ID="BT_APPROVED" runat="server" CssClass="ASPButton" OnClick="BT_APPROVED_Click" Text="APPROVED" Visible="false" />
                                            <asp:Button ID="BT_REJECT" runat="server" CssClass="ASPButton" OnClick="BT_REJECT_Click" Text="REJECT" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>

                    <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            Password
                        </td>
                        <td>:</td>
                        <td><asp:TextBox ID="TXT_PASS" TextMode="Password" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: left;"></asp:TextBox></td>
                        <td><asp:TextBox ID="TXT_TYPE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: left;" Visible="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td><asp:Button ID="BT_SUBMIT" runat="server" OnClick="BT_SUBMIT_Click" CssClass="ASPButton" Text="Submit" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

