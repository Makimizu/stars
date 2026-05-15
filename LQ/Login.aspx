<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LQ.Login" %>

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
        <div class="container">
            <div class="card card-login mx-auto mt-5">
                <div class="login-wrap">
                    <div class="login-html">
                        <div class="group">
                            <center>
                            <asp:Image ID="IMG_LOGO" Height="60px" runat="server" />
                            </center>
                            <br />
                        </div>
                        <input id="tab-1" type="radio" name="tab" class="sign-in" checked><label for="tab-1" class="tab">Sign In</label>
                        <input id="tab-2" type="radio" name="tab" class="sign-up"><label for="tab-2" class="tab">Disclaimer</label>
                        <div class="login-form">
                            <div class="sign-in-htm">
                                <div class="group">
                                    <label for="user" class="label">Username</label>
                                    <asp:TextBox ID="TXT_UID" runat="server" CssClass="input"></asp:TextBox>
                                </div>
                                <div class="group">
                                    <label for="pass" class="label">Password</label>
                                    <asp:TextBox ID="TXT_PWD" runat="server" class="input" data-type="password" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="group" style="visibility: hidden;">
                                    <input id="check" type="checkbox" class="check" checked>
                                    <label for="check"><span class="icon"></span>Keep me Signed in</label>
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

