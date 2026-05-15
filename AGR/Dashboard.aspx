<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AGR.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <%--<link href="Standard/CommonStyle.css" rel="stylesheet" />
    <!-- Bootstrap core CSS-->
    <link href="include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom fonts for this template-->
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <!-- Page level plugin CSS-->
    <link href="include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="include/css/sb-admin.css" rel="stylesheet" />--%>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script src="https://code.jquery.com/jquery-1.10.1.min.js"></script>
    <link href="Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body style="background-color: #cfd4f2;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <asp:Label ID="LB_CHART1" runat="server" Text=""></asp:Label>
        <asp:Label ID="LB_CHART2" runat="server" Text=""></asp:Label>
        <asp:Label ID="LB_CHART3" runat="server" Text=""></asp:Label>
        <asp:Label ID="LB_CHART4" runat="server" Text=""></asp:Label>



        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td >
                    <center>
                    <div id="chart1" style="width: 95%; height: 300px;"></div>
                    <div id="chart3" style="width: 95%;"></div>
                    </center>
                </td>
                <td style="width: 300px;">
                    <center><span style="font-size: 10pt; font-family: Arial;">YTD COMMISSION BY TYPE</span></center>
                    <asp:DataGrid ID="DGR_COMMTYPE" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False">
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
                    <center><span style="font-size: 10pt; font-family: Arial;">YTD TOP AGENT BY LEVEL</span></center>
                    <asp:DataGrid ID="DGR_TOPAGENT" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False">
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
                </td>
            </tr>
        </table>


        <asp:Label ID="LB_CARDS" runat="server"></asp:Label>
    </form>
</body>
</html>
