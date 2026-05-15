<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_FRAME.aspx.cs" Inherits="AGR.PARAMETER_MASTER_FRAME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="60,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="ParameterMasterHeader" src="PARAMETER_MASTER_BUTTON.ASPX?mode=<%=Request.QueryString["mode"]%>" scrolling="none" >
<frame name="ParameterMasterBody" src="PARAMETER_MASTER_LIST.ASPX?mode=<%=Request.QueryString["mode"]%>" scrolling="auto" >

</frameset>
</html>
