<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductReinsuranceFrame.aspx.cs" Inherits="UWBOX.Form_TC.ProductReinsuranceFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="350,*" frameborder="0" border="0" framespacing="0">  
    <frame name="productReinsHeader" src="ProductReinsurance.aspx?CODE=<%=Request.QueryString["CODE"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="productReinsBody" src="" style="border:inset;">
</frameset>
</html>
