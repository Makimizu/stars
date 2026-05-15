<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationReportFrame.aspx.cs" Inherits="LQ.Form_Client.QuotationReportFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="200,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="ReportHeader" src="QuotationReportHeader.aspx?REGNO=<%=Request.QueryString["REGNO"]%>" scrolling="auto" >
<frame name="ReportBody" src=""auto" >

</frameset>
</html>
