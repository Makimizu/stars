<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductTC_Frame.aspx.cs" Inherits="UWBOX.Form_TC.ProductTC_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="300,*" frameborder="0" border="0" framespacing="0">  
    <frame name="ProductTCheader" src="ProductTC.aspx?CODE=<%=Request.QueryString["CODE"]%>" scrolling="auto">
    <frame name="ProductTCbody" src="" scrolling="auto" style="border:inset;">
</frameset>
</html>
