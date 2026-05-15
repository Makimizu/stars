<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationFrameSpecialRate.aspx.cs" Inherits="GLIFE.Form_App.ApplicationFrameSpecialRate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="160,*" frameborder="0" border="0" framespacing="0">  
    <frame name="appheader" src="ApplicationHeaderSpecialRate.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="appbody" src="" scrolling="auto">
</frameset>
</html>
