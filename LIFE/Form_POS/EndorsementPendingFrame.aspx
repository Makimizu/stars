<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementPendingFrame.aspx.cs" Inherits="LIFE.Form_POS.EndorsementPendingFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="POSpendingheader" src="EndorsementPending.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>" scrolling="no">
    <frame name="POSpendingbody" src="../Standard/default.html" scrolling="auto">
</frameset>
</html>
