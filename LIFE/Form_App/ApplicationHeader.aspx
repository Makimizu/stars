<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationHeader.aspx.cs" Inherits="LIFE.Form_App.ApplicationHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style>
        .alert {
            display: block;
            border: 3px solid red;
            padding: 10px;
            animation: blinker 5s linear infinite;
            color: red;
        }

        @keyframes blinker {
            25% {
                opacity: 0.5;
            }
            50% {
                opacity: 0;
            }
            75% {
                opacity: 0.5;
            }
        }
    </style>
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
                            <td>
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 160px;">APPLICATION NO</td>
                                        <td>
                                            <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="true"></asp:Label>&nbsp;-&nbsp;
                                            <asp:Label ID="LB_REGNO" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td style="width: 100px;">SUM INSURED</td>
                                        <td>
                                            <asp:Label ID="LB_SUMINS" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>MAIN INSURED</td>
                                        <td>
                                            <asp:Label ID="LB_FULLNAME" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td>BASIC PREMIUM</td>
                                        <td>
                                            <asp:Label ID="LB_PREMIUM" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>VIRTUAL ACC.</td>
                                        <td>
                                            <asp:Label ID="LB_VACC" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td>TOPUP REGULER</td>
                                        <td>
                                            <asp:Label ID="LB_TOPUP_REGULER" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>DOB</td>
                                        <td>
                                            <asp:Label ID="LB_DOB" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td>TOPUP IRREGULER</td>
                                        <td>
                                            <asp:Label ID="LB_TOPUP_IRREGULER" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT GROUP</td>
                                        <td>
                                            <asp:Label ID="LB_PRODUCTGROUP" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td>START AGE</td>
                                        <td>
                                            <asp:Label ID="LB_AGE" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT NAME</td>
                                        <td>
                                            <asp:Label ID="LB_PRODUCTNAME" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td>INSUR. PERIOD</td>
                                        <td>
                                            <asp:Label ID="LB_INSPERIOD" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>FREQUENCY OF PAYMENT</td>
                                        <td>
                                            <asp:Label ID="LB_FOP" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td>PAYMENT PERIOD</td>
                                        <td>
                                            <asp:Label ID="LB_PAYMENTPERIOD" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>U/W CODE</td>
                                        <td>
                                            <asp:Label ID="LB_UWCODE" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 200px;">
                                <div style="width: 100%; height: 100px; overflow: auto;">
                                    <asp:DataGrid ID="DGR_TRACK" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" Width="98%" ShowHeader="False" OnItemCommand="DGR_TRACK_ItemCommand">
                                        <HeaderStyle VerticalAlign="Top" />
                                        <ItemStyle VerticalAlign="Top" Font-Size="XX-Small" />
                                        <Columns>
                                            <asp:BoundColumn DataField="TRACK" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DONE" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_TRACK" runat="server" CssClass="ASPButton" CommandName="Track" Width="100%" />
                                                    <asp:TextBox ID="TXT_TRACK" runat="server" CssClass="ASPTextBox" Width="98%" Height="30" TextMode="MultiLine" MaxLength="100" placeholder="Reason .."></asp:TextBox>
                                                    <asp:Label ID="LB_TRACK" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="LB_WARNING" runat="server"></asp:Label></td>
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
                                <asp:Button ID="BT5" runat="server" Text="LETTERS &amp; ARCHIEVE" CssClass="ASPButton" Width="96%" OnClick="BT5_Click" />
                            </td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT6" runat="server" Text="REMARKS & TRACKS" CssClass="ASPButton" Width="65%" OnClick="BT6_Click" />
                                <asp:Button ID="BT8" runat="server" Text="HISTORY" CssClass="ASPButton" Width="32%" OnClick="BT8_Click"/>
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
