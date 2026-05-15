<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationEndorsementPendingFrame.aspx.cs" Inherits="GLIFE.Form_App.ApplicationEndorsementPendingFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="POSpendingheader" src="ApplicationEndorsementPending.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" scrolling="no">
    <frame name="POSpendingbody" src="../Standard/default.html" scrolling="auto">
</frameset>
</html>
