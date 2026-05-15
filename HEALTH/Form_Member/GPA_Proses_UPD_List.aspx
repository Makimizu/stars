<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_UPD_List.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Peserta_UpdateDate_List" %>

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
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR_TEMP" runat="server" CellPadding="4" ForeColor="#333333" OnItemCommand="DGR_TEMP_ItemCommand"
                        CssClass="ASPDatagrid" AutoGenerateColumns="False" ItemStyle-Wrap="false" ShowFooter="True" GridLines="Vertical" OnItemDataBound="DGR_TEMP_ItemDataBound" BorderColor="#CCCCCC">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" VerticalAlign="Top" Wrap="False" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" BackColor="Red" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REGNO" HeaderText="REGNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PACKAGE" HeaderText="PACKAGE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GROUP_AGE" HeaderText="GROUP_AGE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_GROUP_AGE" HeaderText="NEW_GROUP_AGE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DATA PESERTA">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; width:300px;">
                                        <tr>
                                            <td style="width:80px;">NAMA</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_NAMA" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>REGNO</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_REGNO" runat="server" CssClass="ASPLabel"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>TGL LAHIR</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_DOB" runat="server" CssClass="ASPLabel"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>PAKET</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_PACKAGE" runat="server" CssClass="ASPLabel"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>GRUP/USIA</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_GROUPAGE" runat="server" CssClass="ASPLabel"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>GRUP/USIA BARU</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_NEWGROUPAGE" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA ENDORSEMENT">
                                <HeaderStyle Width="500px" />
                                <ItemTemplate>
                                    <asp:DataGrid ID="DGR_ENDORS_DETAIL" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="Vertical" PageSize="20" ShowHeader="False" BorderColor="#333300">
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <ItemStyle BackColor="#E3EAEB" />
                                        <Columns>
                                            <asp:BoundColumn DataField="FINANCIAL">
                                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" Width="100px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="REFF" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCR">
                                                <ItemStyle Width="150px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="OLD_VAL_DESCR" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NEW_VAL_DESCR" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle Width="150px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="LB_OLDVAL" runat="server" CssClass="ASPLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle Width="150px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="LB_NEWVAL" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    </asp:DataGrid>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="OLD_PREMIUM" HeaderText="PREMI LAMA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_PREMIUM" HeaderText="PREMI BARU">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OFFSET" HeaderText="OFFSET">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CHARGE" HeaderText="CHARGE">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <ItemStyle Wrap="false" BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle ForeColor="#333333" BackColor="#C5BBAF" Font-Bold="true" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
