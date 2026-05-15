<%@ Page Language="C#" MasterPageFile="Parent.Master" AutoEventWireup="true" CodeBehind="Corporate.aspx.cs" Inherits="CORPORATE_PORTAL.Corporate" %>

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
        <%--<asp:ScriptManager runat="server" ID="ScriptManager" />--%>
        <%--<asp:UpdatePanel ID="panel1" runat="server">
            <ContentTemplate>--%>
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        
        <div class="card mb-3" runat="server" visible="true" id="DV_DETAIL">
            <div class="card-header">
                <asp:Label ID="LB_COMPANY_NAME" runat="server" Text="" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="TXT_CODE" runat="server" Visible="false" CssClass="TextBox"></asp:TextBox>
                &nbsp;<asp:Button ID="BT_SAVE" runat="server" OnClick="BT_SAVE_Click" Text="SAVE" CssClass="Button" />
            </div>
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                        <table style="border-spacing: 0px; width: 100%; font-size: small; text-align: left;">
                            <tr>
                                <td style="width: 120px;">Type</td>
                                <td>
                                    <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Category</td>
                                <td>
                                    <asp:DropDownList ID="DDL_CATEGORY" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Line Of Business</td>
                                <td>
                                    <asp:DropDownList ID="DDL_LOB" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Address 1</td>
                                <td>
                                    <asp:TextBox ID="TXT_ADDRESS1" runat="server" CssClass="TextBox" MaxLength="255" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Address 2</td>
                                <td>
                                    <asp:TextBox ID="TXT_ADDRESS2" runat="server" CssClass="TextBox" MaxLength="255" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>City</td>
                                <td>
                                    <asp:TextBox ID="TXT_KOTAMADYA" runat="server" CssClass="TextBox" MaxLength="50" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Province</td>
                                <td>
                                    <asp:DropDownList ID="DDL_PROPINSI" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>ZIP</td>
                                <td>
                                    <asp:TextBox ID="TXT_ZIPCODE" runat="server" CssClass="TextBox" MaxLength="10" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-6 mb-3">
                    <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                        <table style="border-spacing: 0px; width: 100%; font-size: small; text-align: left;">
                            <tr>
                                <td style="width: 120px">Phone</td>
                                <td>
                                    <asp:TextBox ID="TXT_PHONE" runat="server" CssClass="TextBox" MaxLength="50" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Fax</td>
                                <td>
                                    <asp:TextBox ID="TXT_FAX" runat="server" CssClass="TextBox" MaxLength="50" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Email</td>
                                <td>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="TextBox" MaxLength="100" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>PIC 1</td>
                                <td>
                                    <asp:TextBox ID="TXT_PIC1" runat="server" CssClass="TextBox" MaxLength="50" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>PIC 2<td>
                                    <asp:TextBox ID="TXT_PIC2" runat="server" CssClass="TextBox" MaxLength="50" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>PIC title</td>
                                <td>
                                    <asp:TextBox ID="TXT_PICTITLE" runat="server" CssClass="TextBox" MaxLength="100" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>NPWP</td>
                                <td>
                                    <asp:TextBox ID="TXT_NPWP" runat="server" CssClass="TextBox" MaxLength="25" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="card mb-3">
            <div class="card-header">
                <table style="border-spacing: 0px; width: 100%; font-size: small; text-align: left;">
                    <tr>
                        <td style="width: 120px;">
                            <asp:Label ID="Label1" runat="server" Text="BRANCH" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDL_BRANCH" runat="server" CssClass="DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_BRANCH_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                        <table style="border-spacing: 0px; width: 100%; font-size: small; text-align: left;">
                            <tr>
                                <td style="width: 120px;">Branch Code</td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_KDCAB" runat="server" Enabled="False" MaxLength="30" Width="50%"
                                        CssClass="TextBox"></asp:TextBox>
                                    <asp:Button ID="BT_NEWCAB" runat="server" OnClick="BT_NEWCAB_Click" Text="NEW" CssClass="Button" />
                                    &nbsp;<asp:Button ID="BT_SAVE_CAB" runat="server" OnClick="BT_SAVE_CAB_Click" Text="SAVE" CssClass="Button" />
                                </td>
                            </tr>
                            <tr>
                                <td>Branch Name
                                </td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_NAMACAB" runat="server" MaxLength="50" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Address 1</td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_ALAMATCAB1" runat="server" MaxLength="255" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Address 2 </td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_ALAMATCAB2" runat="server" MaxLength="255" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Phone</td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_PHNCAB" runat="server" MaxLength="50" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Fax</td>
                                <td>
                                    <asp:TextBox ID="TXT_FAXCAB" runat="server" MaxLength="50" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Email
                                </td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_EMAILCAB" runat="server" MaxLength="100" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-6 mb-3">
                    <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                        <table style="border-spacing: 0px; width: 100%; font-size: small; text-align: left;">
                            <tr>
                                <td style="width: 120px;">PIC
                                </td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_PICCAB" runat="server" MaxLength="50" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>PIC title</td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_PICTITLECAB" runat="server" MaxLength="50" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>PIC email</td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_PICEMAILCAB" runat="server" MaxLength="100" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>City</td>
                                <td class="style6">
                                    <asp:TextBox ID="TXT_KOTAMADYACAB" runat="server" MaxLength="50" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Province</td>
                                <td class="style6">
                                    <asp:DropDownList ID="DDL_PROPCAB" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>ZIP</td>
                                <td>
                                    <asp:TextBox ID="TXT_KODEPOSCAB" runat="server" MaxLength="10" Width="100%" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </form>

</asp:Content>
