<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TRAINING_SCHEDULE_LIST.aspx.cs" Inherits="AGR.TRAINING_SCHEDULE_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 50%" class="TDBGColor">TRAINING SCHEDULE ATRIBUTES</td>
                <td style="width: 50%" class="TDBGColor">TRAINING SCHEDULE LIST</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="font-size: x-small; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">SCHEDULE CODE</td>
                                        <td style="border-bottom: ridge;">
                                            <asp:Label ID="LB_SCHEDULE_CODE" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TRAINING</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TRAINING" runat="server" CssClass="ASPDropDownList" BackColor="#FFFFCC" Width="100%"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">DATE&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="TXT_START_DATE" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small" CssClass="ASPTextBox"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_END_DATE" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small" CssClass="ASPTextBox"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE">
                                            </ajaxToolkit:CalendarExtender>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">PRESENCE TIME</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_HH_START" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                            :
                                            <asp:DropDownList ID="DDL_MM_START" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                            &nbsp;-&nbsp;
                                            <asp:DropDownList ID="DDL_HH_END" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                            :
                                            <asp:DropDownList ID="DDL_MM_END" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px;">DURATION</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DURATION" runat="server" Width="50px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px;">QUOTA</td>
                                        <td>
                                            <asp:TextBox ID="TXT_QUOTA" runat="server" Width="50px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>CITY</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CITY" runat="server" CssClass="ASPDropDownList" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>MODE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MODE" CssClass="ASPDropDownList" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>LOCATION / URL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_LOCATION" runat="server" Width="90%" CssClass="ASPTextBox" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TRAINER</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TRAINER" runat="server" CssClass="ASPDropDownList" Width="100%"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PUBLISH</td>
                                        <td>
                                            <asp:CheckBox ID="CB_PUB" runat="server" Text="&amp;nbsp;YES" CssClass="ASPTextBox" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ACTIVE</td>
                                        <td>
                                            <asp:CheckBox ID="CB_VACC" runat="server" Text="&amp;nbsp;YES" CssClass="ASPTextBox" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" Width="80" CssClass="ASPButton" OnClick="BT_SAVE_Click" />
                                            &nbsp;
                                            <asp:Button ID="BT_NEW" runat="server" Text="NEW" Width="80" CssClass="ASPButton" BackColor="Blue" ForeColor="White" OnClick="BT_NEW_Click" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="TR_BUTTONS" runat="server" visible="false" style="border-spacing: 0px; font-size: x-small; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; font-size: x-small; width: 100%;">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Button ID="BT_SUB" runat="server" Text="SUB MODULE" Width="100%" Font-Size="X-Small" OnClick="BT_SUB_Click" /></td>
                                        <td style="width: 50%">
                                            <asp:Button ID="BT_CHANNEL" runat="server" Text="PARTICIPANT CHANNEL" Width="100%" Font-Size="X-Small" OnClick="BT_CHANNEL_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe id="IF" runat="server" src="" style="width: 100%; height: 85vh; overflow: auto; border: 0;"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="border-spacing: 0px; font-size: x-small; width: 100%;">
                        <tr>
                            <td style="width: 150px;">TRAINING LIBRARY</td>
                            <td>
                                <asp:DropDownList ID="DDL_TRAINING_LIBRARY" runat="server" CssClass="ASPDropDownList" Width="100%"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>SCHEDULE START</td>
                            <td>
                                <asp:TextBox ID="TXT_START_DATE_FROM" runat="server" Width="70px" Style="text-align: center;" Font-Size="X-Small" CssClass="ASPTextBox"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE_FROM">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" Width="80" OnClick="BT_SEARCH_Click" /></td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="TRAINING NAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TRAINING_NAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SCHEDULE_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENABLE_DELETE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PARTICIPANTS" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PERIOD" HeaderText="PERIOD"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRAINER_NAME" HeaderText="TRAINER"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_PARTICIPANT" runat="server" CommandName="Participant" CssClass="ASPButton" />
                                    <asp:Button ID="BT_DEL" runat="server" Text="X" CommandName="Delete" ForeColor="White" BackColor="Red" CssClass="ASPButton" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                    </asp:DataGrid>




                </td>
            </tr>
        </table>
    </form>
</body>
</html>
