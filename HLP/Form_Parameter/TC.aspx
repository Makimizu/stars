<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TC.aspx.cs" Inherits="HLP.Form_Parameter.TC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 80px;">BENEFIT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_BENEFIT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_BENEFIT_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">KODE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" Width="80px" BackColor="Yellow" MaxLength="10"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>TC</td>
                                        <td>
                                            <asp:TextBox ID="TXT_TC" runat="server" CssClass="ASPTextBox" Width="300px" BackColor="Yellow"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_INSERT" runat="server" CssClass="ASPButton" Text="TAMBAH TC" OnClick="BT_INSERT_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>

                                <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                                    BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                                    PageSize="20" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundColumn DataField="BENEFIT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TC" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BENEFIT_DESCR" HeaderText="BENEFIT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TC_DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MALE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FEMALE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CHILD" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CHILD_D" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="KODE">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Select" CssClass="ASPLabel"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="TC">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_TCDESCR" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <asp:Button ID="BT_TCSAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px;">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CB_MALE" runat="server" CssClass="ASPButton" Text="M" /></td>
                                                        <td>
                                                            <asp:CheckBox ID="CB_FEMALE" runat="server" CssClass="ASPButton" Text="F" /></td>
                                                        <td>
                                                            <asp:CheckBox ID="CB_CHILD" runat="server" CssClass="ASPButton" Text="C" /></td>
                                                        <td>
                                                            <asp:CheckBox ID="CB_CHILD_D" runat="server" CssClass="ASPButton" Text="D" /></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
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
                <td id="TDSUB" runat="server" visible="false">
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TCCODE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="Small"></asp:Label>
                                &nbsp;-
                                <asp:Label ID="LB_TCDESCR" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr style="vertical-align: top;">
                                        <td>

                                            <asp:DataGrid ID="DGR_SUBTC" runat="server" BackColor="White"
                                                BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                                                PageSize="20" AutoGenerateColumns="False" OnItemCommand="DGR_SUBTC_ItemCommand">
                                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="CODE"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="FACTOR" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NOTE" Visible="False"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="SUB TC">
                                                        <ItemTemplate>

                                                            <table style="border-spacing: 0px;">
                                                                <tr>
                                                                    <td>
                                                                        <table style="border-spacing: 0px;">
                                                                            <tr style="vertical-align: top;">
                                                                                <td>DESCR</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="vertical-align: top;">
                                                                                <td>NOTE</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TXT_NOTE" runat="server" CssClass="ASPTextBox" Width="400px" TextMode="MultiLine" Height="40"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DataGrid ID="DGR_BENVAR2" runat="server" AutoGenerateColumns="False" CellPadding="4"  Font-Size="X-Small" ForeColor="#333333" GridLines="None" PageSize="20" ShowHeader="False" Visible="False">
                                                                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                            <AlternatingItemStyle BackColor="White" />
                                                                            <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Right" VerticalAlign="Top" Wrap="False" />
                                                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundColumn DataField="CODE">
                                                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="DESCR">
                                                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="TAKEN" Visible="false">
                                                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                                                </asp:BoundColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="CB_BENVAR" runat="server" CssClass="ASPButton" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                            <EditItemStyle BackColor="#7C6F57" />
                                                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                        </asp:DataGrid>
                                                                    </td>
                                                                </tr>
                                                            </table>




                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="% FACTOR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_FACTOR" runat="server" CssClass="ASPTextBoxNumber" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:Button ID="BT_NEW" runat="server" CommandName="New" CssClass="ASPButton" Text="NEW" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />
                                                            &nbsp;<asp:Button ID="BT_DEL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="DELETE" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                                    Mode="NumericPages" />
                                            </asp:DataGrid>

                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="DDL_BENEFITDETAIL" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                            <asp:Button ID="BT_ADDBENEFITDETAIL" runat="server" CssClass="ASPButton" OnClick="BT_ADDBENEFITDETAIL_Click" Text="ADD VARIAN BENEFIT" />
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>

                                            <asp:DataGrid ID="DGR_BENVAR" runat="server" CellPadding="4"  Font-Size="X-Small" GridLines="None"
                                                PageSize="20" AutoGenerateColumns="False" OnItemCommand="DGR_BENVAR_ItemCommand" ForeColor="#333333" ShowHeader="False">
                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#E3EAEB" Wrap="False" VerticalAlign="Top" HorizontalAlign="Right" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="CODE">
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR">
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_DELBENEFITDETAIL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="DELETE" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            </asp:DataGrid>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
