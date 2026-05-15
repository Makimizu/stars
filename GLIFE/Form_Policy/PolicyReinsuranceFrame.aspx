<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyReinsuranceFrame.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyReinsuranceFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="100,*" frameborder="0" border="0" framespacing="0">  
    <frame name="policyReinsheader" src="PolicyReinsurance.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="policyReinsbody" src="" scrolling="auto" style="border-top:inset;">
</frameset>
</html>
