<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdditionalInfo.aspx.cs" Inherits="HEALTH.Form_Tools.AdditionalInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxtoolkit:toolkitscriptmanager id="tkScriptManager" runat="server">
        </ajaxtoolkit:toolkitscriptmanager>
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;">
            <tr>
                <td>
                    <asp:Label ID="LB_TIPE" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_OWNER" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_READONLY" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="ASPDatagrid"
                                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="0px" ShowHeader="False">
                                    <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <ItemStyle BackColor="White" ForeColor="#003399" />
                                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Center" />
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GROUP_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_LEN" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GROUP_DESCR" HeaderText="GROUP">
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ADD_DESCR" HeaderText="ADDITIONAL INFO">
                                            <ItemStyle Font-Bold="False" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="VALUE">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_VAL" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="354px"></asp:TextBox>
                                                <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Visible="False" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxtoolkit:calendarextender id="ceDate" runat="server" format="dd/MM/yyyy" targetcontrolid="TXT_DATE">
                                                </ajaxtoolkit:calendarextender>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BTN_SAVE" runat="server" Text="SAVE" OnClick="BTN_SAVE_Click" CssClass="ASPButton" />
                                <asp:Button ID="BTN_PRINT" runat="server" Text="PRINT" OnClick="BTN_PRINT_Click" CssClass="ASPButton" /><br />
                                <asp:Label ID="LB_MSG" runat="server" CssClass="ASPLabel" ForeColor="Blue" EnableViewState="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
