<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturValidasi.aspx.cs" Inherits="FINANCE.Form_Settlement.ReturValidasi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td></td>
                <td class="TDBGColor">
                    <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 120px;">REASON :</td>
                            <td><asp:DropDownList ID="DDL_REASON" runat="server" CssClass="ASPDropDownList" AutoPostBack="True"></asp:DropDownList></td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%">
                        <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="NBR" HeaderText="NO">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>

                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_FILE_ALL" runat="server" BackColor="Blue" CommandName="IBALL" CssClass="ASPButton" ForeColor="White" Text="RETUR" />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TRXID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCNO" HeaderText="DOC NO">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RESERVED_PARAM1" HeaderText="REFF DOC NO">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Red" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
