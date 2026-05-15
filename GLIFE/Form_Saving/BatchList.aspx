<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchList.aspx.cs" Inherits="GLIFE.Form_Saving.BatchList" %>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 120px;">POLICY</td>
                            <td>
                                <asp:TextBox ID="TXT_POLICYSEARCH" runat="server" AutoPostBack="true" Width="100%" CssClass="ASPTextBox"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>UPLOAD DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_REGDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_REGDATE1">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-&nbsp;
                            <asp:TextBox ID="TXT_REGDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_REGDATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH BATCH" OnClick="BT_SEARCH_Click" Width="100px" />&nbsp;<asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW UPLOAD" OnClick="BT_NEW_Click" BackColor="Blue" ForeColor="White" Width="100px" /></td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_POLICY" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" OnItemCommand="DGR_POLICY_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_POLICY_PageIndexChanged" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="BATCH_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TC_DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INBOUND_TRX" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="POLICY NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_POLICY" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="POLICY HOLDER<BR>BRANCH"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL" HeaderText="UPLOADED<BR>EXPORTED">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CONTRIB" HeaderText="CONTRIBUTION">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FORMAT_DESCR" HeaderText="FORMAT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UPLOADBY" HeaderText="UPLOAD BY"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_RAW" runat="server" CssClass="ASPButton" Text="R" CommandName="Raw" />
                                    <asp:Button ID="BT_RK" runat="server" CssClass="ASPButton" Text="$" BackColor="Green" ForeColor="White" CommandName="RK" Visible="false" />
                                    <asp:Button ID="BT_DELETE" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="90%" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 60px; border: outset 2px gray; padding: 5px; display: none; overflow: auto;">
                <table id="TBL_BANK_STATEMENT" runat="server" style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_POLICY" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LB_BATCHID" runat="server" Visible="false"></asp:Label>

                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td style="width: 45%; border: inset;">
                                        <table style="border-spacing: 0px; width: 100%;">
                                            <tr>
                                                <td style="width: 100px;">POST DATE</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_POSTDATE" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;-
                                                    <asp:TextBox ID="TXT_POSTDATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE2">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>BANK ACCOUNT</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_NOREK" runat="server" CssClass="ASPDropDownList">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="BT_RK_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_RK_SEARCH_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 45%; border: inset;">
                                        <table style="border-spacing: 0px; width: 100%;">
                                            <tr>
                                                <td style="width: 180px;">DATA AMOUNT</td>
                                                <td>
                                                    <asp:Label ID="LB_DATA_AMOUNT" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 180px;">SELECTED BANK AMOUNT</td>
                                                <td>
                                                    <asp:Label ID="LB_BANK_AMOUNT" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <asp:DataGrid ID="DGR_SELECTED" runat="server" BackColor="White"
                                                BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                                                PageSize="40" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_SELECTED_ItemCommand">
                                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="false" />
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="POST_DATE" HeaderText="POST DATE">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BOOK_NAME" HeaderText="BANK ACCOUNT"></asp:BoundColumn>
                                                </Columns>
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                                    Mode="NumericPages" />
                                            </asp:DataGrid>
                                        </table>
                                    </td>
                                    <td style="width: 10%; border: inset; vertical-align: middle; text-align: center;">
                                        <asp:Button ID="BT_EXECUTE" runat="server" CssClass="ASPButton" Text="EXECUTE" OnClick="BT_EXECUTE_Click" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <div style="width: 100%; height: 100px; overflow: auto; border: ridge;">
                                <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                                    BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                                    PageSize="40" ItemStyle-Wrap="true" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" ShowHeader="False">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="false" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_STL" runat="server" BackColor="Green" CommandName="Settle" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="TRXID" HeaderText="TRXID" Visible="False">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="POST_DATE" HeaderText="POST DATE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION">
                                            <ItemStyle Wrap="true" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BOOK_NAME" HeaderText="BANK ACCOUNT"></asp:BoundColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                        Mode="NumericPages" />
                                </asp:DataGrid>

                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
