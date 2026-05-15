<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationDocument.aspx.cs" Inherits="GLIFE.Form_App.ApplicationDocument" %>

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
            <tr id="TR_DOCS_REMAINED" runat="server">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">ADDITIONAL REQUIRED DOCUMENTS
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 100%; height: 80px; overflow: auto; border: inset;">
                                    <asp:DataGrid ID="DGR_DOCS_REMAINED" runat="server" BackColor="White"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_DOCS_REMAINED_ItemCommand" Width="100%" CssClass="ASPDatagrid">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            </asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">REQUIRED MEDICAL DOCUMENTS
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_DOCS" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_DOCS_ItemCommand" Width="100%" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="DOCTYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMAFILE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RECEIVE_DATE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="LB_DESCR" runat="server" Visible="False"></asp:Label>
                                    <asp:LinkButton ID="LBT_DESCR" runat="server" CommandName="Download" Visible="False" ToolTip="Download"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:FileUpload ID="FU_DOC" runat="server" Width="100px" CssClass="ASPButton" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_UPLOAD" runat="server" CommandName="Upload" CssClass="ASPButton" Text="UPLOAD" ForeColor="White" BackColor="Blue" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_CLEAR" runat="server" CommandName="Clear" CssClass="ASPButton" Text="C" BackColor="Yellow" />
                                    <asp:Button ID="BT_DELETE" runat="server" CommandName="Delete" CssClass="ASPButton" Text="X" ForeColor="White" BackColor="Red" />
                                </ItemTemplate>
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
