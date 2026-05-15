<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_MOVEMENT_ACTIVE.aspx.cs" Inherits="AGR.Form_Agent.AGENT_MOVEMENT_ACTIVE" %>

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
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:Button ID="BT_LEVEL" runat="server" CssClass="ASPButton" Width="98%" Text="LEVEL" OnClick="BT_LEVEL_Click" />
                    <asp:Button ID="BT_AGENCY" runat="server" CssClass="ASPButton" Width="98%" Text="AGENCY" OnClick="BT_AGENCY_Click" />
                    <asp:Button ID="BT_STRUCTURE" runat="server" CssClass="ASPButton" Width="98%" Text="UPLINER" OnClick="BT_STRUCTURE_Click" />
                </td>
                <td>
                    <table style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label runat="server" ID="LB_TITLE"></asp:Label></td>
                        </tr>
                    </table>
                    <div id="DV_LEVEL" runat="server">
                        <table style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
                            <tr>
                                <td style="width: 100px;">START DATE</td>
                                <td>
                                    <asp:TextBox ID="TXT_START_DATE_LEVEL" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE_LEVEL">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>DIVISION IN CHARGE</td>
                                <td>
                                    <asp:DropDownList ID="DDL_CHANNEL" CssClass="ASPDropDownList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>LEVEL</td>
                                <td>
                                    <asp:DropDownList ID="DDL_LEVEL" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_SAVE_LEVEL" runat="server" Text="SET LEVEL" CssClass="ASPButton" Width="150px" OnClick="BT_SAVE_LEVEL_Click" /></td>
                            </tr>
                        </table>
                        <asp:DataGrid ID="DGR_LEVEL" runat="server" CellPadding="2" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                            <ItemStyle Wrap="False" BackColor="#FFFBD6" Font-Size="XX-Small" ForeColor="#333333" />
                            <SelectedItemStyle BackColor="#FFCC66" ForeColor="Navy" Font-Bold="True" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <HeaderStyle BackColor="#990000" Font-Bold="False" ForeColor="White" Font-Size="XX-Small" />
                            <Columns>
                                <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE">
                                    <HeaderStyle Width="60" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CHANNEL_DESCR" HeaderText="CHANNEL">
                                    <HeaderStyle Width="100" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="LEVEL_DESCR" HeaderText="LEVEL">
                                    <HeaderStyle Width="150" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MODE" HeaderText="MODE">
                                    <HeaderStyle Width="100" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ASSIGN_BY" HeaderText="ASSIGN BY">
                                    <HeaderStyle Width="150" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="REMARK" HeaderText="REMARK"></asp:BoundColumn>
                            </Columns>
                            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Font-Size="X-Small" />
                        </asp:DataGrid>
                    </div>

                    <div id="DV_UPLINER" runat="server" visible="false">
                        <table style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
                            <tr>
                                <td style="width: 100px;">START DATE</td>
                                <td>
                                    <asp:TextBox ID="TXT_START_DATE_STRUCTURE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE_STRUCTURE">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>UPLINER LAYER LEVEL</td>
                                <td>
                                    <asp:DropDownList ID="DDL_UPLINER_LAYER" runat="server" CssClass="ASPDropDownList">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr style="vertical-align: top;">
                                <td>UPLINER</td>
                                <td>
                                    <asp:Label ID="LB_UPLINER" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="LB_UPLINER_NAME" runat="server"></asp:Label>
                                    <asp:Button ID="BT_SEARCH_UPLINER" runat="server" Text="SEARCH UPLINER" CssClass="ASPButton" OnClick="BT_SEARCH_UPLINER_Click" Width="150px" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_SAVE_STRUCTURE" runat="server" Text="SET UPLINER" CssClass="ASPButton" Width="150px" Visible="False" OnClick="BT_SAVE_STRUCTURE_Click" />
                                    <asp:Button ID="BT_SET_NOUPLINER" runat="server" Text="SET NO UPLINER" CssClass="ASPButton" Width="150px" OnClick="BT_SET_NOUPLINER_Click" />
                                </td>
                            </tr>
                        </table>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 50%;" class="TDBGColor">CURRENT UPLINER STRUCTURE</td>
                                <td style="width: 50%;" class="TDBGColor">UPLINER HISTORY</td>
                            </tr>
                            <tr style="vertical-align: top;">
                                <td>
                                    <asp:DataGrid ID="DGR_UPLINER_LIST" runat="server" CellPadding="2" PageSize="20"
                                        GridLines="None" CssClass="ASPDatagrid" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" Width="95%">
                                        <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="XX-Small" />
                                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Size="XX-Small" />
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" />
                                    </asp:DataGrid>
                                </td>
                                <td>
                                    <asp:DataGrid ID="DGR_STRUCTURE" runat="server" CellPadding="2" PageSize="20"
                                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="95%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                                        <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="XX-Small" />
                                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Size="XX-Small" />
                                        <Columns>
                                            <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE">
                                                <HeaderStyle Width="80" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="UPLINER_NAME" HeaderText="UPLINER NAME"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="STEP_LEVEL"></asp:BoundColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" />
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div id="DV_AGENCY" runat="server" visible="false">
                        <table style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
                            <tr>
                                <td style="width:100px;">START DATE</td>
                                <td style="width:350px;">
                                    <asp:TextBox ID="TXT_START_DATE_MOVEMENT" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE_MOVEMENT">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>RO</td>
                                <td>
                                    <asp:DropDownList ID="DDL_RO" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_RO_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="TXT_SEARCH_RO" runat="server" CssClass="ASPTextBox" AutoPostBack="true" OnTextChanged="TXT_SEARCH_RO_TextChanged" ToolTip="SEARCH RO"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>AGENCY</td>
                                <td>
                                    <asp:DropDownList ID="DDL_AGENCY" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="TXT_SEARCH_AGENCY" runat="server" CssClass="ASPTextBox" AutoPostBack="true" OnTextChanged="TXT_SEARCH_AGENCY_TextChanged" ToolTip="SEARCH AGENCY"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>AREA</td>
                                <td>
                                    <asp:DropDownList ID="DDL_AREA" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="TXT_SEARCH_AREA" runat="server" CssClass="ASPTextBox" AutoPostBack="true" OnTextChanged="TXT_SEARCH_AREA_TextChanged" ToolTip="SEARCH AREA"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_AGENCY_SAVE" runat="server" Text="SUBMIT" Width="100" CssClass="ASPButton" OnClick="BT_AGENCY_SAVE_Click" />
                                </td>
                            </tr>
                            <%-- <tr style="vertical-align: top;">
                                <td>AGENCY</td>
                                <td>
                                    <asp:Label ID="LB_BRANCH_CODE" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="LB_BRANCH_NAME" runat="server"></asp:Label>
                                    <asp:Button ID="BT_SEARCH_BRANCH" runat="server" Text="SEARCH AGENCY" CssClass="ASPButton" OnClick="BT_SEARCH_COMPANY_Click" Width="150px" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_SAVE_MOVEMENT" runat="server" Text="SET AGENCY" CssClass="ASPButton" Width="150px" Visible="False" OnClick="BT_SAVE_MOVEMENT_Click" /></td>
                            </tr>--%>
                        </table>

                        <asp:DataGrid ID="DGR_MOVEMENT" runat="server" CellPadding="2" PageSize="20"
                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                            <ItemStyle Wrap="False" BackColor="#E7E7FF" Font-Size="XX-Small" ForeColor="#4A3C8C" />
                            <SelectedItemStyle BackColor="#738A9C" ForeColor="#F7F7F7" Font-Bold="True" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="False" ForeColor="#F7F7F7" Font-Size="XX-Small" />
                            <Columns>
                                <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE">
                                    <HeaderStyle Width="80" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BRANCH_CODE" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="AGENCY NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BRANCH_NAME" HeaderText="BRANCH NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AREA_NAME" HeaderText="AREA NAME"></asp:BoundColumn>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Font-Size="X-Small" Mode="NumericPages" />
                        </asp:DataGrid>

                    </div>

                    <div style="background-color: transparent !important; opacity: 0.9;">
                        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="350px" Width="90%" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 15px; border: outset 2px gray; padding: 5px; display: none; overflow: auto;">
                            <asp:LinkButton ID="LB_BACK" runat="server" ForeColor="Red" Font-Size="X-Small" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;">Back ..</asp:LinkButton>
                            <table id="TBL_UPLINER" runat="server" visible="false" style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
                                <tr>
                                    <td class="TDBGColor">UPLINER SEARCH</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
                                            <tr>
                                                <td style="width: 100px;">UPLINER NAME</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_UPLINER" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>LEVEL</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_LEVEL" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="BT_UPLINER_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" OnClick="BT_UPLINER_SEARCH_Click" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LB_UPLINER_RECORDS" runat="server"></asp:Label>
                                        <asp:DataGrid ID="DGR_UPLINER" runat="server" CellPadding="4"
                                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" AllowPaging="True" OnItemCommand="DGR_UPLINER_ItemCommand" OnPageIndexChanged="DGR_UPLINER_PageIndexChanged">
                                            <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="X-Small" />
                                            <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="AGENT CODE">
                                                    <ItemStyle Width="100" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LB_UPLINER_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="CODE" HeaderText="AGENT CODE" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SUBCD_DESCR" HeaderText="FULLNAME"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="AGENCY_NAME" HeaderText="AGENCY NAME"></asp:BoundColumn>
                                            </Columns>
                                            <EditItemStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" />
                                        </asp:DataGrid>

                                    </td>
                                </tr>
                            </table>

                            <%--<table id="TBL_AGENCY" runat="server" visible="false" style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
                                <tr>
                                    <td class="TDBGColor">AGENCY SEARCH</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
                                            <tr>
                                                <td style="width: 100px;">AGENCY NAME</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_AGENCYNAME" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="BT_AGENCY_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" OnClick="BT_AGENCY_SEARCH_Click" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LB_AGENCY_RECORDS" runat="server"></asp:Label>
                                        <%--<asp:DataGrid ID="DGR_AGENCY" runat="server" CellPadding="4"
                                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" AllowPaging="True" OnItemCommand="DGR_AGENCY_ItemCommand" OnPageIndexChanged="DGR_AGENCY_PageIndexChanged">
                                            <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="X-Small" />
                                            <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="BRANCH CODE">
                                                    <ItemStyle Width="100" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LB_BRANCH_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="BRANCH_CODE" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BRANCH_NAME"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="COMPANY_NAME"></asp:BoundColumn>
                                            </Columns>
                                            <EditItemStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" />
                                        </asp:DataGrid>

                                    </td>
                                </tr>
                            </table>--%>
                        </asp:Panel>

                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
