<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="GO.Menu" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 117px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>APPLICATION :
                    <asp:DropDownList ID="DDL_APP" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td class="TDBGColor" style="width: 300px;"><strong>MENU GROUP</strong></td>
                            <td class="TDBGColor" style=""><strong>MENU</strong></td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <center>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 80px;">GROUP CODE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_GRP_CODE" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>GROUP NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_GRP_NAME" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="120px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_GROUP" runat="server" CssClass="ASPButton" Text="SUBMIT" OnClick="BT_GROUP_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <asp:DataGrid ID="DGR_GROUP" runat="server"
                                    BorderWidth="1px" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20"
                                    AutoGenerateColumns="False" ShowHeader="False" CssClass="ASPDatagrid" OnItemCommand="DGR_GROUP_ItemCommand" BorderColor="#999999">
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LB_GROUP" runat="server" CommandName="Select" CssClass="ASPLabel">LBG</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="GROUP_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GROUP_DESCR"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LB_GROUP_DEL" runat="server" CommandName="Delete" CssClass="ASPLabel">Delete</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                    </center>
                            </td>
                            <td>
                                <center>                                

                                <asp:DataGrid ID="DGR_MENU" runat="server"
                                    BorderWidth="1px" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20" ShowHeader="False" CssClass="ASPDatagrid" OnItemCommand="DGR_MENU_ItemCommand" BorderColor="#999999" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundColumn DataField="MENU_CODE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MENU_DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MENU_PATH" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ACTIVE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MENU_GROUP" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MENU_FONT" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_MENU_CODE" runat="server" CssClass="ASPTextBox" Width="40px" style="text-align:center;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_MENU_DESCR" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="120px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_MENU_PATH" runat="server" CssClass="ASPTextBox" MaxLength="1000" Width="300px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_MENU_GROUP" runat="server" CssClass="ASPDropDownList">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_ACTIVE" runat="server" CssClass="ASPDropDownList">
                                                    <asp:ListItem Value="1">ENABLE</asp:ListItem>
                                                    <asp:ListItem Value="0">DISABLE</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_MENU_FONT" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="80px" style="text-align:center;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_MENU_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="S" Font-Bold="True" ForeColor="White" BackColor="Green" />
                                                <asp:Button ID="BT_MENU_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Text="X" Font-Bold="True" ForeColor="White" BackColor="Red" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                </center>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
