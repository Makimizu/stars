<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocDB.aspx.cs" Inherits="GO.DocDB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 74px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td class="auto-style1">
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">DATABASE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_DB" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_DB_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">OBJECT TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TYPE" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_TYPE_SelectedIndexChanged">
                                                <asp:ListItem>P</asp:ListItem>
                                                <asp:ListItem>U</asp:ListItem>
                                                <asp:ListItem>V</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>OBJECT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_OBJ" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">USED</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_USED" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_USED_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1">USED</asp:ListItem>
                                                <asp:ListItem Value="0">NOT USED</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>REMARK</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_REMARK" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_REMARK_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1">NOT EMPTY</asp:ListItem>
                                                <asp:ListItem Value="0">EMPTY</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" />
                                        </td>
                                    </tr>
                                </table>
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
                            <asp:BoundColumn DataField="DBNAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OBJNAME" HeaderText="OBJECT NAME">
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TYPE" HeaderText="TYPE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="USED" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="USED">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="True" CssClass="ASPTextBox" Text="USED ?&lt;BR&gt;" TextAlign="Left" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="REMARK">
                                <HeaderTemplate>
                                    <table style="border-spacing:0px; width:100%;">
                                        <tr style="vertical-align:top;">
                                            <td>REMARK</td>
                                            <td style="text-align:right;"><asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" OnClick="BT_SAVE_Click" Text="SAVE" /></td>
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
