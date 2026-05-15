<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationHeader.aspx.cs" Inherits="LQ.Form_Client.QuotationHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
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
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td style="width: 35%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="text-wrap: none; width: 180px; color: gray; color: gray;">REGNO</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_REGNO" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">PESERTA UTAMA</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_FULLNAME" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">PEMEGANG POLIS</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_POLICY_HOLDER" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">TGL LAHIR/JENIS KELAMIN</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_DOB" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">NAMA PRODUK</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_PRODUCTNAME" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">GRUP PRODUK</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_PRODUCTGROUP" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">INPUTER</td>
                            <td style="color: green;">
                                <asp:Label ID="LB_INPUTER" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="text-wrap: none; width: 150px; color: gray;">PERIODE POLIS</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_POLICY_PERIOD" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 100px; color: gray;">PERIODE PEMBAYARAN</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_PAYMENT_PERIOD" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 100px; color: gray;">USIA MULAI</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_STARTAGE" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 100px; color: gray;">UANG PERTANGGUNGAN</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_SUMINS" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 100px; color: gray;">KODE U/W</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_UWCODE" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 15%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label ID="LB_TRACK" runat="server" Visible="false"></asp:Label>
                                <asp:Button ID="BT_NEXT" runat="server" Text="" CssClass="ASPButton" Width="200" Height="40" OnClick="BT_NEXT_Click" Font-Size="9" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <asp:Label ID="LB_WARNING" runat="server"></asp:Label>

        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td style="border-top: ridge;">
                    <table style="border-spacing: 0px; width: 100%; font-size: ">
                        <tr>
                            <td style="width: 14%;">
                                <asp:Button ID="BT2" runat="server" Text="INFO POLIS" CssClass="ASPButton" Width="100%" BackColor="#3399ff" Font-Size="8pt" ForeColor="White" OnClick="BT2_Click" /></td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT1" runat="server" Text="DATA NASABAH" CssClass="ASPButton" Width="100%" BackColor="#3399ff" Font-Size="8pt" ForeColor="White" OnClick="BT1_Click" /></td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT4" runat="server" Text="PERTANYAAN NASABAH" CssClass="ASPButton" Width="100%" BackColor="#3399ff" Font-Size="8pt" ForeColor="White" OnClick="BT4_Click" /></td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT7" runat="server" Text="INFO AGEN" CssClass="ASPButton" Width="100%" BackColor="#3399ff" Font-Size="8pt" ForeColor="White" OnClick="BT7_Click" /></td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT3" runat="server" Text="DOKUMEN & ARSIP" CssClass="ASPButton" Width="100%" BackColor="#3399ff" Font-Size="8pt" ForeColor="White" OnClick="BT3_Click" /></td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT5" runat="server" Text="CATATAN" CssClass="ASPButton" Width="100%" BackColor="#3399ff" Font-Size="8pt" ForeColor="White" OnClick="BT5_Click" /></td>
                            <td style="width: 14%;">
                                <asp:Button ID="BT6" runat="server" Text="CETAK" CssClass="ASPButton" Width="100%" BackColor="#3399ff" Font-Size="8pt" ForeColor="White" OnClick="BT6_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
        </table>

    </form>
</body>
</html>
