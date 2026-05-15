<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenjaminanApprovalFrame.aspx.cs" Inherits="HEALTH.Form_Klaim.PenjaminanApprovalFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="100,*" frameborder="0" border="0" framespacing="0">  
    <frame name="clmprovsjapproval" src="PenjaminanApproval.aspx?NOSURAT=<%=Request.QueryString["NOSURAT"]%>" scrolling="auto" style="border-style:inset;">
    <frame name="clmprovsjheader" src="Penjaminan.aspx?NOSURAT=<%=Request.QueryString["NOSURAT"]%>" scrolling="auto">
</frameset>
</html>
