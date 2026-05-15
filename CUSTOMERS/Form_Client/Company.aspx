<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="CUSTOMERS.Form_Client.Company" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div>
            <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
                <tr>
                    <td>
                        <asp:Label ID="LB_APP_ID" runat="server" Visible="false"></asp:Label>
                        <table style="border-spacing: 0px;">
                            <tr style="vertical-align: top;">
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
                                                <asp:DropDownList ID="DDL_CATEGORY" AutoPostBack="true" runat="server" Font-Size="X-Small" Font-Names="Tahoma" CssClass="ASPDropDownList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="120">SUB CHANNEL
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" Font-Size="X-Small" Font-Names="Tahoma" CssClass="ASPDropDownList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="120">LOB</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_LOB" AutoPostBack="true" runat="server" Font-Size="X-Small" Font-Names="Tahoma" CssClass="ASPDropDownList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20px;"></td>
                                <td>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td width="120">STATUS
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDL_COMPANY_STATUS" runat="server" Font-Size="X-Small" Font-Names="Tahoma" CssClass="ASPDropDownList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="120">KODE AGEN</td>
                                            <td>
                                                <asp:TextBox ID="TXT_AGENT" runat="server" Width="80px" CssClass="ASPTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="120">NAMA AGEN</td>
                                            <td>
                                                <asp:TextBox ID="TXT_AGENT_NAME" runat="server" Width="200px" CssClass="ASPTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1">TGL. REGISTRASI
                                            </td>
                                            <td valign="top" class="auto-style1">
                                                <asp:TextBox ID="TXT_REGDATEFROM" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" TargetControlID="TXT_REGDATEFROM" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                &nbsp;-&nbsp;
                                                <asp:TextBox ID="TXT_REGDATETO" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True" EnableViewState="false" ForeColor="Blue"></asp:Label>
                        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                            OnPageIndexChanged="DGR_PageIndexChanged"
                            AllowPaging="True" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="None" ForeColor="#333333">
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#E3EAEB" Wrap="False" BorderColor="Black" BorderWidth="0" />
                            <Columns>
                                <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="KODE" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="KODE">
                                    <ItemTemplate>
                                        <asp:Label ID="LB_CODE" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="NAMA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_CATEGORY_DESCR" HeaderText="KATEGORI"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROPINSI_DESCR" HeaderText="PROPINSI"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_REGISTERED_DATE" HeaderText="TGL REG"></asp:BoundColumn>
                                <asp:BoundColumn DataField="STAT_DESCR" HeaderText="STATUS"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AGENT" HeaderText="AGEN"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AGENT_NAME" HeaderText="NAMA AGEN"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SUB_CHANEL_DIST_DECSR" HeaderText="SUBCHANEL DIST"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_LOB_DESCR" HeaderText="LOB"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                                Wrap="False" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <EditItemStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
