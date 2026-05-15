<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicalLabChildFrame.aspx.cs" Inherits="UWBOX.Form_Partners.MedicalLabChildFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="60px,*" frameborder="0" border="0" framespacing="0">  
    <frame name="medlabbutton" src="MedicalLabButton.aspx?code=<%=Request.QueryString["code"]%>" scrolling="no">
    <frame name="medlabchild" src="MedicalLabItems.aspx?code=<%=Request.QueryString["code"]%>" scrolling="auto">
</frameset>
</html>
