<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationBenefitCycle.aspx.cs" Inherits="LIFE.Form_App.ApplicationBenefitCycle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB" runat="server" ></asp:Label>
        <asp:DataGrid ID="DGR_BENEFIT_CYCLE" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
            <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="TOBEPAID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="SEQ" HeaderText="#YEAR"></asp:BoundColumn>
                <asp:BoundColumn DataField="THEDATE" HeaderText="DATE"></asp:BoundColumn>
                <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                <asp:BoundColumn DataField="PCT_VAL" HeaderText="%">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AMOUNT_VAL" HeaderText="AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="TOBE PAID">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="40" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        </asp:DataGrid>
    </form>
</body>
</html>
