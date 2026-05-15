<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_EVALUATION.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_EVALUATION" %>

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
        <table style="width: 100%; border-spacing: 0px; font-size: x-small;">
            <tr>
                <td class="TDBGColor">PROMOTION DEMOTION</td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" CellPadding="2" Font-Names="Verdana" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" ShowFooter="True" Width="100%" OnItemCommand="DGR_ITEM_ItemCommand" OnSelectedIndexChanged="DGR_ITEM_SelectedIndexChanged">
                        <ItemStyle VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LEN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR">
                                <ItemStyle Width="150px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>

                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_REFF" runat="server" Visible="False" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TXT_VAL" runat="server" Visible="False" Width="98%" Font-Size="XX-Small"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <table style="width: 100%; border-spacing: 0px;">
                        <tr>
                            <td style="width: 200px;">EVALUATION FOR</td>
                            <td>
                                <asp:DropDownList ID="DDL_MODE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_MODE_SelectedIndexChanged">
                                    <asp:ListItem Value="1">PROMOTION</asp:ListItem>
                                    <asp:ListItem Value="0">MAINTENANCE</asp:ListItem>
                                    <%--DEMOTION--%>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>DIVISION IN CHARGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>CURRENT LEVEL to NEXT LEVEL</td>
                            <td>
                                <asp:DropDownList ID="DDL_LEVELFROM" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_LEVELFROM_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;to
                                <asp:DropDownList ID="DDL_LEVELTO" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style3">MINIMUM ANP</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="TXT_ANP" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">MINIMUM <%--TEAM MEMBER--%>SALES FORCE</td>
                            <td>
                                <asp:DataGrid ID="DGR_LEVELMEMBER" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="50%" CellPadding="2" ForeColor="#333333" GridLines="None">
                                    <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle
                                        Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SUB_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="LEVEL"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TEAM_MEMBER" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="SALES FORCE">
                                            <HeaderStyle Width="100" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_TEAM_MEMBER" CssClass="ASPTextBoxNumber" Width="50" runat="server" AutoPostBack="true" OnTextChanged="TXT_TEAM_MEMBER_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>

                                <asp:TextBox ID="TXT_MEMBER" runat="server" CssClass="ASPTextBox" Width="40px" ReadOnly="true"></asp:TextBox>
                                &nbsp;persons
                            </td>
                        </tr>
                        <tr>
                            <td>MINIMUM PERSISTENCE</td>
                            <td>
                                <asp:TextBox ID="TXT_PERS" runat="server" CssClass="ASPTextBox" Width="40px"></asp:TextBox>
                                &nbsp;%</td>
                        </tr>
                        <tr>
                            <td>MINIMUM ACTIVE PERIOD</td>
                            <td>
                                <asp:TextBox ID="TXT_ACTIVEPERIOD" runat="server" CssClass="ASPTextBox" Width="40px"></asp:TextBox>
                                &nbsp;months</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="TXT_SUBMIT" runat="server" CssClass="ASPButton" Text="SUBMIT" Width="100px" OnClick="TXT_SUBMIT_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: x-small;">
                        <tr>
                            <td style="width: 50%;" class="TDBGColor">PARAMETERS</td>
                            <td style="width: 30%;" class="TDBGColor">
                                <asp:Label ID="LB_INFO" runat="server"> </asp:Label>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                                    GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
                                    <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="30" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Button ID="BT_EDIT" runat="server" Text="EDIT" CommandName="Edit" CssClass="ASPButton" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="CURRENT_AGENT_LEVEL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NEXT_AGENT_LEVEL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CHANNEL" HeaderText="DIVISION IN CHARGE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CURRENT_AGENT_LEVEL_DESCR" HeaderText="CURRENT<BR>LEVEL"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NEXTT_AGENT_LEVEL_DESCR" HeaderText="NEXT<BR>LEVEL"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MINIMUM_ANP" HeaderText="MINIMUM<BR>ANP">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MINIMUM_TEAM_MEMBER" HeaderText="MINIMUM<br>SALES FORCE">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MINIMUM_PERSISTENCE" HeaderText="MINIMUM<br>% PERSISTC">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MINIMUM_MONTH_MEMBERSHIP" HeaderText="MINIMUM<br>MONTH M'SHIP">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CREATEBY" HeaderText="CREATE BY" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CREATEDATE" HeaderText="CREATE<BR>DATE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="LASTCHANGEBY" HeaderText="LAST<BR>CHANGE BY" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="30" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Button ID="BT_LVLMEMBER" runat="server" Text="SALES FORCE" CommandName="LvlMember" CssClass="ASPButton" />
                                                <asp:Button ID="BT_PERIOD" runat="server" Text="PERIOD" CommandName="Period" CssClass="ASPButton" />
                                                <asp:Button ID="BT_TRAINING" runat="server" Text="TRAINING" CommandName="Training" CssClass="ASPButton" />
                                                <asp:Button ID="BT_DELETE" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" ForeColor="White" Text="X" OnClientClick="return confirm('Are you sure you want to delete this Evaluation Data Parameter?');" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="CURRENT_AGENT_LEVEL" Visible="False"></asp:BoundColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                </asp:DataGrid></td>
                            <td>
                                <asp:Label ID="LB_CURRENT_LEVEL" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LB_NEXT_LEVEL" runat="server" Visible="false"></asp:Label>
                                <table id="TBL_TRAINING" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="border-spacing: 0px; width: 100%;">
                                                <tr>
                                                    <td style="width: 100px;">MODE</td>
                                                    <td>
                                                        <asp:Label ID="LB_MODE_TRAINING" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>LEVEL FROM</td>
                                                    <td>
                                                        <asp:Label ID="LB_CURRENT_LEVEL_TRAINING" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>LEVEL TO</td>
                                                    <td>
                                                        <asp:Label ID="LB_NEXT_LEVEL_TRAINING" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="DGR_TRAINING" runat="server" CellPadding="4" PageSize="20"
                                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand" ShowHeader="False">
                                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="TRAINING_CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TRAINING_NAME"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TRAINING_LEVEL"></asp:BoundColumn>
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
                                            </asp:DataGrid></td>
                                    </tr>
                                </table>

                                <table id="TBL_PERIOD" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="border-spacing: 0px; width: 100%;">
                                                <tr>
                                                    <td style="width: 100px;">MODE</td>
                                                    <td>
                                                        <asp:Label ID="LB_MODE_PERIOD" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>LEVEL FROM</td>
                                                    <td>
                                                        <asp:Label ID="LB_CURRENT_LEVEL_PERIOD" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>LEVEL TO</td>
                                                    <td>
                                                        <asp:Label ID="LB_NEXT_LEVEL_PERIOD" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BT_PERIOD_ADD" runat="server" Text="ADD PERIOD" CssClass="ASPButton" Width="100%" OnClick="BT_PERIOD_ADD_Click" /></td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_PERIOD_START" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PERIOD_START">
                                                        </ajaxToolkit:CalendarExtender>
                                                        &nbsp;-&nbsp;
                                                        <asp:TextBox ID="TXT_PERIOD_END" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PERIOD_END">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="DGR_PERIOD" runat="server" CellPadding="4" PageSize="20"
                                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" OnItemCommand="DGR_PERIOD_ItemCommand">
                                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="START_DATE"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="END_DATE"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemStyle Width="30" HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_DELETE" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" ForeColor="White" Text="X" OnClientClick="return confirm('Are you sure you want to delete this Period Data Parameter?');" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                            </asp:DataGrid></td>
                                    </tr>
                                </table>

                                <table id="TBL_LVLMEMBER" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="border-spacing: 0px; width: 100%;">
                                                <tr>
                                                    <td style="width: 100px;">MODE</td>
                                                    <td>
                                                        <asp:Label ID="LB_MODE_LVLMEMBER" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>LEVEL FROM</td>
                                                    <td>
                                                        <asp:Label ID="LB_CURRENT_LEVEL_LVLMEMBER" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>LEVEL TO</td>
                                                    <td>
                                                        <asp:Label ID="LB_NEXT_LEVEL_LVLMEMBER" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="DGR_LVLMEMBER" runat="server" CellPadding="4" PageSize="20"
                                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand" ShowHeader="False">
                                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="SUB_CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR" HeaderText="LEVEL"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TEAM_MEMBER" HeaderText="SALES FORCE"></asp:BoundColumn>
                                                    <%--<asp:TemplateColumn HeaderText="SALES FORCE">
                                                        <HeaderStyle Width="100" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_TEAM_MEMBER" CssClass="ASPTextBoxNumber" Width="50" runat="server" AutoPostBack="true" OnTextChanged="TXT_TEAM_MEMBER_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>--%>
                                                </Columns>
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                            </asp:DataGrid></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
