<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationOutstanding.aspx.cs" Inherits="LIFE.Form_Finance.ApplicationOutstanding" %>

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
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="background-color: aqua;">
                                <br />
                                <asp:Label ID="LB_DESCR" runat="server"></asp:Label><br />
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">POST DATE</td>
                            <td>
                                <asp:Label ID="LB_POSTDATE" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>BALANCE</td>
                            <td>
                                <asp:Label ID="LB_BALANCE" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>RECORD ID</td>
                            <td>
                                <asp:Label ID="LB_TRXID" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_FULLNAME" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
                    <table>
                        <tr>
                            <td style="width: 150px;">POLICY NO/VIRTUAL ACC</td>
                            <td>
                                <asp:Label ID="LB_POLICYNO" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>PRODUCT</td>
                            <td>
                                <asp:Label ID="LB_PRODUCT" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>INSURANCE PERIOD</td>
                            <td>
                                <asp:Label ID="LB_IP" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>PAYMENT PERIOD</td>
                            <td>
                                <asp:Label ID="LB_PP" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>FREQUENCY OF PAYMENT</td>
                            <td>
                                <asp:Label ID="LB_FOP" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_REGNO" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" PageSize="20" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_REGNO_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="INVOICENO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PAYMENT_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENABLE_SETTLE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DUEDATE" HeaderText="DUE DATE">
                                <HeaderStyle Width="60" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="YEARSEQ" HeaderText="#YEAR">
                                <HeaderStyle Width="40" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGING" HeaderText="AGING (days)">
                                <HeaderStyle Width="60" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE" HeaderText="INVOICE TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Width="60" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="30" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderTemplate>
                                    <asp:Button ID="BT_SETTLE" runat="server" CssClass="ASPButton" Text="S" Width="100%" CommandName="Settle" BackColor="Green" ForeColor="White" OnClientClick="ShowProgress()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="CB" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
