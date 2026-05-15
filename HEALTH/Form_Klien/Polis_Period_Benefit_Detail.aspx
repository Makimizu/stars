<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_Benefit_Detail.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Period_Benefit_Detail" %>

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
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" OnClick="BT_SAVE_Click" />
                                &nbsp;<asp:DropDownList ID="DDL_PAKET" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_PAKET_SelectedIndexChanged"></asp:DropDownList>
                                <asp:Label ID="LB_PERIOD" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                                <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="text-align: right;">

                                <asp:Label ID="LB_REPORT_URL" runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="BT_TPA_PLAN_IMPORT" runat="server" CssClass="ASPButton" OnClick="BT_TPA_PLAN_IMPORT_Click" Text="TPA PLAN IMPORT" Visible="False" BackColor="Green" Font-Bold="True" ForeColor="White" />

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:DataGrid ID="DGR_PAKET_BENEFIT" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" OnItemCommand="DGR_PAKET_BENEFIT_ItemCommand">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN" Visible="false">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="LB_BENEFIT" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                    <br />
                                    <asp:Button ID="BT_BENEFITADD" runat="server" CommandName="Add" CssClass="ASPButton" Text="ADD :" />
                                    <asp:DropDownList ID="DDL_BENEFIT" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                    <asp:DataGrid ID="DGR_DETAIL" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_DETAIL_ItemCommand">
                                        <EditItemStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Left" BackColor="#507CD1" ForeColor="White" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DIS_BENEFIT_DETAIL_NAME" HeaderText="BENEFIT">
                                                <HeaderStyle Width="600px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="KUNJUNGAN" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FREQ_ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="UP_P" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="UP_R" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="UNIT">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DDL_UNIT" runat="server" CssClass="ASPDropDownList">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="MAX">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MAX" runat="server" CssClass="ASPTextBox" MaxLength="4" Width="40px" Style="text-align: center;" BackColor="#FFFF66"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="UP PROVIDER">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_UPP" runat="server" CssClass="ASPTextBoxNumber" Width="100px" BackColor="#CCFFCC"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="UP REIMBURSE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_UPR" runat="server" CssClass="ASPTextBoxNumber" Width="100px" BackColor="#66FFFF"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <ItemStyle BackColor="#EFF3FB" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
