<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPremiumTermFrame.aspx.cs" Inherits="LIFE.Form_App.ApplicationPremiumTermFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="200,*" frameborder="0" border="0" framespacing="0">  
    <frame name="premiumterm" src="ApplicationTransactionHistory.aspx?REGNO=<%=Request.QueryString["ID"]%>" scrolling="auto">
    <frame name="premiumhistory" src="" scrolling="auto">
</frameset>
</html>
