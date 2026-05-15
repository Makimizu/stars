<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyFrame.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="245,*" frameborder="0" border="0" framespacing="0">  
    <frame name="policyheader" src="Policy.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>&userid=<%=Request.QueryString["userid"]%>" scrolling="no">
    <frame name="policybody" src="" scrolling="auto">
</frameset>
</html>
