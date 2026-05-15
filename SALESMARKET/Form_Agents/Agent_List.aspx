<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_List.aspx.cs" Inherits="SALESMARKET.Form_Agents.Agent_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td style="width:100px;">AGENT CODE</td>
                            <td>
                                            <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                            <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td>UPLINER NAME</td>
                            <td>
                                            <asp:TextBox ID="TXT_UPLINER" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td>CHANNEL</td>
                            <td>
                                            <asp:TextBox ID="TXT_CHANNEL" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td>STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                    <asp:ListItem Value="0">NOT ACTIVE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                &nbsp;<asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" OnClick="BT_XLS_Click" BackColor="Green" Font-Bold="True" ForeColor="White" />
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                                <asp:Label ID="LB_RECORDS" runat="server" Font-Bold="True"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <ItemStyle Wrap="False" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="AGENT&lt;BR&gt;CODE">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACTIVE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAME">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SUBCD_DESCR" HeaderText="CHANNEL">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UPLINER" HeaderText="UPLINER&lt;BR&gt;CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UPLINER_NAME" HeaderText="UPLINER NAME">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BRANCH_DESCR" HeaderText="BRANCH">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="ACTIVE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="DB_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
