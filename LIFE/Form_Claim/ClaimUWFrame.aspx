<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimUWFrame.aspx.cs" Inherits="LIFE.Form_Claim.ClaimUWFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimUWheader" src="ClaimUWFrameChild.aspx?REGNO=<%=Request.QueryString["REGNO"]%>" scrolling="auto">
    <frame name="claimUWbody" src="../Form_App/ApplicationQuestion.aspx?ID=<%=Request.QueryString["REGNO"]%>" scrolling="auto">
</frameset>
</html>