<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modul.aspx.cs" Inherits="GO.Modul" %>

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
    <link href="CommonStyle.css" rel="stylesheet" />
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
                            <asp:Image ID="IMG1" runat="server" ImageUrl="~/Images/default_photo.png" />
                            <br />
                            <asp:Label ID="LB_SES" runat="server" CssClass="ASPLabel" Visible="False"></asp:Label>
                            <br />
                            <asp:Label ID="LB_UID" runat="server" CssClass="ASPLabel" ForeColor="White" Font-Size="Medium"></asp:Label>
                            <br />
                            <asp:Label ID="LB_GROUP" runat="server" CssClass="ASPLabel" Visible="False"></asp:Label>
                            <br />

                            <asp:DataGrid ID="DGR" runat="server"
                                BorderWidth="0px" Font-Names="Tahoma" Font-Size="X-Small" PageSize="20"
                                OnItemCommand="DGR1_ItemCommand" AutoGenerateColumns="False" ShowHeader="False" ShowFooter="true">
                                <Columns>
                                    <asp:BoundColumn DataField="APP_CODE" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="APP_NAME" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PATH" Visible="False"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:Button ID="BT_APP" runat="server" CommandName="Go" CssClass="ASPButton" Font-Size="Small" Text="Button" BackColor="#99ccff" Width="300px" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="BT_LOGOUT" runat="server" CommandName="Logout" CssClass="ASPButton" Font-Bold="True" BackColor="Gray" ForeColor="White" Font-Size="Small" Text="L O G O U T" Width="300px" />
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                            </center>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
