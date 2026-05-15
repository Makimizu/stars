<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocScreen.aspx.cs" Inherits="GO.DocScreen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
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
                            <td>SCREEN NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_SCREEN" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="20" Width="350px" BackColor="White" BorderColor="#000066" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="DGR_PageIndexChanged">
                        <ItemStyle VerticalAlign="Top" BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" />
                        <Columns>
                            <asp:BoundColumn DataField="APP_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MENU_CODE" HeaderText="CODE">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MENU_DESCR" HeaderText="MENU NAME">
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MENU_GROUP" HeaderText="GROUP">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MENU_PATH" HeaderText="PATH"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="REMARK">
                                <HeaderTemplate>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr style="vertical-align: top;">
                                            <td>REMARK</td>
                                            <td style="text-align: right;">
                                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" OnClick="BT_SAVE_Click" Text="SAVE" /></td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_REMARK" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Height="50px" MaxLength="4000" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle Wrap="False" BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
