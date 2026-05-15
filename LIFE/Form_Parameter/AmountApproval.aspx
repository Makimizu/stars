<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmountApproval.aspx.cs" Inherits="LIFE.Form_Parameter.AmountApproval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="top: 0px; left: 0px; border-spacing: 0px;">
                        <tr>
                            <td style="background-color: #CCCCCC; border-style: outset; text-align: left; width: 150px;">PROCESS
                            </td>
                            <td style="background-color: #CCCCCC; border-style: outset; text-align: right; width: 100px;">BOTTOM LIMIT
                            </td>
                            <td style="background-color: #CCCCCC; border-style: outset; text-align: right; width: 100px;">UPPER LIMIT
                            </td>
                            <td style="width: 100px;"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="DDL_PROCESS" runat="server" CssClass="ASPDropDownList" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DDL_PROCESS_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_BOTTOM" runat="server" CssClass="ASPTextBoxNumber" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_UPPER" runat="server" CssClass="ASPTextBoxNumber" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="ADD" Width="80px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:DataGrid ID="DGRQUERY" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
                        GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" OnItemCommand="DGRQUERY_ItemCommand" ShowHeader="False">
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR">
                                <ItemStyle Width="150px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="START_AMOUNT">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_AMOUNT">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="60px" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" Text="X" ForeColor="White" BackColor="Red" CommandName="Delete" />
                                    <asp:Button ID="BT_DETAIL" runat="server" CssClass="ASPButton" Text="D" BackColor="Yellow" CommandName="Detail" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>

                </td>
                <td>
                    <table id="TBL_DETAIL" runat="server" visible="false" style="top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">PROCESS</td>
                                        <td>
                                            <asp:Label ID="LB_PROCESS" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LB_DESCR" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BOTTOM LIMIT</td>
                                        <td>
                                            <asp:Label ID="LB_BOTTOM" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>UPPER LIMIT</td>
                                        <td>
                                            <asp:Label ID="LB_UPPER" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="background-color: #CCCCCC; border-style: outset; text-align: center; width: 50%;">UNSELECTED USER
                                        </td>
                                        <td style="background-color: #CCCCCC; border-style: outset; text-align: center; width: 50%;">SELECTED USER
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 100%; overflow: auto; height: 500px;">
                                                <asp:DataGrid ID="DGR_UNSELECTED" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
                                                    GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" ShowHeader="False" Width="100%" OnItemCommand="DGR_UNSELECTED_ItemCommand">
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

                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%; overflow: auto; height: 500px;">
                                                <asp:DataGrid ID="DGR_SELECTED" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
                                                    GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" ShowHeader="False" Width="100%" OnItemCommand="DGR_SELECTED_ItemCommand">
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

                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
