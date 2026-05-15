<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GL_COA.aspx.cs" Inherits="FINANCE.Form_Accounting.GL_COA" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">            
            <tr>
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td>
                                <asp:TextBox ID="TXT_FIND_CODE" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_FIND_CODE_TextChanged" Width="80px" BackColor="#FFFF66"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_FIND_DESCR" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_FIND_DESCR_TextChanged" Width="400px" BackColor="#FFFF66"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="BT_NEW" runat="server" BackColor="LightGreen" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="N" OnClick="BT_NEW_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                        PageSize="40" ItemStyle-Wrap="true" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" OnItemDataBound="DGR_ItemDataBound">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="false" />
                        <Columns>
                            <asp:BoundColumn DataField="COA" HeaderText="CODE">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DESCRIPTION">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="400px" MaxLength="100"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S"/>
                                    <asp:Button ID="BT_DELETE" runat="server" CommandName="Delete" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
