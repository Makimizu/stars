<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_Frame.aspx.cs" Inherits="AGR.Agent_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="250,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="AgentHeader" src="Agent_Registration.aspx?AGENTCODE=<%=Request.QueryString["AGENTCODE"]%>" >
<frame name="AgentBody" src="" scrolling="auto" >

</frameset>
</html>
