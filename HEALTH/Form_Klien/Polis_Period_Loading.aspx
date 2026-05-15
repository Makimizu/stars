<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_Loading.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Period_Loading" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
        <style type="text/css">
            .auto-style7 {
                width: 185px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="True"></asp:Label>
        <asp:Label ID="LB_PERIOD" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
        <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
        <asp:Label ID="LB_TOTAL_UJROH" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
        <asp:Label ID="LB_TOTAL_TABARU" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td class="auto-style7">
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" OnClick="BT_SAVE_Click" />
                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="4" ForeColor="Black" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="LOADING_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="READONLY" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LOADING_DESCR" HeaderText="LOADING UJROH"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="% VALUE">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_LOADING" runat="server" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Font-Size="Small" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle BackColor="#F7F7DE" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid></td>
                <td style="width: 200px;">
                    <asp:DataGrid ID="DGR_TABARRU" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="4" ForeColor="Black" OnItemDataBound="DGR_TABARRU_ItemDataBound" ShowFooter="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="LOADING_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="READONLY" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LOADING_DESCR" HeaderText="LOADING - TABARRU"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="% VALUE">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_LOADING_TABARRU" runat="server" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Font-Size="Small" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle BackColor="#F7F7DE" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE_TABARRU" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" OnClick="BT_SAVE_TABARRU_Click" />
                </td>
                <td>
                    <asp:Button ID="BT_SAVE_AGENT" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE COMMISSION" Width="150px" OnClick="BT_SAVE_AGENT_Click" />
                    <asp:DataGrid ID="DGR_AGENT" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="4" ForeColor="Black" OnItemDataBound="DGR_ItemDataBound" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="COMMISSION TYPE"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="AGENT CODE">
                                <HeaderStyle Width="100" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AGENTCODE" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CHANNEL" HeaderText="CHANNEL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="%">
                                <HeaderStyle Width="50" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_COMM" runat="server" CssClass="ASPTextBoxNumber" Width="90%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle BackColor="#F7F7DE" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
