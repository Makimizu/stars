<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyGroup.aspx.cs" Inherits="CUSTOMERS.Form_Client.CompanyGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td width="120">GROUP NAME
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" Width="250px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button runat="server" Text="Simpan" ID="BTN_SAVE" CssClass="ASPButton" OnClick="BTN_SAVE_Click" />
                                <asp:Button runat="server" Text="Cancel" ID="BTN_CANCEL" CssClass="ASPButton" OnClick="BTN_CANCEL_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField runat="server" ID="HID_ID" />
                    <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True" EnableViewState="false" ForeColor="Blue"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        OnPageIndexChanged="DGR_PageIndexChanged" OnItemCommand="DGR_ItemCommand"
                        AllowPaging="True" AutoGenerateColumns="False" CssClass="ASPDatagrid" ForeColor="#333333">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" BorderColor="Black" BorderWidth="1" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="ACTION">
                                <ItemTemplate>
                                    <asp:Button ID="LB_CODE" runat="server" CssClass="ASPLabel" Text="Pilih" Font-Bold="True" ForeColor="Blue" CommandName="Edit" ></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAME" HeaderText="NAMA"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                            Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
