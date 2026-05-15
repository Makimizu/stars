<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer_Block_Service.aspx.cs" Inherits="FINANCE.Form_Collection.Customer_Block_Service" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                    <asp:Label ID="LB_MODE" runat="server" CssClass="ASPLabel" Visible="false"></asp:Label>
                    <asp:Label ID="LB_PRIVILEDGE" runat="server" CssClass="ASPLabel" Visible="false"></asp:Label>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 120px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>COMPANY</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>POLICY NO</td>
                            <td>
                                <asp:TextBox ID="TXT_NOPOL" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
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
            <tr id="TR_BLOCK" runat="server">
                <td>
                    <asp:Label ID="LB_RESULT" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" BorderColor="#003300" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged">
                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" Wrap="false" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="POLICY NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_NO" runat="server" CommandName="Select" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CUSTOMER_CODE" HeaderText="POLICY NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="COMPANY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_AGING" HeaderText="START&lt;BR&gt;AGING DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MAX_AGING" HeaderText="AGING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE" HeaderText="# INVOICE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PIC_EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                    <asp:Button ID="BT_EMAIL" runat="server" CommandName="Send" CssClass="ASPButton" Text="SEND" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="false" />
                        <HeaderStyle
                            Wrap="False" BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr id="TR_UNBLOCK" runat="server" visible="false">
                <td>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
