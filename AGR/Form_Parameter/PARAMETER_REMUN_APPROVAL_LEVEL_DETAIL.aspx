<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_REMUN_APPROVAL_LEVEL_DETAIL.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_REMUN_APPROVAL_LEVEL_DETAIL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <table style="top: 0px; left: 0px; border-spacing: 0px; width: 99%; font-size: xx-small">
            <tr>
                <td style="width: 50%; vertical-align: top">
                    <table style="top: 0px; left: 0px; border-spacing: 0px; width: 99%;">
                        <tr>
                            <td style="width: 120px">DIVISION IN CHARGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_MARKET_SEGMENT" Font-Size="XX-Small" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DDL_MARKET_SEGMENT_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>AMOUNT RANGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_AMOUNT_RANGE" Font-Size="XX-Small" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DDL_AMOUNT_RANGE_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%;">
                    <asp:DataGrid ID="DGR_LEVEL" runat="server" AutoGenerateColumns="False"
                        CellPadding="2" Font-Names="Tahoma" Font-Size="XX-Small"
                        GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" OnItemCommand="DGR_LEVEL_ItemCommand" Width="99%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="LEVEL" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="LEVEL">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_LEVEL" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="NUM_PEOPLE" HeaderText="PEOPLE">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr id="TR_TITLE" runat="server" visible="false">
                <td class="TDBGColor">
                    <asp:Label ID="LB_UNSELECTED" runat="server" Text="UNSELECTED APPROVAL"></asp:Label>
                </td>
                <td class="TDBGColor">
                    <asp:Label ID="LB_SELECTED" runat="server" Text="SELECTED APPROVAL"></asp:Label>
                </td>
            </tr>
            <tr id="TR_CONTENT" runat="server" visible="false" style="vertical-align: top;">
                <td>
                    <asp:Label runat="server" ID="LB_LEVEL_ID" Visible="false"></asp:Label>
                    <table style="width:99%">
                    <tr>
                        <td style="width:10%">SEARCH</td>
                        <td>
                            <asp:TextBox ID="TXT_SEARCH" runat="server" Font-Size="XX-Small" Width="100%" OnTextChanged="TXT_SEARCH_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    <asp:DataGrid ID="DGR_UNSELECTED" runat="server" AutoGenerateColumns="False"
                        CellPadding="2" Font-Names="Tahoma" Font-Size="XX-Small"
                        GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" ShowHeader="False" Width="100%" OnItemCommand="DGR_UNSELECTED_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ROLE_DESCR"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
                <td>
                    <asp:DataGrid ID="DGR_SELECTED" runat="server" AutoGenerateColumns="False"
                        CellPadding="2" Font-Names="Tahoma" Font-Size="XX-Small"
                        GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" ShowHeader="False" Width="100%" OnItemCommand="DGR_SELECTED_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_CODE2" runat="server" CommandName="Delete" ForeColor="Red"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ROLE_DESCR"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
