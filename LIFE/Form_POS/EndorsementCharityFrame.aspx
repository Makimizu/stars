<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementCharityFrame.aspx.cs" Inherits="LIFE.Form_POS.EndorsementCharityFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="Endorsementcharityheader" src="EndorsementSubmission.aspx?regno=<%=Request.QueryString["regno"]%>&seq=<%=Request.QueryString["seq"]%>&type=<%=Request.QueryString["type"]%>" >
    <frame name="EndorsementcharityOrg" src="EndorsementCharityOrg.aspx?regno=<%=Request.QueryString["regno"]%>&seq=<%=Request.QueryString["seq"]%>&type=<%=Request.QueryString["type"]%>" >
</frameset>
</html>