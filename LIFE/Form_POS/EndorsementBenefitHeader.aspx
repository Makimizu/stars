<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementBenefitHeader.aspx.cs" Inherits="LIFE.Form_POS.EndorsementBenefitHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%">
                                <asp:Button ID="BT_MEMBER" runat="server" CssClass="ASPButton" Text="MEMBER LIST" Width="100%" OnClick="BT_MEMBER_Click" /></td>
                            <td style="width: 50%">
                                <asp:Button ID="BT_BENEFIT" runat="server" CssClass="ASPButton" Text="BENEFIT LIST" Width="100%" OnClick="BT_BENEFIT_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_SUBTITLE" runat="server"></asp:Label></td>
            </tr>
        </table>
    </form>
</body>
</html>
