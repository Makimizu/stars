<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimApp.aspx.cs" Inherits="LQ.Form_Claim.ClaimApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
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
                            <td style="border-right-style: groove;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">CLAIM NO</td>
                                        <td>
                                            <asp:Label ID="LB_REGNO" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                            &nbsp;-&nbsp;
                                            <asp:DropDownList ID="DDL_SEQ" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_SEQ_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY NO</td>
                                        <td>
                                            <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="LB_READONLY" runat="server" Font-Bold="False" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT NAME</td>
                                        <td>
                                            <asp:Label ID="LB_PRODUCT" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="LB_TC_ID" runat="server" Font-Bold="False" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY HOLDER</td>
                                        <td>
                                            <asp:Label ID="LB_NAME" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>DATE OF BIRTH</td>
                                        <td>
                                            <asp:Label ID="LB_DOB" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>GENDER / START AGE</td>
                                        <td>
                                            <asp:Label ID="LB_GENDER" runat="server" Font-Bold="True"></asp:Label>&nbsp;-
                                            <asp:Label ID="LB_AGE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PERIOD</td>
                                        <td>
                                            <asp:Label ID="LB_PERIOD" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>SUM INSURED</td>
                                        <td>
                                            <asp:Label ID="LB_SUMINS" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>U/W CODE</td>
                                        <td>
                                            <asp:Label ID="LB_UWCODE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_PENDING" runat="server" CssClass="ASPButton" Text="PENDING" Width="100px" BackColor="Yellow" ForeColor="Red" OnClick="BT_PENDING_Click" Visible="False" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr id="TR_TYPE" runat="server" visible="false">
                                        <td>CLAIM TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList" Enabled="False"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CLAIM CAUSE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CAUSE" runat="server" CssClass="ASPDropDownList" Enabled="False"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">CLAIM DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CLAIMDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" Enabled="False"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CLAIMDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DOC RECEIVE DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RECEIVEDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" Enabled="False"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_RECEIVEDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>OCCURED DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_OCCUREDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" Enabled="False"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_OCCUREDDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>LOCATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_LOCATION" runat="server" CssClass="ASPDropDownList" Enabled="False"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="TR_MEMBERSHIP" runat="server" visible="false" style="vertical-align: top;">
                                        <td>LENGTH OF MEMBERSHIP</td>
                                        <td>
                                            <asp:Label ID="LB_LOM" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_BUTTONS" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="SUBMISSION" Width="100%" OnClick="BT1_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Text="SHARES & PAYMENT" Width="100%" OnClick="BT3_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="DOC. & ARCHIEVE" Width="100%" OnClick="BT2_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT4" runat="server" CssClass="ASPButton" Text="U/W INFO" Width="100%" OnClick="BT4_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT7" runat="server" CssClass="ASPButton" Text="LOADING & CHARGES" Width="100%" OnClick="BT7_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT_6" runat="server" CssClass="ASPButton" Text="TRANSACTION HISTORY" Width="100%" OnClick="BT_6_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT5" runat="server" CssClass="ASPButton" Text="REMARK & NOTIFICATION" Width="100%" OnClick="BT5_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

