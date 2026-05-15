<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Endorsement_Header.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Endorsement_Header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">NO POLIS</td>
                                        <td>
                                            <asp:Label ID="LB_POLICY" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="LB_ID" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">PERUSAHAAN</td>
                                        <td class="auto-style1">
                                            <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PERIOD</td>
                                        <td>
                                            <asp:Label ID="LB_PERIOD" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 60px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width:100px;">SEQUENCE</td>
                                        <td>
                                            <asp:Label ID="LB_SEQ" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>USER REG</td>
                                        <td>
                                            <asp:Label ID="LB_USERREG" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DATE REG</td>
                                        <td>
                                            <asp:Label ID="LB_DATEREG" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 25%;">
                                <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Font-Bold="True" Text="PAKET &amp; BENEFIT" Width="100%" OnClick="BT1_Click" />
                            </td>                            
                            <td style="width: 25%;">
                                <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Font-Bold="True" Text="PREMIUM" Width="100%" OnClick="BT3_Click" />
                            </td>
                            <td style="width: 25%;">
                                <asp:Button ID="BT4" runat="server" CssClass="ASPButton" Font-Bold="True" Text="BENEFIT DETAIL" Width="100%" OnClick="BT4_Click" />
                            </td>
                            <td style="width: 25%;">
                                <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Font-Bold="True" Text="ARSIP" Width="100%" OnClick="BT2_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
            </tr>
        </table>
    </form>
</body>
</html>
