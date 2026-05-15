<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Mobile/MobileParent.Master" CodeBehind="MobileQuotation.aspx.cs" Inherits="LQR.Mobile.MobileQuotation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <iframe id="IF" runat="server" style="width: 100%; border: 0; height: 1000px;"></iframe>
    </form>
</asp:Content>
