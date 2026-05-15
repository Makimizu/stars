<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimPending.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimPending" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="95%" CssClass="ASPDatagrid" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#DBDBDB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PENDING TYPE">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_DETAIL" runat="server" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="PENDINGSTART" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PENDINGSTOP" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PENDING START">
                                <ItemTemplate>
                                    <asp:Button ID="BT_START" runat="server" Text="START" CssClass="ASPButton" ForeColor="Red" Font-Bold="true" CommandName="Start" Visible="false" />
                                    <asp:Label ID="LB_START" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PENDING STOP">
                                <ItemTemplate>
                                    <asp:Button ID="BT_STOP" runat="server" Text="STOP" CssClass="ASPButton" ForeColor="Blue" Font-Bold="true" CommandName="Stop" Visible="false" />
                                    <asp:Label ID="LB_STOP" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
