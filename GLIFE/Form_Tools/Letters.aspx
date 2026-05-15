<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Letters.aspx.cs" Inherits="GLIFE.Form_Tools.Letters" %>

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
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>

                    <asp:DataGrid ID="DGR_LETTER" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_LETTER_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LETTER_DATE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_APP_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="LETTER NAME"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DOCUMENT NO.">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="100%" MaxLength="100"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DOC. DATE">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="100%" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="90px" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_PDF" runat="server" CssClass="ASPButton" Text="PDF" CommandName="PDF" />
                                    <asp:Button ID="BT_EMAIL" runat="server" CssClass="ASPButton" Text="EMAIL" CommandName="Email" />                                    
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="100" placeholder="Email address ..."></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
