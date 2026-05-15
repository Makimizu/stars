<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationQuestionFrame.aspx.cs" Inherits="LQ.Form_Client.QuotationQuestionFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="250,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="QuestionHeader" src="QuotationQuestionHeader.aspx?REGNO=<%=Request.QueryString["REGNO"]%>"  >
<frame name="QuestionBody" src="" scrolling="auto" >

</frameset>
</html>
