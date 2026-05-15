<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametersAdv.aspx.cs" Inherits="CUSTOMERS.Form_Parameter.ParametersAdv" %>

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
        <table style="top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_PARAM" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                    Font-Size="Small"></asp:Label>
                                <asp:Label ID="LB_PREFIX" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGRQUERY" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
                                    GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" OnItemDataBound="DGRQUERY_ItemDataBound" OnItemCommand="DGRQUERY_ItemCommand">
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="F1" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F2" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F3" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F4" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F5" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F6" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F7" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F8" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F9" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F10" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F11" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F12" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F13" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F14" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F15" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F16" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F17" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F18" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F19" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F20" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F1" runat="server" Font-Bold="True"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL1" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>

                                                <asp:TextBox ID="TXT_VAL2" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL2" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL3" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL3" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL4" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL4" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL5" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL5" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL6" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL6" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL7" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL7" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL8" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL8" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL9" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL9" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL10" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL10" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL11" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL11" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL12" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL12" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL13" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL13" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL14" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL14" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL15" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL15" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL16" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL16" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL17" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL17" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL18" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL18" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL19" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL19" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_VAL20" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL20" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_SAVEITEM" runat="server" CommandName="Save" CssClass="ASPButton" Text="S" ForeColor="White" BackColor="Green" />
                                                <asp:Button ID="BT_DELITEM" runat="server" CommandName="Delete" CssClass="ASPButton" Text="X" ForeColor="White" BackColor="Red" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                </asp:DataGrid>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
