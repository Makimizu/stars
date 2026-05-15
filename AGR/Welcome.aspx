<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="AGR.Welcome" %>

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

    <script src="https://code.jquery.com/jquery-1.10.1.min.js"></script>

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <asp:Label ID="LB_CHART1" runat="server" Text=""></asp:Label>
        <asp:Label ID="LB_CHART2" runat="server" Text=""></asp:Label>
        <asp:Label ID="LB_CHART3" runat="server" Text=""></asp:Label>
        <asp:Label ID="LB_CHART4" runat="server" Text=""></asp:Label>

        <div class="row">
            <div id="chart1" style="width: 100%;"></div>
        </div>
        <div class="row">
            <div class="col-xl-8 col-sm-6 mb-3" style="padding: 1px;">
                <div class="card text-dark bg-transparent o-hidden" style="border-color: silver;">
                    <div id="chart3" style="width: 100%;"></div>
                    <%--<div id="chart2" style="width: 100%;"></div>--%>
                </div>
            </div>
            <div class="col-xl-4 col-sm-6 mb-3" style="padding: 1px;">
                <div class="card text-dark bg-transparent o-hidden" style="border-color: silver; font-size: small;">
                    <center>YTD COMMISSION BY TYPE</center>
                    <asp:DataGrid ID="DGR_COMMTYPE" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False">
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                    </asp:DataGrid>

                    <br />
                    <center>YTD TOP AGENT BY LEVEL</center>
                    <asp:DataGrid ID="DGR_TOPAGENT" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False">
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" Font-Size="X-Small" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                    </asp:DataGrid>

                </div>
            </div>
        </div>
        <asp:Label ID="LB_CARDS" runat="server"></asp:Label>
    </form>


</asp:Content>
