<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TPA_IncomingClaim.aspx.cs" Inherits="HEALTH.Form_Klaim.TPA_IncomingClaim" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; left: 0px; top: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">FTP</td>
                            <td>
                                <asp:DropDownList ID="DDL_FTP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_FTP_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">TAHUN</td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>BULAN</td>
                            <td>
                                <asp:DropDownList ID="DDL_MONTH" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_MONTH_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">Jun</asp:ListItem>
                                    <asp:ListItem Value="7">Jul</asp:ListItem>
                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" CssClass="ASPDatagrid" GridLines="Vertical" Width="100%" ForeColor="#333333" PageSize="15" AutoGenerateColumns="False" BorderColor="#336600" OnItemCommand="DGR_ItemCommand">
                        <Columns>
                            <asp:BoundColumn DataField="PATH" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FOLDER" HeaderText="FOLDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATE" HeaderText="DATE"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="FILE">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_FILENAME" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                    <asp:Button ID="BT_SET" runat="server" CommandName="Set" CssClass="ASPButton" Text="SET" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="HEADER FILE">
                                <ItemTemplate>
                                    <asp:Label ID="LB_HEADER" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DETAIL FILE">
                                <ItemTemplate>
                                    <asp:Label ID="LB_DETAIL" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PROCESS BY">
                                <ItemTemplate>
                                    <asp:Label ID="LB_PROCESSBY" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PROCESS DATE">
                                <ItemTemplate>
                                    <asp:Label ID="LB_PROCESSDATE" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="BATCH ID">
                                <ItemTemplate>
                                    <asp:Label ID="LB_BATCH" runat="server" CssClass="ASPLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#ERROR">
                                <ItemTemplate>
                                    <asp:Label ID="LB_EXC" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" Visible="false" />
                                    <asp:Button ID="BT_HEADER" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="H" Visible="false" />
                                    <asp:Button ID="BT_DETAIL" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="D" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                            Wrap="False" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
