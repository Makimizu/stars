<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationList.aspx.cs" Inherits="LQ.Form_Client.QuotationList" %>

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
        <asp:Label ID="LB_TRACK" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 130px;">REGNO</td>
                            <td>
                                <asp:TextBox ID="TXT_REGNO" runat="server" CssClass="ASPTextBox" Width="300"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>POLICY HOLDER NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="300"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>POLICY HOLDER DOB</td>
                            <td>
                                <asp:TextBox ID="TXT_DOB" runat="server" CssClass="ASPTextBoxUPPER" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>PRODUCT GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCTGROUP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_PRODUCTGROUP_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>PRODUCT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_PRODUCTNAME" runat="server" CssClass="ASPTextBox" Width="300"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical"
                        PageSize="20" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" CssClass="ASPDatagrid" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="False" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="MEMBERID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REGNO" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_HOLDER" HeaderText="POLICY HOLDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT" HeaderText="PRODUCT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_INFO" HeaderText="POLICY INFO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CREATEBY" HeaderText="CREATE BY"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
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
