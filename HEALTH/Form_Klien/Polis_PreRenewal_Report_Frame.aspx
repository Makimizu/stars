<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_PreRenewal_Report_Frame.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_PreRenewal_Report_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="50,*" frameborder="0" border="0" framespacing="0">  
    <frame name="prerenewalhead" src="Polis_PreRenewal_Report.aspx?CODE=<%=Request.QueryString["CODE"]%>&ID=<%=Request.QueryString["ID"]%>" scrolling="auto">
    <frame name="prerenewalbody" src="" scrolling="auto"">
</frameset>
</html>
