<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleReport.aspx.cs" Inherits="GO.RoleReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td style="width:100px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_APP" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ROLES</td>
                            <td>
                                <asp:DropDownList ID="DDL_ROLE" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_ROLE_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>

                                <asp:DataGrid ID="DGR" runat="server" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20"
                                    AutoGenerateColumns="False" CssClass="ASPDatagrid" OnItemCommand="DGR_GROUP_ItemCommand" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemDataBound="DGR_ItemDataBound">
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="APP_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CODE" HeaderText="CODE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REPORT_DESCR" HeaderText="REPORT NAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="UNIT_DESCR" HeaderText="UNIT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <asp:DropDownList ID="DDL_UNIT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_UNIT_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LB_UNIT" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
