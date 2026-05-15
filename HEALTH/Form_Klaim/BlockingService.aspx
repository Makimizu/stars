<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlockingService.aspx.cs" Inherits="HEALTH.Form_Klaim.BlockingService" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
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
    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        .auto-style1 {
            width: 150px;
            height: 16px;
        }

        .auto-style2 {
            height: 16px;
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
        <table style="top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 33%;">
                                <asp:Button ID="BT1" runat="server" Text="FINDING LIST" CssClass="ASPButton" Width="100%" OnClick="BT1_Click" OnClientClick="ShowProgress();" />
                            </td>
                            <td style="width: 33%;">
                                <asp:Button ID="BT2" runat="server" Text="MARKED LIST" CssClass="ASPButton" Width="100%" OnClick="BT2_Click" OnClientClick="ShowProgress();" />
                            </td>
                            <td style="width: 33%;">
                                <asp:Button ID="BT3" runat="server" Text="BLOCKED LIST" CssClass="ASPButton" Width="100%" OnClick="BT3_Click" OnClientClick="ShowProgress();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR_FINDING" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20"
                        GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_FINDING_ItemCommand">
                        <ItemStyle Wrap="False" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundColumn DataField="URL_REPORT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL_OUTSTANDING" HeaderText="TOTAL OUTSTANDING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_COUNT" HeaderText="#">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MAX_AGING" HeaderText="MAX AGING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_D" runat="server" CssClass="ASPButton" Text="D" BackColor="Blue" ForeColor="White" ToolTip="Detail" CommandName="Detail" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                    </asp:DataGrid>
                    <asp:DataGrid ID="DGR_MARKED" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20"
                        GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_MARKED_ItemCommand">
                        <ItemStyle Wrap="False" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundColumn DataField="POLICY_ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EXCLUDED" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_REPORT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MARK_DATE" HeaderText="MARK DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL_OUTSTANDING" HeaderText="TOTAL OUTSTANDING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_COUNT" HeaderText="#">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MAX_AGING" HeaderText="MAX AGING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_D_MARKED" runat="server" CssClass="ASPButton" Text="D" BackColor="Blue" ForeColor="White" ToolTip="Detail" CommandName="Detail" />
                                    <asp:Button ID="BT_EXCLUDE" runat="server" CssClass="ASPButton" Text="E" BackColor="Yellow" ToolTip="Exclude" CommandName="Exclude" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                    </asp:DataGrid>
                    <asp:DataGrid ID="DGR_BLOCKED" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20"
                        GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_BLOCKED_ItemCommand">
                        <ItemStyle Wrap="False" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundColumn DataField="POLICY_ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BLOCKED_DATE" HeaderText="BLOCKED DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_REPORT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL_OUTSTANDING" HeaderText="TOTAL OUTSTANDING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_COUNT" HeaderText="#">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MAX_AGING" HeaderText="MAX AGING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_D_BLOCKED" runat="server" CssClass="ASPButton" Text="D" BackColor="Blue" ForeColor="White" ToolTip="Detail" CommandName="Detail" />
                                    <asp:Button ID="BT_UNBLOCK" runat="server" CssClass="ASPButton" Text="U" BackColor="Green" ForeColor="White" ToolTip="Unblock" CommandName="Unblock" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="180px" Width="70%" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">POLICY NO</td>
                        <td>
                            <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Label ID="LB_POLICY_ID" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>COMPANY</td>
                        <td>
                            <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="true"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>MARKED DATE</td>
                        <td>
                            <asp:Label ID="LB_MARKED_DATE" runat="server" Font-Bold="true"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>OVERDUE OUTSTANDING</td>
                        <td>
                            <asp:Label ID="LB_OUTSTANDING" runat="server" Font-Bold="true"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>MAX AGING</td>
                        <td>
                            <asp:Label ID="LB_AGING" runat="server" Font-Bold="true"></asp:Label></td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td>EXCLUDE REASON</td>
                        <td>
                            <asp:TextBox ID="TXT_EXCLUDE_REASON" runat="server" CssClass="ASPTextBox" Width="100%" MaxLength="255" TextMode="MultiLine" Height="50px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="BT_EXCLUDE_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_EXCLUDE_SAVE_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
