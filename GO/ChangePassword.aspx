<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="GO.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet"/>
    <style type="text/css">
        .auto-style1 {
            width: 157px;
        }

        .auto-style2 {
            width: 157px;
            height: 22px;
            font-weight: bold;
            color: #000000;
        }

        .auto-style3 {
            height: 22px;
            text-align: left;
        }

        .auto-style4 {
            width: 157px;
            font-weight: bold;
            color: #000000;
        }

        .auto-style5 {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; width: 100%;">
            <tr>
                <td style="vertical-align: middle; text-align: center;">
                    <asp:Image ID="IMG1" runat="server" />
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td class="auto-style4">NAME</td>
                            <td class="auto-style5">
                                <asp:Label ID="LB_NAME" runat="server" Font-Bold="True" ForeColor="Lime" style="color: #0000FF"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">OLD PASSWORD</td>
                            <td class="auto-style5">
                                <asp:TextBox ID="TXT_PWD_OLD" runat="server" CssClass="ASPTextBox" TextMode="Password" Width="212px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">NEW PASSWORD</td>
                            <td class="auto-style5">
                                <asp:TextBox ID="TXT_PWD_NEW1" runat="server" CssClass="ASPTextBox" TextMode="Password" Width="212px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">RETYPE NEW PASSWORD</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="TXT_PWD_NEW2" runat="server" CssClass="ASPTextBox" TextMode="Password" Width="212px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1"></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SUBMIT" />
                                <asp:Label ID="LB_SES" runat="server" Visible="False"></asp:Label>
                                <br />
                                <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
