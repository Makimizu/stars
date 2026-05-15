<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ENDORSEMENT_FRAME.aspx.cs" Inherits="AGR.Form_Agent.ENDORSEMENT_FRAME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="50,*" frameborder="0" border="0" framespacing="0">   
    <frame name="MovementHeader" src="ENDORSEMENT_HEADER.ASPX?mode=<%=Request.QueryString["mode"]%>" scrolling="no" >
    <frameset cols="200,*" frameborder="0" border="0" framespacing="0">
        <frame name="MovementHeader" src="ENDORSEMENT_GROUP.ASPX?mode=<%=Request.QueryString["mode"]%>" scrolling="auto"  >
        <frame name="MovementBody" src="" scrolling="auto" style="border-left-style:inset;border-top-style:inset">
    </frameset>
</frameset>
</html>
