<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_OVERRIDING_PLACEMENT.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_OVERRIDING_PLACEMENT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" CssClass="ASPButton" />
        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
            CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal"
            PageSize="20" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False">
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
            <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <Columns>
                <asp:BoundColumn DataField="AGENT_LEVEL_CODE" HeaderText="LEVEL CODE">
                    <ItemStyle Width="100" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_LEVEL_DESCR" HeaderText="LEVEL"></asp:BoundColumn>
                <asp:BoundColumn DataField="DIVISION_IN_CHARGE" HeaderText="DIVISION IN CHARGE"></asp:BoundColumn>
                <asp:BoundColumn DataField="OR1" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="OR2" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="OR3" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="OR4" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="OR5" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="OR1">
                    <HeaderStyle Width="100" />
                    <ItemTemplate>
                        <asp:DropDownList ID="DDL_OR1" runat="server" CssClass="ASPDropDownList" Width="100"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="OR2">
                    <HeaderStyle Width="100" />
                    <ItemTemplate>
                        <asp:DropDownList ID="DDL_OR2" runat="server" CssClass="ASPDropDownList" Width="100"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="OR3">
                    <HeaderStyle Width="100" />
                    <ItemTemplate>
                        <asp:DropDownList ID="DDL_OR3" runat="server" CssClass="ASPDropDownList" Width="100"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="OR4">
                    <HeaderStyle Width="100" />
                    <ItemTemplate>
                        <asp:DropDownList ID="DDL_OR4" runat="server" CssClass="ASPDropDownList" Width="100"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="OR5">
                    <HeaderStyle Width="100" />
                    <ItemTemplate>
                        <asp:DropDownList ID="DDL_OR5" runat="server" CssClass="ASPDropDownList" Width="100"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                Mode="NumericPages" />
        </asp:DataGrid>

    </form>
</body>
</html>
