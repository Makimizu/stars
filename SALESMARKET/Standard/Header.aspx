<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header.aspx.cs" Inherits="SALESMARKET.Standard.Header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body class="Gradient">
    <form id="form1" runat="server">
        <table style="width: 100%; top: 0px; left: 0px; position: absolute;">
            <tr>
                <td style="width: 50%; text-align: left;">
                    <asp:Image ID="IMG_LOGO" runat="server" Height="80%" />
                </td>
                <td style="text-align: right;">
                    <asp:Label ID="LBL_USER" runat="server" Font-Names="Tahoma" Font-Size="X-Small" ForeColor="Yellow"></asp:Label>
                    <br />                    
                    <asp:LinkButton ID="LB_LOGOUT" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="X-Small" ForeColor="Yellow" OnClick="LB_LOGOUT_Click">LOGOUT</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LB_PASSWORD" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="X-Small" ForeColor="Yellow" OnClick="LB_PASSWORD_Click">CHANGE PASSWORD</asp:LinkButton>
                    &nbsp;<a href="javascript:" onclick="window.open('main.aspx');" target="_blank"><B style="color: #FFFF00">NEW TAB</B></a>
                    <br />
                    <asp:DropDownList ID="DDL_APP" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td style="width: 70px; text-align: center;">
                    <asp:Image ID="IMG1" runat="server" Height="60px" />
                </td>
            </tr>
        </table>
        <asp:Label runat="server" ID="LB_SES" Visible="false"></asp:Label>
        <asp:ScriptManager runat="server" ID="SM1"></asp:ScriptManager>
        <asp:Timer runat="server" ID="TM_SESSION" OnTick="TM_SESSION_Tick"></asp:Timer>
    </form>
</body>
</html>
