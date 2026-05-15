<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationHeader.aspx.cs" Inherits="LQ.Form_App.ApplicationHeader" %>

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
        <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td style="width: 33%;">
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 160px;">APPLICATION NO</td>
                                        <td>
                                            <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="true"></asp:Label>&nbsp;-&nbsp;
                                            <asp:Label ID="LB_REGNO" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>MAIN INSURED</td>
                                        <td>
                                            <asp:Label ID="LB_FULLNAME" runat="server" Font-Bold="true"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>VIRTUAL ACC.</td>
                                        <td>
                                            <asp:Label ID="LB_VACC" runat="server" Font-Bold="true"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>DOB</td>
                                        <td>
                                            <asp:Label ID="LB_DOB" runat="server" Font-Bold="true"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>PRODUCT GROUP</td>
                                        <td>
                                            <asp:Label ID="LB_PRODUCTGROUP" runat="server" Font-Bold="true"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>PRODUCT NAME</td>
                                        <td>
                                            <asp:Label ID="LB_PRODUCTNAME" runat="server" Font-Bold="true"></asp:Label></td>

                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%;">
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 150px;">SUM INSURED</td>
                                        <td>
                                            <asp:Label ID="LB_SUMINS" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>BASIC PREMIUM</td>
                                        <td>
                                            <asp:Label ID="LB_PREMIUM" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>TOPUP REGULER</td>
                                        <td>
                                            <asp:Label ID="LB_TOPUP_REGULER" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>TOPUP IRREGULER</td>
                                        <td>
                                            <asp:Label ID="LB_TOPUP_IRREGULER" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>START AGE</td>
                                        <td>
                                            <asp:Label ID="LB_AGE" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%;">
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 150px;">FREQ OF PAYMENT</td>
                                        <td>
                                            <asp:Label ID="LB_FOP" runat="server" Font-Bold="true"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>INSUR. PERIOD</td>
                                        <td>
                                            <asp:Label ID="LB_INSPERIOD" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>PAYMENT PERIOD</td>
                                        <td>
                                            <asp:Label ID="LB_PAYMENTPERIOD" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>U/W CODE</td>
                                        <td>
                                            <asp:Label ID="LB_UWCODE" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>POLICY STATUS</td>
                                        <td>
                                            <asp:Label ID="LB_STAT" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px;">
                        <tr>
                            <td style="width: 14%;">
                                <asp:Button ID="BT1" runat="server" Text="POLICY INFO" CssClass="ASPButton" Width="100%" OnClick="BT1_Click" />
                            </td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT4" runat="server" Text="UNDERWRITING" CssClass="ASPButton" Width="100%" OnClick="BT4_Click" />
                            </td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT7" runat="server" Text="TRANSACTION HISTORY" CssClass="ASPButton" Width="100%" OnClick="BT7_Click" />
                            </td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT2" runat="server" Text="MEMBER & AGENT" CssClass="ASPButton" Width="100%" OnClick="BT2_Click" />
                            </td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT3" runat="server" Text="LOADING &amp; CHARGES" CssClass="ASPButton" Width="100%" OnClick="BT3_Click" />
                            </td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT5" runat="server" Text="LETTERS &amp; ARCHIEVE" CssClass="ASPButton" Width="100%" OnClick="BT5_Click" />
                            </td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT6" runat="server" Text="REMARKS & TRACKS" CssClass="ASPButton" Width="100%" OnClick="BT6_Click" />
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
    </form>
</body>
</html>
