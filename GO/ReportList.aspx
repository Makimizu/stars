<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="GO.ReportList" %>

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
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_APP" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>UNIT</td>
                            <td>
                                <asp:DropDownList ID="DDL_UNIT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_UNIT_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>

                    <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20" ShowHeader="False" CssClass="ASPDatagrid" OnItemCommand="DGR_MENU_ItemCommand" AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundColumn DataField="APP_ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CODE" Visible="False">
                                <HeaderStyle Width="40px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_ENGLISH" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FOLDER" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_NAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNIT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SHARE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 80px;">DESCR</td>
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
                                            <td style="width: 80px;">URL</td>
                                            <td>
                                                <asp:TextBox ID="TXT_URL" runat="server" CssClass="ASPTextBox" MaxLength="1000" Width="300px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>URL ENG</td>
                                            <td>
                                                <asp:TextBox ID="TXT_URL_ENG" runat="server" CssClass="ASPTextBox" MaxLength="1000" Width="300px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 100px;">REPORT NAME</td>
                                            <td>
                                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>FOLDER</td>
                                            <td>
                                                <asp:TextBox ID="TXT_FOLDER" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>SHARE</td>
                                            <td>
                                                <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" Visible="true"></asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LB_CODE" CssClass="ASPLabel" Font-Bold="true" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_MENU_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="S" Font-Bold="True" ForeColor="White" BackColor="Green" />
                                    <asp:Button ID="BT_MENU_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Text="X" Font-Bold="True" ForeColor="White" BackColor="Red" />
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <ItemStyle VerticalAlign="Top" Wrap="False" BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
