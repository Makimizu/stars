<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_TERMINATE_REACTIVATE.aspx.cs" Inherits="AGR.AGENT_TERMINATE_REACTIVATE" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>

        <div id="DV_MAIN" runat="server">
            <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                <tr style="vertical-align: top;">
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 150px;">AGENT CODE</td>
                                <td>
                                    <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>AGENT NAME</td>
                                <td>
                                    <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>UPLINER NAME</td>
                                <td>
                                    <asp:TextBox ID="TXT_UPLINER" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td>AGENCY</td>
                                <td>
                                    <asp:TextBox ID="TXT_AGENCY" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 150px;">DIVISION IN CHARGE</td>
                                <td>
                                    <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>LEVEL</td>
                                <td>
                                    <asp:TextBox ID="TXT_CHANNEL" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: left;">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_RECORDS" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                GridLines="None" CssClass="ASPDatagrid"
                OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemDataBound="DGR_ItemDataBound">
                <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="XX-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
                <Columns>
                    <asp:TemplateColumn HeaderText="AGENT CODE">
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60" />
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="PERFORMANCE" HeaderText="PERFORMANCE"></asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Left" Width="450" />
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderTemplate>
                            <table style="border-spacing: 0px; width: 100%">
                                <tr>
                                    <td style="width: 30px;">
                                        <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_ALL_CheckedChanged" /></td>
                                    <td>
                                        <asp:Button ID="BT_A" runat="server" CssClass="ASPButton" Text="" BackColor="Blue" ForeColor="White" CommandName="Approve" Width="100" />
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="border-spacing: 0px; width: 100%">
                                <tr style="vertical-align: top;">
                                    <td style="width: 30px;">
                                        <asp:CheckBox ID="CB" runat="server" /></td>
                                    <td>
                                        <table style="border-spacing: 0px; font-size: xx-small; width: 100%;">
                                            <tr id="TR_TRNS" runat="server">
                                                <td style="width: 80px; border-bottom: ridge;">TRANSFER TO</td>
                                                <td style="border-bottom: ridge;">
                                                    <table style="border-spacing: 0px; width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LB_AGENTCODE" runat="server" Font-Bold="true"></asp:Label>
                                                                <asp:Label ID="LB_AGENTNAME" runat="server" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <asp:Button ID="BT_TRNS" runat="server" CssClass="ASPButton" Text="?" CommandName="Transfer" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style="vertical-align: top;">
                                                <td>REASON</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_REASON" runat="server" CssClass="ASPTextBox" Width="100%" Height="40" TextMode="MultiLine" MaxLength="1000" placeholder="Reason .." BackColor="#ffe8ff"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <EditItemStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
            </asp:DataGrid>
        </div>

        <div id="DV_TRANSFER" runat="server" visible="false">
            <asp:LinkButton ID="LB_BACK" runat="server" Text="Back .." Font-Size="X-Small" ForeColor="Red" OnClick="LB_BACK_Click"></asp:LinkButton>
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td class="TDBGColor">SEARCH AGENT TO RECEIVE TRANSFERED CASES</td>
                </tr>
                <tr>
                    <td style="border-bottom: ridge;">
                        <asp:Label ID="LB_TRNS_AGENTCODE" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="LB_TRNS_AGENT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td>
                                    <table style="border-spacing: 0px; font-family: Tahoma; width: 100%;">
                                        <tr>
                                            <td style="width: 80px;">AGENT NAME</td>
                                            <td>
                                                <asp:TextBox ID="TXT_TRNS_NAME" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>LEVEL</td>
                                            <td>
                                                <asp:TextBox ID="TXT_TRNS_LEVEL" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_TRNS_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" OnClick="BT_UPLINER_SEARCH_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LB_TRNS_RECORDS" runat="server"></asp:Label>
                                    <asp:DataGrid ID="DGR_TRNS" runat="server" CellPadding="4"
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
                                                    <asp:LinkButton ID="LB_TRNS_CODE" runat="server" CommandName="Select"></asp:LinkButton>
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
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>

