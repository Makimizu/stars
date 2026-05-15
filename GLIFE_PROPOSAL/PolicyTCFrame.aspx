<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyTCFrame.aspx.cs" Inherits="GLIFE_PROPOSAL.PolicyTCFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="50,*" frameborder="0" border="0" framespacing="0">  
    <frame name="policyTCheader" src="PolicyTC.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="policyTCbody" src="" scrolling="auto" style="border:inset;">
</frameset>
</html>
