<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimLetterFrame.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimLetterFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="150px,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimletter" src="ClaimDocLetter.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>" scrolling="no">
    <frame name="claimletterreport"  src="../Standard/default.html">
</frameset>
</html>
