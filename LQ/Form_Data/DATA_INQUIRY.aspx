<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DATA_INQUIRY.aspx.cs" Inherits="LQ.Form_Data.DATA_INQUIRY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_S" runat="server" Visible="false"></asp:Label>
        <div id="DV_MAIN" runat="server" visible="false">
            <table style="border-spacing: 0px; width: 100%; top: 0px; left: 0px;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr style="vertical-align: top;">
                                <td style="width: 200px;">
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 80px;">DATABASE</td>
                                            <td>:
                                            <asp:DropDownList ID="DDL_DB" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_DB_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>OBJECT TYPE</td>
                                            <td>:
                                            <asp:DropDownList ID="DDL_OBJECT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_OBJECT_SelectedIndexChanged">
                                                <asp:ListItem Value="U">TABLE</asp:ListItem>
                                                <asp:ListItem Value="V">VIEW</asp:ListItem>
                                                <asp:ListItem Value="P">PROCEDURE</asp:ListItem>
                                                <asp:ListItem Value="FN">FUNCTION</asp:ListItem>
                                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_VIEW" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_VIEW_Click" Text="V" />
                                                &nbsp;<asp:Button ID="BT_PICK" runat="server" BackColor="Yellow" CssClass="ASPButton" Font-Bold="True" OnClick="BT_PICK_Click" Text="P" /></td>
                                        </tr>
                                    </table>
                                    <asp:ListBox ID="LB_OBJECT" runat="server" BackColor="#66FF33" CssClass="ASPDropDownList" Height="200px" Width="200px"></asp:ListBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TXT_SQL" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Height="200px" TextMode="MultiLine" Width="95%"></asp:TextBox><br />
                                    <asp:Button ID="BT_SQL" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_SQL_Click" Text="!" Width="50px" />
                                    &nbsp;<asp:Button ID="BT_XLS" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_XLS_Click" Text="XLS" Width="50px" />
                                    <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-top-style: ridge;">
                        <asp:Label ID="LB_RESULT" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                            BorderColor="#003366" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                            PageSize="50" ItemStyle-Wrap="true" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged">
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                Mode="NumericPages" Position="Top" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
