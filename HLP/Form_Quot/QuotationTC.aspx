<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationTC.aspx.cs" Inherits="HLP.Form_Quot.QuotationTC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Script/PleaseWait.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:DropDownList ID="DDL_BENEFIT" CssClass="ASPDropDownList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDL_BENEFIT_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_SAVE_Click" Text="SAVE" Width="100px" />
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="true"></asp:Label>
                    <asp:Label ID="LB_QUOTNO" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_VER" runat="server" Visible="False"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" AutoGenerateColumns="False">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TC_CODE" HeaderText="KODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="ITEM"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="VALUE">                                
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_VAL" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FACTOR" HeaderText="% FACTOR">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
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
