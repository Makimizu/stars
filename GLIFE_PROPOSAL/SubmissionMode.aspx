<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="SubmissionMode.aspx.cs" Inherits="GLIFE_PROPOSAL.SubmissionMode" %>

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

    <form id="form1" runat="server">

        <div class="row">
            <div class="col-xl-3 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: white;">
                        <a href="MemberEntry.aspx?ID=&QUOT=1">
                            <table style="width: 100%; color: green;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 30pt; width: 50px;"><i class="fa fa-edit"></i></td>
                                                <td style="text-align: left;">
                                                    <span style="font-size: 30px"></span>
                                                    NEW SUBMISSION<br />
                                                    ENTRY
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </a>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: white;">
                        <a href="MemberListPick.aspx">
                            <table style="width: 100%; color: blue;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 30pt; width: 50px;"><i class="fa fa-user"></i></td>
                                                <td style="text-align: left;">
                                                    <span style="font-size: 30px"></span>
                                                    NEW SUBMISSION FROM EXISTING CUSTOMER
                                                    <%--<br />
                                                    <span style="font-size: small;">FROM EXISTING CUSTOMER</span>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </a>
                    </div>
                </div>
            </div>

            <div id="trow1" runat="server" class="col-xl-3 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: white;">
                        <a href="BatchUpload.aspx">
                            <table style="width: 100%; color: green;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 30pt; width: 50px;"><i class="fa fa-upload"></i></td>
                                                <td style="text-align: left;">
                                                    <span style="font-size: 30px"></span>
                                                    NEW SUBMISSION<br />
                                                    UPLOAD
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </a>
                    </div>
                </div>
            </div>

            <div id="trow2" runat="server" class="col-xl-3 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: white;">
                        <a href="BatchUploadList.aspx">
                            <table style="width: 100%; color: darkgreen;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 30pt; width: 50px;"><i class="fa fa-upload"></i></td>
                                                <td style="text-align: left;">
                                                    <span style="font-size: 30px"></span>
                                                    UPLOAD<br />
                                                    BATCH
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="visibility: hidden;">
            <div id="trow3" runat="server" class="col-xl-3 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: #ebe0e0;">
                        <a href="QuotationNew.aspx">
                            <table style="width: 100%; color: purple;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 30pt; width: 50px;"><i class="fa fa-edit"></i></td>
                                                <td style="text-align: left;">
                                                    <span style="font-size: 30px"></span>
                                                    NEW BUSINESS & RENEWAL<br>
                                                    <span style="font-size: small;">NEW QUOTATION</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </a>
                    </div>
                </div>
            </div>

            <div id="trow4" runat="server" class="col-xl-3 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: #ebe0e0;">
                        <a href="QuotationBatchList.aspx">
                            <table style="width: 100%; color: purple;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 30pt; width: 50px;"><i class="fa fa-list"></i></td>
                                                <td style="text-align: left;">
                                                    <span style="font-size: 30px"></span>
                                                    NEW BUSINESS & RENEWAL<br>
                                                    <span style="font-size: small;">QUOTATION LIST</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </form>


</asp:Content>
