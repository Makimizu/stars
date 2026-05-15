<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Provider.aspx.cs" Inherits="HEALTH.Form_Klaim.Provider" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            height: 33px;
        }
    </style>
</head>
<body> <%--By Ferdi V2--%>
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
                                        <td class="auto-style1"></td>
                                        <td class="auto-style1">PKS BERAKHIR</td>
                                        <td class="auto-style1">
                                            <asp:DropDownList ID="DDL_PKS" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                                <asp:ListItem Value="0">BELUM</asp:ListItem>
                                            </asp:DropDownList>
                                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox
                                               ID="chk_2_5Bulan"
                                                  runat="server"
                                                 Text="2,5 Bulan Akan Berakhir"
                                                 CssClass="ASPCheckbox" />
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
                            <td style="vertical-align: top;">
                                <asp:Button runat="server" ID="BT_CP_LIST" CssClass="ASPButton" Text="CP by RS" OnClick="BT_CP_LIST_Click" Visible="false"/>
                                <asp:Button runat="server" ID="BT_CP" CssClass="ASPButton" Text="Report CP by Provider" OnClick="BT_CP_Click" Font-Size="16px" />
                                <asp:Button runat="server" ID="BT_TR_QUO" CssClass="ASPButton" Text="Report Tarif by Quotation" OnClick="BT_TR_Quo_Click" Font-Size="16px" />
                                <asp:Button runat="server" ID="BT_TR0" CssClass="ASPButton" Text="Report Tarif by Provider" OnClick="BT_TR_Click" Font-Size="16px" />
                                <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="700px" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                                                <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" onClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                <iframe id="ifClaim" runat="server" src=""  width="100%" height="450"></iframe>
                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                           
                                <asp:Button runat="server" ID="BT_TR1" CssClass="ASPButton" Text="Report Tarif ICD by Provider" OnClick="BT_TR_ICD_Click" Font-Size="16px" />                           

                            </td>
                            <td>   &nbsp;</td>
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
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" OnItemCommand="DGR_ItemCommand" AutoGenerateColumns="False">
                        <ItemStyle Wrap="False" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
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
                            <asp:BoundColumn DataField="TGL_AKHIR_PKS" HeaderText="TGL AKHIR PKS" DataFormatString="{0:dd MMM yyyy}">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EXP" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS_TGL_AKHIR_PKS" HeaderText="STATUS TGL AKHIR PKS">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"/>
                                <ItemStyle HorizontalAlign="Center" ForeColor="Red" Font-Bold="True" BorderColor="Black" BorderWidth="0px" />
                             </asp:BoundColumn>
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
