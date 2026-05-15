<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member_Frame.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Health.Member_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="200,*" frameborder="0" border="0" framespacing="0">  
    <frame name="memberheader" src="Member_Header.aspx?REGNO=<%=Request.QueryString["REGNO"]%>" scrolling="auto">
    <frame name="membercontent" src="" scrolling="auto">
</frameset>
</html>
