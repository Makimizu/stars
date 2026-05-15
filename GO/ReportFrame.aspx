<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportFrame.aspx.cs" Inherits="GO.ReportFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="60,*" frameborder="0" border="0" framespacing="0">  
    <frame name="reportheader" src="Report.aspx?APPID=<%=Request.QueryString["APPID"]%>" scrolling="no">    
    <frameset rows="*" frameborder="0" border="0" framespacing="0">
        <frame name="reportcontent" src="default.html" marginheight="0" marginwidth="0" scrolling="auto" noresize>
    </frame>
</frameset>
</html>
