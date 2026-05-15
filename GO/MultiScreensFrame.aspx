<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiScreensFrame.aspx.cs" Inherits="GO.MultiScreensFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="50,*" frameborder="0" border="0" framespacing="0">  
    <frame name="multiscreensheader" src="MultiScreensHeader.aspx?CODE=<%=Request.QueryString["CODE"]%>" scrolling="auto">
    <frame name="multiscreensbody" src="" scrolling="auto"">
</frameset>
</html>
