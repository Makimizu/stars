<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_PRODUCTION_TRANSFER_DETAIL.aspx.cs" Inherits="AGR.Form_Agent.AGENT_PRODUCTION_TRANSFER_DETAIL" %>

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
        <asp:Label runat="server" ID="LB_CODE" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td>
                    <div style="width: 100%; height: 100px; overflow: auto;">
                        <asp:DataGrid ID="DGR" runat="server" CellPadding="2" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Xx-Small" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                        </asp:DataGrid>
                    </div>

                    <%--<table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 80px;">AGENT CODE</td>
                            <td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:Label runat="server" ID="LB_FULLNAME" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>LEVEL</td>
                            <td>
                                <asp:Label runat="server" ID="LB_LEVEL"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>BRANCH</td>
                            <td>
                                <asp:Label runat="server" ID="LB_BRANCH"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>UPLINER</td>
                            <td>
                                <asp:Label runat="server" ID="LB_UPLINER"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 100px;">TOTAL CASES</td>
                            <td>
                                <asp:Label runat="server" ID="LB_CASES" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>TOBE TRANSFERED</td>
                            <td>
                                <asp:Label runat="server" ID="LB_TCASES" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>LAST REQUEST BY</td>
                            <td>
                                <asp:Label runat="server" ID="LB_REQBY"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>LAST REQUEST DATE</td>
                            <td>
                                <asp:Label runat="server" ID="LB_REQDATE"></asp:Label>
                            </td>
                        </tr>
                    </table>--%>
                </td>
                <td style="width: 300px;">
                    <asp:FileUpload ID="FU" runat="server" CssClass="ASPTextBox" />
                    <asp:Button runat="server" ID="BT_UPLOAD" CssClass="ASPButton" Text="UPLOAD" ForeColor="White" BackColor="Blue" Width="100" OnClick="BT_UPLOAD_Click" OnClientClick="ShowProgress();" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
