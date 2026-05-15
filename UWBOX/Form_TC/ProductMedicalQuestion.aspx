<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductMedicalQuestion.aspx.cs" Inherits="UWBOX.Form_TC.ProductMedicalQuestion" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 95%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 130px;">QUESTION</td>
                            <td>
                                <asp:TextBox ID="TXT_Q1" CssClass="ASPTextBox" Width="100%" MaxLength="1000" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>NEXT QUESTION</td>
                            <td>
                                <asp:TextBox ID="TXT_Q2" CssClass="ASPTextBox" Width="100%" MaxLength="255" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ANSWER DATA TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_DATATYPE" CssClass="ASPDropDownList" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>ANSWER DATA SQL REFF.</td>
                            <td>
                                <asp:TextBox ID="TXT_SQLREFF" CssClass="ASPTextBox" Width="100%" MaxLength="255" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_INSERT" runat="server" Text="INSERT NEW QUESTION" CssClass="ASPButton" Width="200" OnClick="BT_INSERT_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None" AutoGenerateColumns="False" ShowHeader="False" Width="100%" OnItemCommand="DGR_ItemCommand">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle Wrap="False" BackColor="#EEEEEE" ForeColor="Black" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ">
                                <ItemStyle Width="30" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEXT_QUESTION" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 130px;">QUESTION</td>
                                            <td>
                                                <asp:TextBox ID="TXT_Q1" CssClass="ASPTextBox" Width="100%" MaxLength="1000" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>NEXT QUESTION</td>
                                            <td>
                                                <asp:TextBox ID="TXT_Q2" CssClass="ASPTextBox" Width="100%" MaxLength="255" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>ANSWER DATA TYPE</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_DATATYPE" CssClass="ASPDropDownList" runat="server"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>ANSWER DATA SQL REFF.</td>
                                            <td>
                                                <asp:TextBox ID="TXT_SQLREFF" CssClass="ASPTextBox" Width="100%" MaxLength="255" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="100" CommandName="Save" /></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_X" runat="server" CommandName="Delete" Text="X" CssClass="ASPButton" BackColor="Red" ForeColor="White" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle Wrap="False"
                            HorizontalAlign="Center" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
