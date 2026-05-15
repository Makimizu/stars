<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_Frame.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Proses_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="170,*" frameborder="0" border="0" framespacing="0">  
    <frame name="gpaheader" src="GPA_Proses.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="no">
    <frame name="gpabody" src="" scrolling="auto">
</frameset>
</html>
