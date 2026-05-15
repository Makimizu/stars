<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationMemberAddInfo.aspx.cs" Inherits="LQ.Form_Client.QuotationPolicySendAddress" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function CheckNumeric() {
            return event.keyCode >= 48 && event.keyCode <= 57;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_MEMBERID" runat="server" Visible="false"></asp:Label>
        <table id="TBL_SEND_ADDRESS" runat="server" style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td class="TDBGColor">POLICY ADDRESS</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 400px;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr style="vertical-align: top;">
                                        <td style="width: 120px;">ALAMAT PENGIRIMAN POLIS</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICY_ADDRESS" runat="server" CssClass="ASPTextBox" AutoPostBack="true" Width="90%" OnTextChanged="TXT_POLICY_ADDRESS_TextChanged" placeholder="Cari Nama Kantor .."></asp:TextBox>
                                            <asp:DropDownList ID="DDL_POLICY_ADDRESS" runat="server" CssClass="ASPDropDownList" Width="90%" BackColor="LightYellow"></asp:DropDownList>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table id="TBL_MEDICAL_HISTORY" runat="server" style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server" Font-Bold="true" Text="RIWAYAT KESEHATAN"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_MEDICAL_HISTORY" runat="server" AutoGenerateColumns="False" CellPadding="3" Font-Names="Tahoma" Font-Size="8pt" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" Width="100%" CssClass="ASPDatagrid" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" VerticalAlign="Top" ForeColor="#4A3C8C" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ" HeaderText="No">
                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RELATION_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ALIVE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="HEALTH_CONDITION" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGE_AT_DEATH" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="YEAR_OF_DEATH" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CAUSE_OF_DEATH" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RELATION" HeaderText="Hubungan"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_ALIVE" runat="server" CssClass="ASPDropDownList">
                                        <asp:ListItem Value="1">HIDUP</asp:ListItem>
                                        <asp:ListItem Value="0">MENINGGAL</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Umur">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AGE" runat="server" CssClass="ASPTextBoxNumber" Width="50px" MaxLength="3" onkeypress="return CheckNumeric();"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kondisi<br/>Kesehatan">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_HEALTH_CONDITION" runat="server" CssClass="ASPTextBoxUPPER" Width="250px" MaxLength="255"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Umur<br/>Ketika Meninggal">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AGE_AT_DEATH" runat="server" CssClass="ASPTextBoxNumber" Width="50px" MaxLength="3" onkeypress="return CheckNumeric();"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tahun<br/>Meninggal">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_YEAR_OF_DEATH" runat="server" CssClass="ASPTextBoxNumber" Width="50px" MaxLength="4" onkeypress="return CheckNumeric();"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Sebab<br/>Meninggal">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_CAUSE_OF_DEATH" runat="server" CssClass="ASPTextBoxUPPER" Width="250px" MaxLength="255"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <br/>
                    <asp:Button ID="BT_SAVE_MEDICAL_HISTORY" runat="server" CssClass="ASPButton" Text="SAVE" Width="150" OnClick="BT_SAVE_MEDICAL_HISTORY_Click" Visible="True" />
                </td>
            </tr>
            <tr id="TR_IF" runat="server" visible="false">
                <td>
                    <iframe id="IF" runat="server" src="" style="width: 100%; height: 400px; overflow: auto; border: 0;"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
