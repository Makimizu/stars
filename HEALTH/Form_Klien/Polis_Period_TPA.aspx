<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_TPA.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Period_TPA" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px;">
            <tr>
                <td>

                    <asp:Label ID="LB_PERIOD" runat="server" Visible="False"></asp:Label>
                    <table>
                        <tr>
                            <td style="width: 100px;">TPA</td>
                            <td>
                                <asp:Label ID="LB_TPA" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">CORPORATE CODE</td>
                            <td>
                                <asp:Label ID="LB_CORPORATECODE" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>SHOW</td>
                            <td>
                                <asp:DropDownList ID="DDL_SHOW" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_SHOW_SelectedIndexChanged">
                                    <asp:ListItem>PLAN CODE</asp:ListItem>
                                    <asp:ListItem>BENEFIT MAPPING</asp:ListItem>
                                    <asp:ListItem>CHARGE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr id="TR1" runat="server">
                <td>

                    <asp:DataGrid ID="DGR_PLANCODE" runat="server" CellPadding="4" Font-Names="Tahoma"
                        Font-Size="X-Small" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_PLANCODE_ItemCommand">
                        <ItemStyle BackColor="#99FFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="KODE" Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT" HeaderText="BENEFIT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PACKAGE" HeaderText="PAKET"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN_CODE_TPA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAN_CODE" HeaderText="PLAN CODE">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PLAN CODE">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PLANCODE" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                    <asp:Button ID="BT_PLANCODE_TPA_SAVE" runat="server" BackColor="Green" CommandName="Save" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr id="TR2" runat="server" visible="false">
                <td>

                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" Font-Names="Tahoma"
                        Font-Size="X-Small" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" ForeColor="#333333" GridLines="None">
                        <ItemStyle BackColor="#CCFFFF" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="KODE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DIS_BENEFIT_DETAIL_NAME" HeaderText="BENEFIT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_CODE_TPA" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="KODE VERSI TPA">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_BENCODE_TPA" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                    <asp:Button ID="BT_BENCODE_TPA_SAVE" runat="server" BackColor="Green" CommandName="Save" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr id="TR3" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">BASIC CHARGE</td>
                            <td>
                                <asp:TextBox ID="TXT_BASIC_CHG" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>EXT CHARGE 1</td>
                            <td>
                                <asp:TextBox ID="TXT_EXT_CHG1" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>EXT CHARGE 2</td>
                            <td>
                                <asp:TextBox ID="TXT_EXT_CHG2" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
						<tr>
                            <td>BIAYA DELETION</td>
                            <td>
                                <asp:TextBox ID="TXT_BIAYA_DEL" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_CHARGE_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_CHARGE_SAVE_Click" Text="SAVE" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
