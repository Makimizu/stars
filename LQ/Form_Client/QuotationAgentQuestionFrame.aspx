<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationAgentQuestionFrame.aspx.cs" Inherits="LQ.Form_Client.QuotationAgentQuestionFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="350,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="AgentHeader" src="QuotationAgentInfo.aspx?REGNO=<%=Request.QueryString["REGNO"]%>"  >
<frame name="AgentBody"  scrolling="auto" >

</frameset>
</html>
