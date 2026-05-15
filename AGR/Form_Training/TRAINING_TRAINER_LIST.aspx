<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TRAINING_TRAINER_LIST.aspx.cs" Inherits="AGR.TRAINING_TRAINER_LIST" %>


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
                <td style="width: 50%" class="TDBGColor">TRAINERS&nbsp; ATRIBUTES</td>
                <td style="width: 50%" class="TDBGColor">TRAINERS LIST</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; font-size: x-small; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; font-size: x-small; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">TRAINER CODE</td>
                                        <td style="border-bottom: ridge">
                                            <asp:Label ID="TXT_TRAINER_CODE" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>TRAINER_NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_TRAINER_NAME" runat="server" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NARASUMBER</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NARASUMBER" runat="server" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
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
                            <asp:TemplateColumn HeaderText="TRAINER NAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TRAINER_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRAINER_NAME" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NARASUMBER" HeaderText="NARASUMBER"></asp:BoundColumn>
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
