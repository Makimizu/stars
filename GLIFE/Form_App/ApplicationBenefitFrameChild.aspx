<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationBenefitFrameChild.aspx.cs" Inherits="GLIFE.Form_App.ApplicationBenefitFrameChild" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="70px,*" frameborder="0" border="0" framespacing="0">  
    <frame name="appbenefit" src="ApplicationBenefitButton.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
    <frame name="appchild" src="ApplicationBenefitReins.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="auto">
</frameset>
</html>
