<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderTarif.aspx.cs" Inherits="HEALTH.Form_Klaim.ProviderTarif" %>

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
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="width: 80px;">
                            <td>JENIS TARIF</td>
                            <td>
                                <asp:DropDownList ID="DDL_TRF_KODE" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_TRF_KODE_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>KELAS KAMAR</td>
                            <td>
                                <asp:DropDownList ID="DDL_TRF_KAMAR" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_TRF_KODE_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>SUB TARIF</td>
                            <td>
                                <asp:DropDownList ID="DDL_TRF_SUB" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_TRF_KODE_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>KETERANGAN</td>
                            <td>
                                <asp:TextBox ID="TXT_TRF_DESCR" runat="server" CssClass="ASPTextBox" Width="184px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TARIF</td>
                            <td>
                                <asp:TextBox ID="TXT_TRF_AMOUNT" runat="server" CssClass="ASPTextBox" Width="76px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL BERLAKU</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_TRF_TGLBERLAKU" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TRF_TGLBERLAKU">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_TRF_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_TRF_SAVE_Click" Text="TAMBAH TARIF" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_TARIF" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_TARIF_ItemCommand">
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KODE_TARIF_DESCR" HeaderText="JENIS TARIF"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KELAS_KAMAR_DESCR" HeaderText="KELAS KAMAR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUB_TARIF_DESCR" HeaderText="SUB TARIF"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TARIF_DESCR" HeaderText="KETERANGAN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TARIF" HeaderText="TARIF" Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_BERLAKU" HeaderText="TGL BERLAKU" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="KETERANGAN">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_TRF_KET" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TARIF">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_TRF_RP" runat="server" CssClass="ASPTextBoxNumber" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TGL BERLAKU">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TXT_TRF_TGL" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender0" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TRF_TGL">
                                            </ajaxToolkit:CalendarExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_EDIT" runat="server" CommandName="Edit" CssClass="ASPButton" Text="SAVE" />
                                    &nbsp;<asp:Button ID="BT_DELETE" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="DELETE" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <PagerStyle BackColor="#999999" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
