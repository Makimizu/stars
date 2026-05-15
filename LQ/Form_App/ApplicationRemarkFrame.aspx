<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationRemarkFrame.aspx.cs" Inherits="LQ.Form_App.ApplicationRemarkFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="remark" src="ApplicationRemark.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
    <frame name="track" src="../Form_Tools/Track.aspx?TRACK_TYPE=UW&REGNO=<%=Request.QueryString["ID"]%>&PARAM_VALUE=" scrolling="auto">
</frameset>
</html>
