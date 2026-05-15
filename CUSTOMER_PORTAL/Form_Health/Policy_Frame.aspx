<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Policy_Frame.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Health.Policy_Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="140,*" frameborder="0" border="0" framespacing="0">  
    <frame name="polisheader" src="Policy_Header.aspx?ID=<%=Request.QueryString["ID"]%>" scrolling="no">
    <frame name="polisbody" src="" scrolling="auto">
</frameset>
</html>
