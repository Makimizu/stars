<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicySaving.aspx.cs" Inherits="GLIFE.Form_Policy.PolicySaving" %>

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
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr id="TR_ID" runat="server">
                            <td>ID</td>
                            <td>
                                <asp:Label ID="LB_ID" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">POLICY NO</td>
                            <td class="auto-style2">
                                <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>COMPANY</td>
                            <td>
                                <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>PRODUCT</td>
                            <td>
                                <asp:Label ID="LB_PRODUCT" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>BALANCE</td>
                            <td>
                                <asp:Label ID="LB_BALANCE" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr id="TR_STAT" runat="server">
                            <td>STATUS</td>
                            <td>
                                <asp:Label ID="LB_STAT" runat="server" Font-Bold="True"></asp:Label>
                                &nbsp;
                                <asp:Label ID="LB_REPORT" runat="server" Visible="False" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                        <tr id="TR_SAVE" runat="server">
                            <td></td>
                            <td>
                                <asp:Button ID="BT_FREELOOK" runat="server" CssClass="ASPButton" Text="REQUEST FREELOOK" Width="140px" OnClick="BT_FREELOOK_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
