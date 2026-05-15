<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_Finance_Frame.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Period_Finance_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="30,*" frameborder="0" border="0" framespacing="0">  
    <frame name="polisfinanceheader" src="Polis_Period_Finance_Header.aspx?PolicyPeriod=<%=Request.QueryString["PolicyPeriod"]%>" scrolling="no">
    <frame name="polisfinancebody" src="" scrolling="auto">
</frameset>
</html>
