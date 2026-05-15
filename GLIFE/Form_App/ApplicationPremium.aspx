<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPremium.aspx.cs" Inherits="GLIFE.Form_App.ApplicationPremium" %>

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

        <table style="border-spacing: 0px; width: 95%;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" CssClass="ASPDatagrid" OnItemCommand="DGR_ItemCommand" ShowHeader="False" Width="100%">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR">
                                <ItemStyle Width="250px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PRINCIPAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DIVIDER" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="READONLY" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="LB_PRINCIPAL" runat="server"></asp:Label>
                                    <asp:Label ID="LB_MULTIPLY" runat="server"></asp:Label>
                                    <asp:TextBox ID="TXT_RATE" runat="server" Width="40px" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="UNIT">
                                <ItemStyle Width="20px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="LB_AMOUNT" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_REMARK" runat="server" Width="100%" CssClass="ASPTextBox" MaxLength="255" Visible="false" BackColor="Gainsboro" placeholder="Remark ..."></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE PREMIUM" Width="100px" OnClick="BT_SAVE_Click" />
                </td>
            </tr>
            <tr id="TR_CYCLE" runat="server">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">PERIODIC CYCLE</td>
                        </tr>
                        <tr>
                            <td>
                                <iframe id="IF" runat="server" src="" style="position: fixed; width: 95%; height: 70%; background-color: white; border: none; margin: 0px; padding: 0px;"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
