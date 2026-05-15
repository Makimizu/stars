<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementPayoutFrame.aspx.cs" Inherits="LIFE.Form_POS.EndorsementPayoutFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="50%,*" frameborder="0" border="0" framespacing="0">  
    <frame name="EndorsementPayoutheader" src="EndorsementPayOut.aspx?regno=<%=Request.QueryString["regno"]%>&seq=<%=Request.QueryString["seq"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" >
        <frameset rows="130,*" frameborder="0" border="0" framespacing="0">  
            <frame name="EndorsementPayoutAcc" src="../Form_Tools/PaymentAcc.aspx?REGNO=<%=Request.QueryString["REGNO"]%>-<%=Request.QueryString["SEQ"]%>-<%=Request.QueryString["TYPE"]%>&SEQ=1&CODE=POS" >
            <frame name="EndorsementPayoutPreview" src="EndorsementPayOutBalance.aspx?REGNO=<%=Request.QueryString["REGNO"]%>&SEQ=<%=Request.QueryString["SEQ"]%>&TYPE=<%=Request.QueryString["TYPE"]%>&REMARK=<%=Request.QueryString["REMARK"]%> " >            
        </frameset>
</frameset>
</html>
