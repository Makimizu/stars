<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modul.aspx.cs" Inherits="GO.Modul" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body class="Gradient">
    <form id="form1" runat="server">
        <table style="position: absolute; width: 100%; height: 100%">
            <tr>
                <td style="vertical-align: middle; text-align: center">

                    <asp:Image ID="IMG1" runat="server" ImageUrl="~/Images/default_photo.png" />
                    <br />
                    <asp:Label ID="LB_SES" runat="server" CssClass="ASPLabel" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="LB_UID" runat="server" CssClass="ASPLabel" Font-Size="Medium"></asp:Label>
                    <br />
                    <asp:Label ID="LB_GROUP" runat="server" CssClass="ASPLabel" Visible="False"></asp:Label>
                    <br />
                    <center>
                    <asp:DataGrid ID="DGR" runat="server"
                        BorderWidth="0px" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20"
                        OnItemCommand="DGR1_ItemCommand" AutoGenerateColumns="False" ShowHeader="False" ShowFooter="true">
                        <Columns>
                            <asp:BoundColumn DataField="APP_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="APP_NAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PATH" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_APP" runat="server" CommandName="Go" CssClass="ASPButton" Font-Bold="True" Font-Size="Medium" Text="Button" Width="300px" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="BT_LOGOUT" runat="server" CommandName="Logout" CssClass="ASPButton" Font-Bold="True" BackColor="Gray" ForeColor="White" Font-Size="Medium" Text="L O G O U T" Width="300px" />
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    </center>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
