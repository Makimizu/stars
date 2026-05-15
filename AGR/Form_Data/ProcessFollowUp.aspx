<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessFollowUp.aspx.cs" Inherits="AGR.Form_Data.ProcessFollowUp" %>

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
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:DataGrid ID="DGR_CD" runat="server" PageSize="20" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" Width="100%" ShowHeader="False" OnItemCommand="DGR_CD_ItemCommand" GridLines="None" CellPadding="0">
                        <ItemStyle Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_CD" runat="server" Font-Size="X-Small" Width="100%" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle VerticalAlign="Top" />
                        <ItemStyle BackColor="#EFF3FB" />
                    </asp:DataGrid>
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="LB_CD" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                    <tr style="vertical-align: top;">
                                        <td style="width: 50%;">
                                            <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                                <tr>
                                                    <td style="width: 200px;">YEAR</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td>MONTH</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_MONTH" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td>UNPROCESS COMMISSION TYPE</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_COMM" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td>PRODUCT SEGMENT</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_SEGMENT" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_SEGMENT_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">RETAIL</asp:ListItem>
                                                            <asp:ListItem Value="1">CORPORATE</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td>PRODUCT</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_PRODUCT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%;">
                                            <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                                <tr>
                                                    <td style="width: 150px;">PRODUCTION YEAR</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_PRODYEAR" runat="server" CssClass="ASPDropDownList">
                                                            <asp:ListItem>&lt;= 2</asp:ListItem>
                                                            <asp:ListItem>&gt; 2</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td>AGENT CODE</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_AGENT_CODE" runat="server" Font-Size="XX-Small" Width="200"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>AGENT NAME</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_AGENT_NAME" runat="server" Font-Size="XX-Small" Width="200"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" Font-Size="XX-Small" Width="100" OnClick="BT_SEARCH_Click" OnClientClick="ShowProgress();" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Label ID="LB_CNT" runat="server" Font-Size="XX-Small"></asp:Label>
        <asp:DataGrid ID="DGR_PERIOD" runat="server" CellPadding="2" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="100%" AllowPaging="True" OnPageIndexChanged="DGR_PERIOD_PageIndexChanged">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="ID_SETTLEMENT" HeaderText="ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="NEW_SETTLEDATE" HeaderText="ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMM_TYPE_DESCR" HeaderText="COMMISSION<BR>TYPE"></asp:BoundColumn>
                <asp:BoundColumn DataField="SETTLEDATE" HeaderText="PRODUCTION&lt;BR&gt;DATE">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DUE_DATE" HeaderText="CYCLE&lt;BR&gt;DATE">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT" HeaderText="AGENT"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                <asp:BoundColumn DataField="YEARSEQ" HeaderText="#YEAR">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PRODUCT" HeaderText="PRODUCT"></asp:BoundColumn>
                <asp:BoundColumn DataField="PRODUCT_SEGMENT" HeaderText="SEGMENT"></asp:BoundColumn>
                <asp:BoundColumn DataField="TRANS_TYPE" HeaderText="TRANS TYPE">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="PAID&lt;BR&gt;AMOUNT">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                <asp:BoundColumn DataField="FOLLOWUP_PERIOD" HeaderText="FOLLOWUP&lt;BR&gt;PERIOD">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <HeaderStyle Width="30" HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderTemplate>
                        <asp:Button ID="BT_FOLLOWUP" runat="server" Text="FOLLOW UP" Font-Size="XX-Small" CommandName="FollowUp" />
                        <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_ALL_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CB" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Top" />
            <ItemStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
        </asp:DataGrid>
    </form>
</body>
</html>
