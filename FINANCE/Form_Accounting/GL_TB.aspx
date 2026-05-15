<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GL_TB.aspx.cs" Inherits="FINANCE.Form_Accounting.GL_TB" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">PERIOD</td>
                            <td>
                                <asp:DropDownList ID="DDL_PERIOD" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_PERIOD_SelectedIndexChanged">
                                </asp:DropDownList>
                                <span class="auto-style1"><strong><asp:Button ID="BT_XLS" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="XLS" OnClick="BT_XLS_Click" />
                                </strong></span>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <span class="auto-style1"><strong>TRIAL BALANCE TABLE </strong></span>
                                <asp:DataGrid ID="DGR" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                                    PageSize="40" ItemStyle-Wrap="true" AutoGenerateColumns="False" ForeColor="#333333" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True">
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#EFF3FB" Wrap="true" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="false" />
                                    <Columns>
                                        <asp:BoundColumn DataField="COA" HeaderText="COA">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="COA DESCRIPTION"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DEBET" HeaderText="DEBET">
                                            <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CREDIT" HeaderText="CREDIT">
                                            <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_DETAIL" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="D" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="White" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                </asp:DataGrid>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <asp:Label ID="LB_UNBALANCE" runat="server" Font-Bold="True" Font-Underline="True" Text="UNBALANCED JOURNAL"></asp:Label>
&nbsp;<asp:DataGrid ID="DGR_UNBALANCE" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                                    PageSize="40" ItemStyle-Wrap="true" AutoGenerateColumns="False" ForeColor="#333333" OnItemDataBound="DGR_ItemDataBound" Width="500px">
                                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#FFFBD6" Wrap="true" VerticalAlign="Top" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" Wrap="false" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_DETAIL" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="D" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="VOUCHERNO" HeaderText="VOUCHER NO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CODE" HeaderText="CODE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION">
                                            <HeaderStyle Width="200px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="THEDATE" HeaderText="DATE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DIFF" HeaderText="DIFFERENCE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
