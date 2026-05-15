<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimShareFrame.aspx.cs" Inherits="LQ.Form_Claim.ClaimShareFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="claimshareheader" src="ClaimShare.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
        <frameset rows="120,*" frameborder="0" border="0" framespacing="0">
            <frame name="claimsharebody" src="../Form_Tools/PaymentAcc.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&CODE=CLM&DISABLE=0" scrolling="auto">
            <frame name="claimorgbeneficiary" src="ClaimOrgBeneficiary.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&ORGTYPE=1&MODE=1" scrolling="auto">
        </frameset>
</frameset>
</html>
