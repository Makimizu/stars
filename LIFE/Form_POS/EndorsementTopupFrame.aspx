<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementTopupFrame.aspx.cs" Inherits="LIFE.Form_POS.EndorsementTopupFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="60%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="EndorsementTopupheader" src="EndorsementTopup.aspx?regno=<%=Request.QueryString["regno"]%>&seq=<%=Request.QueryString["seq"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" >
    <frame name="EndorsementTopupPreview" src="EndorsementPayOutBalance.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&TYPE=<%=Request.QueryString["TYPE"]%>&REMARK=<%=Request.QueryString["REMARK"]%>" >
</frameset>
</html>

