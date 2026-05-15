<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimRemarkFrame.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimRemarkFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimremarkheader" src="ClaimRemark.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
    <frame name="claimremarkbody" src="ClaimLetterFrame.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&CODE=CLM" scrolling="auto">
</frameset>
</html>
