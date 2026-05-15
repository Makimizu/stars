<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Peserta_ReprintCard_Frame.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Peserta_ReprintCard_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="350,*" frameborder="0" border="0" framespacing="0">  
    <frame name="gparpcheader" src="GPA_Proses_RPC.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="no">
    <frame name="gparpcbody" src="GPA_Proses_RPC_List.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="auto">
</frameset>
</html>
