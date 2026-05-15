<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_CASES.aspx.cs" Inherits="AGR.AGENT_CASES" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                        <tr>
                            <td style="width: 130px;">AGENT CODE</td>
                            <td>
                                <asp:Label ID="LB_CODE" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:Label ID="LB_FULLNAME" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>CHANNEL</td>
                            <td>
                                <asp:Label ID="LB_CHANNEL" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>LEVEL</td>
                            <td>
                                <asp:Label ID="LB_LEVEL" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AllowPaging="True" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnPageIndexChanged="DGR_PageIndexChanged">
                        <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="X-Small" />
                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INSURED_NAME" HeaderText="INSURED NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_NAME" HeaderText="PRODUCT NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMUN_TYPE" HeaderText="REMUN TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOMINAM" HeaderText="AMOUNT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
