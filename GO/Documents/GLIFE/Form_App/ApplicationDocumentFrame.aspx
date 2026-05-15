<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationDocumentFrame.aspx.cs" Inherits="GLIFE.Form_App.ApplicationDocumentFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="appdocument" src="ApplicationDocument.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="appdocumentchild" src="ApplicationDocumentFrameChild.aspx?ID=<%=Request.QueryString["ID"]%>">
</frameset>
</html>
