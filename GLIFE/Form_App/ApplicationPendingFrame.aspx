<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPendingFrame.aspx.cs" Inherits="GLIFE.Form_App.ApplicationPendingFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="apppendingheader" src="ApplicationPending.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="appapppendingbody" src="../Standard/default.html" scrolling="auto">
</frameset>
</html>
