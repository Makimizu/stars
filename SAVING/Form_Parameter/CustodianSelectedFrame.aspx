<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustodianSelectedFrame.aspx.cs" Inherits="SAVING.Form_Parameter.CustodianSelectedFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="40%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="CustCompany" src="CustodianCompany.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="auto">
    <frameset rows="30,*" frameborder="0" border="0" framespacing="0">
        <frame name="CustButton" src="CustodianButton.aspx?ID=<%=Request.QueryString["ID"]%>" >     
        <frame name="CustDetail" src="CustodianProduct.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="auto"> 
    </frameset>
</frameset>
</html>
