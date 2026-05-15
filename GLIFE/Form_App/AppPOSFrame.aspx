<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppPOSFrame.aspx.cs" Inherits="GLIFE.Form_App.AppPOSFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="190,*" frameborder="0" border="0" framespacing="0">  
    <frame name="POSheader" src="ApplicationEndorsement.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" scrolling="auto">
    <frame name="POSbody" src="ApplicationEndorsement<%=Request.QueryString["TYPE"]%>.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>" scrolling="auto">
</frameset>
</html>
