<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="QuotationNew.aspx.cs" Inherits="GLIFE_PROPOSAL.QuotationNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Bootstrap core CSS-->
    <link href="include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom fonts for this template-->
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <!-- Page level plugin CSS-->
    <link href="include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="include/css/sb-admin.css" rel="stylesheet" />
    <link href="include/css/controls.css" rel="stylesheet" />
    <form id="form1" runat="server">

        <table style="border-spacing: 0px; width: 100%; font-size: small;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: small;">
                        <tr>
                            <td>PRODUCT GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCT" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_PRODUCT_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">SEARCH COMPANY</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANYSEARCH" runat="server" AutoPostBack="true" Width="90%" OnTextChanged="TXT_COMPANYSEARCH_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">SEARCH PRODUCT</td>
                            <td>
                                <asp:TextBox ID="TXT_PRODUCTSEARCH" runat="server" AutoPostBack="true" Width="90%" OnTextChanged="TXT_PRODUCTSEARCH_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_COMPANY" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical" OnItemCommand="DGR_POLICY_ItemCommand" Width="90%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" OnPageIndexChanged="DGR_POLICY_PageIndexChanged">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="False" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="COMPANY NAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_COMPANY" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TC_DESCR" HeaderText="PRODUCT"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</asp:Content>

