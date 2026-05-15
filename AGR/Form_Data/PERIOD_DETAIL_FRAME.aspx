<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PERIOD_DETAIL_FRAME.aspx.cs" Inherits="AGR.Form_Data.PERIOD_DETAIL_FRAME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="200,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="PeriodDetailHeader" src="PERIOD_DETAIL.ASPX?CD=<%=Request.QueryString["CD"]%>&START_DATE=<%=Request.QueryString["START_DATE"]%>&END_DATE=<%=Request.QueryString["END_DATE"]%>&TYPE=<%=Request.QueryString["TYPE"]%>"  >
<frame name="PeriodDetailBody" src="" scrolling="auto" >

</frameset>
</html>
