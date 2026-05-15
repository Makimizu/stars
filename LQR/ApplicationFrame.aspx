<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationFrame.aspx.cs" Inherits="LQR.ApplicationFrame" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="190,*" frameborder="0" border="0" framespacing="0">  
        <frame name="appheader" src="ApplicationHeader.aspx" scrolling="no">
        <frameset cols="60%,*" frameborder="0" border="0" framespacing="0">  
                <frame name="appquestion" src="ApplicationQuestionRundown.aspx" scrolling="auto">
                <frame name="appsignature" src="" scrolling="auto">
        </frameset>
</frameset>
</html>
