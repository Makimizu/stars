<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistic.aspx.cs" Inherits="GO.Statistic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>

                    <asp:DataGrid ID="DGR_DBSIZE" runat="server"
                        BorderWidth="1px" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20"
                        AutoGenerateColumns="False" CssClass="ASPDatagrid" BorderColor="#999999" BackColor="White" BorderStyle="Solid" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                        <AlternatingItemStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:BoundColumn DataField="DBNAME" HeaderText="DB NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DB_SIZE" HeaderText="DB SIZE (MB)">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LOG_SIZE" HeaderText="LOG SIZE (MB)">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DEVICE_NAME" HeaderText="DEVICE NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LOG_NAME" HeaderText="LOG NAME"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR_JOB" runat="server" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20" CssClass="ASPDatagrid" BorderColor="#0033CC" CellPadding="4" ForeColor="#333333" GridLines="Vertical">
                        <AlternatingItemStyle BackColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <ItemStyle BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
