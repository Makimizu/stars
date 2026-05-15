<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationDoc.aspx.cs" Inherits="LQ.Form_Client.QuotationDoc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td style="width: 50%" class="TDBGColor">MEDICAL DOCUMENTS</td>
                <td style="width: 50%" class="TDBGColor">OTHER ARCHIEVE</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:DataGrid ID="DGR_DOCS" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="8pt" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_DOCS_ItemCommand" Width="100%" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#CCCCCC" ForeColor="#666666" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
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
                                    <asp:LinkButton ID="LBT_UPLOAD" runat="server" CommandName="Upload" ToolTip="Upload">
                                                <span class="fa fa-upload"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_DELETE" runat="server" CommandName="Delete" ToolTip="Delete">
                                                <span class="fa fa-remove" style="color:red;"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    </asp:DataGrid>

                </td>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: 8pt;">
                        <tr>
                            <td>
                                <asp:TextBox ID="TXT_ARCHIEVE" runat="server" placeholder="Addition File Remark ..." Width="100%" MaxLength="1000" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                            <td style="width: 130px; text-align: right;">
                                <asp:FileUpload ID="FU_ARCHIEVE" runat="server" Width="100px" CssClass="ASPButton" />
                                &nbsp;
                                <asp:LinkButton ID="LBT_ARCHIEVE" runat="server" ToolTip="Upload" OnClick="LBT_ARCHIEVE_Click">
                                <span class="fa fa-upload"></span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="DGR_ARCHIEVE" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" PageSize="20"
                        OnItemCommand="DGR_ARCHIEVE_ItemCommand" CssClass="ASPDatagrid" Width="100%" Font-Size="Small" ShowHeader="False">
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMAFILE" HeaderText="FILE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_DOWNLOAD" runat="server" ToolTip="Download" CommandName="Download">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_ARCHIEVE_DELETE" runat="server" ToolTip="Delete" CommandName="Delete" ForeColor="Red">
                                                            <span class="fa fa-remove"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
