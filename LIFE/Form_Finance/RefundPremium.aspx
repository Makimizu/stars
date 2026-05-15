<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RefundPremium.aspx.cs" Inherits="LIFE.Form_Finance.RefundPremium" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="DV_SUMMARY" runat="server">
            <asp:DataGrid ID="DGR_SUMMARY" runat="server" BackColor="White"
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" ItemStyle-Wrap="true" PageSize="20" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_SUMMARY_ItemCommand">
                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <AlternatingItemStyle BackColor="#F7F7F7" />
                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                <Columns>
                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="POLICY STATUS">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Button ID="BT_POLSTAT" runat="server" CssClass="ASPButton" Width="300" CommandName="Detail" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="CASES" HeaderText="#CASES">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="AMOUNT" HeaderText="TOTAL BALANCE">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Green" />
                    </asp:BoundColumn>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
            </asp:DataGrid>
        </div>

        <div id="DV_DETAIL" runat="server" visible="false">
            <asp:LinkButton ID="LB_BACK" runat="server" ForeColor="Red" OnClick="LB_BACK_Click">Back ..</asp:LinkButton>
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td class="TDBGColor">
                        <asp:Label ID="LB_TITLE" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LB_POLSTAT" runat="server" Visible="false"></asp:Label>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 100px;">REGNO</td>
                                <td>
                                    <asp:TextBox ID="TXT_REGNO" runat="server" CssClass="ASPTextBox" Width="100"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>FULLNAME</td>
                                <td>
                                    <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="400"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" Width="100" OnClick="BT_SEARCH_Click" /></td>
                            </tr>
                        </table>
                        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" PageSize="20" CssClass="ASPDatagrid" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_ItemCommand">
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundColumn DataField="TRXID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ACCNO" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ACCNAME" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ACCBANK" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="POLICY">
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BANKSTATEMENT">
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemStyle Width="400" />
                                    <ItemTemplate>
                                        <b>TRANSFER TO :</b>
                                        <table style="border-spacing: 0px; width: 100%;">
                                            <tr>
                                                <td style="width: 100px;">ACC NO</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" Width="90%" MaxLength="100"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>ACC NAME</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" Width="90%" MaxLength="100"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>ACC BANK</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="BT_PROCESS" runat="server" Text="REFUND" CssClass="ASPButton" Width="100" CommandName="Refund" /></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
