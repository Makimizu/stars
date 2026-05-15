<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderLayanan.aspx.cs" Inherits="HEALTH.Form_Klaim.ProviderLayanan" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>

                    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>

                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" AutoGenerateColumns="False" CssClass="ASPDatagrid" OnItemCommand="DGR_BENEFIT_ItemCommand">
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="STATUS">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_BENSAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_LAY_ADD" runat="server" CssClass="ASPButton" OnClick="BT_LAY_ADD_Click" Text="TAMBAHAN LAYANAN" />
                    <table id="TBL_LAY_ADD" runat="server" visible="false" style="border-spacing: 0px">
                        <tr>
                            <td>TIPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_LAY_TIPE_ADD" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>LAYANAN</td>
                            <td>
                                <asp:TextBox ID="TXT_LAY_LAYANAN_ADD" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>UNGGULAN</td>
                            <td>
                                <asp:DropDownList ID="DDL_LAY_UNG_ADD" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Selected="True" Value="0">TDK</asp:ListItem>
                                    <asp:ListItem Value="1">YA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>TARIF</td>
                            <td>
                                <asp:TextBox ID="TXT_LAY_TARIF_ADD" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL BERLAKU</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_LAY_TGL_ADD" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_LAY_TGL_ADD">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_LAY_ADD2" runat="server" CssClass="ASPButton" Text="TAMBAH" OnClick="BT_LAY_ADD2_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR_LAYANAN" runat="server" CellPadding="4" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" ForeColor="#333333" AutoGenerateColumns="False" BorderColor="#006600" OnItemCommand="DGR_LAYANAN_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_LAYANAN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAYANAN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNGGULAN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TARIF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_BERLAKU" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="TIPE">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_LAY_TIPE" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="LAYANAN">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_LAY_LAYANAN" runat="server" CssClass="ASPTextBox" Width="300px" MaxLength="255"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="UNGGULAN">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_LAY_UNG" runat="server" CssClass="ASPDropDownList">
                                        <asp:ListItem Selected="True" Value="0">TDK</asp:ListItem>
                                        <asp:ListItem Value="1">YA</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TARIF">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_LAY_TARIF" runat="server" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TGL BERLAKU">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TXT_LAY_TGLEDIT" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender40" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_LAY_TGLEDIT">
                                            </ajaxToolkit:CalendarExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_LAY_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="Save" />
                                    &nbsp;<asp:Button ID="BT_LAY_DELETE" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
