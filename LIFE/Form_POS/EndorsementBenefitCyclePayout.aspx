<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementBenefitCyclePayout.aspx.cs" Inherits="LIFE.Form_POS.EndorsementBenefitCyclePayout" MaintainScrollPositionOnPostback="true" %>

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
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ALERT" runat="server" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#EFF3FB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="THEDATE" HeaderText="DUE DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="60" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOBEPAID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ALERT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PCT_VAL" HeaderText="%<BR>BENEFIT">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT<BR>BENEFIT">
                                <HeaderStyle HorizontalAlign="Right" Width="80" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="TOBE<BR>PAID">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="40" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" Style="padding-top: 0px; padding-bottom: 0px;" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
