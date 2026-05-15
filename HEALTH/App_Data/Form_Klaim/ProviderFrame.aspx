<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderFrame.aspx.cs" Inherits="HEALTH.Form_Klaim.ProviderFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="350,*" frameborder="0" border="0" framespacing="0">  
    <frame name="provheader" src="ProviderEntry.aspx?CODE=<%=Request.QueryString["CODE"]%>" scrolling="auto">
    <frame name="provbody" src="../default.html" scrolling="auto"">
</frameset>
</html>
