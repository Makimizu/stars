<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Endorsement_BenefitDetail.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Endorsement_BenefitDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:ListBox ID="LB_PLAN" runat="server" AutoPostBack="True" BackColor="#CCCCCC" CssClass="ASPDropDownList" Height="400px" OnSelectedIndexChanged="LB_PLAN_SelectedIndexChanged" Width="120px"></asp:ListBox>
                </td>
                <td>
                    <table id="TBL_PACKAGE" runat="server" visible="false" style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <asp:Label ID="LB_PLANCODE" runat="server" CssClass="ASPLabel" Font-Bold="False"></asp:Label>
                                <asp:Label ID="LB_PLANCODEID" runat="server" CssClass="ASPLabel" Font-Bold="True" Visible="False"></asp:Label>
                                <asp:Label ID="LB_BENEFITID" runat="server" CssClass="ASPLabel" Font-Bold="True" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CommandName="Add" CssClass="ASPButton" Text="SAVE" Font-Bold="True" ForeColor="Blue" OnClick="BT_SAVE_Click" Width="100px" />
                                <br />
                                <asp:Button ID="BT_BENEFITADD" runat="server" CommandName="Add" CssClass="ASPButton" Text="ADD :" OnClick="BT_BENEFITADD_Click" Width="100px" />
                                <asp:DropDownList ID="DDL_BENEFIT" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                                <br />
                                <asp:Button ID="BT_PLANCOPY" runat="server" CommandName="Add" CssClass="ASPButton" Text="COPY FROM :" OnClick="BT_PLANCOPY_Click" Width="100px" />
                                <asp:DropDownList ID="DDL_PLANBENEFIT" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
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
                                                <asp:TextBox ID="TXT_UPP" runat="server" CssClass="ASPTextBoxNumber" Width="80px" BackColor="#CCFFCC"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="UP REIMBURSE">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_UPR" runat="server" CssClass="ASPTextBoxNumber" Width="80px" BackColor="#66FFFF"></asp:TextBox>
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
