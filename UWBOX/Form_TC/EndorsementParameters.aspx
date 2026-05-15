<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementParameters.aspx.cs" Inherits="UWBOX.Form_TC.EndorsementParameters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="top: 0px; left: 0px; width: 100%; position: absolute;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_PARAM" runat="server" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="Small"></asp:Label>
                    <asp:Label ID="LB_PREFIX" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW RECORD" OnClick="BT_NEW_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">CODE NO</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" MaxLength="10" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>DESCRIPTION</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>DATA TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_DATA_TYPE" runat="server" AutoPostBack="false" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>SQL REFF</td>
                            <td>
                                <asp:TextBox ID="TXT_SQL_REFF" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
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
                        OnPageIndexChanged="DGR1_PageIndexChanged" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonColumn>
                            <asp:BoundColumn DataField="CODE" HeaderText="CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_TYPE" HeaderText="DATA TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" HeaderText="SQL REFF"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" ForeColor="White" BackColor="Red" CommandName="Delete" />
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

