<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_Search.aspx.cs" Inherits="AGR.Form_Tool.Agent_Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_TARGET1" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TARGET2" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_PARENT" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">
            <tr>
                <td class="TDBGColor">AGENT SEARCH</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">
                        <tr>
                            <td style="width: 100px;">UPLINER NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_UPLINER" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>LEVEL</td>
                            <td>
                                <asp:TextBox ID="TXT_LEVEL" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>AGENCY</td>
                            <td>
                                <asp:TextBox ID="TXT_AGENCY" CssClass="ASPTextBox" Width="200" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" OnClick="BT_SEARCH_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR_AGENT" runat="server" CellPadding="4"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" AllowPaging="True" OnItemCommand="DGR_AGENT_ItemCommand" OnPageIndexChanged="DGR_AGENT_PageIndexChanged">
                        <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="X-Small" />
                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="AGENT CODE">
                                <ItemStyle Width="100" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_UPLINER_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CODE" HeaderText="AGENT CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUBCD_DESCR" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENCY_NAME" HeaderText="AGENCY NAME"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
