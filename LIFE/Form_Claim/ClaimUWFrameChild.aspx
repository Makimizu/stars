<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimUWFrameChild.aspx.cs" Inherits="LIFE.Form_Claim.ClaimUWFrameChild" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimUWheader" src="../Form_App/ApplicationDocument.aspx?ID=<%=Request.QueryString["REGNO"]%>" scrolling="auto">
    <frame name="claimUWbody" src="../Form_App/ApplicationRemark.aspx?ID=<%=Request.QueryString["REGNO"]%>" scrolling="auto">
</frameset>
</html>
