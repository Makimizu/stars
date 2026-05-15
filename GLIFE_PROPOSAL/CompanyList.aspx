<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="CompanyList.aspx.cs" Inherits="GLIFE_PROPOSAL.CompanyList" %>

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
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <table style="border-spacing: 0px; font-size: 8pt; font-family: Verdana;">
                        <tr>
                            <td style="width: 150px;">POLICY NO
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_POLICYNO" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>COMPANY CODE
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY_CODE" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>COMPANY NAME
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY_NAME" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <table style="border-spacing: 0px; font-size: 8pt; font-family: Verdana;">
                        <tr>
                            <td style="width: 150px;">LINE OF BUSINESS</td>
                            <td>
                                <asp:DropDownList ID="DDL_LOB" AutoPostBack="true" runat="server" Width="100%" CssClass="DropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>PROVINCE
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_PROVINCE" runat="server" Width="100%" CssClass="DropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_CARI" runat="server" Text="SEARCH" OnClick="BT_CARI_Click" CssClass="ButtonColor" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>


        <asp:Label ID="LB_RECORD" runat="server" EnableViewState="False" ForeColor="Blue" Font-Size="Small"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            OnPageIndexChanged="DGR_PageIndexChanged"
            AllowPaging="True" AutoGenerateColumns="False" GridLines="None" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Width="100%">
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#E3EAEB" Wrap="False" BorderColor="Black" BorderWidth="0" />
            <Columns>
                <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="CODE" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="CODE">
                    <ItemTemplate>
                        <asp:Label ID="LB_CODE" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMPANY_LOB_DESCR" HeaderText="LOB"></asp:BoundColumn>
                <asp:BoundColumn DataField="PROPINSI_DESCR" HeaderText="PROVINCE"></asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White"
                Wrap="False" HorizontalAlign="Left" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <HeaderStyle HorizontalAlign="Left" />
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
        </asp:DataGrid>
    </form>


</asp:Content>


