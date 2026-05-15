<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementSubmissionFrameChild.aspx.cs" Inherits="LIFE.Form_POS.EndorsementSubmissionFrameChild" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="endorsementsubmissionheader" src="EndorsementSubmission.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" scrolling="auto">
    <frame name="endorsementsubmissionbody" src="EndorsementSubmissionPreview<%=Request.QueryString["TYPE"]%>.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" scrolling="auto">
</frameset>
</html>
