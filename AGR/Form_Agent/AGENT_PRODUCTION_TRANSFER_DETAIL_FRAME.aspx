<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_PRODUCTION_TRANSFER_DETAIL_FRAME.aspx.cs" Inherits="AGR.Form_Agent.AGENT_PRODUCTION_TRANSFER_DETAIL_FRAME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="100,*" frameborder="0" border="0" framespacing="0" scrolling="none">


    <frame name="ProdTransferHeader" src="AGENT_PRODUCTION_TRANSFER_DETAIL.ASPX?AGENTCODE=<%=Request.QueryString["AGENTCODE"]%>"></frame>
    <frame name="ProdTransferBody" scrolling="auto"></frame>

</frameset>
</html>
