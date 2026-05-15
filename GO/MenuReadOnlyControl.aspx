<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuReadOnlyControl.aspx.cs" Inherits="GO.MenuReadOnlyControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>APPLICATION :<asp:DropDownList ID="DDL_APP" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>

                                <asp:DataGrid ID="DGR_MENU" runat="server" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20" CssClass="ASPDatagrid" OnItemCommand="DGR_MENU_ItemCommand" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanged="DGR_MENU_PageIndexChanged">
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="MENU_CODE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MENU_DESCR" HeaderText="MENU NAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GROUP_DESCR" HeaderText="MENU GROUP"></asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="Select" Text="Set Control"></asp:ButtonColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <ItemStyle BackColor="#E3EAEB" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </td>
                            <td></td>
                            <td id="TD_CONTROL" runat="server" visible="false">
                                <table style="border-spacing: 0px; width: 500px;">
                                    <tr>
                                        <td class="TDBGColor" style=""><strong>CONTROL LIST</strong></td>
                                    </tr>
                                    <tr>
                                        <td>MENU NAME :
                                            <asp:Label ID="LB_MENU_DESCR" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="LB_MENU_CODE" runat="server" CssClass="ASPLabel" Font-Bold="True" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr style="vertical-align:top;">
                                                    <td>
                                                        <asp:TextBox ID="TXT_CONTROLID" runat="server" CssClass="ASPTextBox" MaxLength="20" style="margin-bottom: 0px" Width="180px" BackColor="Yellow"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:Button ID="BT_ADD" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Green" Text="ADD &gt;&gt;" Width="100px" OnClick="BT_ADD_Click" />
                                                        <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="&lt;&lt; REMOVE" Width="100px" OnClick="BT_DEL_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ListBox ID="LB_CONTROL_LIST" runat="server" CssClass="ASPDropDownList" Height="247px" Width="180px" BackColor="#CCFFCC"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
