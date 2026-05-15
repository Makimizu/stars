<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Retur.aspx.cs" Inherits="FINANCE.Form_Settlement.Retur" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.blockUI.js"></script>
    
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
        $('.Load').live("click", function () {
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
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr id="TR1" runat="server">
                                        <td style="width: 120px;">APPLICATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr id="TR_TIPE" runat="server">
                                        <td>TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_TIPE_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>ACC SOURCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ACCSOURCE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_ACCSOURCE_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>STATUS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_STATUS_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">
                                            <asp:Label ID="Label2" runat="server" Text="DESTINATION ACC" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_DESTACC" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">DOC NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RETUR DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>READY PAID DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DATE3" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE3">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_DATE4" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE4">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton Load" OnClick="BT_SEARCH_Click" Text="SEARCH" Width="100px" />
                                            <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" OnClick="BT_XLS_Click" BackColor="Green" Font-Bold="True" ForeColor="White" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
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
                            <asp:TemplateColumn HeaderText="ACTION">
                                <ItemTemplate>
                                    <asp:Button ID="BT_PENDING" runat="server" BackColor="Yellow" CommandName="Pending" CssClass="ASPButton Load" Font-Bold="True" ForeColor="Black" Text="cancel" />
                                    <asp:Button ID="BT_RETUR" runat="server" BackColor="Blue" CommandName="Retur" CssClass="ASPButton Load" Font-Bold="True" ForeColor="White" Text="submit" />
                                    <asp:Button ID="BT_SAVE" runat="server" BackColor="Blue" CommandName="Save" CssClass="ASPButton Load" Font-Bold="True" ForeColor="White" Text="accept" />
                                    <asp:Button ID="BT_ROLLBACK" runat="server" BackColor="Blue" CommandName="Rollback" CssClass="ASPButton Load" Font-Bold="True" ForeColor="White" Text="back to user" Visible="false" />
                                </ItemTemplate>
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="RETUR_ID" HeaderText="RETUR ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REKAP_ID" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DOC NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_REKAPID" runat="server" CssClass="ASPLabel" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ACC_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_BANK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_BANK_CODE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="ACC NO">
                                <ItemTemplate>
                                        <asp:TextBox ID="TXT_ACC_NO" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        <asp:Label ID="LB_ACC_NO" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ACC NAME">
                                <ItemTemplate>
                                        <asp:TextBox ID="TXT_ACC_NAME" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        <asp:Label ID="LB_ACC_NAME" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ACC BANK">
                                <ItemTemplate>
                                        <%--<asp:TextBox ID="TXT_ACC_BANK" runat="server" CssClass="ASPTextBox"></asp:TextBox>--%>
                                        <asp:DropDownList ID="DDL_ACC_BANK" runat="server" CssClass="ASPDropDownList" AutoPostBack="false"></asp:DropDownList>
                                        <asp:Label ID="LB_ACC_BANK" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ACC_NO_BARU" HeaderText="ACC NO NEW"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NAME_BARU" HeaderText="ACC NAME NEW"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_BANK_BARU" HeaderText="ACC BANK NEW"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFICIARY" HeaderText="BENEFICIARY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRANS_TYPE" HeaderText="TRANS. TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REASON" HeaderText="REASON"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE" HeaderText="JENIS RETUR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS_BY" HeaderText="STATUS BY">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS_DATE" HeaderText="STATUS DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RETUR_BY" HeaderText="RETUR BY">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RETUR_DATE" HeaderText="RETUR DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAID_BY" HeaderText="PAID BY">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAID_DATE" HeaderText="PAID DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_BANK_CODE_BARU" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_F" Visible="false"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="300px" Width="600px" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td>
                                        <table style="border-spacing: 0px;">
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_RETURID" Font-Bold="true" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px;">DOC NO</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_DOCNO" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>ACC NO</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_ACCNO" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ACC NAME</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_ACCNAME" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ACC BANK</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_ACCBANK" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>TRANS. TYPE</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_TRANSTYPE" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton Load" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="DGR_DETAIL" runat="server" BackColor="LightGoldenrodYellow" BorderColor="#996633"
                                BorderWidth="1px" CellPadding="2"
                                GridLines="Vertical" CssClass="ASPDatagrid"
                                AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="600px" AllowPaging="True" ForeColor="Black" OnPageIndexChanged="DGR_DETAIL_PageIndexChanged">
                                <ItemStyle Wrap="true" VerticalAlign="Top" BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <AlternatingItemStyle BackColor="PaleGoldenrod" VerticalAlign="Top" Wrap="true" />
                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundColumn DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="USER_BY" HeaderText="USER_BY"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="USER_DATE" HeaderText="USER_DATE"></asp:BoundColumn>
                                </Columns>
                                <FooterStyle BackColor="Tan" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
