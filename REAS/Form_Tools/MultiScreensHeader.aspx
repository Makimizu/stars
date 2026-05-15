<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiScreensHeader.aspx.cs" Inherits="REAS.Form_Tools.MultiScreensHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>
    <script src="/Scripts/WebForms/Sys.js" type="text/javascript"></script>
    <script src="/Scripts/WebForms/WebForms.js" type="text/javascript"></script>
    <script src="/Scripts/WebForms/WebUIValidation.js" type="text/javascript"></script>
    <script src="/Scripts/WebForms/WebFormsBundle.js" type="text/javascript"></script>

     <script type="text/javascript">
         function showLoading() {
             document.getElementById("loading").style.display = "block";
         }

         function hideLoading() {
             document.getElementById("loading").style.display = "none";
         }

         // Show loading indicator on Ajax start
         Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
             showLoading();
         });

         // Hide loading indicator on Ajax end
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
             hideLoading();
         });

         // Hide loading indicator when the window is fully loaded
         window.onload = function () {
             hideLoading();
         };
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <!-- Loading indicator -->
        <div id="loading" style="display:none; position:absolute; top:50%; left:50%; transform:translate(-50%, -50%); background-color: rgba(255, 255, 255, 0.8); padding: 20px; border: 1px solid #ccc; z-index: 1000;">
            <img src="../images/loading.gif" alt="Loading..." />
            <p>Loading data, please wait...</p>
        </div>

       <table style="top: 0px; left: 0px; position: absolute; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:Label ID="LB_CAPTION" runat="server" Font-Bold="True">REPORTS : </asp:Label>
                    &nbsp;<asp:DropDownList ID="DDL_SCREENS" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_SCREENS_SelectedIndexChanged1" >
                    </asp:DropDownList>
                    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr style="margin-top:2px">
                <td class="TDBGColor">
                    <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="XX-Small"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
   
</html>
