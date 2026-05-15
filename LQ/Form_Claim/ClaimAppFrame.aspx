<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimAppFrame.aspx.cs" Inherits="LQ.Form_Claim.ClaimAppFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="180,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimheader" src="ClaimApp.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
    <frame name="claimbody" src="ClaimBenefitFrame.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
</frameset>
</html>
