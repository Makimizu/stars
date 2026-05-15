<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenjaminanAging.aspx.cs" Inherits="HEALTH.Form_Klaim.PenjaminanAging" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">PROVIDER</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PROVIDER" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PASIEN</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">TIPE RAWAT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>AGING</td>
                                        <td>
                                            <asp:TextBox ID="TXT_AGING1" runat="server" CssClass="ASPTextBox" Width="30px" Style="text-align: center;"></asp:TextBox>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_AGING2" runat="server" CssClass="ASPTextBox" Width="30px" Style="text-align: center;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="CARI" OnClick="BT_SEARCH_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <asp:Label ID="LB_RECORDS" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="20" OnPageIndexChanged="DGR_PageIndexChanged" OnItemCommand="DGR_ItemCommand"
                        AllowPaging="True" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="NO KLAIM">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_CLAIMNO" runat="server" CommandName="ClaimNo"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NO PENJAMINAN">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_PENJAMINAN" runat="server" CommandName="Penjaminan"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CLAIM_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOMOR_SURAT_JAMINAN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVIDER" HeaderText="PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE" HeaderText="TIPE RAWAT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INCURRED" HeaderText="PENGAJUAN">
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_MASUK" HeaderText="TGL MASUK">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_AKHIR" HeaderText="TGL AKHIR">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PHONE" HeaderText="TELEPON"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="EMAIL">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                    <asp:Button ID="BT_EMAIL" runat="server" CommandName="Email" CssClass="ASPButton" Text="SEND" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
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
