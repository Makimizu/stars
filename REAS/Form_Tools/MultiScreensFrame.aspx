<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiScreensFrame.aspx.cs" Inherits="REAS.Form_Tools.MultiScreensFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="50,*" frameborder="0" border="0" framespacing="0">  
    <frame name="multiscreensheader" src="MultiScreensHeader.aspx?mode=<%=Request.QueryString["MODE"]%>" scrolling="auto">
    <frame name="multiscreensbody" src="" scrolling="auto"">
</frameset>
</html>
