<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementBenefitCyclePayoutFrame.aspx.cs" Inherits="LIFE.Form_POS.EndorsementBenefitCyclePayoutFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="60%,*" frameborder="0" border="0" framespacing="0">  
    <frameset rows="30,*" frameborder="0" border="0" framespacing="0"> 
        <frame name="EndorsementPayoutButton" src="EndorsementBenefitCyclePayoutButton.aspx?regno=<%=Request.QueryString["regno"]%>&seq=<%=Request.QueryString["seq"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" scrolling="no">
        <frame name="EndorsementPayoutheader" src="EndorsementBenefitCyclePayout.aspx?regno=<%=Request.QueryString["regno"]%>&seq=<%=Request.QueryString["seq"]%>&TYPE=<%=Request.QueryString["TYPE"]%>" >
    </frameset>
    <frameset rows="120,*" frameborder="0" border="0" framespacing="0">  
        <frame name="EndorsementPayoutAcc" src="../Form_Tools/PaymentAcc.aspx?REGNO=<%=Request.QueryString["REGNO"]%>-<%=Request.QueryString["SEQ"]%>-<%=Request.QueryString["TYPE"]%>&SEQ=1&CODE=POS" >
        <frame name="EndorsementPayoutPreview" src="../Standard/default.html" >            
    </frameset>
</frameset>
</html>
