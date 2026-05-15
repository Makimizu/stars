<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollDep.aspx.cs" Inherits="FINANCE.Form_Collection.CollDep" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">APPLICATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POSTDATE" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_POSTDATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center"></asp:TextBox>
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
                                        <td>BALANCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_BALANCE" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value="">-- ALL --</asp:ListItem>
                                                <asp:ListItem Value="and a.BALANCE &gt; 0">&gt; 0</asp:ListItem>
                                                <asp:ListItem Value="and a.BALANCE = 0">= 0</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">Records</td>
                                        <td>
                                            <asp:Label ID="LB_RECORDS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Total Amount</td>
                                        <td>
                                            <asp:Label ID="LB_CREDIT" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Green"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Total Used</td>
                                        <td>
                                            <asp:Label ID="LB_DEBET" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Total Balance</td>
                                        <td>
                                            <asp:Label ID="LB_BALANCE" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                                    </tr>
                                </table>
                                            <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                        PageSize="40" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" ItemStyle-Wrap="true" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="false" />
                        <Columns>
                            <asp:BoundColumn DataField="TRXID" HeaderText="REFUND NO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RFDDATE" HeaderText="DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_SETTLEMENT_DESCR" HeaderText="TYPE">
                                <HeaderStyle Width="180px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RK_DESCR" HeaderText="DESCRIPTION">
                                <HeaderStyle Width="400px" />
                                <ItemStyle Wrap="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="USED" HeaderText="USED">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COVER_LTR_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_SEND_DATE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_HST" runat="server" BackColor="Silver" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="H" />
                                                <asp:Button ID="BT_DETAIL" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="D" />
                                                <asp:Button ID="BT_COVLTR" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="L" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_EMAIL" runat="server" BackColor="Yellow" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="E" CommandName="Email" />
                                                <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LB_LASTSEND" runat="server" CssClass="ASPLabel" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
