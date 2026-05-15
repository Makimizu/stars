<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationFrame.aspx.cs" Inherits="GLIFE.Form_App.ApplicationFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="220,*" frameborder="0" border="0" framespacing="0">  
    <frame name="appheader" src="ApplicationHeader.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="appbody" src="" scrolling="auto">
</frameset>
</html>
