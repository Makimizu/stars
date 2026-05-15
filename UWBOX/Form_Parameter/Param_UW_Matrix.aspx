<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Param_UW_Matrix.aspx.cs" Inherits="UWBOX.Form_Parameter.Param_UW_Matrix" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />

    <script type="text/javascript">
        function Submit() {
            form1.submit();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">SEARCH</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">PRODUCT GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCT_GROUP_SEARCH" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_PRODUCT_GROUP_SEARCH_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>MATRIX&nbsp; NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_SEARCH" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_SEARCH_TextChanged" Width="300"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="LB_RECORD" runat="server"></asp:Label></td>
                        </tr>
                    </table>

                    <div style="width: 100%; height: 100px; overflow: auto; border: inset;">
                        <asp:DataGrid ID="DGR_LIST" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" OnItemCommand="DGR_LIST_ItemCommand" BackColor="White" BorderColor="#E7E7FF" BorderWidth="1px" CellPadding="1" GridLines="None" Width="100%" BorderStyle="None">
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LB_ID" runat="server" CommandName="Detail"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CODE" ItemStyle-Width="200" Visible="False">
                                    <ItemStyle Width="200px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemStyle Width="60" />
                                    <ItemTemplate>
                                        <asp:Button ID="BT_DELETE" runat="server" Text="X" BackColor="Red" ForeColor="White" CssClass="ASPButton" CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle Wrap="False"
                                HorizontalAlign="Center" BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle PageButtonCount="5" BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                            <SelectedItemStyle BackColor="#738A9C" ForeColor="#F7F7F7" Font-Bold="True" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">TABLE INFO</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr id="TR_NEW" runat="server" visible="false">
                            <td></td>
                            <td>
                                <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW" Width="100px" OnClick="BT_NEW_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">CODE</td>
                            <td>
                                <asp:Label ID="LB_CODE" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>MATRIX NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PRODUCT GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCT_GROUP" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" BackColor="Blue" ForeColor="White" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                                <asp:Button ID="BT_CLEAR" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="CLEAR" Width="100px" OnClick="BT_CLEAR_Click" Visible="False" />
                            </td>
                        </tr>
                    </table>
                    <table id="TBL_XLS" runat="server" visible="false" style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 150px;"><a href="UW_MATRIX.xls">DOWNLOAD XLS TEMPLATE</a></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>UPLOAD DATA</td>
                            <td>
                                <asp:FileUpload ID="FU" runat="server" CssClass="ASPButton" onchange="javascript:Submit()" Visible="False" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                    <table id="TBL_DETAIL" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">DATA</td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 100%; height: 800px; overflow: auto;">
                                    <asp:DataGrid ID="DGR_UW" runat="server" CssClass="ASPDatagrid" OnItemCommand="DGR_LIST_ItemCommand" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="3" BorderStyle="None">
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" ForeColor="#000066" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle Wrap="False"
                                            HorizontalAlign="Center" BackColor="#006699" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <PagerStyle PageButtonCount="5" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" Mode="NumericPages" />
                                        <SelectedItemStyle BackColor="#669999" ForeColor="White" Font-Bold="True" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
