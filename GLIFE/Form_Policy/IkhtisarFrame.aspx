<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IkhtisarFrame.aspx.cs" Inherits="GLIFE.Form_Policy.IkhtisarFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="25%,25%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="appdocument" src="IkhtisarInput.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>&s=<%=Session["s"]%>">
    <frame name="appdocumentchild" src="IkhtisarReport.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
    <frame name="appdocumentarchieve"  src="">
</frameset>
</html>
