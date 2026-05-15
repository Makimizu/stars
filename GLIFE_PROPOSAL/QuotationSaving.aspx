<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="QuotationSaving.aspx.cs" Inherits="GLIFE_PROPOSAL.QuotationSaving" %>

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

        <div id="DV_COMPANY" runat="server">
            <table style="border-spacing: 0px; width: 100%; font-size: small;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%; font-size: small;">
                            <tr>
                                <td style="width: 120px;">SEARCH COMPANY</td>
                                <td>
                                    <asp:TextBox ID="TXT_COMPANYSEARCH" runat="server" AutoPostBack="true" Width="100%" OnTextChanged="TXT_COMPANYSEARCH_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="DGR_COMPANY" runat="server" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical" OnItemCommand="DGR_POLICY_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_POLICY_PageIndexChanged">
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <Columns>
                                <asp:BoundColumn DataField="COMPANY_CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COMPANY NAME">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBT_COMPANY" runat="server" CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>

        <div id="DV_QUOT" runat="server" visible="false">
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    <div class="card-body" style="left: 0px; top: 0px;">
                        <table style="border-spacing: 0px; font-size: 10pt;">
                            <tr id="TR_QUOTNO" runat="server" visible="false">
                                <td>QUOTATION NO</td>
                                <td>
                                    <asp:Label ID="LB_QUOTNO" runat="server"></asp:Label>
                                    <asp:Label ID="LB_COMPANYCODE" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr id="TR_VERNO" runat="server" visible="false">
                                <td>VERSION NO</td>
                                <td>
                                    <asp:DropDownList ID="DDL_VERNO" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_VERNO_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Button ID="BT_NEW" runat="server" CssClass="ButtonColor" OnClick="BT_NEW_Click" Text="New" ForeColor="Green" BackColor="Gainsboro" Font-Italic="true" />
                                    <asp:Button ID="BT_COPY" runat="server" CssClass="ButtonColor" OnClick="BT_COPY_Click" Text="Copy" ForeColor="Blue" BackColor="Gainsboro" Font-Italic="true" Visible="false" />
                                    <asp:Button ID="BT_DEL" runat="server" CssClass="ButtonColor" Text="Del" Font-Bold="False" ForeColor="Red" BackColor="Gainsboro" Font-Italic="true" OnClick="BT_DEL_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px;">COMPANY</td>
                                <td>
                                    <asp:Label ID="LB_COMPANY" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>MEMBER</td>
                                <td>
                                    <asp:Label ID="LB_MEMBER" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>LAST BALANCE</td>
                                <td>
                                    <asp:Label ID="LB_BALANCE" runat="server"></asp:Label></td>
                            </tr>
                            <tr id="TR_BTSAVE" runat="server">
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ButtonColor" Width="100px" OnClick="BT_SAVE_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="col-xl-6 col-sm-6 mb-3">
                    <div class="card-body" style="width: 100%; left: 0px; top: 0px;">

                        <asp:DataGrid ID="DGR_REPORT" runat="server" Font-Size="X-Small" GridLines="None"
                            PageSize="20" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_REPORT_ItemCommand">
                            <ItemStyle Wrap="False" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundColumn DataField="URLAPP" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Button ID="BT_REPORT" runat="server" CssClass="ASPButton" Width="200" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>

                    </div>
                </div>
            </div>
        </div>

        <div id="DV_QUOTVER" runat="server" visible="false">
            <div id="DV_BUTTONS" runat="server">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr>
                                    <td style="width: 20%;">
                                        <asp:Button ID="BT_PACKAGE" runat="server" CssClass="ButtonColor" Text="PARAMETERS" Width="100%" OnClick="BT_PACKAGE_Click" Font-Size="8pt" />
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:Button ID="BT_MEMBER" runat="server" CssClass="ButtonColor" Text="MEMBER" Width="100%" OnClick="BT_MEMBER_Click" Font-Size="8pt" />
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:Button ID="BT_BRANCH" runat="server" CssClass="ButtonColor" Text="BRANCH" Width="100%" OnClick="BT_BRANCH_Click" Font-Size="8pt" />
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:Button ID="BT_ARCHIEVE" runat="server" CssClass="ButtonColor" Text="ARCHIEVE" Width="100%" OnClick="ARCHIEVE_Click" Font-Size="8pt" />
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:Button ID="BT_ACTUARY" runat="server" CssClass="ButtonColor" Text="CLOSING" Width="100%" OnClick="ACTUARY_Click" Font-Size="8pt" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #DCDCDC; color: black; border: 2px groove #FFFFFF; text-align: center;">
                            <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="DV_FRAME" runat="server">
                <iframe id="IF" runat="server" src="" style="width: 100%; height: 800px; overflow: auto; border: 0;"></iframe>
            </div>
        </div>
    </form>
</asp:Content>
