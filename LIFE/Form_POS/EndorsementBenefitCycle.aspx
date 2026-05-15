<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementBenefitCycle.aspx.cs" Inherits="LIFE.Form_POS.EndorsementBenefitCycle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ALERT" runat="server" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="THEDATE" HeaderText="EXISTING DUE DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="100" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ENABLE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEWDATE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="THEDATE_BEFORE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="THEDATE_AFTER" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOBEPAID" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PCT_VAL" HeaderText="% BENEFIT">
                                <HeaderStyle HorizontalAlign="Center" Width="100" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_VAL" HeaderText="AMOUNT BENEFIT">
                                <HeaderStyle HorizontalAlign="Right" Width="150" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CYCLE_SEQ" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="NEW DUE DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_DATE" runat="server" CssClass="ASPDropDownList" Width="100"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TOBE PAID">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="40" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
