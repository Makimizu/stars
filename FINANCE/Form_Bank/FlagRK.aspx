<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlagRK.aspx.cs" Inherits="FINANCE.Form_Bank.FlagRK" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
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
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="DV_LOADING" class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>MODE&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DDL_MODE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_MODE_SelectedIndexChanged" OnChange="ShowProgress()">
                    <asp:ListItem>FLAG</asp:ListItem>
                    <asp:ListItem>UNFLAG</asp:ListItem>
                </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="DV_FLAG" runat="server">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px;">
                            <tr style="vertical-align: top;">
                                <td>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 100px;">IN/OUT</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_INOUT" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_INOUT_SelectedIndexChanged" OnChange="ShowProgress()">
                                                    <asp:ListItem Value="and a.CREDIT&gt;0">INBOUND</asp:ListItem>
                                                    <asp:ListItem Value="and a.DEBET&gt;0">OUTBOUND</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>ACC NO</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_NOREK" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_NOREK_SelectedIndexChanged" OnChange="ShowProgress()">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>POST DATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_POSTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE">
                                                </ajaxToolkit:CalendarExtender>
                                                &nbsp;-
                                            <asp:TextBox ID="TXT_POSTDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE2">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>DESCRIPTION</td>
                                            <td>
                                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" Width="100px" OnClientClick="ShowProgress()" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20px;"></td>
                                <td>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 100px;">RECORDS</td>
                                            <td>:
                                            <asp:Label ID="LB_RECORDS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>TOTAL AMOUNT</td>
                                            <td>:
                                            <asp:Label ID="LB_AMOUNT" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:DropDownList ID="DDL_FLAG" runat="server" CssClass="ASPDropDownList">
                        </asp:DropDownList>
                        <asp:Button ID="BT_SET" runat="server" BackColor="#0033CC" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S" OnClick="BT_SET_Click" />
                        
                        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                            BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                            PageSize="40" OnPageIndexChanged="DGR_PageIndexChanged" ItemStyle-Wrap="true" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" AllowPaging="True">
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" Wrap="false" />
                            <Columns>
                                <asp:BoundColumn DataField="TRXID" HeaderText="TRXID">
                                    <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="POST_DATE" HeaderText="POST DATE">
                                    <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION">
                                    <HeaderStyle Width="400px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Wrap="true" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                    <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CB_ALL" runat="server" CssClass="ASPTextBox" Text="ALL" AutoPostBack="True" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                        <%-- Settlement Retur di STARS - ANDEZ 09-06-2023 START --%>
                                        <asp:Button ID="BT_RETUR" runat="server" BackColor="Red" CommandName="Retur" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="R" />
                                        <%-- Settlement Retur di STARS - ANDEZ 09-06-2023 END --%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                        <asp:Button ID="BT_BREAKDOWN" runat="server" BackColor="Green" CommandName="Breakdown" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="B" ToolTip="Breakdown" Visible="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                Mode="NumericPages" />
                        </asp:DataGrid>

                    </td>
                </tr>
            </table>
        </div>


        <div id="DV_UNFLAG" runat="server" visible="false">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td style="width: 100px;">IN/OUT</td>
                    <td>
                        <asp:DropDownList ID="DDL_UNFLAG_DC" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_UNFLAG_DC_SelectedIndexChanged" OnChange="ShowProgress()">
                            <asp:ListItem Value="C">INBOUND</asp:ListItem>
                            <asp:ListItem Value="D">OUTBOUND</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>ACC NO</td>
                    <td>
                        <asp:DropDownList ID="DDL_UNFLAG_NOREK" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_UNFLAG_NOREK_SelectedIndexChanged" OnChange="ShowProgress()">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>POST DATE</td>
                    <td>
                        <asp:TextBox ID="TXT_UNLFAG_POSTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_UNLFAG_POSTDATE">
                        </ajaxToolkit:CalendarExtender>
                        &nbsp;-
                                    <asp:TextBox ID="TXT_UNLFAG_POSTDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_UNLFAG_POSTDATE2">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>DESCRIPTION</td>
                    <td>
                        <asp:TextBox ID="TXT_UNFLAG_DESCR" runat="server" CssClass="ASPTextBox" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>FLAG</td>
                    <td>
                        <asp:TextBox ID="TXT_UNFLAG_VALIDATION" runat="server" CssClass="ASPTextBox" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="BT_UNFLAG_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_UNFLAG_SEARCH_Click" Text="SEARCH" Width="100px" OnClientClick="ShowProgress()" />
                    </td>
                </tr>
            </table>
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="LB_UNFLAG_RECORDS" runat="server"></asp:Label></td>
                    <td style="text-align: right;">
                        <asp:Button ID="BT_UNFLAG" runat="server" Text="UNFLAG" CssClass="ASPButton" Width="100" BackColor="Red" ForeColor="White" OnClick="BT_UNFLAG_Click" /></td>
                </tr>
            </table>

            <asp:DataGrid ID="DGR_UNFLAG" runat="server" BackColor="White"
                BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                PageSize="20" OnPageIndexChanged="DGR_UNFLAG_PageIndexChanged" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnItemCommand="DGR_UNFLAG_ItemCommand">
                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <AlternatingItemStyle BackColor="#F7F7F7" />
                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" Wrap="false" />
                <Columns>
                    <asp:BoundColumn DataField="TRXID" HeaderText="TRXID">
                        <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="100px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="POST_DATE" HeaderText="POST DATE">
                        <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION">
                        <HeaderStyle Width="400px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle Wrap="true" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="BANK" HeaderText="ACCNO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="VALIDASI_DESCR" HeaderText="FLAG"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DC" HeaderText="IN/OUT"></asp:BoundColumn>
                    <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                        <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:CheckBox ID="CB_UNFLAG_ALL" runat="server" CssClass="ASPTextBox" Text="ALL" AutoPostBack="True" OnCheckedChanged="CB_UNFLAG_ALL_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CB_UNFLAG" runat="server" CssClass="ASPTextBox" />
                            <asp:Button ID="BT_BREAKDOWN_UNFLAG" Visible="false" runat="server" BackColor="Green" CommandName="BreakdownUnflag" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="B" ToolTip="Breakdown" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:TemplateColumn>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                    Mode="NumericPages" />
            </asp:DataGrid>

        </div>
        <%--ANDEZ--%>
        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="ReturPopup" runat="server" BackColor="White" Height="300px" Width="600px" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td>
                                        <asp:Label runat="server" ID="IDGROUP" Font-Bold="true" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="DGR_RETUR" runat="server" BackColor="LightGoldenrodYellow" BorderColor="#996633"
                                BorderWidth="1px" CellPadding="2"
                                GridLines="Vertical" CssClass="ASPDatagrid"
                                AutoGenerateColumns="False" OnItemCommand="DGR_RETUR_ItemCommand" Width="600px" AllowPaging="True" ForeColor="Black" OnPageIndexChanged="DGR_RETUR_PageIndexChanged">
                                <ItemStyle Wrap="true" VerticalAlign="Top" BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <AlternatingItemStyle BackColor="PaleGoldenrod" VerticalAlign="Top" Wrap="true" />
                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundColumn DataField="RETUR_ID" HeaderText="RETUR_ID" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="RETUR_ID_GROUP" HeaderText="RETUR_ID_GROUP" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="REKAP_ID" HeaderText="DOC NO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ACC_NO" HeaderText="ACC NO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ACC_NAME" HeaderText="ACC NAME"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Red" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        REASON
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Button ID="BT_SAVE" runat="server" BackColor="Blue" CommandName="Save" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="save" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                                <asp:DropDownList ID="DDL_REASON" runat="server" CssClass="ASPDropDownList" AutoPostBack="false" >
                                                </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <FooterStyle BackColor="Tan" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <%--ANDEZ--%>
    </form>
</body>
</html>
