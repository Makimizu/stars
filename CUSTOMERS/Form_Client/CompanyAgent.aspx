<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyAgent.aspx.cs" Inherits="CUSTOMERS.Form_Client.CompanyAgent" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <center>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20" AutoGenerateColumns="False" CssClass="ASPDatagrid" ForeColor="#333333">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" BorderColor="Black" BorderWidth="1" />
                        <Columns>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="TGL MULAI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAME" HeaderText="NAMA AGEN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SETBY" HeaderText="SET BY"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                            Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                    </center>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
