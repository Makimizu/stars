<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustodianSelect.aspx.cs" Inherits="SAVING.Form_Parameter.CustodianSelect" %>

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
        <table style="width: 100%; border-spacing: 0px;">
            <tr>
                <td style="width: 50%;" class="TDBGColor">AVAILABLE CORPORATE</td>
                <td style="width: 50%;" class="TDBGColor">SELECTED CUSTODIAN</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:TextBox ID="TXT_SEARCH" runat="server" CssClass="ASPTextBox" placeholder="Search available custodian .." Width="99%" AutoPostBack="True" OnTextChanged="TXT_SEARCH_TextChanged"></asp:TextBox>
                    <div style="width: 100%; height: 130px; overflow: auto;">
                        <asp:DataGrid ID="DGR_SEARCH" runat="server" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="0px"
                            CellPadding="1" Font-Names="Tahoma" Font-Size="X-Small"
                            GridLines="Vertical" OnItemCommand="DGR_SEARCH_ItemCommand" PageSize="20"
                            ShowHeader="False" Width="100%" ForeColor="Black" CellSpacing="1">
                            <SelectedItemStyle BackColor="#00CC00" Font-Bold="True" ForeColor="White"
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:BoundColumn DataField="COMPANY_CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBT" runat="server" CommandName="Add" ForeColor="Blue"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
                <td>
                    <div style="width: 100%; height: 150px; overflow: auto;">
                        <asp:DataGrid ID="DGR_SELECTED" runat="server" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="1" Font-Names="Tahoma" Font-Size="X-Small"
                            GridLines="Vertical" PageSize="20"
                            ShowHeader="False" Width="100%" ForeColor="Black" OnItemCommand="DGR_SELECTED_ItemCommand" CellSpacing="1">
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:BoundColumn DataField="COMPANY_CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBT" runat="server" CommandName="Select" ForeColor="Red"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemStyle Width="30" />
                                    <ItemTemplate>
                                        <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%; border-spacing: 0px;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label runat="server" Font-Bold="true" ID="LB_TITLE"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
