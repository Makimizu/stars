<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_Setup.aspx.cs" Inherits="HEALTH.Form_Tools.Report_Setup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <table class="style1">
        <tr>
            <td align="center">
                <b>ROLES : </b>
                <asp:DropDownList ID="DDL_ROLES" runat="server" CssClass="ASPDropDownList" 
                    AutoPostBack="True" onselectedindexchanged="DDL_ROLES_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table >
                    <tr>
                        <td>
                            <asp:ListBox ID="LB1" runat="server" CssClass="ASPDropDownList" Height="600px" 
                                Width="300px"></asp:ListBox>
                        </td>
                        <td>
                            <asp:Button ID="BT_INSERT" runat="server" CssClass="ASPButton" 
                                onclick="BT_INSERT_Click" Text="PILIH &gt;&gt;" Width="140px" />
                            <br />
                            <br />
                            <asp:Button ID="BT_DELETE" runat="server" CssClass="ASPButton" 
                                onclick="BT_DELETE_Click" Text="&lt;&lt; HAPUS" Width="140px" />
                        </td>
                        <td>
                            <asp:ListBox ID="LB2" runat="server" CssClass="ASPDropDownList" Height="600px" 
                                Width="300px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
