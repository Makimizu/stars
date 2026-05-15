<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InquiryScreen.aspx.cs" Inherits="UWBOX.Form_Tools.InquiryScreen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    </link>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:DataGrid ID="DGR_PARAM" runat="server" OnItemCommand="DGR_ItemCommand" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20" ShowHeader="False" BorderColor="Gray" ForeColor="#333333">
                                    <Columns>
                                        <asp:BoundColumn DataField="FIELD" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FIELD_ALIAS">
                                            <ItemStyle Width="200px" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPE_DATA" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_PARAM" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_PARAM" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                </asp:DropDownList>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TXT_DATE" runat="server" Style="text-align: center;" CssClass="ASPTextBox" Width="60px" Visible="false"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <asp:TextBox ID="TXT_DATE2" runat="server" Style="text-align: center;" CssClass="ASPTextBox" Width="60px" Visible="false"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid></td>
                            <td style="width: 30px;"></td>
                            <td>
                                <asp:DataGrid ID="DGR_PARAM2" runat="server" OnItemCommand="DGR_ItemCommand" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20" ShowHeader="False" BorderColor="Gray" ForeColor="#333333">
                                    <Columns>
                                        <asp:BoundColumn DataField="FIELD" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FIELD_ALIAS">
                                            <ItemStyle Width="200px" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPE_DATA" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_PARAM" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_PARAM" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                </asp:DropDownList>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TXT_DATEa" runat="server" Style="text-align: center;" CssClass="ASPTextBox" Width="60px" Visible="false"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="ceDate1a" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATEa">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <asp:TextBox ID="TXT_DATEa2" runat="server" Style="text-align: center;" CssClass="ASPTextBox" Width="60px" Visible="false"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1a" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATEa2">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" />
                                <br />
                                <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW" Font-Bold="True" ForeColor="Blue" Visible="False" OnClick="BT_NEW_Click" />
                                <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label><br />
                                <asp:Label ID="LB_NEW" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_SQL" runat="server" Visible="False"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" Font-Bold="True" ForeColor="White" BackColor="Green" Visible="False" OnClick="BT_XLS_Click" />
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20" OnPageIndexChanged="DGR_PageIndexChanged" OnItemCommand="DGR_ItemCommand"
                        AllowPaging="True" GridLines="None" CssClass="ASPDatagrid" ForeColor="#333333">
                        <ItemStyle BackColor="#66FFFF" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left"
                            Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Position="TopAndBottom" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
