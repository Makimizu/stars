<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="LIFE.Form_Tools.ReportView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_APPID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_REPORTCODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_PARAM" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_VALUE" runat="server" Visible="false"></asp:Label>

        <div style="height: auto; width: 100%; overflow: auto;">
            <rsweb:ReportViewer ID="RV1" runat="server" Width="100%" Height="1000px" ZoomMode="Percent" ShowParameterPrompts="false">
            </rsweb:ReportViewer>100%
        </div>
    </form>
</body>
</html>
