<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurplusUWPreviewFrame.aspx.cs" Inherits="UWBOX.Form_Tools.SurplusUWPreviewFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="80,*" frameborder="0" border="0" framespacing="0">  
    <frame name="appheader" src="SurplusUWPreviewHeader.aspx?ID=<%=Request.QueryString["ID"]%>&Group=<%=Request.QueryString["GROUP"]%>" scrolling="no">
    <frame name="appbody" src="" scrolling="auto">
</frameset>
</html>
