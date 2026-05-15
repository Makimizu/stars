<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationMainInfoFrame.aspx.cs" Inherits="GLIFE.Form_App.ApplicationMainInfoFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="maininfo" src="ApplicationMainInfo.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="maininfoquestion" src="ApplicationQuestion.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
</frameset>
</html>
