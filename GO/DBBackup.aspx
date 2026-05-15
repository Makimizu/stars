<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DBBackup.aspx.cs" Inherits="GO.DBBackup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td style="width:150px">BACKUP FOLDER</td>
                <td>
                    <asp:TextBox ID="TXT_FOLDER" runat="server" CssClass="ASPTextBox" Width="300px" BackColor="#99FFCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style1">
                    <asp:Button ID="BT_START" runat="server" CssClass="ASPButton" OnClick="BT_START_Click" Text="START BACKUP" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="LB_STATUS" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>SOURCE FILES FOLDER</td>
                <td>
                    <asp:TextBox ID="TXT_SOURCE_FOLDER" runat="server" CssClass="ASPTextBox" Width="300px" BackColor="Aqua"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_START_RESTORE" runat="server" CssClass="ASPButton" Text="START RESTORE" OnClick="BT_START_RESORE_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="LB_STATUS_RESTORE" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
