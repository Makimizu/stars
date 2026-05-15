<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationSavingFrame.aspx.cs" Inherits="GLIFE.Form_Saving.ApplicationSavingFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="30,*" frameborder="0" border="0" framespacing="0">  
    <frame name="savingheader" src="ApplicationSavingHeader.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
    <frame name="savingbody" src="" scrolling="auto">
</frameset>
</html>