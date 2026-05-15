<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="REPORTFRAME.aspx.cs" Inherits="AGR.REPORTFRAME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="*,0" frameborder="0" border="0" framespacing="0">
    <frameset cols="200,*" frameborder="0" border="0" framespacing="0">
        <frame name="ReportHeader" src="REPORTHEADER.ASPX" scrolling="auto"></frame>
        <frame name="ReportBody" src=""></frame>
    </frameset>
    <!-- Some report list items expect a footer frame (e.g. parent.ReportFooter.prepareFooter). Keep a hidden frame for compatibility. -->
    <frame name="ReportFooter" src="about:blank" scrolling="no" noresize="noresize"></frame>
</frameset>
</html>

