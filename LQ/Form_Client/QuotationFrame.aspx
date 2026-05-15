<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationFrame.aspx.cs" Inherits="LQ.Form_Client.QuotationFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="200,*" frameborder="0" border="0" framespacing="0">   

<frame name="QuotationHeader" src="QuotationHeader.aspx?REGNO=<%=Request.QueryString["REGNO"]%>"  >
<frame name="QuotationBody" src="" scrolling="auto" >

</frameset>
</html>
