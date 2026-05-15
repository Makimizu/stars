<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArchieveFrame.aspx.cs" Inherits="Archieve.ArchieveFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="250,*" frameborder="0" border="0" framespacing="0">  
    <frame name="archieveheader" src="ArchieveHeader.aspx?APPID=<%=Request.QueryString["APPID"]%>" scrolling="no">
    <frame name="archievebody" src="" scrolling="auto">
</frameset>
</html>