<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="HLP.Standard.Main" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="70,*" frameborder="0" border="0" framespacing="0">  
    <frame name="header" src="header.aspx" scrolling="no">
    <frameset cols="200,*" frameborder="0" border="0" framespacing="0">
	    <frame name="menu" src="bodymenu.aspx?URL=" marginheight="0" marginwidth="0" scrolling="yes" noresize>
	    <frameset rows="30,*" frameborder="0" border="0" framespacing="0">
	        <frame name="headercontent" src="title.aspx" marginheight="0" marginwidth="0" scrolling="no" noresize>
	        <frame name="content" src="default.html" marginheight="0" marginwidth="0" scrolling="auto" noresize>
        </frameset>
    </frameset>
</frameset>
</html>
