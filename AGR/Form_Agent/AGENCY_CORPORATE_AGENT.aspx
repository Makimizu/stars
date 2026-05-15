<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENCY_CORPORATE_AGENT.aspx.cs" Inherits="AGR.AGENCY_CORPORATE_AGENT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
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
                        <tr>
                            <td>STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_STATUS_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">CHANNEL</td>
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
                            <td>STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                    <asp:ListItem Value="0">NOT ACTIVE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                &nbsp;
                            <asp:Label ID="LB_RECORDS" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>



        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid"
            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
            <Columns>
                <asp:TemplateColumn HeaderText="AGENT CODE">
                    <ItemTemplate>
                        <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ACTIVE" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="TRACK" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="SUBCD_DESCR" HeaderText="LEVEL"></asp:BoundColumn>
                <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER_NAME" HeaderText="UPLINER NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="BRANCH_DESCR" HeaderText="BRANCH"></asp:BoundColumn>
                <asp:BoundColumn DataField="TRACK_DESCR" HeaderText="STATUS"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="ACTIVE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="40" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="DB_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
        </asp:DataGrid>
    </form>
</body>
</html>
