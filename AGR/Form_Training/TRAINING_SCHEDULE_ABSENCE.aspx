<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TRAINING_SCHEDULE_ABSENCE.aspx.cs" Inherits="AGR.TRAINING_SCHEDULE_ABSENCE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_SCHEDULE_CODE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td class="TDBGColor">TRAINING ATTENDANCE</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 130px;">TRAINING NAME</td>
                                        <td>
                                            <asp:Label ID="LB_TRAININGNAME" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>DATE</td>
                                        <td>
                                            <asp:Label ID="LB_DATE" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>HOUR</td>
                                        <td>
                                            <asp:Label ID="LB_HOUR" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>QUOTA</td>
                                        <td style="text-wrap: initial;">
                                            <asp:Label ID="LB_QUOTA" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>CITY</td>
                                        <td>
                                            <asp:Label ID="LB_CITY" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>TRAINER NAME</td>
                                        <td>
                                            <asp:Label ID="LB_TRAINERNAME" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">PUBLISH</td>
                                        <td>
                                            <asp:Label ID="LB_PUBLISH" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>ACTIVE</td>
                                        <td>
                                            <asp:Label ID="LB_ACTIVE" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>PARTICIPANTS</td>
                                        <td>
                                            <asp:Label ID="LB_PARTICIPANTS" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>ATTENDEES</td>
                                        <td>
                                            <asp:Label ID="LB_ATTENDEES" runat="server" ForeColor="#0000CC"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>MODE</td>
                                        <td>
                                            <asp:Label ID="LB_MODE" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>LOCATION/URL</td>
                                        <td>
                                            <asp:Label ID="LB_LOCATION" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border-top: ridge;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; font-size: x-small; width: 100%;">
                                    <tr>
                                        <td style="width: 130px;">CHANNEL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CHANNEL" runat="server" CssClass="ASPTextBox" Width="200px" AutoPostBack="true" OnTextChanged="TXT_CHANNEL_TextChanged"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>LEVEL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_LEVEL" runat="server" CssClass="ASPTextBox" Width="200px" AutoPostBack="true" OnTextChanged="TXT_LEVEL_TextChanged"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>AGENT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="400" AutoPostBack="true" OnTextChanged="TXT_FULLNAME_TextChanged"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>AGENCY NAME</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_AGENCY" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_AGENCY_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                                    GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" OnItemCommand="DGR_ItemCommand">
                                    <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundColumn DataField="AGENT_CODE" HeaderText="AGENT CODE">
                                            <HeaderStyle Width="80" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AGENCY_NAME" HeaderText="AGENCY NAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CHANNEL" HeaderText="CHANNEL"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PRESENCE_TIME" HeaderText="PRESENCE TIME"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="ATTEND">
                                            <ItemStyle Width="30" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="60" HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Delete" Text="REMOVE" /><br />
                                                <asp:CheckBox ID="CB_X_ALL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_X_ALL_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB_X" runat="server" />
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
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
