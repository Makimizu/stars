<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_DEDUCTION_APPROVAL.aspx.cs" Inherits="AGR.AGENT_DEDUCTION_APPROVAL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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



        function ShowAlert(msg) {
            alert(msg);
        }
    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: transparent;
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

        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td style="width: 150px;">AGENT CODE</td>
                <td>
                    <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>AGENT NAME</td>
                <td>
                    <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>DEDUCTION TYPE</td>
                <td>
                    <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_TYPE_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                </td>
            </tr>
        </table>


        <asp:Label ID="LB_RECORDS" runat="server" Font-Size="X-Small"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid"
            OnPageIndexChanged="DGR_PageIndexChanged" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
            <Columns>
                <asp:TemplateColumn HeaderText="AGENT CODE">
                    <HeaderStyle Width="100" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="DEDUCTION_ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTION_TYPE_DESCR" HeaderText="DEDUCTION TYPE"></asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTION_AMOUNT" HeaderText="AMOUNT">
                    <HeaderStyle Width="100" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="Green" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTION_TERM" HeaderText="#TERM"></asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTION_TERM_AMOUNT" HeaderText="TERM AMOUNT">
                    <HeaderStyle Width="100" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="Green" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REQUESTBY" HeaderText="REQUEST BY"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <HeaderStyle Width="160" />
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Button ID="BT_APPROVE" runat="server" Text="APPROVE" CssClass="ASPButton" Width="80" BackColor="Blue" ForeColor="White" CommandName="Approve" />
                        <asp:Button ID="BT_REJECT" runat="server" Text="REJECT" CssClass="ASPButton" Width="80" BackColor="Red" ForeColor="White" CommandName="Reject" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
        </asp:DataGrid>


    </form>
</body>
</html>


