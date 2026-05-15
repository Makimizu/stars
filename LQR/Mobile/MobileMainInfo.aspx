<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/MobileParent.Master" AutoEventWireup="true" CodeBehind="MobileMainInfo.aspx.cs" Inherits="LQR.Mobile.MobileDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="border-spacing: 0px; width: 100%; font-size: 9pt; font-family: Arial;">
            <tr>
                <td style="width: 160px; color: gray; color: gray;">REGNO</td>
                <td style="color: purple;">
                    <asp:Label ID="LB_REGNO" runat="server"></asp:Label></td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="width: 160px; color: gray;">NAMA LENGKAP</td>
                <td style="color: purple;">
                    <asp:Label ID="LB_FULLNAME" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 160px; color: gray;">TANGGAL LAHIR/GENDER</td>
                <td style="color: purple;">
                    <asp:Label ID="LB_DOB" runat="server"></asp:Label></td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="width: 160px; color: gray;">NAMA PRODUK</td>
                <td style="color: purple;">
                    <asp:Label ID="LB_PRODUCTNAME" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="width: 160px; color: gray;">KELOMPOK PRODUK</td>
                <td style="color: purple;">
                    <asp:Label ID="LB_PRODUCTGROUP" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 160px; color: gray;">PERIODE POLIS</td>
                <td style="color: blue;">
                    <asp:Label ID="LB_POLICY_PERIOD" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 160px; color: gray;">PERIODE PEMBAYARAN</td>
                <td style="color: blue;">
                    <asp:Label ID="LB_PAYMENT_PERIOD" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 160px; color: gray;">USIA MASUK</td>
                <td style="color: blue;">
                    <asp:Label ID="LB_STARTAGE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 160px; color: gray;">UANG PERTANGGUNGAN</td>
                <td style="color: blue;">
                    <asp:Label ID="LB_SUMINS" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 160px; color: gray;">KODE MEDIS</td>
                <td style="color: blue;">
                    <asp:Label ID="LB_UWCODE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 100px; color: gray;">KODE AGEN</td>
                <td style="color: green;">
                    <asp:Label ID="LB_AGENT_CODE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="color: gray;">NAMA AGEN</td>
                <td style="color: green;">
                    <asp:Label ID="LB_AGENT_NAME" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="color: gray;">CHANNEL</td>
                <td style="color: green;">
                    <asp:Label ID="LB_CHANNEL" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="color: gray;">LEVEL AGEN</td>
                <td style="color: green;">
                    <asp:Label ID="LB_LEVEL" runat="server"></asp:Label></td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="color: gray;">AGENCY</td>
                <td style="color: green;">
                    <asp:Label ID="LB_AGENCY" runat="server"></asp:Label></td>
            </tr>
        </table>
    </form>


</asp:Content>
