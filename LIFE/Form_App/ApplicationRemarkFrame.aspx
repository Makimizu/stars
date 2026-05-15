<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationRemarkFrame.aspx.cs" Inherits="LIFE.Form_App.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<%--<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>        
    <div>    
    </div>
        <table style="width: 100%; border-spacing: 0px; ">
        //<table style="border-spacing: 0px; width: 100%;">
            <tr>
                 <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">UNDERWRITING REMARKS
                 </td>
            </tr>
            <tr id="TR_INSERT" runat="server">
                <td>
                    <asp:TextBox ID="TXT_REMARK" runat="server" Width="60%" TextMode="MultiLine" Height="80px" Font-Size="Small" placeholder="Type your remark here ..." CssClass="ASPTextBox"></asp:TextBox>
                    
                </td>
            </tr>
   </table>
    </form>
</body>--%>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="remark" src="ApplicationRemark.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
    <frame name="track" src="../Form_Tools/Track.aspx?TRACK_TYPE=UW&REGNO=<%=Request.QueryString["ID"]%>&PARAM_VALUE=" scrolling="auto">
</frameset>
</html>
