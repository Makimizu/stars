<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiFrame.aspx.cs" Inherits="LIFE.Form_Tools.MultiFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="200,*" frameborder="0" border="0" framespacing="0">  
    <frame name="multiheader" src="MultiHeader.aspx?mode=<%=Request.QueryString["mode"]%>" scrolling="no">
    <frame name="multibody" src="" scrolling="auto">
</frameset>
</html>
