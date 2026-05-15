<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Provider_List.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Health.Provider_List" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="vertical-align: top;">
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td class="auto-style2">NAMA PROVIDER</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">NAMA GROUP</td>
                                        <td>
                                            <asp:TextBox ID="TXT_GROUP" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">KOTA</td>
                                        <td>
                                            <asp:TextBox ID="TXT_KOTA" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">PROPINSI</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PROPINSI" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">TITLE</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_TITLE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">JENIS</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_JENIS" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">STATUS</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True" Value="1">AKTIF</asp:ListItem>
                                                <asp:ListItem Value="0">NON AKTIF</asp:ListItem>
                                                <asp:ListItem Value="">AKTIF & NON AKTIF</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top;">
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">PKS BERAKHIR</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PKS" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                                <asp:ListItem Value="0">BELUM</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">RI - RAWAT INAP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_RI" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">RJ - RAWAT JALAN</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_RJ" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">MCU - MEDICAL CHECK UP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MCU" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">OPT - OPTIK</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_OPT" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">TOPUP - TOP UP BPJS</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_TOPUP" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">IDV - INDIVIDU</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_IDV" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">DRB - DOKTER BOOKING</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_DRB" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">LAB - RUJUKAN LAB</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_LAB" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style3">&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="CARI" OnClick="BT_SEARCH_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False">
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_SELECT" runat="server" CausesValidation="False">Select</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="KODE_PROVIDER" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TITLE" HeaderText="TITLE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA PROVIDER">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="JENIS_PROVIDER_DESCR" HeaderText="JENIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KOTA_DESCR" HeaderText="KOTA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PROPINSI_DESCR" HeaderText="PROPINSI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT_DESCR" HeaderText="STATUS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVIDER_GROUP" HeaderText="GROUP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RI" HeaderText="RI">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RJ" HeaderText="RJ">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MCU" HeaderText="MCU">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OPT" HeaderText="OPT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOPUP" HeaderText="TOPUP">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IDV" HeaderText="IDV">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DRB" HeaderText="DRB">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LAB" HeaderText="LAB">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_AKHIR_PKS" HeaderText="TGL AKHIR PKS">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EXP" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle Wrap="False" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
