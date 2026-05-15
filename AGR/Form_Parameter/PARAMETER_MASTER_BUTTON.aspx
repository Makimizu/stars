<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_BUTTON.aspx.cs" Inherits="AGR.PARAMETER_MASTER_BUTTON" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <button onclick="parent.ParameterMasterBody.location.href='PARAMETER_MASTER_LIST.ASPX?mode=<%=Request.QueryString["mode"]%>'" type="button" style="width: 100%; height: 25px;" class="ASPButton"><i class="fa fa-list"></i>&nbsp;PARAMETER LIST</button>
                            </td>
                            <td style="width: 50%;">
                                <button onclick="parent.ParameterMasterBody.location.href='PARAMETER_MASTER_ENTRY.ASPX?mode=<%=Request.QueryString["mode"]%>'" type="button" style="width: 100%; height: 25px;" class="ASPButton"><i class="fa fa-edit"></i>&nbsp;NEW PARAMETER</button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
