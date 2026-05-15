<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Email_List.aspx.cs" Inherits="GO.Email_List" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
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
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR" runat="server" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20" ShowHeader="False" CssClass="ASPDatagrid" OnItemCommand="DGR_MENU_ItemCommand" AutoGenerateColumns="False" CellPadding="2" GridLines="None" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" ForeColor="Black">
                        <AlternatingItemStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNIT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DEFAULT_SENDER" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CC" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BCC" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 60px;">CODE</td>
                                            <td>
                                                <asp:Label ID="LB_CODE" CssClass="ASPLabel" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 60px;">DESCR</td>
                                            <td>
                                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="200px" Font-Bold="true" BackColor="#c0c0c0"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UNIT</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_UNIT" runat="server" CssClass="ASPDropDownList" BackColor="#c0c0c0"></asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 60px;">SENDER</td>
                                            <td>
                                                <asp:TextBox ID="TXT_SENDER" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="255px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>CC</td>
                                            <td>
                                                <asp:TextBox ID="TXT_CC" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="255px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>BCC</td>
                                            <td>
                                                <asp:TextBox ID="TXT_BCC" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="255px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="S" Font-Bold="True" ForeColor="White" BackColor="Green" /><br />
                                    <asp:Button ID="BT_VIEW" runat="server" CommandName="View" CssClass="ASPButton" Text="V" Font-Bold="True" ForeColor="White" BackColor="Blue" /><br />
                                    <asp:Button ID="BT_DELETE" runat="server" CommandName="Delete" CssClass="ASPButton" Text="X" Font-Bold="True" ForeColor="White" BackColor="Red" />
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="Tan" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <ItemStyle VerticalAlign="Top" Wrap="False" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
