<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductFactor.aspx.cs" Inherits="HLP.Form_Parameter.ProductFactor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; left: 0px; top: 0px; border-spacing: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                    <span class="auto-style1"><strong>GENDER FACTOR<br />
                    </strong></span>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <asp:DropDownList ID="DDL_GENDER" runat="server" AutoPostBack="True" BackColor="Yellow" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_GENDER_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td>START AGE :<asp:TextBox ID="TXT_AGE1" runat="server" BackColor="Yellow" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                &nbsp;</td>
                            <td></td>
                            <td>END AGE :<asp:TextBox ID="TXT_AGE2" runat="server" BackColor="Yellow" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" OnClick="BT_NEW_Click" Text="INSERT NEW AGE RANGE" />
                            </td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" AutoGenerateColumns="False" ShowFooter="True" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GENDER_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GENDER" HeaderText="GENDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_AGE" HeaderText="START AGE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_AGE" HeaderText="END AGE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="% FACTOR">
                                <FooterTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_FACTOR" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="DELETE" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
                <td style="width: 30px;"></td>
                <td>

                    <asp:DropDownList ID="DDL_FACTOR" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_FACTOR_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:DataGrid ID="DGR2" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" AutoGenerateColumns="False" ShowFooter="True" OnItemCommand="DGR2_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="% FACTOR">
                                <FooterTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />
                                </FooterTemplate>
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
            </tr>
        </table>
    </form>
</body>
</html>
