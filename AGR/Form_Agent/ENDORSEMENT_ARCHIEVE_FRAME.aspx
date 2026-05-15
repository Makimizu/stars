<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ENDORSEMENT_ARCHIEVE_FRAME.aspx.cs" Inherits="AGR.Form_Agent.ENDORSEMENT_ARCHIEVE_FRAME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="80,*" frameborder="0" border="0" framespacing="0" scrolling="none">
    <frame name="EndorsementHeader" src="ENDORSEMENT_ARCHIEVE_HEADER.ASPX?ID=<%=Request.QueryString["ID"]%>&mode=<%=Request.QueryString["MODE"]%>"></frame>
    <frame name="EndorsementBody" src="../../ARCHIEVE/Arsip.aspx?app=AGR&tipe=AGR_3&owner1=<%=Request.QueryString["ID"]%>&owner2=&owner3=&user=<%=Request.QueryString["USERID"]%>" scrolling="auto"></frame>
</frameset>
</html>
