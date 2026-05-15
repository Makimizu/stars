<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationFrame.aspx.cs" Inherits="GLIFE_PROPOSAL.QuotationFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="200,*" frameborder="0" border="0" framespacing="0">  
    <frame name="QuotationHeader" src="QuotationGTL.aspx?QUOTNO=<%=Request.QueryString["QUOTNO"]%>" scrolling="auto">
    <frame name="QuotationBody" src="" scrolling="auto" >
</frameset>
</html>
