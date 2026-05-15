<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member_Header.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Health.Member_Header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <table style="border-spacing: 0px;">
                                                <tr style="vertical-align: top;">
                                                    <td>
                                                        <table style="border: 1px solid grey; border-collapse: collapse;" border="1">
                                                            <tr>
                                                                <td style="width: 120px;">
                                                                    <asp:Label ID="Label1" runat="server" Text="NO PESERTA" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_REGNO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text="NAMA" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_NAMA" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text="NO POLIS" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_NOPOLIS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" Text="PERUSAHAAN" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_COMPANY" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Text="CABANG" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_CABANG" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text="TGL LAHIR" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_DOB" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Text="GENDER" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_SEX" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label15" runat="server" Text="VIP" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_VIP" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label17" runat="server" Text="FAMILY RELATION" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_FAMILY" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label19" runat="server" Text="TIPE IDENTITAS" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_IDTYPE" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label21" runat="server" Text="NO IDENTITAS" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_IDNO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <table style="border: 1px solid grey; border-collapse: collapse;" border="1">
                                                            <tr>
                                                                <td style="width: 120px;">
                                                                    <asp:Label ID="Label2" runat="server" Text="EMAIL" CssClass="ASPLabel"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="LB_EMAIL" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text="NO TELEPON" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_PHONE" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text="PLAN" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_PACKAGE" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" Text="KELAS" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_KELAS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" Text="TGL MASUK" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_TGLMASUK" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text="TGL KELUAR" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_TGLKELUAR" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Text="NO REKENING" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_NOREK" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label16" runat="server" Text="REKENING A/N" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_NOREKNAMA" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label18" runat="server" Text="BANK" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_NOREKBANK" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label20" runat="server" Text="STATUS PESERTA" CssClass="ASPLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LB_STATUS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                            <td style="width: 16px;"></td>
                            <td>
                                <asp:Label ID="Label24" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Underline="True" Text="MEMBERSHIP PERIOD"></asp:Label>
                                <br />
                                <asp:DropDownList ID="DDL_PERIOD" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_PERIOD_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Label ID="Label22" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Underline="True" Text="FAMILY RELATION"></asp:Label>
                                <asp:DataGrid ID="DGR_FAMILY" runat="server" CellPadding="4" PageSize="8" GridLines="Vertical" CssClass="ASPDatagrid" ForeColor="#333333" AutoGenerateColumns="False" BorderColor="#006600" AllowPaging="True" OnPageIndexChanged="DGR_FAMILY_PageIndexChanged">
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="REGNO" HeaderText="NO PESERTA"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NAMA" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="NAMA">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_FAMILY_NAMA" runat="server" CssClass="ASPLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="FAMILY_GROUP" HeaderText="FAMILY RELATION"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DOB" HeaderText="TGL LAHIR"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SEX" HeaderText="GENDER"></asp:BoundColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <ItemStyle BackColor="#E3EAEB" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>

                            </td>
                            <td style="width: 150px;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_5c" runat="server" CssClass="ASPButton" Font-Bold="True" Text="T/C POLIS" Width="100%" OnClick="BT_5c_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_5d" runat="server" CssClass="ASPButton" Font-Bold="True" Text="BENEFIT POLIS" Width="100%" OnClick="BT_5d_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_5b" runat="server" CssClass="ASPButton" Font-Bold="True" Text="PEMAKAIAN BENEFIT" Width="100%" OnClick="BT_5b_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_5" runat="server" CssClass="ASPButton" Font-Bold="True" Text="RIWAYAT CLAIM" Width="100%" OnClick="BT_5_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_5a" runat="server" CssClass="ASPButton" Font-Bold="True" Text="TRANSAKSI CLAIM" Width="100%" OnClick="BT_5a_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_MODE" runat="server" Font-Bold="True" CssClass="ASPLabel"></asp:Label>
                    <asp:Label ID="LB_MODE0" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
