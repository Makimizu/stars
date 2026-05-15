<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationAgentInfo.aspx.cs" Inherits="LQ.Form_Client.QuotationAgentInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0; width: 100%; font-size: xx-small;">
            <tr>
                <td>AGENT CODE</td>
                <td>
                    <asp:Label ID="LB_CODE" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 130px;">AGENT NAME</td>
                <td>
                    <asp:LinkButton ID="LB_FULLNAME" runat="server" Font-Bold="true" OnClick="LB_FULLNAME_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>DIVISION IN CHARGE</td>
                <td>
                    <asp:Label ID="LB_MARKETSEGMENT" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>LEVEL</td>
                <td>
                    <asp:Label ID="LB_LEVEL" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>AGENCY</td>
                <td>
                    <asp:Label ID="LB_AGENCY" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>LICENCE NO</td>
                <td>
                    <asp:Label ID="LB_LICENCENO" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>LICENCE PERIOD</td>
                <td>
                    <asp:Label ID="LB_PERIOD" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>LICENCE STATUS</td>
                <td>
                    <asp:Label ID="LB_STATUS" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
