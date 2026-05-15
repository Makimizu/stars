<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationPersonFrame.aspx.cs" Inherits="LQ.Form_Client.QuotationPersonFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="*,550" frameborder="0" border="0" framespacing="0">   

   
<frame name="QuotationPersonHeader" src="QuotationPersonHeader.aspx?REGNO=<%=Request.QueryString["REGNO"]%>"  >
<frame name="QuotationPersonBody" src="" scrolling="auto" style="border:inset;" >

</frameset>
</html>

