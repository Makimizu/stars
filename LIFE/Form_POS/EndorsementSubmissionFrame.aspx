<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementSubmissionFrame.aspx.cs" Inherits="LIFE.Form_POS.EndorsementSubmissionFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="200,*" frameborder="0" border="0" framespacing="0">  
    <frame name="EndorsementSubmissionheader" src="EndorsementSubmissionButton.aspx?regno=<%=Request.QueryString["regno"]%>&seq=<%=Request.QueryString["seq"]%>&readonly=<%=Request.QueryString["readonly"]%>" >
    <%--<frame name="EndorsementSubmissionbody" src="../Standard/default.html" scrolling="auto">--%>
    <frame name="EndorsementSubmissionbody" src="EndorsementSubmissionInfo.aspx?regno=<%=Request.QueryString["regno"]%>&seq=<%=Request.QueryString["seq"]%>" scrolling="auto">
</frameset>
</html>
