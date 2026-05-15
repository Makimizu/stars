<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPolicyPrintingFrame.aspx.cs" Inherits="LIFE.Form_App.ApplicationPolicyPrintingFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="PolicyGenerate" src="ApplicationPolicyPrinting.aspx" scrolling="auto">
    <frame name="PolicyFileView" src="ApplicationPolicyFolder.aspx" scrolling="auto">
</frameset>
</html>
