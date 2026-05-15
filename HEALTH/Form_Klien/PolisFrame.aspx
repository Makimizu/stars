<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolisFrame.aspx.cs" Inherits="HEALTH.Form_Klien.PolisFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="140,*" frameborder="0" border="0" framespacing="0">  
    <frame name="polisheader" src="PolisHeader.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
    <frame name="polisbody" src="" scrolling="auto">
</frameset>
</html>
