<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductFrame.aspx.cs" Inherits="UWBOX.Form_TC.ProductFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="150,*" frameborder="0" border="0" framespacing="0">  
   
    
     <frame name="ProductHeader" src="ProductHeader.aspx?CODE=<%=Request.QueryString["CODE"]%>" scrolling="auto">
    <frame name="ProductBody" src="ProductTC.aspx?CODE=<%=Request.QueryString["CODE"]%>" scrolling="auto">
</frameset>
</html>

