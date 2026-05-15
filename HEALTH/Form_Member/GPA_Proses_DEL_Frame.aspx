<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_DEL_Frame.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Proses_DEL_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="350,*" frameborder="0" border="0" framespacing="0">  
    <frame name="gpadelheader" src="GPA_Proses_DEL.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="no">
    <frame name="gpadelbody" src="GPA_Proses_DEL_List.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="auto">
</frameset>
</html>
