<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationHeader.aspx.cs" Inherits="LQR.ApplicationHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 200px; border-bottom-style: ridge;">
                    <asp:Image ID="IMG_LOGO" runat="server" Height="40" />
                </td>
                <td style="border-bottom-style: ridge;">
                    <asp:Label ID="LB_TITLE" runat="server" Font-Size="16pt" Font-Names="Arial Black"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 33%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray; color: gray;">REGNO</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_REGNO" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray;">NAMA LENGKAP</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_FULLNAME" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray;">TANGGAL LAHIR/GENDER</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_DOB" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray;">NAMA PRODUK</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_PRODUCTNAME" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray;">KELOMPOK PRODUK</td>
                            <td style="color: purple;">
                                <asp:Label ID="LB_PRODUCTGROUP" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 33%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray;">PERIODE POLIS</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_POLICY_PERIOD" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray;">PERIODE PEMBAYARAN</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_PAYMENT_PERIOD" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray;">USIA MASUK</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_STARTAGE" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray;">UANG PERTANGGUNGAN</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_SUMINS" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; width: 190px; color: gray;">KODE MEDIS</td>
                            <td style="color: blue;">
                                <asp:Label ID="LB_UWCODE" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 33%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="text-wrap: none; width: 100px; color: gray;">KODE AGEN</td>
                            <td style="color: green;">
                                <asp:Label ID="LB_AGENT_CODE" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">NAMA AGEN</td>
                            <td style="color: green;">
                                <asp:Label ID="LB_AGENT_NAME" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">CHANNEL</td>
                            <td style="color: green;">
                                <asp:Label ID="LB_CHANNEL" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">LEVEL AGEN</td>
                            <td style="color: green;">
                                <asp:Label ID="LB_LEVEL" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-wrap: none; color: gray;">AGENCY</td>
                            <td style="color: green;">
                                <asp:Label ID="LB_AGENCY" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
