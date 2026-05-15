<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Endorsement_Approval.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Endorsement_Approval" %>
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
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;">
            <tr id="TR1" runat="server">
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td style="width:100px;">PERUSAHAAN</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NO POLIS</td>
                            <td>
                                <asp:TextBox ID="TXT_POLNO" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="CARI" />
                            &nbsp;</td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" CssClass="ASPLabel" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr id="TR2" runat="server">
                <td>
                    <asp:Label ID="LB_RECORD" runat="server" Text="" Font-Bold="true" CssClass="ASPLabel"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="40" OnPageIndexChanged="DGR_PageIndexChanged"
                        AllowPaging="True" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="NO POLIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ" HeaderText="#"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PERIOD" HeaderText="MASA POLIS">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CREATEBY" HeaderText="USER REG">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CREATEDATE" HeaderText="USER DATE">
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
            </table>
    </form>
</body>
</html>
