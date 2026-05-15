<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementReportFrame.aspx.cs" Inherits="LIFE.Form_POS.EndorsementReportFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="200,*" frameborder="0" border="0" framespacing="0">  
    <frame name="endorsementreportheader" src="EndorsementReportHeader.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>" scrolling="auto">
    <frame name="endorsementreportbody" src="EndorsementSubmissionInfo.aspx?regno=<%=Request.QueryString["regno"]%>&seq=<%=Request.QueryString["seq"]%>" scrolling="auto">
</frameset>
</html>

