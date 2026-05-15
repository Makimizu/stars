<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimOrgBeneficiary.aspx.cs" Inherits="LQ.Form_Claim.ClaimOrgBeneficiary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_ORGTYPE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">ORGANIZATION BENEFICIARY</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">ORGANIZATION</td>
                            <td>
                                <asp:TextBox ID="TXT_ORG" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CERTIFICATE NO</td>
                            <td>
                                <asp:TextBox ID="TXT_CERNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK ACC NO</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK ACC NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK</td>
                            <td>
                                <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList" Enabled="False"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>% RISK BENEFIT</td>
                            <td>
                                <asp:DropDownList ID="DDL_PCTCLM" runat="server" CssClass="ASPDropDownList" Enabled="False"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>% INVESTMENT BENEFIT</td>
                            <td>
                                <asp:DropDownList ID="DDL_PCTINV" runat="server" CssClass="ASPDropDownList" Enabled="False"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

