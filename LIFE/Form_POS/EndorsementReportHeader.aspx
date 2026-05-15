<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementReportHeader.aspx.cs" Inherits="LIFE.Form_POS.EndorsementReportHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="98%" ShowHeader="False" OnItemCommand="DGR_ItemCommand">
            <HeaderStyle VerticalAlign="Top" />
            <ItemStyle VerticalAlign="Top" Font-Size="XX-Small" />
            <Columns>
                <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="BT" runat="server" CommandName="Select" Width="95%" Style="white-space: normal;" Font-Size="XX-Small" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>

    </form>
</body>
</html>
