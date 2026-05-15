<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_ENTRY.aspx.cs" Inherits="AGR.PARAMETER_MASTER_ENTRY" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <div class="row" style="width: 90%;">
            <div class="col-xl-6 col-sm-6 mb-3">
                <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
                <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                    <tr>
                        <td>DESCRIPTION</td>
                        <td>
                            <asp:TextBox ID="TXT_DESCRIPTION" runat="server" CssClass="ASPTextBox" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px;">START DATE</td>
                        <td>
                            <asp:TextBox ID="TXT_START_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px;">END DATE</td>
                        <td>
                            <asp:TextBox ID="TXT_END_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="80" OnClick="BT_SAVE_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
