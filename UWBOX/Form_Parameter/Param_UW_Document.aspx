<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Param_UW_Document.aspx.cs" Inherits="UWBOX.Form_Parameter.Param_UW_Document" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <table >
        <tr>
            <td>
                UNDERWRITING CODE</td>
            <td>
                <asp:DropDownList ID="DDL_UW" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_UW_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Button ID="BT_SAVE" runat=server CssClass="ASPButton"
                    OnClick="BT_SAVE_Click" Text="SAVE" Width="73px" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                REQUIRED DOCUMENTS</td>
            <td>
                <asp:DataGrid ID="DGR_DOC" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                    CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333"
                    GridLines="None" PageSize="5" ShowHeader="False">
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="#F7F6F3" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCR">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="COUNT" Visible="False">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox ID="CB" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
