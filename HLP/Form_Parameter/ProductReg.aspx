<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductReg.aspx.cs" Inherits="HLP.Form_Parameter.ProductReg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 100px;">PRODUCT CODE</td>
                <td>
                    <asp:TextBox ID="TXT_CODE" runat="server" Font-Bold="True" CssClass="ASPTextBox" MaxLength="10" Width="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>PRODUCT NAME</td>
                <td>
                    <asp:TextBox ID="TXT_PRODUCT" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>START DATE</td>
                <td>
                    <asp:TextBox ID="TXT_TGLSTART" runat="server" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TGLSTART">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" Width="100" CssClass="ASPButton" OnClick="BT_SAVE_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
