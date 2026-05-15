<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchDetail.aspx.cs" Inherits="GLIFE.Form_Saving.BatchDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 100px;">BATCH TIME</td>
                            <td>
                                <asp:Label ID="LB_BATCHTIME" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>POLICY NO</td>
                            <td>
                                <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>COMPANY</td>
                            <td>
                                <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>PRODUCT</td>
                            <td>
                                <asp:Label ID="LB_PRODUCT" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>PARTICIPANTS</td>
                            <td>
                                <asp:Label ID="LB_COUNT" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>CONTRIBUTION</td>
                            <td>
                                <asp:Label ID="LB_CONTRIB" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
                    <asp:DataGrid ID="DGR_POLICY" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" OnItemCommand="DGR_POLICY_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" OnPageIndexChanged="DGR_POLICY_PageIndexChanged" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="REGNO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_REGNO" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UW_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="START_AGE" HeaderText="START AGE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SUMINS" HeaderText="SUM INSURED">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TENOR" HeaderText="TENOR">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UW_CODE_DESCR" HeaderText="UW CODE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
