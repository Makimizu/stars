<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimFrame.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="500,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimheader" src="ClaimHeader.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>&CLAIM_NO=<%=Request.QueryString["CLAIM_NO"]%>" scrolling="auto">
    <frame name="claimbody" src="../default.html" scrolling="auto" style="border:inset;">
</frameset>
</html>
