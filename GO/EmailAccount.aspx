<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailAccount.aspx.cs" Inherits="GO.EmailAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>

                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" />

                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="20" BackColor="White" BorderColor="#000066" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowHeader="False">
                        <ItemStyle VerticalAlign="Top" BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" />
                        <Columns>
                            <asp:BoundColumn DataField="ACCOUNT_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAME" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DISPLAY_NAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL_ADDRESS" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPLYTO_ADDRESS" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SERVERNAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PORT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="USERNAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DISPLAY NAME">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td class="TDBGColor" style="text-align:left;">
                                                ACCOUNT :
                                                <asp:Label ID="LBL_MODE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="X-Small"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">                                            
                                            <td>
                                                <table style="border-spacing: 0px;">
                                                    <tr>
                                                        <td style="width: 100px;">DISPLAY NAME</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_DISPLAYNAME" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Width="200px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>EMAIL ADDRESS</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_EMAIL" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Width="200px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>REPLY ADDRESS</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_REPLYTO" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>SMTP SERVER</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_SERVER" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td></td>
                                            <td>
                                                <table style="border-spacing: 0px;">
                                                    <tr>
                                                        <td style="width: 100px;">SMTP PORT</td>
                                                        <td>                                                            
                                                            <asp:TextBox ID="TXT_PORT" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Width="30px" style="text-align:center;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>SMTP USER ID</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_USERID" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>SMTP PASSWORD</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_PASSWORD" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox" TextMode="Password" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
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
