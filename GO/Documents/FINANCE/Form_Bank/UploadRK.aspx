<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadRK.aspx.cs" Inherits="FINANCE.Form_Bank.UploadRK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 150px;">ACC NO</td>
                            <td>
                                <asp:DropDownList ID="DDL_NOREK" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_NOREK_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>FORMAT TEMPLATE</td>
                            <td>
                                <asp:DropDownList ID="DDL_FORMAT" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_FORMAT_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="TR_XLS" runat="server">
                            <td>GENERAL FORMAT TEMPLATE</td>
                            <td><a href="BANK_STATEMENT.xls"><b>BANK_STATEMENT.xls</b></a></td>
                        </tr>
                        <tr>
                            <td>FILE</td>
                            <td>
                                <input id="TXT_FILE_UPLOAD" runat="server" name="TXT_FILE_UPLOAD" type="file"
                                    class="ASPTextBox" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_UPLOAD_Click" Text="UPLOAD" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="BATCH_ID" HeaderText="BATCH ID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOREK" HeaderText="ACC NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BOOK_NAME" HeaderText="BOOK NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK_NAME" HeaderText="BANK NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FILENAME" HeaderText="FILENAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RECORDS" HeaderText="RECORDS">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="USERBY" HeaderText="USER BY">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="USERDATE" HeaderText="USER DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
