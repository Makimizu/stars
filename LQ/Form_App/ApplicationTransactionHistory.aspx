<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationTransactionHistory.aspx.cs" Inherits="LQ.Form_App.ApplicationTransactionHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="99%" ShowHeader="False" OnItemCommand="DGR_ItemCommand">
            <HeaderStyle VerticalAlign="Top" />
            <ItemStyle VerticalAlign="Top" Font-Size="XX-Small" />
            <Columns>
                <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="BT" runat="server" CssClass="ASPButton" CommandName="Select" Width="95%" Style="white-space: normal;" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>

    </form>
</body>
</html>