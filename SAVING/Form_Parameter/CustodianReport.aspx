<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustodianReport.aspx.cs" Inherits="SAVING.Form_Parameter.CustodianReport" %>

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
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 130px;">AVAILABLE REPORTS</td>
                <td>
                    <asp:DropDownList ID="DDL_REPORT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList><asp:Button ID="BT_ADD" runat="server" Text="ADD" CssClass="ASPButton" OnClick="BT_ADD_Click" /></td>
            </tr>
        </table>
        <asp:DataGrid ID="DGR_SELECTED" runat="server" AutoGenerateColumns="False"
            CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
            GridLines="None" PageSize="20" Width="100%" ForeColor="#333333" OnItemCommand="DGR_SELECTED_ItemCommand">
            <AlternatingItemStyle BackColor="White" />
            <EditItemStyle BackColor="#2461BF" />
            <HeaderStyle BackColor="#507CD1" ForeColor="White" />
            <ItemStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundColumn DataField="REPORT_CODE" HeaderText="CODE">
                    <HeaderStyle Width="30" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REPORT_NAME" HeaderText="REPORT NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="FORMAT" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="DELIMITER" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="FILENAME" Visible="false"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="FORMAT">
                    <HeaderStyle Width="100" />
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_FORMAT" runat="server" CssClass="ASPTextBox" MaxLength="10" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DELIMITER">
                    <HeaderStyle Width="100" />
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_DELIMITER" runat="server" CssClass="ASPTextBox" MaxLength="10" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="FILENAME">
                    <HeaderStyle Width="200" />
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_FILENAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="30" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" CommandName="Delete" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:DataGrid>
    </form>
</body>
</html>
