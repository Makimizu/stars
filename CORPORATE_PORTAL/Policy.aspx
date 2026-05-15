<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="Policy.aspx.cs" Inherits="CORPORATE_PORTAL.Policy" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    <!-- Page level plugin CSS-->
    <link href="include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="include/css/sb-admin.css" rel="stylesheet" />
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script src="https://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>

    <form runat="server">

        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <asp:Label ID="LB_APPID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_REPORTCODE" runat="server" Visible="false"></asp:Label>

        <div class="row">
            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <table style="border-spacing: 0px; width: 100%; font-size: small; text-align: left; color: grey; padding: 0px;">
                        <tr>
                            <td style="width: 100px;">Period</td>
                            <td>
                                <asp:DropDownList ID="DDL_PERIOD" runat="server" CssClass="dropdown" AutoPostBack="True" BackColor="#e8e8e8"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Policy No</td>
                            <td>
                                <asp:Label ID="LB_POLNO" runat="server" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Type</td>
                            <td>
                                <asp:Label ID="LB_TYPE" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>                        
                        <tr>
                            <td>Product</td>
                            <td>
                                <asp:Label ID="LB_PROD" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <table style="border-spacing: 0px; width: 100%; font-size: small; text-align: left; color: grey; padding: 0px;">
                        <tr>
                            <td style="width: 100px;">TPA</td>
                            <td>
                                <asp:Label ID="LB_TPA" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>MOP</td>
                            <td>
                                <asp:Label ID="LB_MOP" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Process Date</td>
                            <td>
                                <asp:Label ID="LB_PROCDATE" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div style="height: auto; width: 100%; overflow: auto;">
            <rsweb:ReportViewer ID="RV1" runat="server" Width="100%" ShowParameterPrompts="false">
            </rsweb:ReportViewer>
        </div>
    </form>

</asp:Content>

