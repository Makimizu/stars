<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationQuestionHeader.aspx.cs" Inherits="LQ.Form_App.ApplicationQuestionHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>

        <asp:DataGrid ID="DGR_BUTTON" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="8pt" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_BUTTON_ItemCommand">
            <ItemStyle VerticalAlign="Top" />
            <Columns>
                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ADD_URL" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="BT_GROUP" runat="server" CssClass="ASPButton" Width="100%" CommandName="Detail" Style="white-space: normal;" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>

    </form>
</body>
</html>
