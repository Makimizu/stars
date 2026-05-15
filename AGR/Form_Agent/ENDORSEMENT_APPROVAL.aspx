<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ENDORSEMENT_APPROVAL.aspx.cs" Inherits="AGR.ENDORSEMENT_APPROVAL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <style>
    .download-button {
        background-color: green !important;
        color: white !important;
        font-style: italic;
        padding: 2px 6px;
        border-radius: 3px;
        border: none;
        cursor: pointer;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="300"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" Width="100" OnClick="BT_SEARCH_Click" /><asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList" Enabled="false" Visible="false"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                        <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="XX-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="AGENT CODE">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COLOR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" HeaderText="#CASES"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MODE_DESCR" HeaderText="ENDORSEMENT">
                                <ItemStyle ForeColor="White" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REMUN_TYPE" HeaderText="REMUN TYPE"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DOCUMENT">
                                <ItemStyle Width="120" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="BTN_DOWNLOAD_DOC"
                                                runat="server"
                                                Text="Download Document"
                                                Font-Size="XX-Small"
                                                CssClass="download-button"
                                                CommandName="DownloadDoc"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQ") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REMARK" HeaderText="REMARK">
                                <ItemStyle Wrap="true" Font-Bold="False" Font-Italic="True" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#006600" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REQUESTBY" HeaderText="REQUEST BY"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="200" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_ARCHIEVE" runat="server" Text="ARCHIEVE" Font-Size="XX-Small" CommandName="Archieve" />
                                    <asp:Button ID="BT_APPROVE" runat="server" Text="APPROVE" Font-Size="XX-Small" ForeColor="White" BackColor="Green" CommandName="Approve" />
                                    <asp:Button ID="BT_REJECT" runat="server" Text="REJECT" Font-Size="XX-Small" ForeColor="White" BackColor="Red" CommandName="Reject" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
                    </asp:DataGrid>


                </td>
            </tr>
        </table>
    </form>
</body>
</html>
