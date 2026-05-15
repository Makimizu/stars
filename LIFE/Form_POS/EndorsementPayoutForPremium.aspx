<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementPayoutForPremium.aspx.cs" Inherits="LIFE.Form_POS.EndorsementPayoutForPremium" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_CURR" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_INVOICE_TYPE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">PREMIUM PAYMENT</td>
            </tr>
            <tr id="TR_LIST" runat="server">
                <td>
                    <asp:Button ID="BT_ADD" runat="server" Text="ADD RECORD" CssClass="ASPButton" Width="100" OnClick="BT_ADD_Click" />

                    <asp:DataGrid ID="DGR_LIST" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" PageSize="20" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_LIST_ItemCommand">
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="PAYMENT_CYCLE_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="THEDATE" HeaderText="CYCLE DATE">
                                <HeaderStyle Width="60" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE" HeaderText="INVOICE TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Width="80" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="20" />
                                <HeaderTemplate>
                                    <asp:Button ID="BT_ALL" runat="server" Text="X" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="DeleteAll" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr id="TR_SEARCH" runat="server" visible="false">
                <td>
                    <asp:LinkButton ID="LB_BACK" runat="server" ForeColor="Red" OnClick="LB_BACK_Click">Back ..</asp:LinkButton>
                    <asp:TextBox ID="TXT_SEARCH" runat="server" CssClass="ASPTextBox" Width="98%" AutoPostBack="true" placeholder="Type POLICY NO/FULLNAME/PRODUCT to search .." OnTextChanged="TXT_SEARCH_TextChanged" onchange="ShowProgress()"></asp:TextBox>

                    <asp:DataGrid ID="DGR_SEARCH" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" PageSize="40" CssClass="ASPDatagrid" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_SEARCH_ItemCommand" AllowPaging="True" OnPageIndexChanged="DGR_SEARCH_PageIndexChanged">
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="80" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Select" OnClientClick="ShowProgress()"></asp:LinkButton>
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
                            <asp:TemplateColumn>
                                <ItemStyle Width="30" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_TOP" CssClass="ASPDropDownList" runat="server"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>

                    <asp:DataGrid ID="DGR_SEARCH_PTIR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" PageSize="40" CssClass="ASPDatagrid" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_SEARCH_PTIR_ItemCommand" AllowPaging="True" OnPageIndexChanged="DGR_SEARCH_PTIR_PageIndexChanged">
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="PAYMENT_CYCLE_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="80" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Select" OnClientClick="ShowProgress()"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT">
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>

                    <table style="border-spacing: 0px; width: 100%;">
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
