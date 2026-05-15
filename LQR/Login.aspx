<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LQR.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <!-- Custom fonts for this template-->
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template-->
    <%--<link href="include/css/sb-admin.css" rel="stylesheet" />--%>
    <link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Open+Sans:600' />
    <link rel="stylesheet" href="include/css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <div class="container">
            <div class="card card-login mx-auto mt-5">
                <div class="login-wrap" style="height: 100vh;">
                    <div class="login-html" style="background-color: darkgreen;">
                        <div class="group">
                            <center>
                            <asp:Image ID="IMG_LOGO" Height="60px" runat="server" />
                            </center>
                            <br />
                        </div>
                        <input id="tab-1" type="radio" name="tab" class="sign-in" checked><label for="tab-1" class="tab" style="font-size: small;">MASUK</label>
                        <input id="tab-2" type="radio" name="tab" class="sign-up"><label for="tab-2" class="tab" style="font-size: small;">DISCLAIMER</label>
                        <div class="login-form">
                            <div class="sign-in-htm">
                                <div class="group">
                                    <label for="pass" class="label">
                                        <p style="text-align: center">
                                            Assalamualaikum Wr Wb<br />
                                            Mohon konfirmasi dari Bapak/Ibu<br />
                                            <asp:Label ID="LB_MEMBER" runat="server" CssClass="ASPLabel" Font-Bold="true" Font-Size="Medium" ForeColor="Gainsboro"></asp:Label><br />
                                            atas pengajuan polis produk<br />
                                            <asp:Label ID="LB_PRODUCT" runat="server" CssClass="ASPLabel" Font-Bold="true" Font-Size="Medium" ForeColor="Gainsboro"></asp:Label>
                                            <br />
                                            <br />
                                            Masukkan tanggal lahir Bapak/Ibu sesuai format <b>DDMMYYYY</b> (contoh:<b>01011980</b>)
                                        </p>
                                    </label>
                                    <asp:TextBox ID="TXT_PWD" runat="server" class="input" data-type="password" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="group">
                                    <asp:Button ID="BT_SUBMIT" runat="server" Text="Sign In" CssClass="button" OnClick="BT_SUBMIT_Click" />
                                </div>
                                <div class="hr"></div>
                                <div class="foot-lnk">
                                    <asp:Label ID="LB_MSG" runat="server" Text="" ForeColor="LightYellow"></asp:Label>
                                    <br />
                                    Copyright &copy; 
                                    <script type="text/javascript">
                                        document.write(new Date().getFullYear());
                                    </script>
                                </div>
                            </div>
                            <div class="sign-up-htm">
                                <div class="group">
                                    <div style="width: 100%; height: 400px; background-color: white; color: black; font-family: Verdana; font-size: small; overflow: auto; font-weight: normal; border-radius: 5px; text-align: justify;">
                                        <br />
                                        <asp:Label ID="LB_NDA" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
