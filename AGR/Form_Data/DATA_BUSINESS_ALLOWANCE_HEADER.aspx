<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DATA_BUSINESS_ALLOWANCE_HEADER.aspx.cs" Inherits="AGR.Form_Data.DATA_BUSINESS_ALLOWANCE_HEADER" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 33%;">
                                <asp:Button ID="BT3" runat="server" Text="PROCESS" CssClass="ASPButton" Width="100%" OnClick="BT3_Click" /></td>
                            <td style="width: 33%;">
                                <asp:Button ID="BT1" runat="server" Text="DETAIL" CssClass="ASPButton" Width="100%" OnClick="BT1_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

