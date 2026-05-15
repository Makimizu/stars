<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validasi_BMI_Batch.aspx.cs" Inherits="FINANCE.Form_Settlement.Validasi_BMI_Batch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.blockUI.js"></script>
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
        .auto-style2 {
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        
        <asp:Label ID="LB_ACCT_NAME" runat="server" Visible="False"></asp:Label>

        <asp:Label ID="LB_ACCT_NO" runat="server" Visible="False"></asp:Label>

        <div id="DV_LOADING" class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>Status :
                    <asp:Label ID="LB_STATUS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                <td></td>
            </tr>
            <tr style="display: none;">
                <td>
                    <table id="TB_TRANSFER" runat="server">
                        <tr>
                            <td style="width: 100px;">Transfer : </td>
                            <td>
                                <asp:DropDownList ID="DDL_TRANSFER" runat="server" CssClass="ASPDropDownList" AutoPostBack="True"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="BT_PROCESS" runat="server" CssClass="ASPButton" OnClick="BT_PROCESS_Click" Text="show" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TB_RESULT" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%">
                        <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" AutoPostBack="True" OnCheckedChanged="CB_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REKAPID" HeaderText="REKAPID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCNAME" HeaderText="DESTINATION NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCNO" visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DESTINATION ACC">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_ACCNO" class="ASPTextBox" runat="server"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <%--<asp:BoundColumn DataField="ACCBANKDESC" HeaderText="DESTINATION BANK"></asp:BoundColumn>--%>
                            <asp:TemplateColumn HeaderText="DESTINATION BANK">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_ACC_BANK_DESC" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_ACC_BANK_DESC_SelectedIndexChanged"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TRANSFER">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_TRANSFER_TYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ACCBANK" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SWIFTCODE" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_INQUIRY" BackColor="Green" ForeColor="White" runat="server" CommandName="Inquiry" CssClass="ASPButton" Text="INQUIRY" />
                                </ItemTemplate>
                             </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ACCNAME_INFO" HeaderText="DESTINATION NAME (From Bank)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRANSACTIONID_INFO" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCNO_INFO" HeaderText="DESTINATION ACC (From Bank)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK_INFO" HeaderText="DESTINATION BANK (From Bank)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SRNAME_INFO" HeaderText="SOURCE NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SRACC_INFO" HeaderText="SOURCE ACC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_INFO" HeaderText="TRANSFER AMOUNT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TFDESC_INFO" HeaderText="TRANSFER DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SOURCE_NAME" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SOURCE_ACC" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TF_DESC" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNTS" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KODEBANK" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCBANK_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TYPETRANSFER" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="ROW_NUMBER" class="ASPTextBox" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top: 10px;">
                    <asp:Button ID="BT_REGISTRATION" runat="server" CssClass="ASPButton" OnClick="BT_REGISTRATION_Click" Text="REGISTRATION" Visible="False" />
                    <asp:Button ID="BT_VERIFICATION" runat="server" CssClass="ASPButton" OnClick="BT_VERIFICATION_Click" Text="VERIFICATION" Visible="False" />
                    <asp:Button ID="BT_APPROVED" runat="server" CssClass="ASPButton" OnClick="BT_APPROVED_Click" Text="APPROVED" Visible="false" />
                    <asp:Button ID="BT_REJECT" runat="server" CssClass="ASPButton" OnClick="BT_REJECT_Click" Text="REJECT" Visible="False" />
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            Password
                        </td>
                        <td>:</td>
                        <td><asp:TextBox ID="TXT_PASS" TextMode="Password" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: left;"></asp:TextBox></td>
                        <td><asp:TextBox ID="TXT_TYPE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: left;" Visible="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td><asp:Button ID="BT_SUBMIT" runat="server" OnClick="BT_SUBMIT_Click" CssClass="ASPButton" Text="Submit" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>