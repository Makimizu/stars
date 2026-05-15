<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustodianProduct.aspx.cs" Inherits="SAVING.Form_Parameter.CustodianProduct" %>

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
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">PRODUCT</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;" class="TDBGColor">AVAILABLE PRODUCTS</td>
                            <td style="width: 50%;" class="TDBGColor">SELECTED PRODUCTS</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:TextBox ID="TXT_SEARCH" runat="server" CssClass="ASPTextBox" placeholder="Search available products .." Width="90%" AutoPostBack="True" OnTextChanged="TXT_SEARCH_TextChanged"></asp:TextBox>
                                <div style="width: 100%; height: 80vh; overflow: auto;">
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
                                            <asp:BoundColumn DataField="PRODUCT_CODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PRODUCT_NAME" Visible="False"></asp:BoundColumn>
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
                                <div style="width: 100%; height: 80vh; overflow: auto;">
                                    <asp:DataGrid ID="DGR_SELECTED" runat="server" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="1" Font-Names="Tahoma" Font-Size="X-Small"
                                        GridLines="Vertical" PageSize="20"
                                        ShowHeader="False" Width="100%" ForeColor="Black" OnItemCommand="DGR_SELECTED_ItemCommand" CellSpacing="1">
                                        <AlternatingItemStyle BackColor="White" />
                                        <ItemStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundColumn DataField="PRODUCT_CODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PRODUCT_NAME" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LBT" runat="server" CommandName="Delete" ForeColor="Red"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
