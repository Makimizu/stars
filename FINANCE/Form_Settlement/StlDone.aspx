<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StlDone.aspx.cs" Inherits="FINANCE.Form_Settlement.StlDone" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_APP" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr id="TR_APPID" runat="server">
                                        <td style="width: 120px;">
                                            <asp:Label ID="Label5" runat="server" Text="APPLICATION" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr id="TR_TIPE" runat="server">
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="TIPE" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_TIPE_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">ACC SOURCE</td>
                                        <td class="auto-style1">
                                            <asp:DropDownList ID="DDL_ACCSOURCE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_ACCSOURCE_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>INHOUSE/CLEARING</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_BANKCONN" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value="and a.BANK_SOURCE_CODE = a.DESTINATION_BANK">INHOUSE</asp:ListItem>
                                                <asp:ListItem Value="and a.BANK_SOURCE_CODE &lt;&gt; a.DESTINATION_BANK">CLEARING</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">
                                            <asp:Label ID="Label1" runat="server" Text="DOC NO" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">
                                            <asp:Label ID="Label2" runat="server" Text="DESCRIPTION" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="PROCESS DATE" CssClass="ASPLabel"></asp:Label></td>
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
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" Width="100px" />
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
                        OnPageIndexChanged="DGR_PageIndexChanged" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="DOC NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_REKAPID" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Button ID="BT_FILE_ALL" runat="server" BackColor="Blue" CommandName="IBALL" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="IB"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DETAIL" runat="server" CommandName="Detail" CssClass="ASPButton" Text="DETAIL" />
                                    <asp:Button ID="BT_SETTLE" runat="server" BackColor="Lime" CommandName="Settle" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="S" />
                                    <asp:Button ID="BT_FILE" runat="server" BackColor="Blue" CommandName="IB" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="IB" Visible="False" />
                                    <asp:Button ID="BT_SLIP" runat="server" BackColor="#006600" CommandName="TL" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="TL" />
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                    <asp:DataGrid ID="DGR_DETAIL" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" Visible="False" BackColor="LightGoldenrodYellow" BorderColor="#FF6600" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Vertical">
                                        <FooterStyle BackColor="Tan" />
                                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                        <ItemStyle />
                                        <AlternatingItemStyle BackColor="PaleGoldenrod" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ACC_NO" HeaderText="ACC NO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ACC_NAME" HeaderText="ACC NAME"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BANK" HeaderText="BANK"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PAYMENT_METHOD" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CHARGE" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="METHOD">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DDL_MODE" runat="server" CssClass="ASPDropDownList">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="CHARGE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_CHARGE" runat="server" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <ItemStyle Wrap="False" />
                                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    </asp:DataGrid>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REKAPID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOREK_SOURCE_DESCR" HeaderText="SOURCE ACC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESTINATION_BANK_DESCR" HeaderText="DESTINATION BANK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CNT" HeaderText="#">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="30px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CHARGE" HeaderText="CHARGE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="60px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL" HeaderText="TOTAL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PROCESS" HeaderText="PROCESS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_DONE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IB" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK_SOURCE_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POSTFILE_URL" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
