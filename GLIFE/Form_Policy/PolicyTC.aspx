<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyTC.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyTC" %>

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

        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="position: relative; top: 0px; border-spacing: 0px; width: 95%;">
            <tr>
                <td>
                    <table id="TBL_TC" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 80px;">START DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>TC</td>
                            <td>
                                <asp:TextBox ID="TXT_TC_SEARCH" runat="server" CssClass="ASPTextBox" Width="100%" AutoPostBack="true" placeholder="Search TC name ..." OnTextChanged="TXT_TC_SEARCH_TextChanged"></asp:TextBox><br />
                                <asp:DropDownList ID="DDL_TC" runat="server" CssClass="ASPDropDownList" Width="100%"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_ADD" runat="server" CssClass="ASPButton" Text="ADD TIMELINE" OnClick="BT_ADD_Click" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 50%;">
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_ItemCommand">
                        <ItemStyle VerticalAlign="Top" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle
                            Wrap="False" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_DATE" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR">
                                <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" CommandName="Delete" Text="X" BackColor="Red" ForeColor="White" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
