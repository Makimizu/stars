<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductFrame.aspx.cs" Inherits="HLP.Form_Parameter.ProductFrame" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="130,*" frameborder="0" border="0" framespacing="0">  
    <frame name="productheader" src="ProductHeader.aspx?code=<%=Request.QueryString["code"]%>" scrolling="no">
    <frame name="productbody" src="" scrolling="auto">
</frameset>
</html>
