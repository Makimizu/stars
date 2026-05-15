<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenjaminanSurat.aspx.cs" Inherits="HEALTH.Form_Klaim.PenjaminanSurat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:Label ID="LB_SURAT_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR_REPORT" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_REPORT_ItemCommand" PageSize="15" ShowHeader="False">
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_PDF1" runat="server" CommandName="PDF" CssClass="ASPButton" Text="CETAK" />
                                    <asp:Button ID="BT_PDF2" runat="server" CommandName="PDF_EN" CssClass="ASPButton" Text="CETAK ENGLISH" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_ENGLISH" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FAX" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_PDF3" runat="server" CommandName="PDF" CssClass="ASPButton" Text="PREVIEW" />
                                    <asp:Button ID="BT_KIRIM" runat="server" CommandName="Send" CssClass="ASPButton" Text="SEND" />
                                    <asp:DropDownList ID="DDL_KIRIM" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_KIRIM_SelectedIndexChanged">
                                        <asp:ListItem Value="0">EMAIL</asp:ListItem>
                                        <asp:ListItem Value="1">FAX</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TXT_EMAILFAX" runat="server" CssClass="ASPTextBox" Width="250px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="EMAIL_DEFAULT_SENDER" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MAIL_STATUS"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
