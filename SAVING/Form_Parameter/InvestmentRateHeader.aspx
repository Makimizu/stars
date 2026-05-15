<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvestmentRateHeader.aspx.cs" Inherits="SAVING.Form_Parameter.InvestmentRateHeader" %>

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
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 150px;">PRODUCT GROUP</td>
                <td>
                    <asp:DropDownList ID="DDL_GROUP" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>CURRENCY</td>
                <td>
                    <asp:DropDownList ID="DDL_CURRENCY" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>RATE DATE</td>
                <td>
                    <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>RATE</td>
                <td>
                    <asp:TextBox ID="TXT_RATE" runat="server" CssClass="ASPTextBox" Width="60px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" Text="SUBMIT" CssClass="ASPButton" Width="100" OnClick="BT_SAVE_Click" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
