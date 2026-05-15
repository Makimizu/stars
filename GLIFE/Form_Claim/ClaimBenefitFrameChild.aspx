<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimBenefitFrameChild.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimBenefitFrameChild" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="60px,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimbenefitbutton" src="ClaimBenefitButton.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
    <frame name="claimbenefitbuttoncontent" src="ClaimBenefitICD.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
</frameset>
</html>
