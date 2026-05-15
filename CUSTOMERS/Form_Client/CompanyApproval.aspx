<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyApproval.aspx.cs" Inherits="CUSTOMERS.Form_Client.CompanyApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td width="120">KODE PERUSAHAAN
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY_CODE" runat="server" Width="250px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">NAMA PERUSAHAAN
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY_NAME" runat="server" Width="250px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">KATEGORI</td>
                            <td>
                                <asp:DropDownList ID="DDL_CATEGORY" AutoPostBack="true" runat="server" Font-Size="X-Small" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">CHANNEL
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" Font-Size="X-Small" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">LOB</td>
                            <td>
                                <asp:DropDownList ID="DDL_LOB" AutoPostBack="true" runat="server" Font-Size="X-Small" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">STATUS
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_COMPANY_STATUS" runat="server" Font-Size="X-Small" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">AGEN</td>
                            <td>
                                <asp:TextBox ID="TXT_AGENT" runat="server" Width="80px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">TGL. REGISTRASI
                            </td>
                            <td valign="top" class="auto-style1">
                                <asp:TextBox ID="TXT_REGDATEFROM" runat="server" Font-Size="X-Small" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" TargetControlID="TXT_REGDATEFROM" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-&nbsp;
                                    <asp:TextBox ID="TXT_REGDATETO" runat="server" Font-Size="X-Small" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="TXT_REGDATETO_CalendarExtender" runat="server" TargetControlID="TXT_REGDATETO" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_CARI" runat="server" Text="CARI" OnClick="BT_CARI_Click" CssClass="ASPButton" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True" EnableViewState="false" ForeColor="Blue"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        OnPageIndexChanged="DGR_PageIndexChanged"
                        AllowPaging="True" AutoGenerateColumns="False" CssClass="ASPDatagrid" ForeColor="#333333">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" BorderColor="Black" BorderWidth="1" />
                        <Columns>
                            <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="KODE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="KODE">
                                <ItemTemplate>
                                    <asp:Label ID="LB_CODE" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="NAMA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_CATEGORY_DESCR" HeaderText="KATEGORI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_LOB_DESCR" HeaderText="LOB"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROPINSI_DESCR" HeaderText="PROPINSI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_REGISTERED_DATE" HeaderText="TGL REGISTRASI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT_DESCR" HeaderText="STATUS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT" HeaderText="AGEN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CHANEL_DIST_DESCR" HeaderText="CHANEL DIST"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_APPROVAL" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                            Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
