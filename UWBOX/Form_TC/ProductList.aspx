<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="UWBOX.Form_TC.ProductList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 130px;">PRODUCT SEGMENT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_SEGMENT" runat="server" AutoPostBack="true" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_SEGMENT_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="0">INDIVIDU</asp:ListItem>
                                                <asp:ListItem Value="1">GROUP</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>PAYDI</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PAYDI" runat="server" AutoPostBack="true" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_PAYDI_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1">PAYDI</asp:ListItem>
                                                <asp:ListItem Value="0">NON PAYDI</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>UNITIZE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_UNITIZE" runat="server" AutoPostBack="true" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_UNITIZE_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1">UNITIZE</asp:ListItem>
                                                <asp:ListItem Value="0">NON UNITIZE</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TYPE" runat="server" AutoPostBack="true" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_TYPE_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT GROUP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_GROUP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_GROUP_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>LINE OF BUSINESS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_LOB" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 130px;">STATUS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                                <asp:ListItem Value="0">NOT ACTIVE</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 130px;">CURRENCY</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CURRENCY" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT CODE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" />
                                            &nbsp;
                                            &nbsp;
                                            <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="CREATE NEW PRODUCT" ForeColor="Blue" OnClick="BT_NEW_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" AllowPaging="True"
                        Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" OnPageIndexChanged="DGR_PageIndexChanged"
                        PagerStyle-Mode="NextPrev"
                        PagerStyle-Position="Bottom"
                        PagerStyle-HorizontalAlign="Center"
                        PagerStyle-NextPageText="Next"
                        PagerStyle-PrevPageText="Prev"
                        Font-Size="XX-Small">
                        <ItemStyle VerticalAlign="Top" BackColor="#EFF3FB" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Wrap="False" BackColor="#507CD1" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="STAT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_DESCR" HeaderText="PRODUCT CODE - PRODUCT NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_GROUP_DESCR" HeaderText="PRODUCT GROUP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_TYPE_DESCR" HeaderText="PRODUCT TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LINE_OF_BUSINESS_DESCR" HeaderText="LINE OF BUSINESS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEGMENT_DESCR" HeaderText="SEGMENT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PAYDI_DESCR" HeaderText="PAYDI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNITIZE_DESCR" HeaderText="UNITIZE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CURRENCY_DESCR" HeaderText="CURRENCY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="ACTIVE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
