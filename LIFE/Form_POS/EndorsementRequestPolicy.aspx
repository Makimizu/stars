<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementRequestPolicy.aspx.cs" Inherits="LIFE.Form_POS.EndorsementRequestPolicy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Class/Global.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 100px;">POLICY TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_TYPE_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>CHARGE</td>
                            <td>
                                <asp:TextBox ID="TXT_CHARGE" runat="server" Width="100" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>SEND TO EMAIL</td>
                            <td>
                                <asp:TextBox ID="TXT_EMAIL" runat="server" Width="90%" CssClass="ASPTextBox" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>SEND TO ADDRESS</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS" TextMode="MultiLine" Height="40" runat="server" Width="90%" CssClass="ASPTextBox" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PROVINCE</td>
                            <td>
                                <asp:DropDownList ID="DDL_PROVINCE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ZIPCODE</td>
                            <td>
                                <asp:TextBox ID="TXT_ZIPCODE" runat="server" Width="100" CssClass="ASPTextBox" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>REMARK</td>
                            <td>
                                <asp:TextBox ID="TXT_REMARK" TextMode="MultiLine" Height="40" runat="server" Width="90%" CssClass="ASPTextBox" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="100" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
