<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceOSEmailBancass_Hist.aspx.cs" Inherits="FINANCE.Form_Collection.InvoiceOSEmailBancass_Hist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20"
                        GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center">
                        <ItemStyle Wrap="True" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="AGENT_CODE" HeaderText="AGENT CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MONTHLY_AGING" HeaderText="MONTHLY AGING"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RECIPIENT" HeaderText="RECIPIENT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REQ_DATE" HeaderText="REQUSET DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REQ_BY" HeaderText="REQUSET BY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEND_DATE" HeaderText="SEND DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>