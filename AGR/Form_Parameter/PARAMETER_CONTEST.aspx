<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_CONTEST.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_CONTEST" %>

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
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr id="TR_LIST" runat="server">
                <td>
                    <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW RECORD" BackColor="Blue" ForeColor="White" Width="100" OnClick="BT_NEW_Click" />
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="20" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" AllowPaging="True" OnItemCommand="DGR_ItemCommand" OnPageIndexChanged="DGR_PageIndexChanged">
                        <ItemStyle VerticalAlign="Top" BackColor="#EFF3FB" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENABLE_PROCESS" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="CONTEST TITLE">
                                <ItemStyle Width="200" Wrap="true" ForeColor="Blue" Font-Bold="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENCY" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PERIOD" HeaderText="PERIOD">
                                <HeaderStyle Width="130" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROCESS" HeaderText="PROCESS"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PARAMETER">
                                <HeaderStyle Width="110" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_GENERAL" runat="server" Font-Size="6pt" Width="50" CommandName="General" Text="GENERAL" /></td>
                                            <td>
                                                <asp:Button ID="BT_DETAIL" runat="server" Font-Size="6pt" Width="50" CommandName="Detail" Text="DETAIL" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_PRODUCT" runat="server" Font-Size="6pt" Width="50" CommandName="Product" Style="white-space: normal;" /></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="60" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_PROCESS" runat="server" Font-Size="6pt" Width="50" Text="PROCESS" CommandName="Process" BackColor="Green" ForeColor="White" />
                                    <asp:Button ID="BT_DELETE" runat="server" Font-Size="6pt" Width="50" Text="DELETE" CommandName="Delete" BackColor="Red" ForeColor="White" />
                                    <asp:Button ID="BT_REPORT" runat="server" Font-Size="6pt" Width="50" Text="REPORT" CommandName="Report" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr id="TR_NEW" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 160px;">CONTEST TITLE</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PRODUCTION PERIOD START</td>
                            <td>
                                <asp:TextBox ID="TXT_PERIOD_START" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PERIOD_START">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>PRODUCTION PERIOD END</td>
                            <td>
                                <asp:TextBox ID="TXT_PERIOD_END" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PERIOD_END">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>COPY FROM OTHER TITLE</td>
                            <td>
                                <asp:DropDownList ID="DDL_MASTER" runat="server" Font-Size="XX-Small" AutoPostBack="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_CREATE" runat="server" CssClass="ASPButton" Text="CREATE" Width="100" OnClick="BT_CREATE_Click" />&nbsp;
                                <asp:Button ID="BT_CANCEL" runat="server" CssClass="ASPButton" Text="CANCEL" ForeColor="Red" Width="100" OnClick="BT_CANCEL_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_DETAIL" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 160px;">CONTEST TITLE</td>
                            <td>
                                <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LB_DESCR" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>PRODUCTION PERIOD</td>
                            <td>
                                <asp:Label ID="LB_PERIOD" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button ID="BT_DETAIL_BACK" runat="server" CssClass="ASPButton" Text="BACK" ForeColor="Red" Width="100" OnClick="BT_DETAIL_BACK_Click" />
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr id="TR_AGENCY" runat="server" visible="false">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td class="TDBGColor">AGENCY</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TR_PRODUCT" runat="server" visible="false">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td class="TDBGColor">PRODUCT</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="border-spacing: 0px; width: 100%;">
                                                <tr>
                                                    <td class="TDBGColor" style="width: 50%;">UNSELECTED</td>
                                                    <td class="TDBGColor" style="width: 50%;">SELECTED</td>
                                                </tr>
                                                <tr style="vertical-align: top;">
                                                    <td>
                                                        <table style="border-spacing: 0px; width: 100%;">
                                                            <tr>
                                                                <td style="width: 130px;">PRODUCT GROUP</td>
                                                                <td>
                                                                    <asp:DropDownList ID="DDL_GROUP_UNSELECTED" Font-Size="XX-Small" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_GROUP_UNSELECTED_SelectedIndexChanged"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>PRODUCT NAME</td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_PRODUCT_0" runat="server" Font-Size="XX-Small" AutoPostBack="true" Width="90%" OnTextChanged="TXT_PRODUCT_0_TextChanged"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>PRODUCT STATUS</td>
                                                                <td>
                                                                    <asp:DropDownList ID="DDL_STAT_UNSELECTED" runat="server" Font-Size="XX-Small" AutoPostBack="true" OnSelectedIndexChanged="DDL_STAT_UNSELECTED_SelectedIndexChanged">
                                                                        <asp:ListItem Value="null">ALL STATUS</asp:ListItem>
                                                                        <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                                                        <asp:ListItem Value="0">NOT ACTIVE</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LB_PRODUCT_CNT_0" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                        <div style="width: 100%; height: 300px; overflow: auto;">
                                                            <asp:DataGrid ID="DGR_PRODUCT_0" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_PRODUCT_0_ItemCommand">
                                                                <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                                                <EditItemStyle BackColor="#7C6F57" />
                                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle
                                                                    Wrap="False" BackColor="#1C5E55" ForeColor="White" VerticalAlign="Top" />
                                                                <AlternatingItemStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="PRODUCT_CODE" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PRODUCT_NAME" HeaderText="PRODUCT NAME">
                                                                        <ItemStyle ForeColor="DarkGreen" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PRODUCT_GROUP" HeaderText="PRODUCT GROUP"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PRODUCT_STAT" HeaderText="ACTIVE">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundColumn>
                                                                    <asp:TemplateColumn>
                                                                        <ItemStyle HorizontalAlign="Right" Width="60" />
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <HeaderTemplate>
                                                                            <table style="border-spacing: 0px;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="BT_V" runat="server" Font-Size="XX-Small" Text="V" BackColor="Blue" ForeColor="White" CommandName="SelectAll" ToolTip="Select Checked items .." /></td>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="CB_UNSELECTED_ALL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_UNSELECTED_ALL_CheckedChanged" Style="width: 7px; height: 7px;" /></td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CB_UNSELECTED" runat="server" Style="width: 7px; height: 7px;" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                            </asp:DataGrid>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table style="border-spacing: 0px; width: 100%;">
                                                            <tr>
                                                                <td style="width: 130px;">PRODUCT GROUP</td>
                                                                <td>
                                                                    <asp:DropDownList ID="DDL_GROUP_SELECTED" runat="server" Font-Size="XX-Small" AutoPostBack="true" OnSelectedIndexChanged="DDL_GROUP_SELECTED_SelectedIndexChanged"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>PRODUCT NAME</td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_PRODUCT_1" runat="server" Font-Size="XX-Small" AutoPostBack="true" Width="90%" OnTextChanged="TXT_PRODUCT_1_TextChanged"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>PRODUCT STATUS</td>
                                                                <td>
                                                                    <asp:DropDownList ID="DDL_STAT_SELECTED" runat="server" Font-Size="XX-Small" AutoPostBack="true" OnSelectedIndexChanged="DDL_STAT_SELECTED_SelectedIndexChanged">
                                                                        <asp:ListItem Value="null">ALL STATUS</asp:ListItem>
                                                                        <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                                                        <asp:ListItem Value="0">NOT ACTIVE</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LB_PRODUCT_CNT_1" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                        <div style="width: 100%; height: 300px; overflow: auto;">
                                                            <asp:DataGrid ID="DGR_PRODUCT_1" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_PRODUCT_1_ItemCommand">
                                                                <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                                                <EditItemStyle BackColor="#7C6F57" />
                                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle
                                                                    Wrap="False" BackColor="#1C5E55" ForeColor="White" VerticalAlign="Top" />
                                                                <AlternatingItemStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="PRODUCT_CODE" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PRODUCT_NAME" HeaderText="PRODUCT NAME">
                                                                        <ItemStyle ForeColor="Blue" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PCT" Visible="false"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PREMIUM_TARGET" Visible="false"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PRODUCT_GROUP" HeaderText="PRODUCT GROUP" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PRODUCT_STAT" HeaderText="ACTIVE" Visible="False">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundColumn>
                                                                    <asp:TemplateColumn HeaderText="%">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="20" />
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TXT_PCT" runat="server" Font-Size="XX-Small" Width="90%" Style="text-align: right;"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="PREMIUM<BR>TARGET">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="50" />
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TXT_PREMIUM" runat="server" Font-Size="XX-Small" Width="90%" Style="text-align: right;" BackColor="LightYellow"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <ItemStyle HorizontalAlign="Right" Width="60" />
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <HeaderTemplate>
                                                                            <table style="border-spacing: 0px;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="BT_X" runat="server" Font-Size="XX-Small" Text="X" BackColor="Red" ForeColor="White" CommandName="DeleteAll" ToolTip="Remove Checked items .." /></td>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="CB_UNSELECTED_ALL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_UNSELECTED_ALL_CheckedChanged1" Style="width: 7px; height: 7px;" /></td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <table style="border-spacing: 0px;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="BT_S" runat="server" Font-Size="XX-Small" Text="S" BackColor="Green" ForeColor="White" CommandName="Save" ToolTip="Save % Value" /></td>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="CB_SELECTED" runat="server" Style="width: 7px; height: 7px;" /></td>
                                                                                </tr>
                                                                            </table>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                            </asp:DataGrid>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TR_GENERAL" runat="server" visible="false">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td class="TDBGColor">GENERAL PARAMETER</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SAVE_GENERAL" runat="server" Text="SAVE" CssClass="ASPButton" Width="100" BackColor="Blue" ForeColor="White" OnClick="BT_SAVE_GENERAL_Click" />
                                            <asp:DataGrid ID="DGR_GENERAL" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" Width="100%" ShowHeader="False">
                                                <ItemStyle VerticalAlign="Top" />
                                                <HeaderStyle
                                                    Wrap="False" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SINGLE_RECORD" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR">
                                                        <ItemStyle Width="130px" Font-Bold="False" Font-Size="XX-Small" BackColor="Gainsboro" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" ForeColor="Blue" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_GENERAL" runat="server" Width="80px" Style="text-align: center;" Font-Size="XX-Small" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="TXT_GENERAL_DATE" runat="server" Width="60px" Style="text-align: center;" Font-Size="XX-Small" Visible="false"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_GENERAL_DATE">
                                                            </ajaxToolkit:CalendarExtender>

                                                            <asp:DropDownList ID="DDL_GENERAL_VALUE" runat="server" Font-Size="XX-Small" Visible="false"></asp:DropDownList>
                                                            <asp:DataGrid ID="DGR_GENERAL_VALUE" runat="server" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" ShowHeader="False" GridLines="None" CellPadding="2">
                                                                <ItemStyle VerticalAlign="Middle" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="VAL2" Visible="False"></asp:BoundColumn>
                                                                    <asp:TemplateColumn>
                                                                        <ItemStyle Width="20" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CB_GENERAL" runat="server" Style="width: 7px; height: 7px;" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn Visible="false">
                                                                        <ItemStyle Width="30" />
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TXT_OTHVAL" runat="server" Style="text-align: center;" Font-Size="XX-Small" Width="80%"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TR_PARAMETER" runat="server" visible="false">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr style="vertical-align: top;">
                                        <td style="width: 160px;">
                                            <asp:DataGrid ID="DGR_CD" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" Width="100%" GridLines="None" ShowHeader="False" OnItemCommand="DGR_CD_ItemCommand">
                                                <ItemStyle VerticalAlign="Top" />
                                                <HeaderStyle
                                                    Wrap="False" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="SUB_CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STAT" Visible="false"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_CD" runat="server" CommandName="Select" Width="100%" Font-Size="XX-Small" Style="white-space: normal;" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemStyle Width="16" />
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_CD_X" runat="server" CommandName="Delete" Width="100%" Font-Size="XX-Small" Text="X" BackColor="Red" ForeColor="White" Visible="false" ToolTip="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                        <td>
                                            <div id="DV_PARAMETER" runat="server" visible="false">
                                                <table id="TBL_PARAMETER" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                                                    <tr>
                                                        <td class="TDBGColor">
                                                            <asp:Label ID="LB_AGENT_LEVEL" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="LB_PARAMETER_TITLE" runat="server" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="border-spacing: 0px; width: 100%;">
                                                                <tr style="vertical-align: top;">
                                                                    <td style="width: 70%;">
                                                                        <table style="border-spacing: 0px; width: 100%;">
                                                                            <tr>
                                                                                <td style="width: 160px;">AUTHORIZATION DATE</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TXT_AUTH_DATE_FROM" runat="server" Width="60px" Style="text-align: center;" Font-Size="XX-Small"></asp:TextBox>
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_AUTH_DATE_FROM">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                    &nbsp;-&nbsp;
                                                                    <asp:TextBox ID="TXT_AUTH_DATE_TO" runat="server" Width="60px" Style="text-align: center;" Font-Size="XX-Small"></asp:TextBox>
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_AUTH_DATE_TO">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:DataGrid ID="DGR_REWARD" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" GridLines="None" OnItemCommand="DGR_REWARD_ItemCommand" CellPadding="4" ForeColor="#333333" Width="100%">
                                                                            <ItemStyle VerticalAlign="Top" BackColor="#EFF3FB" />
                                                                            <EditItemStyle BackColor="#2461BF" />
                                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle
                                                                                Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                            <AlternatingItemStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundColumn DataField="SEQ" HeaderText="#">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle Width="20" HorizontalAlign="Center" />
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="REWARD" Visible="false"></asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="EXST" Visible="false"></asp:BoundColumn>
                                                                                <asp:TemplateColumn HeaderText="REWARD (FROM HIGH TO LOW RANK)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="TXT_REWARD" runat="server" Font-Size="XX-Small" Width="100%" MaxLength="255"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <ItemStyle Width="20" HorizontalAlign="Right" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Button ID="BT_REWARD_X" runat="server" CommandName="Delete" Font-Size="XX-Small" Text="X" BackColor="Red" ForeColor="White" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                        </asp:DataGrid>
                                                                        <asp:Button ID="BT_SAVE_PARAMETER" runat="server" Text="SAVE" CssClass="ASPButton" Width="100" BackColor="Blue" ForeColor="White" OnClick="BT_SAVE_PARAMETER_Click" />
                                                                    </td>
                                                                    <td style="vertical-align: middle;">
                                                                        <center>
                                                                            <asp:Button ID="BT_REWARD_AGENCY" runat="server" Text="AGENCY" Font-Size="XX-Small" Width="80%" OnClick="BT_REWARD_AGENCY_Click" /><br />
                                                                            <asp:Button ID="BT_REWARD_PROD_SOURCE" runat="server" Text="PRODUCTION SOURCE" Font-Size="XX-Small" Width="80%" OnClick="BT_REWARD_PROD_SOURCE_Click" /><br />
                                                                            <asp:Button ID="BT_REWARD_TRAINING" runat="server" Text="MINIMUM TRAINING" Font-Size="XX-Small" Width="80%" OnClick="BT_REWARD_TRAINING_Click" /><br />
                                                                            <asp:Button ID="BT_REWARD_DETAIL" runat="server" Text="REWARD CRITERIA" Font-Size="XX-Small" Width="80%" OnClick="BT_REWARD_DETAIL_Click" />
                                                                        </center>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <div id="DV_REWARD" runat="server" visible="false">
                                                                <table style="border-spacing: 0px; width: 100%;">
                                                                    <tr>
                                                                        <td class="TDBGColor">
                                                                            <asp:Label ID="LB_REWARD_TITLE" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="TR_REWARD_AGENCY" runat="server" visible="false">
                                                                        <td>
                                                                            <table style="border-spacing: 0px; width: 100%;">
                                                                                <tr>
                                                                                    <td class="TDBGColor" style="width: 50%;">UNSELECTED</td>
                                                                                    <td class="TDBGColor" style="width: 50%;">SELECTED</td>
                                                                                </tr>
                                                                                <tr style="vertical-align: top;">
                                                                                    <td>
                                                                                        <table style="border-spacing: 0px; width: 100%;">
                                                                                            <tr>
                                                                                                <td style="width: 130px;">AGENCY NAME</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TXT_AGENCY_0" runat="server" Font-Size="XX-Small" AutoPostBack="true" Width="90%" OnTextChanged="TXT_AGENCY_0_TextChanged"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>AREA NAME</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="DDL_AREA_0" runat="server" Font-Size="XX-Small" AutoPostBack="true" Width="90%" OnSelectedIndexChanged="DDL_AREA_0_SelectedIndexChanged"></asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="LB_AGENCY_CNT_0" runat="server" Text=""></asp:Label>
                                                                                                </td>
                                                                                                <td></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <div style="width: 100%; height: 300px; overflow: auto;">
                                                                                            <asp:DataGrid ID="DGR_AGENCY_0" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_AGENCY_0_ItemCommand">
                                                                                                <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                                                                                <EditItemStyle BackColor="#7C6F57" />
                                                                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                                                <HeaderStyle
                                                                                                    Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                                                                                                <AlternatingItemStyle BackColor="White" />
                                                                                                <Columns>
                                                                                                    <asp:BoundColumn DataField="COMPANY_CODE" Visible="false"></asp:BoundColumn>
                                                                                                    <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="AGENCY NAME"></asp:BoundColumn>
                                                                                                    <asp:BoundColumn DataField="AREA" HeaderText="AREA"></asp:BoundColumn>
                                                                                                    <asp:TemplateColumn>
                                                                                                        <HeaderStyle HorizontalAlign="Right" Width="60" />
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                        <HeaderTemplate>
                                                                                                            <table style="border-spacing: 0px;">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Button ID="BT_V" runat="server" Font-Size="XX-Small" Text="V" BackColor="Blue" CommandName="SelectAll" ForeColor="White" /></td>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="CB_UNSELECTED_ALL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_UNSELECTED_ALL_CheckedChanged2" Style="width: 7px; height: 7px;" /></td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </HeaderTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="CB_UNSELECTED" runat="server" Style="width: 7px; height: 7px;" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateColumn>
                                                                                                </Columns>
                                                                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                                            </asp:DataGrid>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table style="border-spacing: 0px; width: 100%;">
                                                                                            <tr>
                                                                                                <td style="width: 130px;">AGENCY NAME</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TXT_AGENCY_1" runat="server" Font-Size="XX-Small" AutoPostBack="true" Width="90%" OnTextChanged="TXT_AGENCY_1_TextChanged"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>AREA NAME</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="DDL_AREA_1" runat="server" Font-Size="XX-Small" AutoPostBack="true" Width="90%" OnSelectedIndexChanged="DDL_AREA_1_SelectedIndexChanged"></asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="LB_AGENCY_CNT_1" runat="server" Text=""></asp:Label>
                                                                                                </td>
                                                                                                <td></td>
                                                                                            </tr>
                                                                                        </table>

                                                                                        <div style="width: 100%; height: 300px; overflow: auto;">
                                                                                            <asp:DataGrid ID="DGR_AGENCY_1" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_AGENCY_1_ItemCommand">
                                                                                                <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                                                                                <EditItemStyle BackColor="#7C6F57" />
                                                                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                                                <HeaderStyle
                                                                                                    Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                                                                                                <AlternatingItemStyle BackColor="White" />
                                                                                                <Columns>
                                                                                                    <asp:BoundColumn DataField="COMPANY_CODE" Visible="false"></asp:BoundColumn>
                                                                                                    <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="AGENCY NAME"></asp:BoundColumn>
                                                                                                    <asp:BoundColumn DataField="AREA" HeaderText="AREA"></asp:BoundColumn>
                                                                                                    <asp:TemplateColumn>
                                                                                                        <HeaderStyle HorizontalAlign="Right" Width="60" />
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                        <HeaderTemplate>
                                                                                                            <table style="border-spacing: 0px;">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Button ID="BT_X" runat="server" Font-Size="XX-Small" Text="X" BackColor="Red" CommandName="DeleteAll" ForeColor="White" /></td>
                                                                                                                    <td>
                                                                                                                        <asp:CheckBox ID="CB_UNSELECTED_ALL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_UNSELECTED_ALL_CheckedChanged3" Style="width: 7px; height: 7px;" /></td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </HeaderTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="CB_SELECTED" runat="server" Style="width: 7px; height: 7px;" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateColumn>
                                                                                                </Columns>
                                                                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                                            </asp:DataGrid>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="TR_REWARD_PROD_SOURCE" runat="server" visible="false">
                                                                        <td>
                                                                            <asp:DataGrid ID="DGR_ANP" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" ShowHeader="False">
                                                                                <ItemStyle VerticalAlign="Top" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                                <EditItemStyle BackColor="#7C6F57" />
                                                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                                <HeaderStyle
                                                                                    Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                                                                                <AlternatingItemStyle BackColor="White" />
                                                                                <Columns>
                                                                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                                                    <asp:BoundColumn DataField="TAKEN" Visible="false"></asp:BoundColumn>
                                                                                    <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemStyle Width="20" HorizontalAlign="Right" />
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="CB_ANP" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                </Columns>
                                                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                            </asp:DataGrid>
                                                                            <asp:Button ID="BT_REWARD_PROD_SOURCE_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="100" BackColor="Blue" ForeColor="White" OnClick="BT_REWARD_PROD_SOURCE_SAVE_Click" />

                                                                        </td>
                                                                    </tr>
                                                                    <tr id="TR_REWARD_TRAINING" runat="server" visible="false">
                                                                        <td>
                                                                            <asp:DataGrid ID="DGR_TRAINING" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="Black" GridLines="None" ShowHeader="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px">
                                                                                <ItemStyle VerticalAlign="Top" />
                                                                                <FooterStyle BackColor="Tan" />
                                                                                <HeaderStyle
                                                                                    Wrap="False" BackColor="Tan" Font-Bold="True" />
                                                                                <AlternatingItemStyle BackColor="PaleGoldenrod" />
                                                                                <Columns>
                                                                                    <asp:BoundColumn DataField="TRAINING_CODE" Visible="False"></asp:BoundColumn>
                                                                                    <asp:BoundColumn DataField="TAKEN" Visible="false"></asp:BoundColumn>
                                                                                    <asp:BoundColumn DataField="TRAINING_NAME">
                                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#006600" />
                                                                                    </asp:BoundColumn>
                                                                                    <asp:BoundColumn DataField="TRAINING_LEVEL"></asp:BoundColumn>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemStyle Width="20" HorizontalAlign="Right" />
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="CB_TRAINING" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                </Columns>
                                                                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                                                                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                                            </asp:DataGrid>
                                                                            <asp:Button ID="BT_REWARD_TRAINING_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="100" BackColor="Blue" ForeColor="White" OnClick="BT_REWARD_TRAINING_SAVE_Click" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="TR_REWARD_DETAIL" runat="server" visible="false">
                                                                        <td>
                                                                            <table style="border-spacing: 0px;">
                                                                                <tr>
                                                                                    <td style="width: 100px;">REWARD</td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_REWARD" runat="server" AutoPostBack="true" BackColor="Yellow" Font-Size="XX-Small" OnSelectedIndexChanged="DDL_REWARD_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                                                                                    </td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>% PERSISTENCE</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_REWARD_PERSISTENCE" runat="server" Font-Size="XX-Small" Width="80px" Style="text-align: right;" BackColor="LightGreen" ForeColor="Green" Font-Bold="true"></asp:TextBox></td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>TARGET PREMIUM</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_REWARD_PREMIUM" runat="server" Font-Size="XX-Small" Width="80px" Style="text-align: right;" BackColor="#FFCCFF" Font-Bold="True" ForeColor="Red"></asp:TextBox>
                                                                                    </td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>TARGET POLICY</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_REWARD_POLICY" runat="server" Font-Size="XX-Small" Width="80px" Style="text-align: right;" BackColor="#FFCCFF" Font-Bold="True" ForeColor="Red"></asp:TextBox>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>TARGET PA</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_REWARD_PA" runat="server" Font-Size="XX-Small" Width="80px" Style="text-align: right;" BackColor="#FFCCFF" Font-Bold="True" ForeColor="Red"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>TARGET AA</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_REWARD_AA" runat="server" Font-Size="XX-Small" Width="80px" Style="text-align: right;" BackColor="#FFCCFF" Font-Bold="True" ForeColor="Red"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>TARGET NAA</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_REWARD_NAA" runat="server" Font-Size="XX-Small" Width="80px" Style="text-align: right;" BackColor="#FFCCFF" Font-Bold="True" ForeColor="Red"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>TARGET RECRUIT</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_REWARD_RECRUIT" runat="server" Font-Size="XX-Small" Width="80px" Style="text-align: right;" BackColor="#FFCCFF" Font-Bold="True" ForeColor="Red"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <asp:Button ID="BT_REWARD_DETAIL_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="100" BackColor="Blue" ForeColor="White" OnClick="BT_REWARD_DETAIL_SAVE_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
