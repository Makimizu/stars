<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationEndorsementRemarkFrame.aspx.cs" Inherits="GLIFE.Form_App.ApplicationEndorsementRemarkFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="POSremarkheader" src="ApplicationEndorsementRemark.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" scrolling="auto">
    <frame name="POSremarkbody" src="" scrolling="auto">
</frameset>
</html>
