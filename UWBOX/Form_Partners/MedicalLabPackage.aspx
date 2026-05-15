<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicalLabPackage.aspx.cs" Inherits="UWBOX.Form_Partners.MedicalLabPackage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 100px;">YEAR</td>
                            <td>
                                <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList>
                                &nbsp;
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_ITEMS" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_ITEMS_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                <HeaderStyle HorizontalAlign="Right" Width="30px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRICE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CNT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PACKAGE NAME">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PACKAGE" runat="server" Width="100%" CssClass="ASPTextBox" MaxLength="100"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PRICE">
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PRICE" runat="server" Width="100px" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ITEMS">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_ITEMS" runat="server" CommandName="Items"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" Width="25px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_DEL" runat="server" CommandName="Delete" Text="X" Font-Bold="true" ForeColor="Red"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="400px" Width="400px" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none; overflow:auto;">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Button ID="BT_ITEMSAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="100px" OnClick="BT_ITEMSAVE_Click" />
                                        <asp:Label ID="LB_YEAR" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
                                        <asp:DataGrid ID="DGR_DOC" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                                            CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333"
                                            GridLines="None" PageSize="5" ShowHeader="False" Width="100%">
                                            <EditItemStyle BackColor="#999999" />
                                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                            <ItemStyle BackColor="#F7F6F3" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DESCR">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="COUNT" Visible="False">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CB" runat="server"  />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
