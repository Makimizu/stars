<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_REMUN_CORPORATE.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_REMUN_CORPORATE" %>

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
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="DV_LOADING" class="loading" align="center">
            <img src="../include/image/Loader_chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_GROUP" runat="server" Visible="false"></asp:Label>
        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>SEARCH COMPANY NAME&nbsp;&nbsp;:&nbsp;<asp:TextBox ID="TXT_COMPANY" runat="server" AutoPostBack="true" CssClass="ASPTextBox" MaxLength="100" Width="300" OnTextChanged="TXT_COMPANY_TextChanged"></asp:TextBox>&nbsp;<asp:Button ID="BT_REPORT" runat="server" CssClass="ASPButton" Text="REPORT" Width="100" OnClick="BT_REPORT_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DGR" runat="server" CellPadding="1" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" OnItemCommand="DGR_ItemCommand" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellSpacing="1">
            <ItemStyle BackColor="#E7E7FF" Font-Size="XX-Small" ForeColor="#4A3C8C" />
            <SelectedItemStyle BackColor="#738A9C" ForeColor="#F7F7F7" Font-Bold="True" />
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" VerticalAlign="Top" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="False" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
            <Columns>
                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_CODE" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER1" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER2" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER3" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="RO_LEADER" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_CODE_NAME" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER1_NAME" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER2_NAME" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER3_NAME" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="RO_LEADER_NAME" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMM" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="OR1" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="OR2" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="OR3" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="BA" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="ACTIVE_PERIOD" Visible="false"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" Width="40" />
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderTemplate>
                        <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" Text="SAVE" Font-Size="XX-Small" OnClientClick="ShowProgress();" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CB" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="POLICY_HOLDER" HeaderText="POLICY HOLDER">
                    <ItemStyle ForeColor="Blue" Width="300" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PERIOD" HeaderText="PERIOD">
                    <ItemStyle Width="100" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="AGENT">
                    <HeaderStyle Width="150" />
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>CODE</td>
                                <td>
                                    <asp:TextBox ID="TXT_AGENTCODE" runat="server" CssClass="ASPTextBox" Width="60"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>COMM</td>
                                <td>
                                    <asp:TextBox ID="TXT_COMM" runat="server" CssClass="ASPTextBoxNumber" Width="35"></asp:TextBox>&nbsp;%</td>
                            </tr>
                        </table>
                        <asp:Label ID="LB_AGENTNAME" runat="server" ForeColor="Green"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="UPLINER 1">
                    <HeaderStyle Width="150" />
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>CODE</td>
                                <td>
                                    <asp:TextBox ID="TXT_UPLINER1" runat="server" CssClass="ASPTextBox" Width="60"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>OR 1</td>
                                <td>
                                    <asp:TextBox ID="TXT_OR1" runat="server" CssClass="ASPTextBoxNumber" Width="35"></asp:TextBox>&nbsp;%</td>
                            </tr>
                        </table>
                        <asp:Label ID="LB_UPLINER1NAME" runat="server" ForeColor="Green"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="UPLINER 2">
                    <HeaderStyle Width="150" />
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>CODE</td>
                                <td>
                                    <asp:TextBox ID="TXT_UPLINER2" runat="server" CssClass="ASPTextBox" Width="60"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>OR 2</td>
                                <td>
                                    <asp:TextBox ID="TXT_OR2" runat="server" CssClass="ASPTextBoxNumber" Width="35"></asp:TextBox>&nbsp;%</td>
                            </tr>
                        </table>
                        <asp:Label ID="LB_UPLINER2NAME" runat="server" ForeColor="Green"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="UPLINER 3">
                    <HeaderStyle Width="150" />
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>CODE</td>
                                <td>
                                    <asp:TextBox ID="TXT_UPLINER3" runat="server" CssClass="ASPTextBox" Width="60"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>OR 3</td>
                                <td>
                                    <asp:TextBox ID="TXT_OR3" runat="server" CssClass="ASPTextBoxNumber" Width="35"></asp:TextBox>&nbsp;%</td>
                            </tr>
                        </table>
                        <asp:Label ID="LB_UPLINER3NAME" runat="server" ForeColor="Green"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="RO LEADER">
                    <HeaderStyle Width="150" />
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>CODE</td>
                                <td>
                                    <asp:TextBox ID="TXT_ROLEADER" runat="server" CssClass="ASPTextBox" Width="60"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>BA</td>
                                <td>
                                    <asp:TextBox ID="TXT_BA" runat="server" CssClass="ASPTextBoxNumber" Width="35"></asp:TextBox>&nbsp;%</td>
                            </tr>
                        </table>
                        <asp:Label ID="LB_ROLEADERNAME" runat="server" ForeColor="Green"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Font-Size="X-Small" Mode="NumericPages" />
        </asp:DataGrid>


    </form>
</body>
</html>
