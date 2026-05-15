<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemObjects.aspx.cs" Inherits="GO.SystemObjects" ValidateRequest="false" %>

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
                            <td style="width: 100px;">OBJECT CATEGORY</td>
                            <td>
                                <asp:DropDownList ID="DDL_OBJECTS" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_OBJECTS_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>DB NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_DB" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>OBJECT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_OBJ" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td>

                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width:300px;">
                                <asp:Label ID="LB_RECORDS" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LB_DBNAME" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                                &nbsp;-
                                <asp:Label ID="LB_OBJNAME" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:DataGrid ID="DGR" runat="server" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20" CssClass="ASPDatagrid" OnItemCommand="DGR_MENU_ItemCommand" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
                                    <AlternatingItemStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <ItemStyle VerticalAlign="Top" Wrap="False" BackColor="#EEEEEE" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                </asp:DataGrid>
                            </td>
                            <td>

                                <asp:TextBox ID="TXT_OBJTEXT" runat="server" CssClass="ASPTextBox" Height="500px" Width="100%" TextMode="MultiLine" Visible="False" BackColor="#FFFF99" ReadOnly="True" Wrap="false"></asp:TextBox>

                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
