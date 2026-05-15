<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimDocFrame.aspx.cs" Inherits="LQ.Form_Claim.ClaimDocFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimdocheader" src="ClaimDocument.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
    <frame name="claimdocbody" src="" scrolling="auto">
</frameset>
</html>
