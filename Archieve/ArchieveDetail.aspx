<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArchieveDetail.aspx.cs" Inherits="Archieve.ArchieveDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_APPID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server" Font-Size="Small" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                        <tr>
                            <td style="width: 150px;">OWNER CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_OWNER" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>OWNER NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>REMARK</td>
                            <td>
                                <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>UPLOAD DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-&nbsp;
                                <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" Width="100" OnClick="BT_SEARCH_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_RECORDS" runat="server" Font-Size="XX-Small"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" PageSize="20"
                        OnItemCommand="DGR_ItemCommand" CssClass="ASPDatagrid" Width="100%" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged">
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="true" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="DOC_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOC_FILENAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOC_SQL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="FILENAME">
                                <ItemStyle Width="200" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="BT_SELECT" runat="server" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="OWNER_CODE" HeaderText="OWNER CODE">
                                <HeaderStyle Width="150" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OWNER_NAME" HeaderText="OWNER NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" HeaderText="REMARK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UPLOAD_BY" HeaderText="UPLOAD BY">
                                <HeaderStyle Width="200" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
