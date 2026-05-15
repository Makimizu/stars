<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimPendingFrame.aspx.cs" Inherits="LIFE.Form_Claim.ClaimPendingFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimpendingheader" src="ClaimPending.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="claimpendingbody" src="../Standard/default.html" scrolling="auto">
</frameset>
</html>