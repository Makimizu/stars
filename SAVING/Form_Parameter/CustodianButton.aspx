<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustodianButton.aspx.cs" Inherits="SAVING.Form_Parameter.CustodianButton" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 25%;">
                    <asp:Button ID="BT1" runat="server" Width="100%" Text="PRODUCT" OnClick="BT1_Click" Font-Size="X-Small" />
                </td>
                <td style="width: 25%;">
                    <asp:Button ID="BT2" runat="server" Width="100%" Text="ATTRIBUTE" OnClick="BT2_Click" Font-Size="X-Small" />
                </td>
                <td style="width: 25%;">
                    <asp:Button ID="BT3" runat="server" Width="100%" Text="BANK ACCOUNT" OnClick="BT3_Click" Font-Size="X-Small" />
                </td>
                <td style="width: 25%;">
                    <asp:Button ID="BT4" runat="server" Width="100%" Text="EXPORT REPORT" Font-Size="X-Small" OnClick="BT4_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
