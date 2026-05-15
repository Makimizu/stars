<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Note_Manual.aspx.cs" Inherits="FINANCE.Form_Collection.Note_Manual" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td style="width: 100px;">NOTE NO</td>
                <td>
                    <asp:TextBox ID="TXT_NOTENO" runat="server" CssClass="ASPTextBox" ReadOnly="True" Font-Bold="True" BackColor="#CCCCCC" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>APPLICATION</td>
                <td>
                    <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" Enabled="False"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>CUSTOMER</td>
                <td>
                    <asp:Label ID="LB_CUSTOMER" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>INVOICE NO</td>
                <td>
                    <asp:TextBox ID="TXT_INVOICENO" runat="server" CssClass="ASPTextBox" ReadOnly="True" Font-Bold="True" BackColor="#CCCCCC" Width="200px"></asp:TextBox>
                    <asp:Button ID="BT_HST" runat="server" BackColor="Silver" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="H" />
                </td>
            </tr>
            <tr>
                <td>NOTE DATE</td>
                <td>
                    <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" ReadOnly="True" Font-Bold="False" BackColor="#CCCCCC" Width="80px" Style="text-align: center;"></asp:TextBox></td>
            </tr>
            <tr>
                <td>OUTSTANDING</td>
                <td>
                    <asp:Label ID="LB_OUTSTANDING" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>D/C</td>
                <td>
                    <asp:DropDownList ID="DDL_DC" runat="server" CssClass="ASPDropDownList" BackColor="Yellow" AutoPostBack="True" OnSelectedIndexChanged="DDL_DC_SelectedIndexChanged">
                        <asp:ListItem Value="C">CREDIT</asp:ListItem>
                        <asp:ListItem Value="D">DEBET</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>REASON</td>
                <td>
                    <asp:DropDownList ID="DDL_REASON" runat="server" CssClass="ASPDropDownList" BackColor="Yellow">
                    </asp:DropDownList></td>
            </tr>
            <tr style="vertical-align: top;">
                <td>DESCRIPTION</td>
                <td>
                    <asp:TextBox ID="TXT_DESCR" runat="server" BackColor="Yellow" Height="100px" MaxLength="500" TextMode="MultiLine" Width="500px" CssClass="ASPTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>AMOUNT</td>
                <td>
                    <asp:TextBox ID="TXT_AMOUNT" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
