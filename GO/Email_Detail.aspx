<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Email_Detail.aspx.cs" Inherits="GO.Email_Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr>
                <td style="border-bottom-style: ridge;">
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>DESCR</td>
                            <td>
                                <asp:Label ID="LB_DESCR" runat="server" Font-Bold="True"></asp:Label>
                                <asp:Label ID="LB_CODE" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>VIEW</td>
                            <td>
                                <asp:DropDownList ID="DDL_VIEW" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_VIEW_SelectedIndexChanged">
                                    <asp:ListItem Value="0">BODY EMAIL</asp:ListItem>
                                    <asp:ListItem Value="1">ATTACHMENT</asp:ListItem>
                                    <asp:ListItem Value="2">SQL</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr id="TR_BODY" runat="server" visible="true">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <input id="TXT_FILE_UPLOAD" runat="server" name="TXT_FILE_UPLOAD" type="file"
                                    class="ASPTextBox" /><asp:Button ID="BT_BODY_UPLOAD" runat="server" CssClass="ASPButton" OnClick="BT_BODY_UPLOAD_Click" Text="UPLOAD BODY TEMPLATE" />
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: white;">
                                <asp:Label ID="LB_BODY" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_EMAIL" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:TextBox ID="TXT_SEARCH_REPORT" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox" Width="80%" AutoPostBack="True" OnTextChanged="TXT_SEARCH_REPORT_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="LB1" runat="server" BackColor="#CCCCCC" CssClass="ASPDropDownList" Height="200px" Width="100%"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <center>
                                <asp:Button ID="BT_ON" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="PICK" OnClick="BT_ON_Click" />
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td>

                                <asp:DataGrid ID="DGR" runat="server" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20" ShowHeader="False" CssClass="ASPDatagrid" OnItemCommand="DGR_ItemCommand" AutoGenerateColumns="False" CellPadding="2" GridLines="None" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" ForeColor="Black">
                                    <AlternatingItemStyle BackColor="PaleGoldenrod" />
                                    <Columns>
                                        <asp:BoundColumn DataField="REPORT_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FORMAT" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_FORMAT" runat="server" CssClass="ASPDropDownList">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="S" Font-Bold="True" ForeColor="White" BackColor="Green" />
                                                <asp:Button ID="BT_DELETE" runat="server" CommandName="Delete" CssClass="ASPButton" Text="X" Font-Bold="True" ForeColor="White" BackColor="Red" />
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                    <ItemStyle VerticalAlign="Top" Wrap="False" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_SQL" runat="server" visible="false">
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td style="width:100px;">TABLE NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_TABLENAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>DOC NO FIELD</td>
                            <td>
                                <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">DOC DATE FIELD</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="TXT_DOCDATE" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CUST NAME FIELD</td>
                            <td>
                                <asp:TextBox ID="TXT_CUSTNAME" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CUST PIC FIELD</td>
                            <td>
                                <asp:TextBox ID="TXT_CUSTPIC" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CUST EMAIL FIELD</td>
                            <td>
                                <asp:TextBox ID="TXT_CUSTEMAIL" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>REPORT FIELD</td>
                            <td>
                                <asp:TextBox ID="TXT_REPORT" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="vertical-align:top;">
                            <td>BODY FIELD</td>
                            <td>
                                <asp:TextBox ID="TXT_BODY" runat="server" CssClass="ASPTextBox" Height="50px" MaxLength="100" TextMode="MultiLine" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SQL_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SQL_SAVE_Click" Text="SAVE" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
