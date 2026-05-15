<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationMemberDisease.aspx.cs" Inherits="LQ.Form_Client.QuotationMemberDisease" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_MEMBERID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td class="TDBGColor">RIWAYAT PENYAKIT</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 400px;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr style="vertical-align: top;">
                                        <td style="width: 120px;">PENYAKIT</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ICD" runat="server" CssClass="ASPTextBox" AutoPostBack="true" Width="90%" OnTextChanged="TXT_ICD_TextChanged" placeholder="Cari nama penyakit .."></asp:TextBox>
                                            <asp:DropDownList ID="DDL_ICD" runat="server" CssClass="ASPDropDownList" Width="90%" BackColor="LightYellow"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>TEMPAT RAWAT</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PROVIDER" runat="server" CssClass="ASPTextBox" AutoPostBack="true" Width="90%" OnTextChanged="TXT_PROVIDER_TextChanged" placeholder="Cari nama RS/Klinik .."></asp:TextBox>
                                            <asp:DropDownList ID="DDL_PROVIDER" runat="server" CssClass="ASPDropDownList" Width="90%" BackColor="LightGreen"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TANGGAL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>HASIL SAAT ITU</td>
                                        <td>
                                            <asp:TextBox ID="TXT_REMARK1" runat="server" Width="90%" TextMode="MultiLine" MaxLength="255" Height="50" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>HASIL SEKARANG</td>
                                        <td>
                                            <asp:TextBox ID="TXT_REMARK2" runat="server" Width="90%" TextMode="MultiLine" MaxLength="255" Height="50" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="3" Font-Names="Tahoma" Font-Size="8pt" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" Width="100%" CssClass="ASPDatagrid" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" ShowHeader="False" OnItemCommand="DGR_ItemCommand">
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" VerticalAlign="Top" ForeColor="#4A3C8C" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SEQ" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ICD_DESCR" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PROVIDER_DESCR" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="THEDATE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REMARK1" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REMARK2" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                                    <tr>
                                                        <td style="width: 100px;">PENYAKIT</td>
                                                        <td>
                                                            <asp:Label ID="LB_ICD" runat="server" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>TEMPAT RAWAT</td>
                                                        <td>
                                                            <asp:Label ID="LB_PROVIDER" runat="server" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>TANGGAL</td>
                                                        <td>
                                                            <asp:Label ID="LB_DATE" runat="server" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="vertical-align: top;">
                                                        <td>HASIL SAAT ITU</td>
                                                        <td>
                                                            <asp:Label ID="LB_REMARK1" runat="server" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="vertical-align: top;">
                                                        <td>HASIL SEKARANG</td>
                                                        <td>
                                                            <asp:Label ID="LB_REMARK2" runat="server" Font-Bold="true"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="30" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
