<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Email_Data.aspx.cs" Inherits="GO.Email_Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_APP" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 120px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_SENDED" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="is null">NOT SENDED</asp:ListItem>
                                    <asp:ListItem Value="is not null">SENDED</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>DOCUMENT NO</td>
                            <td>
                                <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>RECIPIENT COMPANY</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">DOCUMENT DATE</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="TXT_PROCDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:calendarextender id="CalendarExtender1" runat="server" format="dd/MM/yyyy" targetcontrolid="TXT_PROCDATE">
                                </ajaxToolkit:calendarextender>
                                &nbsp;-
                                <asp:TextBox ID="TXT_PROCDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:calendarextender id="CalendarExtender2" runat="server" format="dd/MM/yyyy" targetcontrolid="TXT_PROCDATE2">
                                </ajaxToolkit:calendarextender>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                            </td>
                        </tr>
                    </table>
                                <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_ItemCommand" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" Width="100%">
                        <ItemStyle VerticalAlign="Top" Wrap="false" BackColor="#99CCFF" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="DOCUMENT NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_REPORT" runat="server" CommandName="Report" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="DOCNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCDATE" HeaderText="DOCUMENT DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY" HeaderText="RECIPIENT COMPANY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="EMAIL">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" BackColor="#ffff99" Width="300px"></asp:TextBox>
                                    <asp:Button ID="BT_EMAIL" runat="server" CommandName="Send" CssClass="ASPButton" Text="SEND" ForeColor="White" BackColor="Green" Font-Bold="True" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REPORT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FIRST_SEND" HeaderText="FIRST SEND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_SEND" HeaderText="LAST SEND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RECIPIENT" HeaderText="RECIPIENT"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Wrap="false" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
