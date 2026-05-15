<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimRemarkFrame.aspx.cs" Inherits="LQ.Form_Claim.ClaimRemarkFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="40%,*" frameborder="0" border="0" framespacing="0">  
    <frameset rows="50,*" frameborder="0" border="0" framespacing="0">  
        <frame name="track" src="../Form_Tools/Track.aspx?TRACK_TYPE=CLM&REGNO=<%=Request.QueryString["regno"]%>&PARAM_VALUE=<%=Request.QueryString["seq"]%>" scrolling="auto">
        <frame name="claimremarkheader" src="ClaimRemark.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
    </frameset>
    <frame name="claimremarkbody" src="ClaimLetterFrame.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&CODE=CLM" scrolling="auto">            
</frameset>
</html>
