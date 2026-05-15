<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_PRODUCTION_TRANSFER.aspx.cs" Inherits="AGR.Form_Agent.AGENT_PRODUCTION_TRANSFER" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
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
        <table>
            <tr style="border-spacing: 0px; font-size: xx-small; vertical-align: top;">
                <td style="width: 60px;">
                    <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="true" Text="ALL" OnCheckedChanged="CB_ALL_CheckedChanged" />
                </td>
                <td>
                    <asp:Button ID="BT_REPORT" runat="server" Font-Size="XX-Small" BackColor="Green" ForeColor="White" Width="200" Text="SHOW SELECTED AGENTS" OnClick="BT_REPORT_Click" />
                </td>
            </tr>
        </table>

        <asp:DataGrid ID="DGR" runat="server" CellPadding="2" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid"
            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False">
            <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="XX-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
            <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
            <Columns>
                <asp:TemplateColumn HeaderText="AGENT CODE">
                    <ItemTemplate>
                        <asp:CheckBox ID="CB" runat="server" />
                        <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail" Visible="false"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="30" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="DOB" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="BRANCH" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="LEVEL" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="YEAR" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="CASES" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ANP" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr style="vertical-align: top;">
                                <td>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 60px;">FULLNAME</td>
                                            <td>:
                                                <asp:Label runat="server" ID="LB_FULLNAME" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>CODE</td>
                                            <td>:
                                                <asp:Label runat="server" ID="LB_AGENT_CODE"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 250px;">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 40px;">UPLINER</td>
                                            <td>:
                                                <asp:Label runat="server" ID="LB_UPLINER"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>BRANCH</td>
                                            <td>:
                                                <asp:Label runat="server" ID="LB_BRANCH"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 150px;">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 40px;">YEAR</td>
                                            <td>:
                                                <asp:Label runat="server" ID="LB_YEAR"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>LEVEL</td>
                                            <td>:
                                                <asp:Label runat="server" ID="LB_LEVEL"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 130px;">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 40px;">CASES</td>
                                            <td>:
                                                <asp:Label runat="server" ID="LB_CASES" Font-Bold="true" ForeColor="Blue"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>ANP</td>
                                            <td>:
                                                <asp:Label runat="server" ID="LB_ANP" Font-Bold="true" ForeColor="Green"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
        </asp:DataGrid>
    </form>
</body>
</html>
