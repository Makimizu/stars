<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicalLabFrame.aspx.cs" Inherits="UWBOX.Form_Partners.MedicalLabFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="medlab" src="MedicalLab.aspx?code=<%=Request.QueryString["code"]%>" scrolling="auto">
    <frame name="medlabchildframe" src="MedicalLabChildFrame.aspx?code=<%=Request.QueryString["code"]%>" scrolling="auto">
</frameset>
</html>
