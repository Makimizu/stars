<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlagRK_Split.aspx.cs" Inherits="FINANCE.Form_Bank.FlagRK_Split" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flagging Breakdown</title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <link href="../Standard/CustomStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_INOUT" runat="server"></asp:Label>
        <div id="container">
            <div class="ASPHeaderButton">
                <asp:Button ID="BT_POST" runat="server" CssClass="btn btn-primary" Text="Post" OnClick="BT_POST_Click"/>
                <asp:Button ID="BT_CANCEL" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="BT_CANCEL_Click"/>
                <asp:Label ID="LB_ERROR_2" runat="server" CssClass="ASPLabelError"></asp:Label>
                <asp:Button ID="BT_BACK" runat="server" CssClass="btn btn-back" Text="Back" OnClick="BT_BACK_Click"/>
            </div>
            <div class="ASPContent">
                <asp:Label ID="LB_TRXID" runat="server" CssClass="ASPLabelTitle"></asp:Label>
                <table class="ASPTableHeader">
                    <tr>
                        <th>Bank Account</th>
                        <td colspan="3"><asp:Label ID="LB_NOREK" runat="server"></asp:Label> - <asp:Label ID="LB_BOOK_NAME" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>Descr</th>
                        <td colspan="3"><asp:Label ID="LB_DESCR" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>Post Date</th>
                        <td><asp:Label ID="LB_POST_DATE" runat="server"></asp:Label></td>
                        <th>Debit</th>
                        <td><asp:Label ID="LB_DEBET" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>Flag Date</th>
                        <td><asp:Label ID="LB_FLAG_DATE" runat="server"></asp:Label></td>
                        <th>Credit</th>
                        <td><asp:Label ID="LB_CREDIT" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <div class="NavTab">Journal Items</div>
                <table class="ASPTableForm">
                    <tr>
                        <td>Journal Code</td>
                        <td>
                            <asp:DropDownList ID="DDL_CODE" runat="server" CssClass="form-input" OnSelectedIndexChanged="DDL_CODE_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>D/C</td>
                        <td>
                            <asp:DropDownList ID="DDL_DC" runat="server" CssClass="form-input" OnSelectedIndexChanged="DDL_DC_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="D">Debet</asp:ListItem>
                                <asp:ListItem Value="C">Credit</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>COA</td>
                        <td>
                            <asp:DropDownList ID="DDL_COA" runat="server" CssClass="form-input"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>T00</td>
                        <td>
                            <asp:DropDownList ID="DDL_T00" runat="server" CssClass="form-input"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Amount</td>
                        <td>
                            <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="form-input" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="BT_SUBMIT" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BT_SUBMIT_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="LB_ERROR" CssClass="ASPLabelError" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="ASPTableDetail">
                    <tr id="TR_FLAGGING_DETAIL" runat="server">
                        <td style="border: none;">
                            <asp:DataGrid ID="DGR_DETAIL" runat="server"
                                GridLines="Vertical" AutoGenerateColumns="False" OnItemCommand="DGR_DETAIL_ItemCommand" ShowFooter="True">
                                <ItemStyle Wrap="False" />
                                <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                                <AlternatingItemStyle BackColor="#DCDCDC" />
                                <ItemStyle ForeColor="Black" />
                                <HeaderStyle ForeColor="Black" Font-Bold="true"/>
                                <Columns>
                                    <asp:BoundColumn DataField="CODE" visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DC" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COA" HeaderText="Account"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCR" HeaderText="Description"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="DEBET">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TXT_DEBET" runat="server" BackColor="#def2ff" CssClass="form-input" Font-Bold="True" ForeColor="Black" Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="CREDIT">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TXT_CREDIT" runat="server" BackColor="#f8dada" CssClass="form-input" Font-Bold="True" ForeColor="Black" Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="USERDATE" HeaderText="Userdate" ItemStyle-Width="80px"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="T00" HeaderText="T00" ></asp:BoundColumn>
                                    <asp:TemplateColumn ItemStyle-Width="10">
                                        <ItemTemplate>
                                            <asp:Button ID="BT_SAVE_DETAIL" runat="server" BackColor="Blue" CommandName="SaveDetail" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S" ToolTip="Save Detail" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-Width="10">
                                        <ItemTemplate>
                                            <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;"><asp:Button ID="BT_SUBMIT_DETAIL" runat="server" CssClass="btn btn-primary" Text="Submit Amount" OnClick="BT_SUBMIT_DETAIL_Click" /></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
