<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurplusUwDetailFrame.aspx.cs" Inherits="UWBOX.Form_Tools.SurplusUwDetailFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="260,*" frameborder="0" border="0" framespacing="0">  
    <frame name="paramheader" src="SurplusUwDetail.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="auto">
    <frame name="parambody" src="" scrolling="auto">
</frameset>
</html>
