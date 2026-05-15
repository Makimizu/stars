<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GL_Data_Detail.aspx.cs" Inherits="FINANCE.Form_Accounting.GL_Data_Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">VOUCHER NO</td>
                            <td>
                                <asp:Label ID="LB_VOUCHERNO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>PERIOO</td>
                            <td>
                                <asp:Label ID="LB_PERIOD" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>JOURNAL TYPE</td>
                            <td>
                                <asp:Label ID="LB_CODE" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>DOC NO</td>
                            <td>
                                <asp:Label ID="LB_PARAMKEY" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>DATE</td>
                            <td>
                                <asp:Label ID="LB_DATE" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>DESCRIPTION</td>
                            <td>
                                <asp:Label ID="LB_DESCRIPTION" runat="server" CssClass="ASPLabel" Font-Bold="False" Font-Italic="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>CONFIRMED BY</td>
                            <td>
                                <asp:Label ID="LB_CONF" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>POSTED BY</td>
                            <td>
                                <asp:Label ID="LB_POST" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><strong><u>
                    <br />
                    DEBET</u></strong><br />

                    <asp:DataGrid ID="DGR_D" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                        PageSize="40" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" Wrap="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="COA" HeaderText="COA">
                                <HeaderStyle Width="60px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="COA DESCRIPTION">
                                <HeaderStyle Width="300px" />
                                <ItemStyle Wrap="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="T00" HeaderText="T00"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T01" HeaderText="T01"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T02" HeaderText="T02"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T03" HeaderText="T03"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T04" HeaderText="T04"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T05" HeaderText="T05"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T06" HeaderText="T06"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T07" HeaderText="T07"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T08" HeaderText="T08"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T09" HeaderText="T09"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td><strong><u>
                    <br />
                    CREDIT</u></strong>

                    <asp:DataGrid ID="DGR_C" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                        PageSize="40" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#006600" Font-Bold="True" ForeColor="#F7F7F7" Wrap="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="COA" HeaderText="COA">
                                <HeaderStyle Width="60px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="COA DESCRIPTION">
                                <HeaderStyle Width="300px" />
                                <ItemStyle Wrap="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="T00" HeaderText="T00"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T01" HeaderText="T01"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T02" HeaderText="T02"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T03" HeaderText="T03"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T04" HeaderText="T04"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T05" HeaderText="T05"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T06" HeaderText="T06"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T07" HeaderText="T07"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T08" HeaderText="T08"></asp:BoundColumn>
                            <asp:BoundColumn DataField="T09" HeaderText="T09"></asp:BoundColumn>
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
