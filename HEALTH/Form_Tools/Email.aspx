<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Email.aspx.cs" Inherits="HEALTH.Form_Tools.Email" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            width: 100px;
            height: 24px;
        }

        .auto-style2 {
            height: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
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
                            <td class="auto-style1">PROCESS DATE</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="TXT_PROCDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PROCDATE">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;- <asp:TextBox ID="TXT_PROCDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PROCDATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" BorderColor="#996633" CellPadding="2" ForeColor="Black" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" BackColor="LightGoldenrodYellow" BorderWidth="1px">
                        <ItemStyle VerticalAlign="Top" Wrap="false" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="DOCUMENT NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_REPORT" runat="server" CommandName="Report" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="DOCNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="APPR_DATE" HeaderText="PROCESS DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY" HeaderText="RECIPIENT COMPANY">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="EMAIL">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                    <asp:Button ID="BT_EMAIL" runat="server" CommandName="Send" CssClass="ASPButton" Text="SEND" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FIRST_SEND" HeaderText="FIRST SEND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_SEND" HeaderText="LAST SEND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="Tan" Wrap="false" />
                        <HeaderStyle
                            Wrap="False" BackColor="Tan" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="PaleGoldenrod" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

