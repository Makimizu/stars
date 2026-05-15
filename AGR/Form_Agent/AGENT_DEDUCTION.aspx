<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_DEDUCTION.aspx.cs" Inherits="AGR.AGENT_DEDUCTION" %>

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
        <div class="loading" align="center">
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td style="width: 150px;">AGENT</td>
                <td>
                    <asp:Label ID="LB_AGENT_CODE" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB_AGENT_NAME" runat="server" Font-Bold="True"></asp:Label><asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" OnClick="BT_SEARCH_Click" /></td>
            </tr>
            <tr>
                <td>DEDUCTION TYPE</td>
                <td>
                    <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>AMOUNT</td>
                <td>
                    <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Deduction File</td>
                <td>
                    <asp:FileUpload ID="DEDUC_DOC" runat="server" Width="200px" CssClass="ASPButton" />
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>REMARK</td>
                <td>
                    <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" Width="90%" TextMode="MultiLine" Height="40"></asp:TextBox></td>
            </tr>
        </table>
        <table id="TBL_ACC" runat="server" visible="false" style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td style="width: 150px;">ACCOUNT NO</td>
                <td>
                    <asp:TextBox ID="TXT_ACCNO" runat="server" Width="200" MaxLength="100" CssClass="ASPTextBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td>BANK</td>
                <td>
                    <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>ACCOUNT NAME</td>
                <td>
                    <asp:TextBox ID="TXT_ACCNAME" runat="server" Width="200" MaxLength="100" CssClass="ASPTextBox"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="80" OnClick="BT_SAVE_Click" />


        <br />
        <table id="TBL_PAYMENT" visible="false" runat="server" style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td style="width: 50%;" class="TDBGColor">PAYMENT SOURCE</td>
                <td style="width: 50%;" class="TDBGColor">PAYMENT TERM</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:DataGrid ID="DGR_PAYMENT_SOURCE" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False">
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SOURCE_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SOURCE_DESCR"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                    </asp:DataGrid>


                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>DUE DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_DUEDATE" runat="server" Width="80px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DUEDATE">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>AMOUNT</td>
                            <td>
                                <asp:TextBox ID="TXT_AMOUNT_INSERT" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_ADD_TERM" runat="server" Text="ADD PAYMENT TERM" CssClass="ASPButton" OnClick="BT_ADD_TERM_Click" /></td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="DGR_PAYMENT_TERM" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Width="90%" OnItemCommand="DGR_PAYMENT_TERM_ItemCommand">
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="DUEDATE" HeaderText="DUE DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE NO."></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="PAYMENT" HeaderText="PAYMENT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" Width="30" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Delete" Text="X" />
                                    <asp:Button ID="BT_D" runat="server" CssClass="ASPButton" BackColor="Yellow" ForeColor="Black" CommandName="Detail" Text="D" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE_TERM" runat="server" Text="SAVE PAYMENT TERM" CssClass="ASPButton" OnClick="BT_SAVE_TERM_Click" />
                </td>
            </tr>
        </table>


        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="90%" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 60px; border: outset 2px gray; padding: 5px; display: none; overflow: auto;">
                <asp:LinkButton ID="LB_BACK" runat="server" ForeColor="Red" Font-Size="X-Small" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;">Back ..</asp:LinkButton>
                <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">
                    <tr>
                        <td class="TDBGColor">UPLINER SEARCH</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">
                                <tr>
                                    <td style="width: 100px;">AGENT NAME</td>
                                    <td>
                                        <asp:TextBox ID="TXT_AGENTNAME" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>AGENCY NAME</td>
                                    <td>
                                        <asp:TextBox ID="TXT_AGENCY" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>LEVEL</td>
                                    <td>
                                        <asp:TextBox ID="TXT_LEVEL" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="BT_AGENT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" OnClick="BT_AGENT_SEARCH_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                            <asp:DataGrid ID="DGR_AGENT" runat="server" CellPadding="4"
                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" AllowPaging="True" OnItemCommand="DGR_AGENT_ItemCommand" OnPageIndexChanged="DGR_AGENT_PageIndexChanged">
                                <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="X-Small" />
                                <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="AGENT CODE">
                                        <ItemStyle Width="100" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LB_AGENT_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="CODE" HeaderText="AGENT CODE" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SUBCD_DESCR" HeaderText="FULLNAME"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AGENCY_NAME" HeaderText="AGENCY"></asp:BoundColumn>
                                </Columns>
                                <EditItemStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" />
                            </asp:DataGrid>

                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
