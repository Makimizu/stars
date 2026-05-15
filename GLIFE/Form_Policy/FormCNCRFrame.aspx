<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCNCRFrame.aspx.cs" Inherits="GLIFE.Form_Policy.FormCNCRFrame" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>

        <table id="TBL_CNCR" runat="server" style="border-spacing: 0px; width: 50%;">
            <tr style="vertical-align: top;">
                <td id="TD_DATE" runat="server">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">SETUP DATE</td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TBL_SETUP_DATE" runat="server" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 80px;">START DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>SETUP NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_SETUP_NAME" runat="server" CssClass="ASPTextBox" Width="150px" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_ADD" runat="server" CssClass="ASPButton" Text="SUBMIT" OnClick="BT_ADD_Click" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;">
                                <asp:DataGrid ID="DGR_SETUP_DATE" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_ItemCommand">
                                    <ItemStyle VerticalAlign="Top" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle
                                        Wrap="False" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBT_DATE" runat="server" CommandName="Select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="START_DATE" Visible="false" ></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SETUP_NAME">
                                            <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Width="150px" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Button ID="BT_KETENTUAN" runat="server" Text="KETENTUAN KEPESERTAAN" CssClass="ASPButton" CommandName="Ketentuan" />
                                                <asp:Button ID="BT_PENGECUALIAN" runat="server" Text="PENGECUALIAN" CssClass="ASPButton" CommandName="Pengecualian" />
                                                <asp:Button ID="BT_KLAIM" runat="server" Text="KLAIM" CssClass="ASPButton" CommandName="Klaim" />
                                                <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" CommandName="Delete" Text="X" BackColor="Red" ForeColor="White" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
