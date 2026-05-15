<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderPeriode.aspx.cs" Inherits="HEALTH.Form_Klaim.ProviderPeriode" %>

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
        <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table id="TBL_PERIODE" runat="server" visible="false" style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 140px;">TGL PERIODE</td>
                            <td>:</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_PERIODEDATE1" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender00" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PERIODEDATE1">
                                        </ajaxToolkit:CalendarExtender>
                                        &nbsp;-
                                                    <asp:TextBox ID="TXT_PERIODEDATE2" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender01" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PERIODEDATE2">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>NO DOKUMEN</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="TXT_PERIODEDOCNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL KONTRAK</td>
                            <td>:</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_PERIODEKONTRAKDATE" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PERIODEKONTRAKDATE">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                        </tr>
                        <tr>
                            <td>RENEW METHOD</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDL_PERIODERENEW" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_PERIODEADD" runat="server" CssClass="ASPButton" Text="TAMBAH PERIODE" OnClick="BT_PERIODEADD_Click" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_PERIODE" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_PERIODE_ItemCommand">
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AWAL PERIODE" HeaderText="AWAL PERIODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AKHIR PERIODE" HeaderText="AKHIR PERIODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NO DOKUMEN" HeaderText="NO DOKUMEN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL KONTRAK" HeaderText="TGL KONTRAK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RENEWAL METHOD" HeaderText="RENEWAL METHOD"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_PERIODEDEL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="DELETE" />
                                </ItemTemplate>
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
