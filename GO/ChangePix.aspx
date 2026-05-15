<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePix.aspx.cs" Inherits="GO.ChangePix" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <table style="left: 0px; top: 0px;">
            <tr>
                <td>
                    <asp:Image ID="IMG1" runat="server" Height="60px" /></td>
                <td>
                    <asp:Label ID="ID" runat="server" Visible="false"></asp:Label>
                    UPLOAD PHOTO FILE :<br />
                    <asp:FileUpload ID="FILEUPLOAD" runat="server" CssClass="ASPTextBox" />
                    <br />
                    <asp:Button ID="BT_UPLOAD" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="U" OnClick="BT_UPLOAD_Click" />
                    <asp:Button ID="BT_EXIT" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" OnClick="BT_EXIT_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
