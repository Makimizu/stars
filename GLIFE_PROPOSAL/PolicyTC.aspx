<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyTC.aspx.cs" Inherits="GLIFE_PROPOSAL.PolicyTC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <div style="width: 100%; height: 100vh; overflow: auto">
            <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_ItemCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                <ItemStyle VerticalAlign="Top" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="Small" />
                <EditItemStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle
                    Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingItemStyle BackColor="White" />
                <Columns>
                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="LBT_DATE" runat="server" CommandName="Select"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="START_DATE" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCR">
                        <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>
                </Columns>
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
