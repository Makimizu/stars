<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceManualEntri.aspx.cs" Inherits="FINANCE.Form_Collection.InvoiceManualEntri" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .auto-style3 {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>INVOICE NO</td>
                            <td>
                                <asp:TextBox ID="TXT_NO" runat="server" CssClass="ASPTextBox" Width="200px" ReadOnly="true" BackColor="#cccccc" Font-Bold="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>INVOICE DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>CUSTOMER NAME</td>
                            <td>
                                <asp:DropDownList ID="DDL_CUSTOMER" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_CUSTOMER_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>BRANCH</td>
                            <td>
                                <asp:DropDownList ID="DDL_BRANCH" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" />
                    <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20"
                        GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True">
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>

                            <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="KETERANGAN">
                                <FooterTemplate>
                                    <div class="auto-style3">
                                        <strong>TOTAL</strong>
                                    </div>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Height="25px" MaxLength="255" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="AMOUNT">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" BackColor="Yellow" Width="120px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="Red" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle Wrap="False" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
