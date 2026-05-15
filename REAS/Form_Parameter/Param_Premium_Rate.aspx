<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Param_Premium_Rate.aspx.cs" Inherits="REAS.Form_Parameter.Param_Premium_Rate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <table style="width: 100%; left: 0px; top: 0px; border-spacing: 0px;">
            <tr>
                <td style="vertical-align: top; ">
                    <asp:TextBox ID="TXT_SEARCH" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_SEARCH_TextChanged" Width="300px"></asp:TextBox>
                    <asp:DataGrid ID="DGR_LIST" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" OnItemCommand="DGR_LIST_ItemCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="100%">
                        <AlternatingItemStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CODE" ItemStyle-Width="200" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DELETE" runat="server" Text="X" BackColor="Red" ForeColor="White" CssClass="ASPButton" CommandName="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle HorizontalAlign="Left" />
                        <FooterStyle BackColor="Tan" />
                        <HeaderStyle Wrap="False"
                            HorizontalAlign="Center" BackColor="Tan" Font-Bold="True" />
                        <PagerStyle PageButtonCount="5" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    </asp:DataGrid>
                </td>
                <td style="width: 30px;"></td>
                <td style="vertical-align: top; width: 100%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr id="TR_NEW" runat="server" visible="false">
                            <td></td>
                            <td>
                                <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW" Width="100px" OnClick="BT_NEW_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">CODE</td>
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
                            <td>TENOR TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TENOR" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" BackColor="Blue" ForeColor="White" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                                <asp:Button ID="BT_CLEAR" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="CLEAR" Width="100px" OnClick="BT_CLEAR_Click" Visible="False" />
                                <asp:FileUpload ID="FU" runat="server" CssClass="ASPButton" onchange="javascript:Submit()" Visible="False" />
                            </td>
                        </tr>
                        <tr id="TR_GENDER" runat="server" visible="false">
                            <td>GENDER</td>
                            <td>
                                <asp:DropDownList ID="DDL_GENDER" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_GENDER_SelectedIndexChanged">
                                    <asp:ListItem Value="M">MALE</asp:ListItem>
                                    <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR_RATE" runat="server" CssClass="ASPDatagrid" OnItemCommand="DGR_LIST_ItemCommand" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle Wrap="False" HorizontalAlign="Center" BackColor="#EEEEEE" ForeColor="Black" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle Wrap="False"
                            HorizontalAlign="Center" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
