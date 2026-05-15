<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Track.aspx.cs" Inherits="LQ.Form_Tools.Track" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:DataGrid ID="DGR" runat="server" BackColor="LightGoldenrodYellow"
                BorderColor="Tan" BorderWidth="1px" CellPadding="2" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" Width="100%" ForeColor="Black">
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <AlternatingItemStyle BackColor="PaleGoldenrod" />
                <ItemStyle Wrap="True" VerticalAlign="Top" />
                <HeaderStyle BackColor="Tan" VerticalAlign="Top" Font-Bold="True" />
                <FooterStyle BackColor="Tan" HorizontalAlign="Right" />
                <Columns>
                    <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCR">
                        <ItemStyle Width="100px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="USERBY"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>

        </div>
    </form>
</body>
</html>
