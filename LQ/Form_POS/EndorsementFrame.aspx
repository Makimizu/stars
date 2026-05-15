<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementFrame.aspx.cs" Inherits="LQ.Form_POS.EndorsementFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="150,*" frameborder="0" border="0" framespacing="0">  
    <frame name="appheader" src="EndorsementHeader.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="appbody" src="" scrolling="auto">
</frameset>
</html>
