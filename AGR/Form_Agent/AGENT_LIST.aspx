<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_LIST.aspx.cs" Inherits="AGR.AGENT_LIST" %>

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
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
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
                            <td>UPLINER1 NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_UPLINER" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>UPLINER2 NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_UPLINER2" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>STATUS APPROVAL</td>
                            <td>
                                <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_STATUS_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>AGENCY</td>
                            <td>
                                <asp:TextBox ID="TXT_AGENCY" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>RO NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_RO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">DIVISION IN CHARGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_MARKET" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_MARKET_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">LEVEL</td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="DDL_LEVEL" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                    <asp:ListItem Value="0">NOT ACTIVE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" OnClientClick="ShowProgress(); return true;" />
                                            &nbsp;<asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" BackColor="Green" Font-Bold="True" ForeColor="White" Visible="False" /></td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_RECORDS" runat="server"></asp:Label></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>



        <asp:DataGrid ID="DGR" runat="server" CellPadding="2" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid"
            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Xx-Small" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" VerticalAlign="Top" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
            <Columns>
                <asp:TemplateColumn HeaderText="AGENT CODE">
                    <ItemTemplate>
                        <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="AGENT_CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="STATUS_ACTIVE" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="STATUS_APPROVAL" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_NAME" HeaderText="FULLNAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                <asp:BoundColumn DataField="TGL_OTORISASI" HeaderText="JOIN DATE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <%--<asp:BoundColumn DataField="DOB" HeaderText="DOB">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="POB" HeaderText="POB"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER_NAME" HeaderText="UPLINER NAME"></asp:BoundColumn>--%>
                <asp:BoundColumn DataField="RO_NAME" HeaderText="RO NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER1_LEVEL" HeaderText="UPLINER1 LEVEL"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER1_NAME" HeaderText="UPLINER1 NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER2_LEVEL" HeaderText="UPLINER2 LEVEL"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER2_NAME" HeaderText="UPLINER2 NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENCY_NAME" HeaderText="AGENCY"></asp:BoundColumn>
                <asp:BoundColumn DataField="STATUS_APPROVAL_DESCR" HeaderText="STATUS"></asp:BoundColumn>
                <asp:BoundColumn DataField="STATUS_LISTING" HeaderText="TINGKAT RESIKO"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="ACTIVE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="40" />
                    <ItemTemplate>
                        <%--<asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="DB_CheckedChanged" />--%>
                        <asp:CheckBox ID="CB" runat="server" Enabled="false" />
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

