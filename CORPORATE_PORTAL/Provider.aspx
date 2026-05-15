<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="Provider.aspx.cs" Inherits="CORPORATE_PORTAL.Provider" %>

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
    <link href="include/css/controls.css" rel="stylesheet" />
    <link href="include/css/tabpanel_ext.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>

    <form runat="server">


        <table style="border-spacing: 0px; width: 100%; font-size: small; color: grey;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 120px;">Provider Name</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">Title</td>
                            <td>
                                <asp:TextBox ID="TXT_TITLE" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">Address</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">City</td>
                            <td>
                                <asp:TextBox ID="TXT_CITY" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="border-spacing: 0px; font-size: 11px; color: grey;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>IP</td>
                                        <td>: Inpatient</td>
                                    </tr>
                                    <tr>
                                        <td>OP</td>
                                        <td>: Outpatient</td>
                                    </tr>
                                    <tr>
                                        <td>MCU</td>
                                        <td>: Medical Check Up</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>OPT</td>
                                        <td>: Optical</td>
                                    </tr>
                                    <tr>
                                        <td>DRB</td>
                                        <td>: Doctor Booking</td>
                                    </tr>
                                    <tr>
                                        <td>LAB</td>
                                        <td>: Medical Laboratoium</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: center; vertical-align: bottom;">
                    <asp:Button ID="BT_SEARCH" runat="server" CssClass="ButtonColor" Text="SEARCH" Width="100px" OnClick="LB_PROV_SEARCH_Click" /><br />
                    <asp:Button ID="BT_EXCEL_EXPORT" runat="server" CssClass="ButtonColor" Text="EXCEL" Width="100px" OnClick="LB_EXCEL_EXPORT_Click" BackColor="Green" />
                    <br />
                    <br />
                    <asp:Label ID="LB_RESULT" runat="server" Text="" Font-Size="8pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><br /></td>
                <td></td>
                <td></td>
            </tr>
        </table>

        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" GridLines="Vertical" CssClass="DataGrid" AutoGenerateColumns="False" ForeColor="#333333" BorderColor="Silver" Width="100%" OnPageIndexChanged="DGR_PageIndexChanged" Font-Size="8pt">
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
            <AlternatingItemStyle BackColor="White" />
            <HeaderStyle BackColor="#CCCCCC" ForeColor="#666666"
                Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:BoundColumn DataField="NAMA" HeaderText="Provider Name">
                    <HeaderStyle Width="200px" />
                    <ItemStyle ForeColor="Blue" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TITLE" HeaderText="Title"></asp:BoundColumn>
                <asp:BoundColumn DataField="ALAMAT" HeaderText="Address"></asp:BoundColumn>
                <asp:BoundColumn DataField="KOTA_DESCR" HeaderText="City"></asp:BoundColumn>
                <asp:BoundColumn DataField="PHONE" HeaderText="Phone"></asp:BoundColumn>
                <asp:BoundColumn DataField="RI" HeaderText="IP">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="RJ" HeaderText="OP">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MCU" HeaderText="MCU">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OPT" HeaderText="OPT">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DRB" HeaderText="DRB">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LAB" HeaderText="LAB">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
            </Columns>
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
        </asp:DataGrid>

    </form>

</asp:Content>
