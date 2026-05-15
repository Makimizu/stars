<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleMenu.aspx.cs" Inherits="GO.RoleMenu" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_APP" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ROLE</td>
                            <td>
                                <asp:DropDownList ID="DDL_ROLE" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_ROLE_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR_MENU" runat="server"
                        BorderWidth="1px" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20" CssClass="ASPDatagrid" OnItemCommand="DGR_MENU_ItemCommand" BorderColor="#999999" AutoGenerateColumns="False" OnItemDataBound="DGR_MENU_ItemDataBound">
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />
                                    <br />
                                    <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="True" OnCheckedChanged="CB_ALL_CheckedChanged" Text="ALL" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="MENU_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MENU_DESCR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GROUP_DESCR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AUTH_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:DropDownList ID="DDL_AUTH_ALL" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_AUTH_ALL_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_AUTH" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
