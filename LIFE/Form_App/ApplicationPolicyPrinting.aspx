<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPolicyPrinting.aspx.cs" Inherits="LIFE.Form_App.ApplicationPolicyPrinting" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr>
                <td style="width: 130px;">YEAR</td>
                <td>
                    <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>MONTH</td>
                <td>
                    <asp:DropDownList ID="DDL_MONTH" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_MONTH_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
        </table>
        <asp:Label ID="LB_STAT" runat="server"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
            CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal"
            PageSize="40" ItemStyle-Wrap="true" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
            <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
            <Columns>
                <asp:BoundColumn DataField="THEDATE" HeaderText="START DATE">
                    <HeaderStyle Width="80" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="THEDAY" HeaderText="DAY">
                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NUM_POLICY" HeaderText="POLICY"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="REPORTS">
                    <ItemTemplate>
                        <asp:DropDownList ID="DDL_REPORT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                        <asp:Button ID="BT_EXPORT" runat="server" Text="GENERATE FILE" CssClass="ASPButton" CommandName="Generate" />
                        <asp:Button ID="BT_GENZIP" runat="server" Text="GENERATE ATTACHMENTS" CssClass="ASPButton" CommandName="Attachment" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                Mode="NumericPages" />
        </asp:DataGrid>
    </form>
</body>
</html>
