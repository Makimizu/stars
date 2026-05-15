<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentProcess.aspx.cs" Inherits="LQ.Form_Finance.PaymentProcess" %>

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
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td style="width: 150px;">REQUEST DATE</td>
                <td>
                    <asp:TextBox ID="TXT_STARTDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE1">
                    </ajaxToolkit:CalendarExtender>
                    &nbsp;-&nbsp;
                    <asp:TextBox ID="TXT_STARTDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE2">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>DESCRIPTION</td>
                <td>
                    <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>TRANSACTION TYPE</td>
                <td>
                    <asp:DropDownList ID="DDL_TRANSTYPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>PAYMENT STATUS</td>
                <td>
                    <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="0">REQUESTED</asp:ListItem>
                        <asp:ListItem Value="1">APPROVED</asp:ListItem>
                        <asp:ListItem Value="2">PAID</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" Width="100px" OnClick="BT_SEARCH_Click" OnClientClick="ShowProgress()" />
                </td>
            </tr>
        </table>

        <asp:Label ID="LB_RESULT" runat="server"></asp:Label>

        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
            CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20" CssClass="ASPDatagrid">
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
            <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <Columns>
                <asp:BoundColumn DataField="REKAPID" HeaderText="IOM ID">
                    <ItemStyle Width="120" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DESCR" HeaderText="TRANSACTION DESCRIPTION"></asp:BoundColumn>
                <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" Width="60" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="REQUEST_DATE" HeaderText="REQUEST DATE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVALDATE" HeaderText="APPROVAL DATE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PAYMENT_DATE" HeaderText="PAYMENT DATE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT" HeaderText="AGENT"></asp:BoundColumn>
            </Columns>
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
        </asp:DataGrid>

    </form>
</body>
</html>
