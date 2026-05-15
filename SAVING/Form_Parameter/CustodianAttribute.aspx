<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustodianAttribute.aspx.cs" Inherits="SAVING.Form_Parameter.CustodianAttribute" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">ATTRIBUTE</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 130px;">ATTRIBUTE 1</td>
                            <td>
                                <asp:TextBox ID="TXT1" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ATTRIBUTE 2</td>
                            <td>
                                <asp:TextBox ID="TXT2" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ATTRIBUTE 3</td>
                            <td>
                                <asp:TextBox ID="TXT3" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ATTRIBUTE 4</td>
                            <td>
                                <asp:TextBox ID="TXT4" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ATTRIBUTE 5</td>
                            <td>
                                <asp:TextBox ID="TXT5" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ATTRIBUTE 6</td>
                            <td>
                                <asp:TextBox ID="TXT6" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ATTRIBUTE 7</td>
                            <td>
                                <asp:TextBox ID="TXT7" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ATTRIBUTE 8</td>
                            <td>
                                <asp:TextBox ID="TXT8" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ATTRIBUTE 9</td>
                            <td>
                                <asp:TextBox ID="TXT9" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ATTRIBUTE 10</td>
                            <td>
                                <asp:TextBox ID="TXT10" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
