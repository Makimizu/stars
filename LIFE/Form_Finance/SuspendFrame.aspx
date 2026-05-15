<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuspendFrame.aspx.cs" Inherits="LIFE.Form_Finance.SuspendFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frameset rows="150,*" frameborder="0" border="0" framespacing="0">  
        <frame name="SuspendAccountList" src="SuspendAccountList.aspx" scrolling="auto">
         <frame name="SuspendBankStatementList" src="../Standard/default.html" scrolling="auto">
    </frameset>
    <frame name="SuspendBankStatement" src="../Standard/default.html" scrolling="auto">
</frameset>
</html>
