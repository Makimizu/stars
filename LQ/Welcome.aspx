<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="LQ.Welcome" %>

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
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_CHART1" runat="server" Text=""></asp:Label>


        <asp:Label ID="LB_USER" runat="server" Font-Bold="true"></asp:Label>
        <div class="row">
            <div class="col-xl-12 col-sm-6 mb-3">
                <asp:Label ID="LB_CARDS" runat="server"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-2 col-sm-6 mb-3">
                <div class="row">
                    <div class="col-xl-12 col-sm-6 mb-3" style="padding: 2px;">
                        <div class="card text-dark bg-transparent o-hidden h-100">
                            <div class="card-body" style="background-color: aquamarine; padding: 2px;">
                                <a href="Frame.aspx?URL=EAAAAGJxv11t2wyIlAvbcxkxTywRjyI6MAFLngFAus4CUf1dHoFe0WqyTPFZluoFK9hQvg==">
                                    <table style="border-spacing: 0px; width: 100%; color: darkblue;">
                                        <tr>
                                            <td style="font-size: 20pt; text-align: left; width: 50px;"><i class="fa fa-apple"></i></td>
                                            <td style="font-size: 6pt; text-align: center;">
                                                <span style="font-size: 8pt;">AGENT<BR />PRODUCTION</span>
                                            </td>
                                        </tr>
                                    </table>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-12 col-sm-6 mb-3" style="padding: 2px;">
                        <div class="card text-dark bg-transparent o-hidden h-100">
                            <div class="card-body" style="background-color: aquamarine; padding: 2px;">
                                <a href="Frame.aspx?URL=EAAAAFcGTbuV1IEXaSRief52lWURMJ5T55p9dn0hBZItwityXVc8kRCPDAWQqVJ1y8JRcA==">
                                    <table style="border-spacing: 0px; width: 100%; color: darkblue;">
                                        <tr>
                                            <td style="font-size: 20pt; text-align: left; width: 50px;"><i class="fas fa-coins"></i></td>
                                            <td style="font-size: 6pt; text-align: center;">
                                                <span style="font-size: 8pt;">AGENT<BR />REMUNERATION</span>
                                            </td>
                                        </tr>
                                    </table>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-12 col-sm-6 mb-3" style="padding: 2px;">
                        <div class="card text-dark bg-transparent o-hidden h-100">
                            <div class="card-body" style="background-color: aquamarine; padding: 2px;">
                                <a href="Frame.aspx?URL=EAAAAHnemmpIoaYhAWhDaVEcxW6uh9qHKgouNeIRv77HGJZ8R1XFU9XXuWfU0s3rqF4ylA==">
                                    <table style="border-spacing: 0px; width: 100%; color: darkblue;">
                                        <tr>
                                            <td style="font-size: 20pt; text-align: left; width: 50px;"><i class="fas fa-chalkboard-teacher"></i></td>
                                            <td style="font-size: 6pt; text-align: center;">
                                                <span style="font-size: 8pt;">AGENT<BR />TRAINING</span>
                                            </td>
                                        </tr>
                                    </table>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-10 col-sm-6 mb-3">
                <div class="row">
                    <div class="col-xl-12 col-sm-6 mb-3" style="padding: 2px;">
                        <div class="card text-dark bg-transparent o-hidden" style="border-color: silver;">
                            <div id="chart1" style="width: 100%; height: 40vh; padding: 0px; font-family: Tahoma; font-size: x-small; padding: 1px;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>


</asp:Content>
