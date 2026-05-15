<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_ADD_Frame.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Proses_ADD_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="400,*" frameborder="0" border="0" framespacing="0">  
    <frame name="gpaaddheader" src="GPA_Proses_ADD.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="no">
    <frame name="gpaaddbody" src="GPA_Proses_ADD_List.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="auto">
</frameset>
</html>
