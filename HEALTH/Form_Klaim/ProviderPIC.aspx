<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderPIC.aspx.cs" Inherits="HEALTH.Form_Klaim.ProviderPIC" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="BT_PIC_ADD" runat="server" CssClass="ASPButton" OnClick="BT_PIC_ADD_Click" Text="TAMBAHAN PIC" />

                    <table id="TBL_PIC_ADD" runat="server" visible="false" style="border-spacing: 0px">
                        <tr>
                            <td style="width: 60px;">NAMA</td>
                            <td class="auto-style13">:</td>
                            <td>
                                <asp:TextBox ID="TXT_PIC_NAMA_ADD" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TITLE</td>
                            <td class="auto-style13">:</td>
                            <td>
                                <asp:DropDownList ID="DDL_PIC_TITLE_ADD" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ALAMAT</td>
                            <td class="auto-style13">:</td>
                            <td>
                                <asp:TextBox ID="TXT_PIC_ALAMAT_ADD" runat="server" CssClass="ASPTextBox" MaxLength="200" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TELP</td>
                            <td class="auto-style13">:</td>
                            <td>
                                <asp:TextBox ID="TXT_PIC_TELP_ADD" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TELP EXT</td>
                            <td class="auto-style13">:</td>
                            <td>
                                <asp:TextBox ID="TXT_PIC_EXT_ADD" runat="server" CssClass="ASPTextBox" MaxLength="10" Width="60px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>HP</td>
                            <td class="auto-style13">:</td>
                            <td>
                                <asp:TextBox ID="TXT_PIC_HP_ADD" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>EMAIL</td>
                            <td class="auto-style13">:</td>
                            <td>
                                <asp:TextBox ID="TXT_PIC_EMAIL_ADD" runat="server" CssClass="ASPTextBox" MaxLength="500" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style13">&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_PIC_ADD0" runat="server" CssClass="ASPButton" OnClick="BT_PIC_ADD0_Click" Text="TAMBAH" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR_PIC" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" AutoGenerateColumns="False" CssClass="ASPDatagrid" OnItemCommand="DGR_PIC_ItemCommand" ShowHeader="False">
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KODE_PIC" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA_PIC" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ALAMAT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TELP" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TELP_EXT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="HP" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px">
                                        <tr>
                                            <td style="text-align: left; vertical-align: top; width: 60px;">NAMA</td>
                                            <td style="text-align: left; vertical-align: top;">:</td>
                                            <td style="text-align: left; vertical-align: top; width: 300px;">
                                                <asp:Label ID="LB_PIC_NAMA" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                <asp:TextBox ID="TXT_PIC_NAMA" runat="server" CssClass="ASPTextBox" MaxLength="100" Visible="False" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; vertical-align: top;">TITLE</td>
                                            <td style="text-align: left; vertical-align: top;">:</td>
                                            <td style="text-align: left; vertical-align: top; width: 300px;">
                                                <asp:Label ID="LB_PIC_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                <asp:DropDownList ID="DDL_PIC_TITLE" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; vertical-align: top;">ALAMAT</td>
                                            <td style="text-align: left; vertical-align: top;">:</td>
                                            <td style="text-align: left; vertical-align: top; width: 300px;">
                                                <asp:Label ID="LB_PIC_ALAMAT" runat="server" CssClass="ASPLabel" Font-Bold="False"></asp:Label>
                                                <asp:TextBox ID="TXT_PIC_ALAMAT" runat="server" CssClass="ASPTextBox" MaxLength="200" Visible="False" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; vertical-align: top;">TELP</td>
                                            <td style="text-align: left; vertical-align: top;">:</td>
                                            <td style="text-align: left; vertical-align: top; width: 300px;">
                                                <asp:Label ID="LB_PIC_TELP" runat="server" CssClass="ASPLabel" Font-Bold="False"></asp:Label>
                                                <asp:TextBox ID="TXT_PIC_TELP" runat="server" CssClass="ASPTextBox" MaxLength="100" Visible="False" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; vertical-align: top;">TELP EXT</td>
                                            <td style="text-align: left; vertical-align: top;">:</td>
                                            <td style="text-align: left; vertical-align: top; width: 300px;">
                                                <asp:Label ID="LB_PIC_EXT" runat="server" CssClass="ASPLabel" Font-Bold="False"></asp:Label>
                                                <asp:TextBox ID="TXT_PIC_EXT" runat="server" CssClass="ASPTextBox" MaxLength="10" Visible="False" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; vertical-align: top;">HP</td>
                                            <td style="text-align: left; vertical-align: top;">:</td>
                                            <td style="text-align: left; vertical-align: top; width: 300px;">
                                                <asp:Label ID="LB_PIC_HP" runat="server" CssClass="ASPLabel" Font-Bold="False"></asp:Label>
                                                <asp:TextBox ID="TXT_PIC_HP" runat="server" CssClass="ASPTextBox" MaxLength="50" Visible="False" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; vertical-align: top;">EMAIL</td>
                                            <td style="text-align: left; vertical-align: top;">:</td>
                                            <td style="text-align: left; vertical-align: top; width: 300px;">
                                                <asp:Label ID="LB_PIC_EMAIL" runat="server" CssClass="ASPLabel" Font-Bold="False"></asp:Label>
                                                <asp:TextBox ID="TXT_PIC_EMAIL" runat="server" CssClass="ASPTextBox" MaxLength="500" Visible="False" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_PIC_EDIT" runat="server" CommandName="Edit" CssClass="ASPButton" Text="Edit" Width="55px" />
                                    <asp:Button ID="BT_PIC_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="Save" Width="55px" Visible="False" />
                                    <br />
                                    <asp:Button ID="BT_PIC_DELETE" runat="server" CommandName="Delete" CssClass="ASPButton" Width="55px" Font-Bold="True" ForeColor="Red" Text="Delete" />
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
