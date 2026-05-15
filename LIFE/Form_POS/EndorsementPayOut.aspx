<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementPayOut.aspx.cs" Inherits="LIFE.Form_POS.EndorsementPayOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_UNITIZE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TOTALAMOUNT" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_DESCRAMOUNT" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_LINK" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemDataBound="DGR_LINK_ItemDataBound" ShowFooter="True">
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#666666" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="FUND_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="READ_ONLY" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNIT_AMOUNT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FUND_DESCR" HeaderText="FUND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_NAV_DATE" HeaderText="LAST<BR>NAV DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UNIT_PRICE" HeaderText="LAST<BR>NAV">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AVAILABLE_UNIT" HeaderText="UNIT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EST_AMOUNT" HeaderText="ESTIMATED<BR>AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="WITHDRAW">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_UNIT_LINK" runat="server" CssClass="ASPTextBoxNumber" Width="45" BackColor="LightCyan" ForeColor="Blue" onkeyup="FormatCurrency(this);"></asp:TextBox>
                                    <asp:TextBox ID="TXT_AMOUNT_LINK" runat="server" CssClass="ASPTextBoxNumber" Width="50" BackColor="LightGreen" ForeColor="DarkGreen" onkeyup="FormatCurrency(this);"></asp:TextBox>
                                    <asp:DropDownList ID="DDL_TOGGLE" CssClass="ASPDropDownList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDL_TOGGLE_SelectedIndexChanged"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:DataGrid ID="DGR" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnItemDataBound="DGR_ItemDataBound" OnItemCommand="DGR_ItemCommand">
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DC" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNIT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="READONLY" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_BUTTON" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="ITEMS"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="80" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_URL" runat="server" CssClass="ASPButton" Width="120" CommandName="URL" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CURRENCY_CODE">
                                <ItemStyle Width="20" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="WITHDRAW<BR>AMOUNT">
                                <HeaderStyle Width="80" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="95%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>



                    <asp:CheckBoxList ID="chkReason" runat="server" RepeatColumns="2">
    <asp:ListItem Text="Butuh Dana" Value="Butuh Dana"></asp:ListItem>
    <asp:ListItem Text="Biaya Sekolah / Kuliah" Value="Biaya Sekolah / Kuliah"></asp:ListItem>
    <asp:ListItem Text="Berhenti menggunakan asuransi" Value="Berhenti menggunakan asuransi"></asp:ListItem>
    <asp:ListItem Text="Kapok Berasuransi / Investasi tidak berkembang" Value="Kapok Berasuransi / Investasi tidak berkembang"></asp:ListItem>
    <asp:ListItem Text="Dampak Covid / Tdk Bekerja / Pensiun" Value="Dampak Covid / Tdk Bekerja / Pensiun"></asp:ListItem>
    <asp:ListItem Text="Tanpa alasan" Value="Tanpa alasan"></asp:ListItem>
</asp:CheckBoxList>

<%--<asp:Button ID="Button1" runat="server" Text="SAVE" OnClick="BT_SAVE_Click" />--%>

                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
