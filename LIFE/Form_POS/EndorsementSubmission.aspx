<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementSubmission.aspx.cs" Inherits="LIFE.Form_POS.EndorsementSubmission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Class/Global.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#EFF3FB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FIELD_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_EXECUTION" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="PARAMETER">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OLD_VAL" HeaderText="OLD VALUE">
                                <ItemStyle ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="NEW VALUE">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="300px"></asp:TextBox>
                                    <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" Visible="false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="BT_SAVE0" runat="server" CssClass="ASPButton" CommandName="Save" Text="SAVE" />
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
