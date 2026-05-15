<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_REMUN_FRAME.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_MASTER_REMUN_FRAME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="160,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="RemunMasterHeader" src="PARAMETER_MASTER_REMUN_LIST.ASPX?mode=<%=Request.QueryString["mode"]%>" scrolling="none" >
<frame name="RemunMasterBody" src="" scrolling="auto" >

</frameset>
</html>
