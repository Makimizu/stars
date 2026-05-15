<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementRemarkFrame.aspx.cs" Inherits="LIFE.Form_POS.EndorsementRemarkFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="50%,50%" frameborder="0" border="0" framespacing="0">  
    <frame name="remark" src="EndorsementRemark.aspx?regno=<%=Request.QueryString["REGNO"]%>&seq=<%=Request.QueryString["SEQ"]%>" scrolling="auto">
    <frame name="track" src="../Form_Tools/Track.aspx?TRACK_TYPE=POS&REGNO=<%=Request.QueryString["regno"]%>&PARAM_VALUE=<%=Request.QueryString["seq"]%>" scrolling="auto">
</frameset>
</html>

