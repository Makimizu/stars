<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationASO.aspx.cs" Inherits="HLP.Form_Quot.QuotationASO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Script/PleaseWait.js"></script>
    <style type="text/css">
        
        .auto-style2 {
            height: 16px;
        }

        .auto-style3 {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_SAVE_Click" Text="SAVE" Width="100px" />
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="true"></asp:Label>
                    <asp:Label ID="LB_QUOTNO" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_VER" runat="server" Visible="False"></asp:Label>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="font-weight: 700; text-decoration: underline">DEPOSIT ASO :</td>
                            <td style="width: 30px;"></td>
                            <td style="font-weight: 700; text-decoration: underline">DEPOSIT EXCESS :</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>

                                <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                                    BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Size="X-Small" GridLines="Vertical"
                                    PageSize="20" AutoGenerateColumns="False">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT" Visible="False">
                                            <HeaderStyle Width="120px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PCT_MIN" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="AMOUNT">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="% MINIMUM">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_MIN" runat="server" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                        Mode="NumericPages" />
                                </asp:DataGrid>

                            </td>
                            <td></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 120px;">AMOUNT</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="TXT_EXCESS" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% MINIMUM</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="TXT_EXCESSMIN" runat="server" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td class="auto-style3">

                                <strong>PREMIUM FACTOR :</strong></td>
                            <td>&nbsp;</td>
                            <td class="auto-style3">
                                <strong>TPA :</strong></td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>

                                <asp:DropDownList ID="DDL_FACTOR" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_FACTOR_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:DataGrid ID="DGR_FACTOR" runat="server" BackColor="White"
                                    BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Size="X-Small" GridLines="Vertical"
                                    PageSize="20" AutoGenerateColumns="False">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="% FACTOR">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_FACTOR0" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                        Mode="NumericPages" />
                                </asp:DataGrid>

                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <table style="border-spacing: 0px; border-style: outset;">
                                    <tr>
                                        <td style="width: 100px;">TPA</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TPA" CssClass="ASPDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_TPA_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>BASIC CHARGE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CHG0" runat="server" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">EXT 1 CHARGE</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="TXT_CHG1" runat="server" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>EXT 2 CHARGE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CHG2" runat="server" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>

                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td class="auto-style3">

                                <strong>% BENEFIT CLAIM :</strong></td>
                            <td>&nbsp;</td>
                            <td class="auto-style3">
                                <strong>BENEFIT REFRESH DAY :</strong></td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>

                                <asp:DataGrid ID="DGR_BENCLAIM" runat="server" BackColor="White"
                                    BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Size="X-Small" GridLines="Vertical"
                                    PageSize="20" AutoGenerateColumns="False">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="% CLAIM">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_CLAIM" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                        Mode="NumericPages" />
                                </asp:DataGrid>

                            </td>
                            <td>&nbsp;</td>
                            <td>

                                <asp:DataGrid ID="DGR_BENREFRESH" runat="server" BackColor="White"
                                    BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Size="X-Small" GridLines="Vertical"
                                    PageSize="20" AutoGenerateColumns="False">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DAY" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="DAY">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_DAY" runat="server" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                        Mode="NumericPages" />
                                </asp:DataGrid>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            </table>
    </form>
</body>
</html>
