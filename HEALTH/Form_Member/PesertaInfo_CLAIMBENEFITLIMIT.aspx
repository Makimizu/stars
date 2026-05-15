<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PesertaInfo_CLAIMBENEFITLIMIT.aspx.cs" Inherits="HEALTH.Form_Member.PesertaInfo_CLAIMBENEFITLIMIT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>

                    <asp:Label ID="LB_REGNO" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_PERIOD" runat="server" Visible="False"></asp:Label>
                    <asp:DataGrid ID="DGR_CLAIMBENEFIT" runat="server" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" BorderColor="#999999" BackColor="White" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundColumn DataField="POLICY_PERIOD_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_CODE" HeaderText="PLAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DIS_BENEFIT_DETAIL_NAME" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BEN_AWAL" HeaderText="BENEFIT LIMIT AWAL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
