<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyApprovalFrame.aspx.cs" Inherits="CUSTOMERS.Form_Client.CompanyApprovalFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="230,*" frameborder="0" border="0" framespacing="0">  
    <frame name="companyheader" src="CompanyHeader.aspx?code=<%=Request.QueryString["code"]%>" scrolling="no">
    <frame name="companybody" src="CompanySimilarity.aspx?code=<%=Request.QueryString["code"]%>"  scrolling="auto">
</frameset>
</html>
