<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenjaminanFrame.aspx.cs" Inherits="HEALTH.Form_Klaim.PenjaminanFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="300,*" frameborder="0" border="0" framespacing="0">  
    <frame name="clmprovsjheader" src="Penjaminan.aspx?NOSURAT=<%=Request.QueryString["NOSURAT"]%>" scrolling="auto">
    <frame name="clmprovsjbody" src="../default.html" scrolling="auto">
</frameset>
</html>
