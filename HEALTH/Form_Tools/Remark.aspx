<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Remark.aspx.cs" Inherits="HEALTH.Form_Tools.Remark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    </link>
    <style type="text/css">
        .style1 {
            width: 93px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing:0px;">

            <tr>
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td class="style1">TIPE REMARK</td>
                            <td>
                                <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" valign="top">ISI REMARK</td>
                            <td valign="top">
                                <asp:TextBox ID="TXT_REMARK" runat="server"
                                    Width="340px" CssClass="ASPTextBox" Height="45px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SUBMIT" runat="server" Height="19px" OnClick="BT_SUBMIT_Click"
                                    Text="SUBMIT" CssClass="ASPButton" />
                                <asp:Label ID="LB_TIPE" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_OWNER" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_REMARK" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" PageSize="20"
                        OnItemCommand="DGR_REMARK_ItemCommand" CssClass="ASPDatagrid" Width="100%">
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_DESCR" HeaderText="TIPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" HeaderText="REMARK"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="Delete" Text="Delete">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:ButtonColumn>
                        </Columns>
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
