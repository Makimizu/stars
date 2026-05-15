<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Policy_Header.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Health.Policy_Header" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        
        .auto-style2 {
            FONT-FAMILY: Tahoma;
            FONT-SIZE: xx-small;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%; position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table border="1" style="border-width: thin; border-spacing: 1px; border-color: silver;">
                                    <tr>
                                        <td>PERIODE</td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="DDL_POLICY_PERIOD" AutoPostBack="true" OnSelectedIndexChanged="DDL_POLICY_PERIOD_SelectedIndexChanged" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">PERUSAHAAN</td>
                                        <td>
                                            <asp:Label ID="LB_COMPANYNAME" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NO POLIS</td>
                                        <td>
                                            <asp:Label ID="LB_POLICYNO" runat="server" CssClass="auto-style2"></asp:Label>
                                            <asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>REG FORM</td>
                                        <td>
                                            <asp:Label ID="LB_REGFORMNO" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>TGL PROSES</td>
                                        <td>
                                            <asp:Label ID="LB_PROCESSDATE" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table border="1" style="border-width: thin; border-spacing: 1px; border-color: silver;">
                                    <tr>
                                        <td style="width: 100px;">TIPE</td>
                                        <td>
                                            <asp:Label ID="LB_POLICY_TYPE" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PRODUK</td>
                                        <td>
                                            <asp:Label ID="LB_PRODUCT" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CARA BAYAR</td>
                                        <td>
                                            <asp:Label ID="LB_MOP" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TPA</td>
                                        <td>
                                            <asp:Label ID="LB_TPA" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table border="1" style="border-width: thin; border-spacing: 1px; border-color: silver;">
                                    <tr>
                                        <td style="width: 100px;">KODE AGEN</td>
                                        <td>
                                            <asp:Label ID="LB_AGENTCODE" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">NAMA AGEN</td>
                                        <td>
                                            <asp:Label ID="LB_AGENT" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CHANNEL</td>
                                        <td>
                                            <asp:Label ID="LB_CHANNEL" runat="server" CssClass="auto-style2"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TIPE KOMISI</td>
                                        <td>
                                            <asp:Label ID="LB_KOMISI" runat="server" CssClass="auto-style2"></asp:Label>
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
                    <table style="border-spacing: 0px; width: 100%">
                        <tr>
                            <td style="width: 10%;">
                                <asp:Button ID="BTN_TC" runat="server" Font-Bold="True" Text="T &amp; C" Width="100%" CssClass="ASPButton" OnClick="BTN_TC_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BTN_BENEFIT_DETAIL" runat="server" Font-Bold="True" Text="BENEFIT" Width="100%" CssClass="ASPButton" OnClick="BTN_BENEFIT_DETAIL_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BTN_PREMI" runat="server" Font-Bold="True" Text="PREMI" Width="100%" CssClass="ASPButton" OnClick="BTN_PREMI_Click" />
                            </td>                            
                            <td style="width: 10%;">
                                <asp:Button ID="BTN_CLAIM" runat="server" Font-Bold="True" Text="CLAIM" Width="100%" CssClass="ASPButton" OnClick="BTN_CLAIM_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BTN_FINANCE" runat="server" Font-Bold="True" Text="FINANCE" Width="100%" CssClass="ASPButton" OnClick="BTN_FINANCE_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BT_PRINT" runat="server" Font-Bold="True" Text="PRINT POLIS" Width="100%" CssClass="ASPButton" OnClick="BT_PRINT_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BT_UR" runat="server" Font-Bold="True" Text="UTIL REPORT" Width="100%" CssClass="ASPButton" OnClick="BT_UR_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BTN_ARSIP" runat="server" Font-Bold="True" Text="ARSIP" Width="100%" CssClass="ASPButton" OnClick="BTN_ARSIP_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">
                    <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

