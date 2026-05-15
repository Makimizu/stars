<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_Premi.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Period_Premi" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px;">
            <tr>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" OnClick="BT_SAVE_Click" />
                    &nbsp;<asp:DropDownList ID="DDL_PAKET" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_PAKET_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="LB_PERIOD" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" AutoGenerateColumns="False" ShowHeader="False">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#4A3C8C" ForeColor="#CCCCFF" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="LB_PLAN" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                    <asp:DataGrid ID="DGR_PREMIUM" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="None" Width="300px">
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Left" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="GENDER" HeaderText="GENDER">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="MIN_AGE" HeaderText="MIN USIA">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="MAX_AGE" HeaderText="MAX USIA">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="PREMIUM" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="PREMIUM">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PREMIUM" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#CCCCFF" HorizontalAlign="Center" />
                                        <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
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
