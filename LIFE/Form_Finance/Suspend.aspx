<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Suspend.aspx.cs" Inherits="LIFE.Form_Finance.Suspend" MaintainScrollPositionOnPostback="true" %>

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
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="DV_LOADING" class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>

        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <div style="width: 100%; height: 100vh; overflow: auto;">
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr style="vertical-align: top;">
                                <td style="width: 50%;">
                                    <asp:Button ID="BT_REFRESH" runat="server" Text="REFRESH IDENTIFICATION" CssClass="ASPButton" BackColor="Green" ForeColor="White" OnClick="BT_REFRESH_Click" OnClientClick="ShowProgress()" Width="150" />
                                    <asp:DataGrid ID="DGR_SUMM" runat="server" BackColor="White"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" PageSize="20" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_SUMM_ItemCommand">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                                        <Columns>
                                            <asp:BoundColumn DataField="NOREK" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="IDENTIFIED" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="UNIDENTIFIED" Visible="False">
                                                <HeaderStyle Width="150" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ACCNO" HeaderText="BANK ACCOUNT"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="IDENTIFIED">
                                                <HeaderStyle Width="80" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BT_IDENTIFIED" runat="server" CommandName="IDENTIFIED" OnClientClick="ShowProgress()" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="UN-IDENTIFIED">
                                                <HeaderStyle Width="80" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BT_UNIDENTIFIED" runat="server" CommandName="UNIDENTIFIED" OnClientClick="ShowProgress()" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="DV_RESULT" runat="server" visible="false" style="width: 100%;">
                                        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                            <tr>
                                                <td class="TDBGColor">
                                                    <asp:Label ID="LB_ACCNO" runat="server" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
                                                    <asp:DropDownList ID="DDL_ACCNO" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                                    <table style="border-spacing: 0px; width: 100%;">
                                                        <tr>
                                                            <td style="width: 100px;">POST DATE</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_POSTDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE1">
                                                                </ajaxToolkit:CalendarExtender>
                                                                &nbsp;-&nbsp;
                                                                <asp:TextBox ID="TXT_POSTDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE2">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="vertical-align: top;">
                                                            <td>DESCRIPTION</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                                                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" Width="100" OnClick="BT_SEARCH_Click" OnClientClick="ShowProgress()" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LB_RESULT" runat="server"></asp:Label></td>
                                                            <td style="text-align: right;">
                                                                <asp:Button ID="BT_SET" runat="server" CssClass="ASPButton" Text="SET FLAG" Width="100" OnClick="BT_SET_Click" />
                                                                <asp:DropDownList ID="DDL_FLAG" runat="server" CssClass="ASPDropDownList">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                                        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" Width="100%" ItemStyle-Wrap="true" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="15" CssClass="ASPDatagrid" AutoGenerateColumns="False">
                                            <AlternatingItemStyle BackColor="#F7F7F7" />
                                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                            <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                                            <Columns>
                                                <asp:BoundColumn DataField="TRXID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="ACCNO" HeaderText="ACC NO." Visible="false">
                                                    <HeaderStyle Width="150" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="POST_DATE" HeaderText="POST DATE">
                                                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                                    <HeaderStyle Width="70" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="50" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CB_ALL" runat="server" CssClass="ASPTextBox" Text="ALL" AutoPostBack="True" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Button ID="BT_SETTLE" runat="server" CssClass="ASPButton" Text="S" CommandName="Select" BackColor="Green" ForeColor="White" />
                                                        <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="width: 50%;">
                    <div id="DV_SELECTED" runat="server" visible="false" style="width: 100%; height: 100vh; overflow: auto;">
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

                        <table id="TBL_OUTSTADING" runat="server" visible="false" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                            <tr>
                                <td class="TDBGColor">OUTSTANDING LIST</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TXT_SEARCH" runat="server" CssClass="ASPTextBox" Width="98%" AutoPostBack="true" placeholder="Type POLICY NO/FULLNAME/PRODUCT to search .." OnTextChanged="TXT_SEARCH_TextChanged" onchange="ShowProgress()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 100%; height: 300px; overflow: auto;">

                                        <asp:DataGrid ID="DGR_SEARCH" runat="server" BackColor="White"
                                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" PageSize="20" CssClass="ASPDatagrid" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_SEARCH_ItemCommand">
                                            <AlternatingItemStyle BackColor="#F7F7F7" />
                                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                            <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                                            <Columns>
                                                <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemStyle Width="80" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="FULLNAME"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PRODUCT"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="TERM">
                                                    <ItemStyle Width="40" HorizontalAlign="Center" ForeColor="Green" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="OUTSTANDING_AMOUNT">
                                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                        </asp:DataGrid>

                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:LinkButton ID="LB_BACK" runat="server" Text="Back .." ForeColor="Red" Visible="false" OnClick="LB_BACK_Click" OnClientClick="ShowProgress()"></asp:LinkButton><br />
                        <iframe id="IF_REGNO" runat="server" visible="false" style="width: 98%; border-style: none;"></iframe>
                    </div>
                </td>
            </tr>
        </table>

        <asp:Button ID="BT_LOAD" runat="server" OnClientClick="ShowProgress()" OnClick="BT_LOAD_Click" Style="visibility: hidden;" />
    </form>
</body>
</html>
