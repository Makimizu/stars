<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanySimilarity.aspx.cs" Inherits="CUSTOMERS.Form_Client.CompanySimilarity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    </head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:Button ID="BT_APPROVAL" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Green" Text="APPROVE" OnClick="BT_APPROVAL_Click" Width="100px" />
                                &nbsp;&nbsp;
                                <asp:Button ID="BT_REJECT" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="REJECT" OnClick="BT_REJECT_Click" Width="100px" />
                                &nbsp;&nbsp;
                                <asp:Button ID="BT_DELETE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="DELETE" Width="100px" OnClick="BT_DELETE_Click" />
                                <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <B>ALASAN :</B>
                                <br />
                                <asp:TextBox ID="TXT_REASON" runat="server" CssClass="ASPTextBox" MaxLength="255" Height="40px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <u><B>SIMILARITY</B></u>
                    <br />
                        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20" CssClass="ASPDatagrid" BorderColor="#CC3300" AutoGenerateColumns="False" ForeColor="#333333" GridLines="Vertical">
                            <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <ItemStyle BackColor="#FFFBD6" Wrap="False" BorderColor="Black" BorderWidth="1" ForeColor="#333333" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"
                                Wrap="False" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="NAMA PERUSAHAAN"></asp:BoundColumn>
                                <asp:BoundColumn DataField="REG_DATE" HeaderText="TGL REGISTRASI"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CREATEBY" HeaderText="USER CREATE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="STAT" HeaderText="STATUS"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SCORE" HeaderText="% SIMILARITY"></asp:BoundColumn>
                            </Columns>
                            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                            <PagerStyle ForeColor="#333333" HorizontalAlign="Center" BackColor="#FFCC66" />
                        </asp:DataGrid>
                    </td>
            </tr>
        </table>
    </form>
</body>
</html>
