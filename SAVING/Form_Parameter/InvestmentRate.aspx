<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvestmentRate.aspx.cs" Inherits="SAVING.Form_Parameter.InvestmentRate" %>

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
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                        <tr>
                            <td style="width: 130px;">PRODUCT GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCT_GROUP" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_RATE_DATE_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 130px;">RATE DATE</td>
                            <td>
                                <asp:DropDownList ID="DDL_RATE_DATE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_RATE_DATE_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>CURRENCY</td>
                            <td>
                                <asp:DropDownList ID="DDL_CURRENCY" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_CURRENCY_SelectedIndexChanged">
                                    <asp:ListItem Text="IDR" Value="IDR"></asp:ListItem>
                                    <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button runat="server" ID="BT_ADD" Text="ADD NEW ENTRY" CssClass="ASPButton" OnClick="BT_ADD_Click" />
                            </td>
                        </tr>
            </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" id="TBL_EDIT" style="background-color: White; border-color: #E7E7FF; border-width: 1px; border-style: None; font-family: Tahoma; font-size: X-Small; width: 600px;" visible="false">
                        <tr style="color: #4A3C8C; background-color: #FFFFCC; font-weight: normal; font-style: normal; text-decoration: none;">
                            <td>PRODUCT GROUP</td>
                            <td>
                                <asp:Label runat="server" ID="LB_PRODUCT_GROUP"></asp:Label></td>
                        </tr>
                        <tr style="color: #4A3C8C; background-color: #CCFFCC; font-weight: normal; font-style: normal; text-decoration: none;">
                            <td>RATE DATE</td>
                            <td>
                                <asp:DropDownList ID="DDL_INPUT_RATE_DATE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="color: #4A3C8C; background-color: #FFFFCC; font-weight: normal; font-style: normal; text-decoration: none;">
                            <td>CURRENCY</td>
                            <td>
                                <asp:DropDownList ID="DDL_INPUT_CURRENCY" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Text="IDR" Value="IDR"></asp:ListItem>
                                    <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="color: #4A3C8C; background-color: #CCFFCC; font-weight: normal; font-style: normal; text-decoration: none;">
                            <td>RATE(%)</td>
                            <td>
                                <asp:TextBox runat="server" ID="TXT_INPUT_RATE" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="color: #4A3C8C; font-weight: normal; font-style: normal; text-decoration: none;">
                            <td></td>
                            <td>
                                <asp:Button runat="server" ID="BT_SUBMIT" Text="SUBMIT" CssClass="ASPButton" OnClick="BT_SUBMIT_Click" />
                                <asp:Button runat="server" ID="BT_CANCEL" Text="CANCEL" CssClass="ASPButton" OnClick="BT_CANCEL_Click" />
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
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal"
                        PageSize="20" OnItemCommand="DGR_ItemCommand"
                        OnPageIndexChanged="DGR_PageIndexChanged" Width="600px" ItemStyle-Wrap="true" AutoGenerateColumns="False">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="PRODUCT_GROUP" HeaderText="PRODUCT GROUP">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE_DATE" HeaderText="RATE DATE">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CURRENCY" HeaderText="CURRENCY">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE" HeaderText="RATE(%)">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="USERBY" HeaderText="USERBY">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="USERDATE" HeaderText="USERDATE">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
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
