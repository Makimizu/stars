<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENCY_CORPORATE_FRAME.aspx.cs" Inherits="AGR.AGENCY_CORPORATE_FRAME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="300,*" frameborder="0" border="0" framespacing="0">   

   
<frame name="AgencyHeader" src="AGENCY_CORPORATE.ASPX?ID=<%=Request.QueryString["ID"]%>" scrolling="auto" >
<frame name="AgencyBody" src="AGENCY_CORPORATE_BRANCH.ASPX?ID=<%=Request.QueryString["ID"]%>" scrolling="auto" >

</frameset>
</html>

