<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationOtherInfoFrame.aspx.cs" Inherits="LIFE.Form_App.ApplicationOtherInfoFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="otherinfoheader" src="ApplicationOtherInfo.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
    <frame name="otherinfobody" src="ApplicationQuestionFrame.aspx?REGNO=<%=Request.QueryString["ID"]%>">
</frameset>
</html>
