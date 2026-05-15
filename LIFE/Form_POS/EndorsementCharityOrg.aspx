<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementCharityOrg.aspx.cs" Inherits="LIFE.Form_POS.EndorsementCharityOrg" %>

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
                <td class="TDBGColor">BADAN<br />
                    PENERIMA ZAKAT</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr>
                            <td>ORGANIZATION</td>
                            <td>
                                <asp:TextBox ID="TXT_ORG" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">CERTIFICATE NO</td>
                            <td>
                                <asp:TextBox ID="TXT_CERNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK ACC NO</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK ACC NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK</td>
                            <td>
                                <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList" Width="90%"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BT_ORG" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_ORG_Click" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
