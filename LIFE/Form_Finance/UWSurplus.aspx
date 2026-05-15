<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UWSurplus.aspx.cs" Inherits="LIFE.Form_Finance.UWSurplus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.blockUI.js"></script>
    
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
        $('.Load').live("click", function () {
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
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="DV_LOADING" class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 40%;">
                        <tr>
                            <td>UPLOAD FILE</td>
                            <td><input id="TXT_FILE_UPLOAD" runat="server" name="TXT_FILE_UPLOAD" type="file" class="ASPTextBox" /></td>
                            <td>
                                <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton Load" Font-Bold="True" ForeColor="Blue" OnClick="BT_UPLOAD_Click" Text="UPLOAD" />
                            </td>
                        </tr>
                        <tr>
                            <td>BATCH ID</td>
                            <td><asp:TextBox ID="TXT_BATCHID" runat="server" CssClass="ASPTextBox"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="BT_SUBMIT" runat="server" CssClass="ASPButton Load" Font-Bold="True" ForeColor="Green" OnClick="BT_SUBMIT_Click" Text="SUBMIT" />
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton Load" Font-Bold="True" ForeColor="Gray" OnClick="BT_SEARCH_Click" Text="SEARCH" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2"><asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label></td>
                        </tr>
                        <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" OnClick="BT_XLS_Click" BackColor="Green" Font-Bold="True" ForeColor="White" Visible="False" />
                                            <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%">
                        <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAME" HeaderText="NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_CODE" HeaderText="PRODUT CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUW_POLIS" HeaderText="SUW POLIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUW_SHF" HeaderText="SUW SHF"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUW_PRF" HeaderText="SUW PRF"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NO" HeaderText="ACC NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NAME" HeaderText="ACC NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_BANK" HeaderText="ACC BANK"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
