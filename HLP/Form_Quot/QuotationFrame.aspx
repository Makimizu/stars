<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationFrame.aspx.cs" Inherits="HLP.Form_Quot.QuotationFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="150,*" frameborder="0" border="0" framespacing="0">  
    <frame name="quotheader" src="QuotationHeader.aspx?code=<%=Request.QueryString["code"]%>&readonly=<%=Request.QueryString["readonly"]%>" scrolling="no">
    <frame name="quotbody" src="" scrolling="auto">
</frameset>
</html>
