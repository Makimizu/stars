<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Print_Frame.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Print_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="30,*" frameborder="0" border="0" framespacing="0">  
    <frame name="polisprintheader" src="Polis_Print_Header.aspx?PolicyPeriod=<%=Request.QueryString["PolicyPeriod"]%>" scrolling="no">
    <frame name="polisprintbody" src="" scrolling="auto">
</frameset>
</html>
