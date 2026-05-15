<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="GLIFE_PROPOSAL.Company" %>

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
                    <table style="border-spacing: 0px; width: 100%; font-size: 8pt; font-family: Verdana;">
                        <tr>
                            <td style="width: 130px">CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" runat="server" BackColor="#CCCCCC" CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY_NAME" runat="server" CssClass="TextBox" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>CATEGORY</td>
                            <td>
                                <asp:DropDownList ID="DDL_CATEGORY" runat="server" CssClass="ASPDropDownList" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>LINE OF BUSINESS</td>
                            <td>
                                <asp:DropDownList ID="DDL_LOB" runat="server" CssClass="ASPDropDownList" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>ADDRESS 1</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS1" runat="server" CssClass="TextBox" MaxLength="255" Width="300px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>ADDRESS 2</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS2" runat="server" CssClass="TextBox" MaxLength="255" Width="300px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CITY</td>
                            <td>
                                <asp:TextBox ID="TXT_KOTAMADYA" runat="server" CssClass="TextBox" MaxLength="50" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PROVINCE</td>
                            <td>
                                <asp:DropDownList ID="DDL_PROPINSI" runat="server" CssClass="ASPDropDownList" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ZIP CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_ZIPCODE" runat="server" CssClass="TextBox" MaxLength="10" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <table style="border-spacing: 0px; width: 100%; font-size: 8pt; font-family: Verdana;">
                        <tr>
                            <td style="width: 130px">PHONE</td>
                            <td>
                                <asp:TextBox ID="TXT_PHONE" runat="server" CssClass="TextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>FAX</td>
                            <td>
                                <asp:TextBox ID="TXT_FAX" runat="server" CssClass="TextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>EMAIL</td>
                            <td>
                                <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="TextBox" MaxLength="100" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PIC 1</td>
                            <td>
                                <asp:TextBox ID="TXT_PIC1" runat="server" CssClass="TextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PIC 2</td>
                            <td>
                                <asp:TextBox ID="TXT_PIC2" runat="server" CssClass="TextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PIC TITLE</td>
                            <td>
                                <asp:TextBox ID="TXT_PICTITLE" runat="server" CssClass="TextBox" MaxLength="100" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>REG. DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_REGDATE" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="TextBox" Width="150px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TAX NO</td>
                            <td>
                                <asp:TextBox ID="TXT_NPWP" runat="server" CssClass="TextBox" MaxLength="25" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ButtonColor" Text="SAVE" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div id="DV_BUTTONS" runat="server">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 50%;">
                                    <asp:Button ID="BT1" runat="server" CssClass="ButtonColor" Text="BRANCH" Width="100%" OnClick="BT1_Click" />
                                </td>
                                <td style="width: 50%;">
                                    <asp:Button ID="BT3" runat="server" CssClass="ButtonColor" Text="ARCHIEVE" Width="100%" OnClick="BT3_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #DCDCDC; color: black; border: 2px groove #FFFFFF; text-align: center;">
                        <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <div id="DV_FRAME" runat="server">
            <iframe id="IF" runat="server" src="" style="height: 800px; width: 100%; border: 0;"></iframe>
        </div>
    </form>


</asp:Content>
