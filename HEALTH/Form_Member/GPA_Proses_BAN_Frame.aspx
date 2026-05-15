<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_BAN_Frame.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Proses_BAN_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <frameset cols="400,*" frameborder="0" border="0" framespacing="0">  
        <frame name="gpachgheader" src="GPA_Proses_BAN.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="no">
        <frame name="gpachgbody" src="GPA_Proses_BAN_List.aspx?BATCH_ID=<%=Request.QueryString["BATCH_ID"]%>" scrolling="auto">
    </frameset>
</html>
