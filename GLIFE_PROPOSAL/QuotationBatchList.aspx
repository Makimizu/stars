<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="QuotationBatchList.aspx.cs" Inherits="GLIFE_PROPOSAL.QuotationBatchList" %>

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
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="row">
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: small;">
                    <tr>
                        <td style="width: 120px;">QUOTATION NO</td>
                        <td>
                            <asp:TextBox ID="TXT_QUOTNO" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>COMPANY</td>
                        <td>
                            <asp:TextBox ID="TXT_COMPANY" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: small;">
                    <tr>
                        <td style="width: 120px;">PRODUCT</td>
                        <td>
                            <asp:TextBox ID="TXT_PRODUCT" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px;">REG. DATE</td>
                        <td>
                            <asp:TextBox ID="TXT_REGDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_REGDATE1">
                            </ajaxToolkit:CalendarExtender>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="TXT_REGDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_REGDATE2">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ButtonColor" Text="SEARCH" Width="100px" OnClick="BT_SEARCH_Click" />
                            &nbsp;
                            <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
            CellSpacing="1" Font-Names="Tahoma" Font-Size="8pt" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20">
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
            <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="LBT_QUOTNO" runat="server" CommandName="Select"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="QUOTNO" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="PRODUCT_GROUP_DESCR" HeaderText="PRODUCT"></asp:BoundColumn>
                <asp:BoundColumn DataField="VERSIONS" HeaderText="#VER"></asp:BoundColumn>
                <asp:BoundColumn DataField="CREATEBY" HeaderText="CREATE BY"></asp:BoundColumn>
                <asp:BoundColumn DataField="CREATEDATE" HeaderText="CREATE DATE"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LBT_DELETE" runat="server" CommandName="Delete" ForeColor="Red" Font-Size="Small" ToolTip="Delete">
                                    <span class="fa fa-trash"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
        </asp:DataGrid>


    </form>
</asp:Content>
