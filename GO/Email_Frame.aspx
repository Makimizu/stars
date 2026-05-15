<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Email_Frame.aspx.cs" Inherits="GO.Email_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="600,*" frameborder="0" border="0" framespacing="0">  
    <frame name="emailheader" src="Email_List.aspx" scrolling="auto">
    <frame name="emailbody" src="default.html" scrolling="auto" style="border:inset;">
</frameset>
</html>
