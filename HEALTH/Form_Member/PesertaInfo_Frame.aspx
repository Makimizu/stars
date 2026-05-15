<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PesertaInfo_Frame.aspx.cs" Inherits="HEALTH.Form_Member.PesertaInfo_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="330,*" frameborder="0" border="0" framespacing="0">  
    <frame name="memberheader" src="PesertaInfo.aspx?REGNO=<%=Request.QueryString["REGNO"]%>" scrolling="no">
    <frame name="membercontent" src="" scrolling="auto">
</frameset>
</html>
