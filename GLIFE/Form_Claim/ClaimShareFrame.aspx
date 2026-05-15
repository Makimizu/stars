<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimShareFrame.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimShareFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimshareheader" src="ClaimShare.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
    <frame name="claimsharebody" src="" scrolling="auto">
</frameset>
</html>
