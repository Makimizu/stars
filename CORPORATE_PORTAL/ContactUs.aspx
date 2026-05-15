<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="CORPORATE_PORTAL.ContactUs" %>

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
    <%--<link href="include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />--%>
    <!-- Custom styles for this template-->
    <link href="include/css/sb-admin.css" rel="stylesheet" />
    <link href="include/css/controls.css" rel="stylesheet" />

    <form runat="server">

        <div class="row">
            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden" style="border-color: silver; padding: 20px;">
                    <table style="border-spacing: 0px; width: 100%; font-size: small;">
                        <tr style="border-bottom: ridge;">
                            <td><b>Head Office</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LB_COMPANY" runat="server"></asp:Label>
                                <table style="border-spacing:0px;">
                                    <tr>
                                        <td style="width:30px;"><span class="fa fa-phone"></span></td>
                                        <td style="width:60px;">Phone</td>
                                        <td>:</td>
                                        <td><asp:Label ID="LB_COMPANYPHONE" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width:30px;"><span class="fa fa-fax"></span></td>
                                        <td style="width:60px;">Fax</td>
                                        <td>:</td>
                                        <td><asp:Label ID="LB_COMPANYFAX" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width:30px;"><span class="fa fa-envelope"></span></td>
                                        <td style="width:60px;">Email</td>
                                        <td>:</td>
                                        <td><asp:Label ID="LB_COMPANYEMAIL" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden" style="border-color: silver; padding: 20px;">
                    <table style="border-spacing: 0px; width: 100%; font-size: small;">
                        <tr style="border-bottom: ridge;">
                            <td><b>Your Agent Contact</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LB_AGENT" runat="server"></asp:Label>
                                <table style="border-spacing:0px;">
                                    <tr>
                                        <td style="width:30px;"><span class="fa fa-phone"></span></td>
                                        <td style="width:60px;">Phone</td>
                                        <td>:</td>
                                        <td><asp:Label ID="LB_AGENTPHONE" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width:30px;"><span class="fa fa-envelope"></span></td>
                                        <td style="width:60px;">Email</td>
                                        <td>:</td>
                                        <td><asp:Label ID="LB_AGENTEMAIL" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

    </form>

</asp:Content>
