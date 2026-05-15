<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="CORPORATE_PORTAL.ChangePassword" %>

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


    <form runat="server">


        <div class="card mb-3">
            <div class="card-header" style="color: lightslategrey">
                <i class="fas fa-key"></i>&nbsp;&nbsp;Change Password
            </div>
            <div class="card-body">
                <table style="border-spacing: 0px; font-size: 9pt; color: grey;">
                    <tr>
                        <td>
                            <asp:TextBox ID="TXT_CURRPWD" runat="server" CssClass="TextBox" TextMode="Password" Width="300px" placeholder="Enter current password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TXT_NEWPWD" runat="server" CssClass="TextBox" TextMode="Password" Width="300px" placeholder="Enter new password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TXT_NEWPWD2" runat="server" CssClass="TextBox" TextMode="Password" Width="300px" placeholder="Re-enter new password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_PASSWORD" runat="server" CssClass="ButtonColor" Text="Submit" OnClick="BT_PASSWORD_Click" />
                        </td>
                    </tr>
                </table>

            </div>
            <div class="card-footer small text-muted">
                <asp:Label ID="LB_PASSWORD_RESULT" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</asp:Content>
