<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DATA_COMMISSION_SUMMARY.aspx.cs" Inherits="AGR.DATA_COMMISSION_SUMMARY" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../include/image/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td style="width: 180px;" class="TDBGColor">PERIOD</td>
                <td style="width: 10px;"></td>
                <td class="TDBGColor">SUMMARY DATA</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>CHANNEL</td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged" Width="100px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>YEAR</td>
                            <td>
                                <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                    </table>
                    <div style="height: 300px; overflow: auto; border: inset;">
                        <asp:DataGrid ID="DGR_PERIOD" runat="server" CellPadding="4" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" Width="100%">
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="START_DATE" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="END_DATE" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LB_PERIOD" runat="server" CommandName="Select"><i class="fa fa-search"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" />
                        </asp:DataGrid>
                    </div>

                </td>
                <td></td>
                <td>
                    <div id="DV_SUMMARY" runat="server" visible="false">
                        <div id="DV_PROCESS" runat="server">
                            <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                                <tr>
                                    <td>PERIOD</td>
                                    <td>
                                        <asp:Label ID="LB_STARTDATE" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="LB_ENDDATE" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">SEARCH AGENT</td>
                                    <td>
                                        <asp:TextBox ID="TXT_AGENT" runat="server" CssClass="ASPTextBox" Width="200" AutoPostBack="true" OnTextChanged="TXT_AGENT_TextChanged"></asp:TextBox></td>
                                </tr>
                            </table>
                            <table style="border-spacing: 0px; width: 100%; font-size: x-small; border-top: ridge;">
                                <tr>
                                    <td style="width: 49%;">
                                        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                                            <tr>
                                                <td class="TDBGColor">UNBOOKED</td>
                                                <td style="width: 100px;">
                                                    <asp:Button ID="BT_BOOKED" runat="server" Text="BOOK" BackColor="Blue" ForeColor="White" Width="100" Font-Size="X-Small" OnClick="BT_BOOKED_Click" OnClientClick="hourglass(); return true;" /></td>  <%--OnClientClick="ShowProgress();"--%>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                    <td style="width: 49%;">
                                        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                                            <tr>
                                                <td style="width: 200px;">
                                                    <asp:Button ID="BT_UNBOOK" runat="server" Text="UNBOOK" BackColor="Red" ForeColor="White" Font-Size="X-Small" OnClick="BT_UNBOOK_Click" OnClientClick="hourglass(); return true;" />  <%--OnClientClick="ShowProgress();"--%>
                                                    <asp:Button ID="BT_FINANCE" runat="server" Text="PROCEED TO PAYMENT" BackColor="Green" ForeColor="White" Font-Size="X-Small" OnClick="BT_FINANCE_Click" OnClientClick="hourglass(); return true;" />  <%--OnClientClick="ShowProgress();"--%>
                                                </td>
                                                <td class="TDBGColor">BOOKED</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 100px;">Total Records</td>
                                                <td>
                                                    <asp:Label ID="LB_RECORD_UNBOOK" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Total Amount</td>
                                                <td>
                                                    <asp:Label ID="LB_AMOUNT_UNBOOK" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 100px;">Total Records</td>
                                                <td>
                                                    <asp:Label ID="LB_RECORD_BOOK" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Total Amount</td>
                                                <td>
                                                    <asp:Label ID="LB_AMOUNT_BOOK" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="vertical-align: top;">
                                    <td>
                                        <asp:DataGrid ID="DGR_UNBOOKED" runat="server" CellPadding="4" PageSize="20"
                                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%" AllowPaging="True" OnPageIndexChanged="DGR_UNBOOKED_PageIndexChanged" OnItemCommand="DGR_UNBOOKED_ItemCommand">
                                            <ItemStyle Wrap="False" BackColor="#FFFBD6" Font-Size="X-Small" ForeColor="#333333" />
                                            <SelectedItemStyle BackColor="#FFCC66" ForeColor="Navy" Font-Bold="True" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <HeaderStyle BackColor="#990000" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                            <Columns>
                                                <asp:BoundColumn DataField="COMM_AGENT" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BANK_STAT" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Detail"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="COMM_AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="CASES" HeaderText="#CASE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ACCNO" HeaderText="ACC. NO" Visible="false">
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
                                        </asp:DataGrid>

                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:DataGrid ID="DGR_BOOKED" runat="server" CellPadding="4" PageSize="20"
                                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%" AllowPaging="True" OnPageIndexChanged="DGR_BOOKED_PageIndexChanged" OnItemCommand="DGR_BOOKED_ItemCommand">
                                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LB_BOOKED_SELECT" runat="server" CommandName="Detail"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="COMM_AGENT" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="COMM_AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="CASES" HeaderText="#CASE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="AMOUNT" HeaderText="GROSS">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="TAX" HeaderText="TAX">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DEDUCTION" HeaderText="DEDUCT.">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="NETT" HeaderText="NETT">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ACCNO" HeaderText="ACC. NO"></asp:BoundColumn>
                                            </Columns>
                                            <EditItemStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div id="DV_UNBOOKED" runat="server" visible="false">
                            <asp:LinkButton ID="LB_UNBOOKED_BACK" runat="server" Text="Back .." ForeColor="Red" OnClick="LB_BACK_Click"></asp:LinkButton>
                            <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                                <tr>
                                    <td class="TDBGColor">UNBOOKED PRODUCTION DATA</td>
                                </tr>
                            </table>
                            <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                                <tr>
                                    <td style="width: 130px;">PERIOD</td>
                                    <td>
                                        <asp:Label ID="LB_UNBOOKED_STARTDATE" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="LB_UNBOOKED_ENDDATE" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>AGENT</td>
                                    <td>
                                        <asp:Label ID="LB_UNBOOKED_AGENTCODE" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="LB_UNBOOKED_AGENTNAME" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>CHANNEL</td>
                                    <td>
                                        <asp:Label ID="LB_UNBOOKED_CHANNEL" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="LB_UNBOOKED_LEVEL" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Label ID="LB_UNBOOKED_RECORDS" runat="server"></asp:Label>
                            <asp:DataGrid ID="DGR_UNBOOKED_PERAGENT" runat="server" CellPadding="4" PageSize="20"
                                GridLines="None" CssClass="ASPDatagrid" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%" AllowPaging="True" OnPageIndexChanged="DGR_UNBOOKED_PERAGENT_PageIndexChanged">
                                <ItemStyle Wrap="False" BackColor="#FFFBD6" Font-Size="X-Small" ForeColor="#333333" />
                                <SelectedItemStyle BackColor="#FFCC66" ForeColor="Navy" Font-Bold="True" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <HeaderStyle BackColor="#990000" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
                            </asp:DataGrid>

                        </div>

                        <div id="DV_BOOKED" runat="server" visible="false">
                            <asp:LinkButton ID="LB_BOOKED_BACK" runat="server" Text="Back .." ForeColor="Red" OnClick="LB_BOOKED_BACK_Click"></asp:LinkButton>
                            <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                                <tr>
                                    <td class="TDBGColor">BOOKED COMMISSION</td>
                                </tr>
                            </table>
                            <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                                <tr style="vertical-align: top;">
                                    <td style="width: 50%;">
                                        <table style="border-spacing: 0px; width: 100%;">
                                            <tr>
                                                <td style="width: 130px;">PERIOD</td>
                                                <td>
                                                    <asp:Label ID="LB_BOOKED_STARTDATE" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="LB_BOOKED_ENDDATE" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>AGENT</td>
                                                <td>
                                                    <asp:Label ID="LB_BOOKED_AGENTCODE" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="LB_BOOKED_AGENTNAME" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>CHANNEL</td>
                                                <td>
                                                    <asp:Label ID="LB_BOOKED_CHANNEL" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="LB_BOOKED_LEVEL" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:DataGrid ID="DGR_BOOKED_PERAGENT_REKAP" runat="server" CellPadding="4" PageSize="20"
                                            GridLines="None" CssClass="ASPDatagrid" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="50%" ShowHeader="False">
                                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <EditItemStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Size="X-Small" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
                                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                        </asp:DataGrid>

                                    </td>
                                </tr>
                            </table>

                            <br />
                            <asp:Label ID="LB_BOOKED_RECORDS" runat="server"></asp:Label>
                            <asp:DataGrid ID="DGR_BOOKED_PERAGENT" runat="server" CellPadding="4" PageSize="20"
                                GridLines="None" CssClass="ASPDatagrid" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%" AllowPaging="True" OnPageIndexChanged="DGR_BOOKED_PERAGENT_PageIndexChanged">
                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <AlternatingItemStyle BackColor="White" />
                                <EditItemStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Size="X-Small" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            </asp:DataGrid>

                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
