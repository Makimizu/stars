<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicySavingFrame.aspx.cs" Inherits="GLIFE.Form_Policy.PolicySavingFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="35%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="policysavingheader" src="PolicySaving.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
    <frame name="policysavingbody" src="" scrolling="auto">
</frameset>
</html>
