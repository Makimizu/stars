<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyFrame.aspx.cs" Inherits="CUSTOMERS.Form_Client.CompanyFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="300,*" frameborder="0" border="0" framespacing="0">  
    <frame name="companyheader" src="CompanyHeader.aspx?code=<%=Request.QueryString["code"]%>" scrolling="no">
    <frame name="companybody" src="" scrolling="auto">
</frameset>
</html>
