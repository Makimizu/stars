<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductRate.aspx.cs" Inherits="HLP.Form_Parameter.ProductRate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; left: 0px; top: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>BENEFIT<asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_BENEFIT" CssClass="ASPDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_BENEFIT_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td style="width:100px;"></td>
                            <td>
                                DOWNLOAD</td>
                            <td>
                                <asp:Button ID="BT_XL" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_DOWNLOAD_Click" Text="XLS" BackColor="Green" />
                            </td>
                        </tr>
                        <tr>
                            <td>PLAN</td>
                            <td>
                                <asp:DropDownList ID="DDL_PLAN" CssClass="ASPDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_PLAN_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                UPLOAD</td>
                            <td>
                                                                    <asp:Button ID="BT_XLS_UPLOAD" runat="server" OnClick="BT_XLS_UPLOAD_Click"
                                                                        Text="UPLOAD XLS" CssClass="ASPButton" Font-Bold="True" ForeColor="White" BackColor="Blue" />
                                                        <input id="TXT_FILE_UPLOAD" runat="server" name="TXT_FILE_UPLOAD" type="file"
                                                            class="ASPTextBox" /></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                    <asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" ItemStyle-Wrap="true" AutoGenerateColumns="False" OnItemDataBound="DGR_ItemDataBound" OnItemCommand="DGR_ItemCommand" ShowFooter="True">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="BENEFIT_DETAIL_ID" HeaderText="CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="RATE">
                                <FooterTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />
                                </FooterTemplate>
                                <HeaderTemplate>
                                    <asp:Button ID="BT_COPY" runat="server" CommandName="Copy" CssClass="ASPButton" Text="COPY FROM :" />
                                    <asp:DropDownList ID="DDL_PLAN" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBoxNumber" Width="130px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>
                    <asp:DataGrid ID="DGR_ALL" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" OnItemCommand="DGR_ALL_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_COPY" runat="server" CommandName="Copy" CssClass="ASPButton" Text="COPY FROM :" />
                                    <asp:DropDownList ID="DDL_PLAN" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
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
