<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DATA_COMMISSION.aspx.cs" Inherits="AGR.Form_Data.DATA_COMMISSION" %>

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
        <div class="loading" align="center">
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                        <tr>
                            <td style="width: 130px;">YEAR</td>
                            <td>
                                <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>DIVISION IN CHARGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged" Width="100px"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DGR_PERIOD" runat="server" CellPadding="3" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="URL_SUMMARY" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="URL_DETAIL" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE"></asp:BoundColumn>
                <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                <asp:BoundColumn DataField="PROCESSED" HeaderText="#PROCESSED&lt;BR&gt;RECORDS">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FINANCE" HeaderText="#FINANCE&lt;BR&gt;RECORDS">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GROSS_AMOUNT" HeaderText="GROSS AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TAX" HeaderText="TAX">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OTH" HeaderText="OTHER DEDUCTION">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NETT" HeaderText="NETT AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="HOLD" HeaderText="HOLD AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Black" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="100" />
                    <ItemTemplate>
                        <asp:Button ID="BT_SUMMARY" runat="server" Text="SUMMARY" CssClass="ASPButton" CommandName="Summary" BackColor="Blue" ForeColor="White" Width="100%" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="100" />
                    <ItemTemplate>
                        <asp:Button ID="BT_DETAIL" runat="server" Text="DETAIL" CssClass="ASPButton" CommandName="Detail" BackColor="Green" ForeColor="White" Width="100%" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Top" Font-Size="XX-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <ItemStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
        </asp:DataGrid>

        <table id="TBL_EXPORTALL" cellspacing="0" cellpadding="2" style="color: #333333; font-size: X-Small; font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;" runat="server" visible="false">
            <tbody>
                <tr valign="top" style="color: #000000; background-color: #cfd4f2; font-size: XX-Small;">
                    <td colspan="7"></td>                    
                    <td align="right"><asp:HyperLink ID="ExportLink" runat="server" NavigateUrl="../../ReportViewer/Viewer.aspx?APPID=AGR&CODE=50" Font-Bold="True">Export</asp:HyperLink> </td>
                    <td align="center" style="font-weight:bold; width: 40px;">&nbsp;&nbsp;</td>
                </tr>
            </tbody>
        </table>
        <asp:DataGrid ID="DGR_PERIOD_ALL" runat="server" CellPadding="3" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <%--<asp:BoundColumn DataField="URL_SUMMARY" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="URL_DETAIL" Visible="false"></asp:BoundColumn>--%>
                <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE"></asp:BoundColumn>
                <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                <asp:BoundColumn DataField="PROCESSED" HeaderText="#PROCESSED&lt;BR&gt;RECORDS&lt;BR&gt;BASIC COMM&lt;BR&gt; AND OVERRIDING">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PROCESSED_2b" HeaderText="#PROCESSED&lt;BR&gt;RECORDS&lt;BR&gt;BONUS ROYALTY">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PROCESSED_2c" HeaderText="#PROCESSED&lt;BR&gt;RECORDS&lt;BR&gt;BONUS RECRUITMENT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FINANCE" HeaderText="#FINANCE&lt;BR&gt;RECORDS&lt;BR&gt;BASIC COMM&lt;BR&gt; AND OVERRIDING">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FINANCE_2b" HeaderText="#FINANCE&lt;BR&gt;RECORDS&lt;BR&gt;BONUS ROYALTY">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FINANCE_2c" HeaderText="#FINANCE&lt;BR&gt;RECORDS&lt;BR&gt;BONUS RECRUITMENT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GROSS_AMOUNT" HeaderText="GROSS AMOUNT&lt;BR&gt;BASIC COMM&lt;BR&gt; AND OVERRIDING">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GROSS_AMOUNT_2b" HeaderText="GROSS AMOUNT&lt;BR&gt;BONUS ROYALTY">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GROSS_AMOUNT_2c" HeaderText="GROSS AMOUNT&lt;BR&gt;BONUS RECRUITMENT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TAX" HeaderText="TAX&lt;BR&gt;BASIC COMM&lt;BR&gt; AND OVERRIDING">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TAX_2b" HeaderText="TAX&lt;BR&gt;BONUS ROYALTY">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TAX_2c" HeaderText="TAX&lt;BR&gt;BONUS RECRUITMENT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OTH" HeaderText="OTHER DEDUCTION&lt;BR&gt;BASIC COMM&lt;BR&gt; AND OVERRIDING">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OTH_2b" HeaderText="OTHER DEDUCTION&lt;BR&gt;BONUS ROYALTY">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OTH_2c" HeaderText="OTHER DEDUCTION&lt;BR&gt;BONUS RECRUITMENT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NETT" HeaderText="NETT AMOUNT&lt;BR&gt;BASIC COMM&lt;BR&gt; AND OVERRIDING">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NETT_2b" HeaderText="NETT AMOUNT&lt;BR&gt;BONUS ROYALTY">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NETT_2c" HeaderText="NETT AMOUNT&lt;BR&gt;BONUS RECRUITMENT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="HOLD" HeaderText="HOLD AMOUNT&lt;BR&gt;BASIC COMM&lt;BR&gt; AND OVERRIDING">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Black" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="HOLD_2b" HeaderText="HOLD AMOUNT&lt;BR&gt;BONUS ROYALTY">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Black" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="HOLD_2c" HeaderText="HOLD AMOUNT&lt;BR&gt;BONUS RECRUITMENT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Black" />
                </asp:BoundColumn>
                <%--<asp:TemplateColumn>
                    <ItemStyle Width="100" />
                    <ItemTemplate>
                        <asp:Button ID="BT_SUMMARY" runat="server" Text="SUMMARY" CssClass="ASPButton" CommandName="Summary" BackColor="Blue" ForeColor="White" Width="100%" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="100" />
                    <ItemTemplate>
                        <asp:Button ID="BT_DETAIL" runat="server" Text="DETAIL" CssClass="ASPButton" CommandName="Detail" BackColor="Green" ForeColor="White" Width="100%" />
                    </ItemTemplate>
                </asp:TemplateColumn>--%>
            </Columns>
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Top" Font-Size="XX-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <ItemStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
        </asp:DataGrid>
    </form>
</body>
</html>
