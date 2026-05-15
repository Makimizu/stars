<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENCY_BRANCH_LIST.aspx.cs" Inherits="AGR.Form_Agent.AGENCY_BRANCH_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="DV_MAIN" runat="server">
            <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                <tr>
                    <td class="TDBGColor">RO LIST</td>
                </tr>
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 150px;">AGENCY NAME</td>
                                <td>
                                    <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="200"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>BRANCH NAME</td>
                                <td>
                                    <asp:TextBox ID="TXT_BRANCH" runat="server" CssClass="ASPTextBox" Width="200"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>BRANCH LEADER NAME</td>
                                <td>
                                    <asp:TextBox ID="TXT_AGENT" runat="server" CssClass="ASPTextBox" Width="200"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" Width="100" OnClick="BT_SEARCH_Click" />&nbsp;
                                    <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" Font-Bold="True" ForeColor="White" BackColor="Green" Visible="True" OnClick="BT_XLS_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                        <asp:DataGrid ID="DGR" runat="server" CellPadding="2" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid"
                            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundColumn DataField="BRANCH_CODE" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BRANCH" HeaderText="BRANCH NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AGENTS" HeaderText="#AGENTS"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemStyle Width="30" />
                                    <ItemTemplate>
                                        <asp:Button ID="BT_E" runat="server" CssClass="ASPButton" Text="E" ForeColor="White" BackColor="Green" CommandName="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="BRANCH_AGENT" HeaderText="BRANCH AGENT LEADER"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CHANNEL" HeaderText="CHANNEL"></asp:BoundColumn>
                                <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>

        <div id="DV_AGENTS" runat="server" visible="false">
            <asp:LinkButton ID="LB_BACK" runat="server" ForeColor="Red" Text="Back .." OnClick="LB_BACK_Click"></asp:LinkButton>
            <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 130px;">AGENCY NAME</td>
                                <td>
                                    <asp:Label ID="LB_COMPANY" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>BRANCH NAME</td>
                                <td>
                                    <asp:Label ID="LB_BRANCH" runat="server"></asp:Label>
                                    <asp:Label ID="LB_BRANCHCODE" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 50%;" class="TDBGColor">LEADER</td>
                                <td style="width: 50%;" class="TDBGColor">AGENTS</td>
                            </tr>
                            <tr style="vertical-align: top;">
                                <td>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 130px;">NEW LEADER LEVEL</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_LEADER_LEVEL" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_LEADER_LEVEL_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>NEW LEADER AGENT</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_LEADER_AGENT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>START DATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_STARTDATE" runat="server" Width="80px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_LEADER_ADD" runat="server" CssClass="ASPButton" Text="ADD NEW LEADER" Width="100" OnClick="BT_LEADER_ADD_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 130px;">SEARCH BY NAME</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TXT_SEARCH_AGENT" CssClass="ASPTextBox" Width="200" AutoPostBack="true" OnTextChanged="TXT_SEARCH_AGENT_TextChanged"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>SEARCH BY LEVEL</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_SEARCH_AGENT" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_SEARCH_AGENT_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="vertical-align: top;">
                                <td>
                                    <asp:DataGrid ID="DGR_LEADER" runat="server" CellPadding="4" PageSize="20"
                                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_LEADER_ItemCommand" ShowHeader="False">
                                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                        <Columns>
                                            <asp:BoundColumn DataField="AGENT_CODE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ACTIVE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="START_DATE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="LEADER_NAME"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CHANNEL"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="LEVEL"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle Width="30" />
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" Text="X" ForeColor="White" BackColor="Red" CommandName="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                    </asp:DataGrid>
                                </td>
                                <td>
                                    <asp:Label ID="LB_AGENT_RECORDS" runat="server"></asp:Label>
                                    <asp:DataGrid ID="DGR_AGENTS" runat="server" CellPadding="4"
                                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" AllowPaging="True" OnPageIndexChanged="DGR_AGENTS_PageIndexChanged">
                                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                        <Columns>
                                            <asp:BoundColumn DataField="LEADER_NAME"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CHANNEL"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="LEVEL"></asp:BoundColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
