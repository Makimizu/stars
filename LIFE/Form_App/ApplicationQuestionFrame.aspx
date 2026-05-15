<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationQuestionFrame.aspx.cs" Inherits="LIFE.Form_App.ApplicationQuestionFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="150,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="QuestionHeader" src="ApplicationQuestionHeader.aspx?REGNO=<%=Request.QueryString["REGNO"]%>"  >
<frame name="QuestionBody" src="" scrolling="auto" >

</frameset>
</html>
