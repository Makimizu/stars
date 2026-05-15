<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenjaminanInquiry.aspx.cs" Inherits="HEALTH.Form_Klaim.PenjaminanInquiry" %>

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
                                        <td class="auto-style2">NO SURAT JAMINAN</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NO_SURJAM" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">NAMA PESERTA</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">NO POLIS</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NOPOL" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">PERUSAHAAN</td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">PROVIDER</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PROVIDER" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">VIP</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_VIP" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                                <asp:ListItem Value="0">TIDAK</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                                <asp:Label ID="LB_MODE" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                            </td>
                            <td style="vertical-align: top;">
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td style="width: 140px;">TIPE SURAT JAMINAN</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td class="auto-style2">SEDANG DIMONITOR ?</td>
                                        <td class="style7">
                                            <asp:DropDownList ID="DDL_MON" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="1">YA</asp:ListItem>
                                                <asp:ListItem Value="0">TIDAK</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td>TGL MASUK</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_MASUK1" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    &nbsp;-
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_MASUK1">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:TextBox ID="TXT_MASUK2" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_MASUK2">
                                                    </ajaxToolkit:CalendarExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>TGL PULANG</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_PULANG1" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    &nbsp;-
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PULANG1">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:TextBox ID="TXT_PULANG2" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PULANG2">
                                                    </ajaxToolkit:CalendarExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>BIAYA AKHIR</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BIAYA1" runat="server" CssClass="ASPTextBoxNumber" Width="90px"></asp:TextBox>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_BIAYA2" runat="server" CssClass="ASPTextBoxNumber" Width="90px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
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
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" OnItemCommand="DGR_ItemCommand">
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
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
