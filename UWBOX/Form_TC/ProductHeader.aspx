<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductHeader.aspx.cs" Inherits="UWBOX.Form_TC.ProductHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">PRODUCT CODE</td>
                                        <td>
                                            <asp:Label ID="LB_CODE" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DESCR" runat="server" Width="90%" MaxLength="100" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT GROUP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_GROUP" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CURRENCY</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CURRENCY" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>LINE OF BUSINESS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_LOB" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">START DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr id="TR_STATUS" runat="server">
                                        <td>STATUS</td>
                                        <td>
                                            <asp:Label ID="LB_STATUS" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" CssClass="ASPButton" runat="server" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                <asp:Button ID="BT_SAVE" CssClass="ASPButton" runat="server" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_BUTTONS" runat="server">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT1" runat="server" Text="TERM &amp; CONDITION" Width="100%" CssClass="ASPButton" OnClick="BT1_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT2" runat="server" Text="MEDICAL QUESTION" Width="100%" CssClass="ASPButton" OnClick="BT2_Click" Enabled="False" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT3" runat="server" Text="LOADING &amp; CHARGES" Width="100%" CssClass="ASPButton" OnClick="BT3_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT6" runat="server" Text="REINSURANCE" Width="100%" CssClass="ASPButton" OnClick="BT6_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT8" runat="server" Text="MIN MAX SUM ASSURED" Width="100%" CssClass="ASPButton" OnClick="BT8_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT7" runat="server" Text="OTHER SETUP" Width="100%" CssClass="ASPButton" OnClick="BT7_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT5" runat="server" Text="DISTRIBUTOR" Width="100%" CssClass="ASPButton" OnClick="BT5_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT4" runat="server" Text="ARCHIEVE" Width="100%" CssClass="ASPButton" OnClick="BT4_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>

