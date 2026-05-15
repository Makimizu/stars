<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TRAINING_LIBRARY_LIST.aspx.cs" Inherits="AGR.TRAINING_LIBRARY_LIST" %>

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
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 50%" class="TDBGColor">TRAINING LIBRARY ATRIBUTES</td>
                <td style="width: 50%" class="TDBGColor">TRAINING LIBRARY LIST</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; font-size: x-small; width: 100%;">
                        <tr>
                            <td>
                                <table style="font-size: x-small; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">TRAINING CODE</td>
                                        <td style="border-bottom: ridge;">
                                            <asp:Label ID="LB_TRAINING_CODE" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>MATERIAL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_TRAINING_NAME" runat="server" Width="98%" BackColor="#FFFFCC" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>SUBJECT</td>
                                        <td>
                                            <asp:TextBox ID="TXT_TRAINING_MATERIAL" runat="server" Width="98%" Height="100px" TextMode="MultiLine" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TRAINING</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TRAINING_LEVEL" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" Width="80" CssClass="ASPButton" OnClick="BT_SAVE_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>
                    <table id="TR_BUTTONS" runat="server" visible="false" style="font-size: x-small; border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 33%">
                                            <asp:Button ID="BT_SUBMODULE" runat="server" Text="SUB MODULE" Width="100%" Font-Size="X-Small" OnClick="BT_SUBMODULE_Click" /></td>
                                        <td style="width: 33%">
                                            <asp:Button ID="BT_DOCUMENT" runat="server" Text="TRAINING MATERIAL" Width="100%" Font-Size="X-Small" OnClick="BT_DOCUMENT_Click" /></td>
                                        <td style="width: 33%">
                                            <asp:Button ID="BT_REMARK" runat="server" Text="TRAINING REMARK" Width="100%" Font-Size="X-Small" OnClick="BT_REMARK_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe id="IF" runat="server" src="" style="width: 100%; height: 85vh; overflow: auto; border: 0;"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>

                <td>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="TRAINING NAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TRAINING_NAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRAINING_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="TRAINING LEVEL"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" Width="30" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" Text="X" CommandName="Delete" ForeColor="White" BackColor="Red" CssClass="ASPButton" />
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
