<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_Benefit.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Period_Benefit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; left: 0px; top: 0px; position: absolute;">
            <tr>
                <asp:Label ID="LB_MSG" runat="server" Font-Bold="True" ForeColor="Blue" EnableViewState="false"></asp:Label>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" OnClick="BT_SAVE_Click" />
                    <asp:Label ID="LB_PERIOD" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" CssClass="ASPDatagrid" Width="100%">
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="PaleGoldenrod" ForeColor="Black" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="BENEFIT_ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_DESCR" HeaderText="BENEFIT" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Font-Bold="True"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="BENEFIT (%)" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="TXT_BENEFIT_PCT" CssClass="ASPTextBoxNumber" Width="30px"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DISCOUNT (%)" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="TXT_DISCOUNT_PCT" CssClass="ASPTextBoxNumber" Width="30px"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PROVIDER" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="CB_PROVIDER" />
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="REIMBURSE" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="CB_REIMBURSE" />
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="KAPITASI" ItemStyle-HorizontalAlign="Center" Visible="False">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="CB_KAPITASI" />
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ASO" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="CB_ASO" />
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="REMARK" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="TXT_REMARK" TextMode="MultiLine" CssClass="ASPTextBox" Width="600px" Height="100px"></asp:TextBox><br />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="600px" />

                                <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
