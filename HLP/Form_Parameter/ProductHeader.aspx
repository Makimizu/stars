<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductHeader.aspx.cs" Inherits="HLP.Form_Parameter.ProductHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />

    <style type="text/css">
        .auto-style1 {
            FONT-FAMILY: verdana;
            FONT-SIZE: x-small;
            MARGIN-TOP: 0px;
            MARGIN-LEFT: 0px;
            MARGIN-RIGHT: 0px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;" class="auto-style1">KODE</td>
                            <td>
                                <asp:Label ID="LB_CODE" runat="server" Font-Bold="True" CssClass="ASPLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">NAMA PRODUK</td>
                            <td>
                                <asp:TextBox ID="TXT_PRODUCT" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">TGL MULAI</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_TGLSTART" runat="server" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TGLSTART">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">TGL AKHIR</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_TGLEND" runat="server" CssClass="ASPTextBox" Width="80px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TGLEND">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 10%;">
                                <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="PLAN" Width="100%" OnClick="BT1_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="BENEFIT" Width="100%" OnClick="BT2_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Text="RATE" Width="100%" OnClick="BT3_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BT4" runat="server" CssClass="ASPButton" Text="UP" Width="100%" OnClick="BT4_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BT5" runat="server" CssClass="ASPButton" Text="T & C" Width="100%" OnClick="BT5_Click" />
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BT6" runat="server" CssClass="ASPButton" Text="FACTOR" Width="100%" OnClick="BT6_Click"/>
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BT_7" runat="server" CssClass="ASPButton" Text="LOADING" Width="100%" OnClick="BT_7_Click"/>
                            </td>
                            <td style="width: 10%;">
                                <asp:Button ID="BT_8" runat="server" CssClass="ASPButton" Text="REINSURANCE" Width="100%" OnClick="BT_8_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
