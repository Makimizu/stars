<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementSubmissionInfo.aspx.cs" Inherits="LIFE.Form_POS.EndorsementSubmissionInfo" %>

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
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" ShowFooter="True" OnItemCommand="DGR_ITEM_ItemCommand" OnItemDataBound="DGR_ITEM_ItemDataBound">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Font-Size="XX-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="READONLY" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR">
                                <ItemStyle Width="300px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="LB_VAL" runat="server" Visible="false" Font-Bold="true"></asp:Label>
                                    <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="150px"></asp:TextBox>
                                    <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" Visible="false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="150" CommandName="Save" /><br /><br />
                                    <asp:Button ID="BT_EMAIL" runat="server" CssClass="ASPButton" Text="SEND QUOTATION EMAIL" Width="150" BackColor="Green" ForeColor="White" Visible="false" CommandName="Email" />
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
                <td style="width: 50%;">
                    <asp:DataGrid ID="DGR_UW" runat="server" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowFooter="True">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Font-Size="XX-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
