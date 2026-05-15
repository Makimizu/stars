<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicalLab.aspx.cs" Inherits="UWBOX.Form_Partners.MedicalLab" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 95%;">
            <tr>
                <td style="width: 120px">CODE</td>
                <td>
                    <asp:TextBox ID="TXT_CODE" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>NAME</td>
                <td>
                    <asp:TextBox ID="TXT_COMPANY_NAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>ADDRESS 1</td>
                <td>
                    <asp:TextBox ID="TXT_ADDRESS1" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="100%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>ADDRESS 2</td>
                <td>
                    <asp:TextBox ID="TXT_ADDRESS2" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="100%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>CITY</td>
                <td>
                    <asp:TextBox ID="TXT_KOTAMADYA" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>PROVINCE</td>
                <td>
                    <asp:DropDownList ID="DDL_PROPINSI" runat="server" CssClass="ASPDropDownList">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>ZIP CODE</td>
                <td>
                    <asp:TextBox ID="TXT_ZIPCODE" runat="server" CssClass="ASPTextBox" MaxLength="10" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>PHONE</td>
                <td>
                    <asp:TextBox ID="TXT_PHONE" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>FAX</td>
                <td>
                    <asp:TextBox ID="TXT_FAX" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>EMAIL</td>
                <td>
                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>PIC</td>
                <td>
                    <asp:TextBox ID="TXT_PIC1" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>PIC TITLE</td>
                <td>
                    <asp:TextBox ID="TXT_PICTITLE" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" />
                </td>
            </tr>

        </table>
        <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
