<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimBatchHeader.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimBatchHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width:100%; border-bottom:ridge;">
            <tr>
                <td>
                    <table style="border-spacing:0px;">
                        <tr">
                            <td style="width:100px;">VIEW MODE</td>
                            <td>
                                <asp:DropDownList ID="DDL_MODE" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_MODE_SelectedIndexChanged">
                                    <asp:ListItem Value="ClaimBatchInquiry.aspx">BATCH INQUIRY</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
