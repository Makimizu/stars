<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_TC.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Period_TC" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing:0px; left:0px; top:0px; position:absolute;">
            <tr>
                <td style="width: 40%">
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" OnClick="BT_SAVE_Click" />
                    <asp:Button ID="BT_PRINT" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="#009999" Text="PRINT" Width="80px" OnClick="BT_PRINT_Click" />
                    <asp:Label ID="LB_PERIOD" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" CssClass="ASPDatagrid" AutoGenerateColumns="False" ShowHeader="False">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#4A3C8C" ForeColor="#CCCCFF" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="LB_BENEFIT" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="ASPDatagrid" Font-Names="Tahoma" Font-Size="X-Small" ForeColor="#333333" GridLines="None">
                                        <AlternatingItemStyle BackColor="White" />
                                        <ItemStyle BackColor="#E3EAEB" />
                                        <Columns>
                                            <asp:BoundColumn DataField="POLICY_PERIOD_ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TC_ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BENEFIT_DESCR" HeaderText="BENEFIT" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TC_VAL" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TC_DESCR" HeaderText="ITEM">
                                                <HeaderStyle Width="400px" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="VALUE">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DDL_LOADING_ID" runat="server" BackColor="Yellow" CssClass="ASPDropDownList" Enabled="true">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="ACTION" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Button ID="BTN_UPDATE" runat="server" CommandName="Update" CssClass="ASPButton" Text="Update" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
