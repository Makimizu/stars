<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_Mode1.aspx.cs" Inherits="HEALTH.Form_Tools.Search_Mode1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    </link>
    <style type="text/css">
        .style1 {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px">
            <tr>
                <td valign="middle">
                    <table style="border-spacing:0px;">
                        <tr>
                            <td style="width:100px;">CODE</td>
                            <td><asp:TextBox ID="TXT_CODE" runat="server" Width="100px" CssClass="ASPTextBox"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>DESCRIPTION</td>
                            <td><asp:TextBox ID="TXT_CARI" runat="server" Width="300px" CssClass="ASPTextBox"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><asp:Button ID="BT_CARI" runat="server" OnClick="BT_CARI_Click" Text="CARI" CssClass="ASPButton" /></td>
                        </tr>
                    </table>                    
                    <asp:Label ID="LB1" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB2" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB21" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB22" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB3" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB4" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB5" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="callbak" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:DataGrid ID="DGR1" runat="server" CellPadding="4" GridLines="None" OnItemCommand="DGR1_ItemCommand"
                        Width="100%" ForeColor="#333333" CssClass="ASPDatagrid">
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonColumn>
                        </Columns>
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
