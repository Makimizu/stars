<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationDocumentFrameChild.aspx.cs" Inherits="LIFE.Form_App.ApplicationDocumentFrameChild" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="appdocumentletter" src="ApplicationDocumentLetter.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
    <frame name="appdocumentarchieve"  src="">
</frameset>
</html>
