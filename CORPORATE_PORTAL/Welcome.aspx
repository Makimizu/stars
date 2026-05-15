<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="CORPORATE_PORTAL.Welcome" %>

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
        <div style="width: 100%; padding: 0px;">

            <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </ajaxToolkit:ToolkitScriptManager>


            <asp:Label ID="LB_CLAIMCOMBO" runat="server" Text=""></asp:Label>
            <asp:Label ID="LB_PROVCOMBO" runat="server" Text=""></asp:Label>
            <asp:Label ID="LB_MEMBERCOMPOS" runat="server" Text=""></asp:Label>


            <div class="row">
                <div class="col-xl-8 col-sm-6 mb-3">
                    <div class="card text-dark bg-transparent o-hidden" style="border-color: silver;">
                        <div id="comboclaimmtd" style="width: 100%; height: 40vh; padding: 0px; font-family: Tahoma; font-size: 9px;"></div>
                    </div>
                    <br />
                    <div class="card text-dark bg-transparent o-hidden" style="border-color: silver;">
                        <div id="comboclaimprov" style="width: 100%; height: 40vh; padding: 0px;"></div>
                    </div>

                </div>
                <div class="col-xl-4 col-sm-6 mb-3" style="font-size: 10pt;">
                    <div class="card text-dark bg-transparent o-hidden" style="border-color: silver;">
                        <div id="membercompos" style="height: 100%; height: 200px; padding: 0px;"></div>
                    </div>
                    <br />
                    <center><b>TOP 10 DIAGNOSES</b></center>
                    <asp:DataGrid ID="DGR_ICD" runat="server" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="DataGrid" AutoGenerateColumns="False" ForeColor="#333333" BorderColor="Transparent" Width="100%" ShowHeader="false">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="DESCR">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CNT">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="60px" Font-Bold="true" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    <br />
                    <center><b>TOP 10 MEMBER BY CLAIM CASES</b></center>
                    <asp:DataGrid ID="DGR_CLM_CASE" runat="server" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="DataGrid" AutoGenerateColumns="False" ForeColor="#333333" BorderColor="Transparent" Width="100%" ShowHeader="false">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="NAMA">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAMILY_GROUP">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CNT">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="60px" Font-Bold="true" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>

        </div>
    </form>

</asp:Content>

