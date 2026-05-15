<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GL_SUN_File.aspx.cs" Inherits="FINANCE.Form_Accounting.GL_SUN_File" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width:100px;">JOURNAL CODE</td>
                            <td>
                                <asp:DropDownList ID="DDL_CODE" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-
                                            <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                            <asp:Button ID="BT_EXPORT" runat="server" CssClass="ASPButton" Text="EXPORT FILE" OnClick="BT_EXPORT_Click" BackColor="Green" Font-Bold="True" ForeColor="White" />
                                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
