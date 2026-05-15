<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyChannelFrame.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyChannelFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="400px,*" frameborder="0" border="0" framespacing="0">  
    <frame name="policychannelheader" src="PolicyChannel.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="policychannelbody" src="../Standard/default.html" scrolling="auto">
</frameset>
</html>
