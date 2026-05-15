<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametersFrame.aspx.cs" Inherits="LIFE.Form_Parameter.ParametersFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="300,*" frameborder="0" border="0" framespacing="0">  
    <frame name="paramheader" src="ParametersList.aspx?prefix=<%=Request.QueryString["prefix"]%>&page=<%=Request.QueryString["page"]%>" scrolling="auto">
    <frame name="parambody" src="../Standard/default.html" scrolling="auto">
</frameset>
</html>
