<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationEndorsement.aspx.cs" Inherits="GLIFE.Form_App.ApplicationEndorsement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            //setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
            //}, 200);
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
            bottom: 0;
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
        .auto-style2 {
            height: 30px;
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
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 40%; border-right-style: groove;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">ENDORSEMENT NO</td>
                                        <td>
                                            <asp:Label ID="LB_REGNO" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                            &nbsp;-&nbsp;
                                            <asp:Label ID="LB_SEQ" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                            <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>FULL NAME</td>
                                        <td>
                                            <asp:Label ID="LB_NAME" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>DATE OF BIRTH</td>
                                        <td>
                                            <asp:Label ID="LB_DOB" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>GENDER / START AGE</td>
                                        <td>
                                            <asp:Label ID="LB_GENDER" runat="server" Font-Bold="True"></asp:Label>&nbsp;-
                                            <asp:Label ID="LB_AGE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">POLICY NO</td>
                                        <td>
                                            <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="True"></asp:Label>&nbsp;-
                                            <asp:Label ID="LB_PRODUCT" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="LB_TC_ID" runat="server" Font-Bold="False" Visible="False"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>POLICY HOLDER</td>
                                        <td>
                                            <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>PERIOD</td>
                                        <td>
                                            <asp:Label ID="LB_PERIOD" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>SUM INSURED</td>
                                        <td>
                                            <asp:Label ID="LB_SUMINS" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>U/W CODE</td>
                                        <td>
                                            <asp:Label ID="LB_UWCODE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BT_PENDING" runat="server" CssClass="ASPButton" Text="PENDING" Width="100px" BackColor="Yellow" ForeColor="Red" OnClick="BT_PENDING_Click" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" Font-Bold="True" ForeColor="Blue" OnClick="BT_SAVE_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"><asp:Label runat="server" ID="LB_ERROR_SAVE" style="color:red; font-size:16px;"></asp:Label></td>
                                    </tr>
                                </table>                                
                            </td>
                            <td style="width: 40%;">
                                <asp:DataGrid ID="DGR_TRACK" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_TRACK_ItemCommand" ShowHeader="False">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMMENT" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 100px;">
                                                            <asp:Button ID="BT_TRACK" runat="server" CommandName="Next" CssClass="ASPButton" Width="100px" /></td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_COMMENT" runat="server" Height="25px" Width="98%" Visible="false" MaxLength="1000" TextMode="MultiLine" CssClass="ASPTextBox" placeholder="Type your reason comment ..."></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>

                                <table id="TBL_STAT" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px; text-align: left;">STATUS</td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="LB_STAT" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td style="text-align: left;">COMMENT</td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="LB_STATCOMMENT" runat="server"></asp:Label></td>
                                    </tr>
                                </table>

                                <table id="TBL_BL" runat="server" style="border-spacing: 0px; width: 100%;">
                                    <tr style="vertical-align: top;">
                                        <td style="text-align: left;" colspan="2">
                                            <asp:Label ID="LB_BLACKLIST" runat="server" Visible="false"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="TR_BLACKLIST" visible="false">
                <td>
                    <table runat="server" id="TBL_BLACKLIST" style="border-spacing:0px; width:100%;">
                        <tr style="vertical-align:top;">
                            <td style="text-align:left;padding:10px;">
                                <asp:Label ID="TEXT_BLACKLIST" Text="" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_BUTTONS" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 33%;">
                                            <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="SUBMISSION" Width="100%" OnClick="BT1_Click"  />
                                        </td>
                                        <td style="width: 33%;">
                                            <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="REMARKS & ARCHIEVE" Width="100%" OnClick="BT2_Click" />
                                        </td>
                                        <td style="width: 33%;">
                                            <asp:Button ID="BT7" runat="server" CssClass="ASPButton" Text="TRACKS" Width="100%" OnClick="BT7_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">
                                <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <center>
            <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
        </center>
    </form>
</body>
</html>
