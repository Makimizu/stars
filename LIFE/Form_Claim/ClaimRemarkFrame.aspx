<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimRemarkFrame.aspx.cs" Inherits="LIFE.Form_Claim.ClaimRemarkFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="200,*" frameborder="0" border="0" framespacing="0">  
    <frame name="ClaimReportHeader" src="ClaimReport.aspx?REGNO=<%=Request.QueryString["regno"]%>&SEQ=<%=Request.QueryString["seq"]%>" scrolling="auto">
    <frame name="ClaimReportBody" src="../Form_Tools/Track.aspx?TRACK_TYPE=CLM&REGNO=<%=Request.QueryString["regno"]%>&PARAM_VALUE=<%=Request.QueryString["seq"]%>" scrolling="auto">
</frameset>
</html>

