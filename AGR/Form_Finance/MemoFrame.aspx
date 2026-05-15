<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemoFrame.aspx.cs" Inherits="AGR.Form_Finance.MemoFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="200,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="MemoHeader" src="MemoGroup.ASPX"  >
<frame id="MemoBodyFrame" name="MemoBody" src="<asp:Literal ID="FrameSrc" runat="server" />" scrolling="auto" runat="server" >

</frameset>
</html>

