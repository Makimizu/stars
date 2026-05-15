<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPolicyFolder.aspx.cs" Inherits="LIFE.Form_App.ApplicationPolicyFolder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; left: 0px; top: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor" style="text-align: left;">REPORT TYPE</td>
                <td class="TDBGColor" style="text-align: left;">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="width: 200px; border: inset;">
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="1" CssClass="ASPDatagrid" GridLines="None" Width="100%" PageSize="15" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" ShowHeader="False" CellSpacing="2">
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FTP_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FOLDER" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FORMATEXT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle Wrap="False" VerticalAlign="Top" />
                        <HeaderStyle
                            Wrap="False" />
                    </asp:DataGrid>
                </td>
                <td>
                    <asp:DataGrid ID="DGR_FILES" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="80%" OnItemCommand="DGR_FILES_ItemCommand">
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="FULLPATH" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FILENAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="FILENAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:BoundColumn DataField="DATE" HeaderText="DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SIZE" HeaderText="SIZE (Kb)">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>


                        </Columns>
                        <ItemStyle Wrap="False" VerticalAlign="Top" BackColor="#EFF3FB" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#507CD1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
