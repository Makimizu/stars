<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_BAN_List.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Proses_BAN_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px;">
            <tr>
                <td>
                    <asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#DEDFDE"
                        BorderWidth="1px" CellPadding="2" ForeColor="Black" OnItemCommand="DGR_ItemCommand"
                        CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" BorderStyle="None" GridLines="Vertical">
                        <ItemStyle BackColor="#F7F7DE" Wrap="False" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle ForeColor="White" BackColor="#CE5D5A" Font-Bold="true" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" BackColor="Red" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REGNO" HeaderText="REGNO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_UPDATE_BANKACC" HeaderText="TGL UPDATE REKENING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OLD_ACC_BANK" HeaderText="BANK LAMA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OLD_ACC_NO" HeaderText="BANK ACC NO LAMA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OLD_ACC_NAMA" HeaderText="BANK ACC NAME LAMA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_ACC_BANK" HeaderText="BANK BARU" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_ACC_NO" HeaderText="BANK ACC NO BARU" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_ACC_NAMA" HeaderText="BANK ACC NAME BARU" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="REKENING LAMA">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr style="vertical-align: top;">
                                            <td>BANK</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_OLD_BANK" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>ACC NO</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_OLD_ACCNO" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>NAMA</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_OLD_ACCNAMA" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="REKENING BARU">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr style="vertical-align: top;">
                                            <td>BANK</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_NEW_BANK" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>ACC NO</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_NEW_ACCNO" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>NAMA</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_NEW_ACCNAMA" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>

                        <EditItemStyle Wrap="True"></EditItemStyle>

                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" Wrap="False" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
