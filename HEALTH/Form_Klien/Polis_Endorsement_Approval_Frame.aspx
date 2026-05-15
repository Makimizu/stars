<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Endorsement_Approval_Frame.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Endorsement_Approval_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="40,*" frameborder="0" border="0" framespacing="0">  
    <frame name="polisendorsapvheader" src="Polis_Endorsement_Approval_Button.aspx?ID=<%=Request.QueryString["ID"]%>&SEQ=<%=Request.QueryString["SEQ"]%>"  style="border:outset;"  scrolling="no">
    <frame name="polisendorsapvbody" src="Polis_Endorsement_Frame.aspx?ID=<%=Request.QueryString["ID"]%>&SEQ=<%=Request.QueryString["SEQ"]%>" scrolling="auto">
</frameset>
</html>
