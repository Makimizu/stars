<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TRAINING_PARTICIPANT_LIST.aspx.cs" Inherits="AGR.TRAINING_PARTICIPANT_LIST" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; font-size: x-small; width: 100%;">
                        <tr>
                            <td style="width: 150px;">CHANNEL</td>
                            <td>
                                <asp:TextBox ID="TXT_CHANNEL" runat="server" CssClass="ASPTextBox" Width="200px" AutoPostBack="true" OnTextChanged="TXT_CHANNEL_TextChanged"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>LEVEL</td>
                            <td>
                                <asp:TextBox ID="TXT_LEVEL" runat="server" CssClass="ASPTextBox" Width="200px" AutoPostBack="true" OnTextChanged="TXT_LEVEL_TextChanged"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="90%" AutoPostBack="true" OnTextChanged="TXT_FULLNAME_TextChanged"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" AllowPaging="True" OnItemCommand="DGR_ItemCommand" OnPageIndexChanged="DGR_PageIndexChanged">
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="AGENT_CODE" HeaderText="AGENT CODE">
                                <HeaderStyle Width="80" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ENABLE_DELETE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CHANNEL" HeaderText="CHANNEL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="30" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DELETE" runat="server" Text="X" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
