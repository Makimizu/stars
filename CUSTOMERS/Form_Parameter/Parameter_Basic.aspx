<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parameter_Basic.aspx.cs" Inherits="CUSTOMERS.Form_Parameter.Parameter_Basic" %>

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
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_PARAM" runat="server" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR2" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal"
                        PageSize="20" Width="90%" AutoGenerateColumns="False"
                        OnItemCommand="DGR2_ItemCommand" ShowFooter="True">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#FFFFCC" ForeColor="#4A3C8C" Font-Bold="False"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <FooterStyle ForeColor="#4A3C8C" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="xtype" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="length" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="isnullable" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="prm" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <FooterTemplate>
                                    <asp:Button ID="BT_SUBMIT" runat="server" CommandName="Submit" Font-Bold="True"
                                        Font-Names="Tahoma" Font-Size="XX-Small" Text="SUBMIT" />
                                </FooterTemplate>
                                <HeaderTemplate>
                                    <asp:Button ID="BT_NEW" runat="server" CommandName="New" Font-Bold="True"
                                        Font-Names="Tahoma" Font-Size="XX-Small" Text="NEW RECORD" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL" runat="server" Font-Names="Tahoma"
                                        Font-Size="X-Small" Width="382px"></asp:TextBox>
                                    <asp:TextBox ID="TXT_DATE" runat="server" Font-Names="Tahoma" Font-Size="X-Small"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="MM/dd/yyyy" TargetControlID="TXT_DATE">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR1" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal"
                        PageSize="20" OnItemCommand="DGR1_ItemCommand"
                        OnPageIndexChanged="DGR1_PageIndexChanged" Width="100%" ItemStyle-Wrap="true">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
