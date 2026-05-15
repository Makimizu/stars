<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainContent.aspx.cs" Inherits="LIFE.Standard.MainContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="30,*" frameborder="0" border="0" framespacing="0">
    <frame name="headercontent" src="title.aspx?URL=<%=Request.QueryString["URL"]%>" marginheight="0" marginwidth="0" scrolling="no" noresize>
	<frame name="content" src="default.html" marginheight="0" marginwidth="0" scrolling="auto" noresize>
</frameset>
</html>