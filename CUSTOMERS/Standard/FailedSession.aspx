<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FailedSession.aspx.cs" Inherits="CUSTOMERS.Standard.FailedSession" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <asp:Image ID="IMG_LOGO" runat="server" Width="200" style="display: block;margin-left: auto;margin-right: auto;" />
            <br />
            <asp:Label ID="LB_MSG" runat="server" Text="Session was expired" Font-Bold="true" style="font-size: xx-large"></asp:Label>
        </center>
    </form>
</body>
</html>
