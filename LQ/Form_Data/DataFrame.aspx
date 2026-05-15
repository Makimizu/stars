<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataFrame.aspx.cs" Inherits="LQ.Form_Data.DataFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="150,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="DataHeader" src="DataHeader.aspx?mode=<%=Request.QueryString["mode"]%>"  >
<frame name="DataBody" src="" scrolling="auto" >

</frameset>
</html>
