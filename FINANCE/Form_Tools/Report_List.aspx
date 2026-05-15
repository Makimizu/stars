<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="V_LINK_SC_REPORT_LIST.aspx.cs" Inherits="FINANCE.Form_Tools.V_LINK_SC_REPORT_LIST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <table style="position: absolute; top: 0px; left: 0px">
        <tr>
            <td>
                PILIH REPORT :
            </td>
            <td>
                <asp:DropDownList ID="DDL_REPORT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList"
                    OnSelectedIndexChanged="DDL_REPORT_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="LB_GO" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
