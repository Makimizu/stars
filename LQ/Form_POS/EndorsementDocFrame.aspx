<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementDocFrame.aspx.cs" Inherits="LQ.Form_POS.EndorsementDocFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="document" src="EndorsementDoc.aspx?regno=<%=Request.QueryString["regno"]%>&type=<%=Request.QueryString["type"]%>&seq=<%=Request.QueryString["seq"]%>" scrolling="auto">
    <frame name="archieve"  scrolling="auto">
</frameset>
</html>

