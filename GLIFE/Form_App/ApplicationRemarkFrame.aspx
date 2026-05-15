<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationRemarkFrame.aspx.cs" Inherits="GLIFE.Form_App.ApplicationRemarkFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="remark" src="ApplicationRemark.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
    <frame name="track" src="../Form_Tools/Track.aspx?tipe=UW&owner=<%=Request.QueryString["ID"]%>" scrolling="auto">
</frameset>
</html>
