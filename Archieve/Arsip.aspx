<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Arsip.aspx.cs" Inherits="Archieve.Arsip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="Standard/CommonStyle.css" rel="stylesheet" />
    </link>
    <style type="text/css">
        .style1 {
            width: 93px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; width: 100%">

            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td class="style1">FILE</td>
                            <td>
                                <input id="TXT_FILE_UPLOAD" runat="server" name="TXT_FILE_UPLOAD" type="file"
                                    class="ASPTextBox" /></td>
                        </tr>
                        <tr>
                            <td class="style1" valign="top">DESCRIPTION</td>
                            <td valign="top">
                                <asp:TextBox ID="TXT_UPLOAD_REMARK" runat="server"
                                    Width="300px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_UPLOAD" runat="server" Height="19px" OnClick="BT_UPLOAD_Click"
                                    Text="UPLOAD" CssClass="ASPButton" />
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                                <asp:Label ID="LB_APP" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_FILE_TIPE" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_OWNER1" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_OWNER2" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_OWNER3" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_USER" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_ARSIP" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" PageSize="20"
                        OnItemCommand="DGR_ARSIP_ItemCommand" CssClass="ASPDatagrid" Width="100%">
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" HeaderText="DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMAFILE" HeaderText="FILE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CREATEDATE" HeaderText="TGL. UPLOAD" Visible="True"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="Download" Text="Download">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:ButtonColumn>
                            <asp:ButtonColumn CommandName="Delete" Text="Delete">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:ButtonColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
