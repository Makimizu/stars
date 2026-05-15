<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyFREELOOKFrame.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyFREELOOKFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="40%,*" frameborder="0" border="0" framespacing="0">  
    <frameset rows="200px,*" frameborder="0" border="0" framespacing="0">  
        <frame name="freelookheader" src="PolicyFREEKLOOKHeader.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
        <frame name="freelookbody" src="" scrolling="auto">
    </frameset>
    <frame name="freelookreport" src="" scrolling="auto">
</frameset>
</html>
