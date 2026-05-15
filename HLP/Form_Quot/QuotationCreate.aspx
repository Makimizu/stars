<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationCreate.aspx.cs" Inherits="HLP.Form_Quot.QuotationCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 120px;">
                                <asp:Label ID="Label3" runat="server" CssClass="ASPLabel" Font-Bold="True" Text="COMPANY"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="DDL_COMPANY" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                                <asp:Label ID="LB_QUOTNO" runat="server" CssClass="ASPLabel" Font-Bold="True" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">
                                <asp:Label ID="Label4" runat="server" CssClass="ASPLabel" Font-Bold="True" Text="PRODUCT"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCT" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                    </table>
                </td>
                <td style="width: 50px;"></td>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" GridLines="None">
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GROUP_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_LEN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GROUP_DESCR" HeaderText="GROUP" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ADD_DESCR" HeaderText="ADDITIONAL INFO">
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="VALUE">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_VAL" runat="server" CssClass="ASPDropDownList" Visible="False">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="354px"></asp:TextBox>
                                    <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Visible="False" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" />
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
