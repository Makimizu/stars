<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_UPD_Frame.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Proses_UPD_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="440,*" frameborder="0" border="0" framespacing="0">  
    <frame name="gpaupdheader" src="GPA_Proses_UPD.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="no">
    <frame name="gpaupdbody" src="GPA_Proses_UPD_List.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="auto">
</frameset>
</html>
