<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/MobileParent.Master" AutoEventWireup="true" CodeBehind="MobileTraining.aspx.cs" Inherits="LQ.Mobile.MobileTraining" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <%--  <link href="../include/css/tabpanel.css" rel="stylesheet" />--%>
    <%--<link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="../include/css/tabpanel_ext.css" rel="stylesheet" />
    <script src="../include/vendor/jquery/jquery.min.js"></script>
    <script src="../include/vendor/bootstrap/js/bootstrap.min.js"></script>

    <form id="form1" runat="server">

        <div class="container ">
            <!-- Header -->
            <div id="content">
                <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                    <li class="active"><a href="#tab1" data-toggle="tab" style="font-size: small;">UPCOMING TRAINING</a></li>
                    <li><a href="#tab2" data-toggle="tab" style="font-size: small;">PAST TRAINING</a></li>
                </ul>
                <div id="my-tab-content" class="tab-content">
                    <div class="tab-pane  active" id="tab1">
                        <div class="row header" style="margin-top: 20px">
                            <div class="col-md-9">
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="C#.Net">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="Vb.Net">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="Asp.Net">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="Asp.Net MVC">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="WPF">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="WCF">
                                </div>

                            </div>
                            <div class="col-md-3">
                                <button type="submit" class="btn btn-primary">
                                    Submit</button>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="tab2">
                        <div class="row header" style="margin-top: 20px">
                            <div class="col-md-9">
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="CoreJava">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="AdvancedJava">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="JDBC">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="Hibernates">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="Springs">
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <input type="checkbox">
                                    </span>
                                    <input type="text" class="form-control" value="Structs">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <button type="submit" class="btn btn-primary">
                                    Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
