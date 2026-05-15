<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemoRemuneration.aspx.cs" Inherits="AGR.Form_Finance.MemoRemuneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_APP" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>
        <div id="DV_TITLE" runat="server">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td class="TDBGColor">
                        <asp:Label ID="LB_TITLE" runat="server" CssClass="ASPLabel" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div id="DV_CONTENT" runat="server">
            <table style="width: 100%; border-spacing: 0px;">
                <tr id="TR_STL1" runat="server">
                    <td>
                        <table style="width: 100%; border-spacing: 0px;">
                            <tr style="vertical-align: top;">
                                <td style="width: 50%;">
                                    <table style="border-spacing: 0px; font-size: x-small;">
                                        <tr>
                                            <td style="width: 120px;">YEAR</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>PERIOD</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_PERIOD" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_PERIOD_SelectedIndexChanged"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>AGENT LEVEL</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_LEVEL" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RANGE APPROVAL</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_APPROVAL_RANGE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APPROVAL_RANGE_SelectedIndexChanged">
                                                    <asp:ListItem Value="">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Within Approval Range</asp:ListItem>
                                                    <asp:ListItem Value="0">Outside Approval Range</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%;">
                                    <table style="width: 100%; border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 120px;">DOCNO</td>
                                            <td>
                                                <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>AGENT CODE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_AGENT_CODE" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>AGENT NAME</td>
                                            <td>
                                                <asp:TextBox ID="TXT_AGENT_NAME" runat="server" CssClass="ASPTextBox" Width="350"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                                                <asp:Label ID="LB_TOTAL" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="TR_STL2" runat="server">
                    <td>
                        <asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" Font-Bold="False" ForeColor="White" OnClick="BT_APPROVE_Click" Text="APPROVE" BackColor="Blue" Width="100px" />
                        <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                            BorderWidth="1px" CellPadding="3" PageSize="40"
                            GridLines="Vertical" CssClass="ASPDatagrid"
                            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%">
                            <ItemStyle Wrap="False" />
                            <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                            <AlternatingItemStyle BackColor="#DCDCDC" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <HeaderStyle BackColor="#000084" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="DOCNO">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LB_DOCNO" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="LB_ALL" runat="server" CommandName="All" CssClass="ASPLabel" ForeColor="White">ALL</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DOCNO" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AUTH" Visible="False"></asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="PERIOD_START" HeaderText="PERIOD START" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PERIOD_END" HeaderText="PERIOD END" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AGENT_CODE" HeaderText="AGENT CODE" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AGENT_LEVEL" HeaderText="AGENT LEVEL"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TOTAL_AMOUNT" HeaderText="AMOUNT">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="GENERATE_DATE" HeaderText="GENERATE DATE" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
