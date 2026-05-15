<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationDocumentLetter.aspx.cs" Inherits="GLIFE.Form_App.ApplicationDocumentLetter" %>

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
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">LETTERS
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_ItemCommand" Width="100%" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_APP_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_CODE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_REPORT" runat="server" CommandName="Report"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
