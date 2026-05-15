<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyFREEKLOOKHeader.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyFREEKLOOKHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 120px;">ID</td>
                            <td>
                                <asp:Label ID="LB_ID" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>POLICY NO</td>
                            <td>
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
                                <asp:Button ID="BT_APPROVAL" runat="server" CssClass="ASPButton" Text="APPROVE" Width="140px" BackColor="Green" ForeColor="White" OnClick="BT_APPROVAL_Click" />
                                &nbsp;
                                <asp:Button ID="BT_REJECT" runat="server" CssClass="ASPButton" Text="REJECT" Width="140px" BackColor="Red" ForeColor="White" OnClick="BT_REJECT_Click" />
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>REASON</td>
                            <td>
                                <asp:TextBox ID="TXT_REMARK" runat="server" Height="60px" MaxLength="255" TextMode="MultiLine" Width="90%"></asp:TextBox>
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
