<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Endorsement_Frame.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Endorsement_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="100,*" frameborder="0" border="0" framespacing="0">  
    <frame name="polisendorsheader" src="Polis_Endorsement_Header.aspx?ID=<%=Request.QueryString["ID"]%>&SEQ=<%=Request.QueryString["SEQ"]%>" scrolling="no">
    <frame name="polisendorsbody" src="" scrolling="auto">
</frameset>
</html>
