<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimLetterFrame.aspx.cs" Inherits="LQ.Form_Claim.ClaimLetterFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="100px,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimletter" src="ClaimDocLetter.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>" scrolling="auto">
    <frame name="claimletterreport"  src="">
</frameset>
</html>
