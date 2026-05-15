<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationRemark.aspx.cs" Inherits="LIFE.Form_App.ApplicationRemark" %>

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
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">UNDERWRITING REMARKS
                </td>
            </tr>
            <tr id="TR_INSERT" runat="server">
                <td>
                    <asp:TextBox ID="TXT_REMARK" runat="server" Width="90%" TextMode="MultiLine" Height="80px" Font-Size="X-Small" placeholder="Type your remark here ..." CssClass="ASPTextBox"></asp:TextBox>
                    <asp:Button ID="BT_REMARK_SAVE" runat="server" CssClass="ASPButton" Text="SAVE REMARK" Width="130px" OnClick="BT_REMARK_SAVE_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_REMARK" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_REMARK_ItemCommand" ShowHeader="False" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemStyle Width="30px" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" Text="X" CommandName="Delete" ForeColor="White" BackColor="Red" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
