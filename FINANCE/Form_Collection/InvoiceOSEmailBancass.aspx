<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceOSEmailBancass.aspx.cs" Inherits="FINANCE.Form_Collection.InvoiceOSEmailBancass" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
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
            background-color: #7697c2;
            z-index: 99;
            opacity: 0.5;
            filter: alpha(opacity=50);
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
            <img src="../Standard/loading.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 120px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_APL" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="">SELECT</asp:ListItem>
                                    <asp:ListItem Value="GL">CORPORATE LIFE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">CHANNEL DISTRIBUTION</td>
                            <td>
                                <asp:DropDownList ID="DDL_CD" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_CD_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">AGENT</td>
                            <td>
                                <asp:DropDownList ID="DDL_AGENT" runat="server" CssClass="ASPDropDownList" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">MONTHLY AGING</td>
                            <td>
                                <asp:DropDownList ID="DDL_MONTHLYAGING" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="">SELECT</asp:ListItem>
                                    <asp:ListItem Value="1 Month">1 Month</asp:ListItem>
                                    <asp:ListItem Value="2 Month">2 Month</asp:ListItem>
                                    <asp:ListItem Value="3 Month">3 Month</asp:ListItem>
                                    <asp:ListItem Value="More Than 3 Months">More Than 3 Months</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <!--<tr>
                            <td>DOCNO</td>
                            <td>
                                <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>REGNO</td>
                            <td>
                                <asp:TextBox ID="TXT_REGNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>FULLNAME</td>
                            <td>
                                <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>RECIPIENT COMPANY</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">CLAIM DATE</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="TXT_CLAIMDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:calendarextender id="CalendarExtender1" runat="server" format="dd/MM/yyyy" targetcontrolid="TXT_CLAIMDATE1">
                                </ajaxToolkit:calendarextender>
                                &nbsp;-
                                <asp:TextBox ID="TXT_CLAIMDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:calendarextender id="CalendarExtender2" runat="server" format="dd/MM/yyyy" targetcontrolid="TXT_CLAIMDATE2">
                                </ajaxToolkit:calendarextender>
                            </td>
                        </tr>-->
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" OnClientClick="ShowProgress()"  Width="100px" />
                                <asp:Label ID="LB_TRACK" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        5CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="AGENT_EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MONTH_PARAM" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT_CODE" HeaderText="AGENT CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CHANNEL_DISTRIBUTION" HeaderText="CHANNEL DISTRIBUTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MONTHLY_AGING" HeaderText="MONTHLY AGING"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOT_OS" HeaderText="TOTAL OUTSTANDING">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="JML_OS" HeaderText="JUMLAH OUTSTANDING">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PIC EMAIL">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" BackColor="#f7ff97" Width="180px"></asp:TextBox>
                                    <asp:Button ID="BT_EMAIL" runat="server" CommandName="Process" CssClass="ASPButton" Text="SEND" ForeColor="White" BackColor="#79d351" Font-Bold="True" />
                                    <asp:Button ID="BT_HST" runat="server" CommandName="History" CssClass="ASPButton" Text="HISTORY" ForeColor="White"  BackColor="#6666FF"  Font-Bold="True" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <%--<asp:BoundColumn DataField="EMAIL_CC" HeaderText="CC"></asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="SEND_DATE" HeaderText="LAST SEND DATE"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Wrap="false" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
