<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationSubmissionAgent.aspx.cs" Inherits="LQ.Form_Client.QuotationSubmissionAgent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">
            <tr>
                <td class="TDBGColor">AGENT SEARCH</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">
                        <tr>
                            <td style="width: 150px;">AGENT CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" CssClass="ASPTextBoxUPPER" Width="200" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>DIVISION IN CHARGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>LEVEL</td>
                            <td>
                                <asp:DropDownList ID="DDL_LEVEL" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_LEVEL_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" CssClass="ASPTextBoxUPPER" Width="200" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>AGENCY NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_AGENCY" CssClass="ASPTextBoxUPPER" Width="200" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" OnClick="BT_UPLINER_SEARCH_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" AllowPaging="True" OnItemCommand="DGR_ItemCommand" OnPageIndexChanged="DGR_PageIndexChanged">
                        <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="X-Small" />
                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="AGENT CODE">
                                <ItemStyle Width="100" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CODE" HeaderText="AGENT CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MARKET_SEGMENT_DESCR" HeaderText="DIVISION IN CHARGE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUBCD_DESCR" HeaderText="LEVEL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENCY_NAME" HeaderText="AGENCY NAME"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="">
                                <ItemStyle Width="100" />
                                <ItemTemplate>
                                    <asp:Label ID="LB_WARN" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="LICENSE_ACTIVE" HeaderText="LICENSE STAT" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
