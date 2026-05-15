<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationBenefitFrame.aspx.cs" Inherits="GLIFE.Form_App.ApplicationBenefitFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="appbenefit" src="ApplicationBenefit.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
    <frame name="appchild" src="ApplicationBenefitFrameChild.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
</frameset>
</html>
