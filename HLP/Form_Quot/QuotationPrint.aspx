<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationPrint.aspx.cs" Inherits="HLP.Form_Quot.QuotationPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="30,*" frameborder="0" border="0" framespacing="0">  
    <frame name="printheader" src="QuotationPrintHeader.aspx?quotno=<%=Request.QueryString["code"]%>&verno=<%=Request.QueryString["ver"]%>" scrolling="no">
    <frame name="printbody" src="" scrolling="auto">
</frameset>
</html>
