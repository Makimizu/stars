<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementBenefitFrame.aspx.cs" Inherits="LIFE.Form_POS.EndorsementBenefitFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="80,*" frameborder="0" border="0" framespacing="0">  
    <frame name="benefitheader" src="EndorsementBenefitHeader.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" scrolling="no">
    <frame name="benefitcontent" src="EndorsementMemberList.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" marginheight="0" marginwidth="0" scrolling="yes" noresize>
</frameset>
</html>
