<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustodianCompany.aspx.cs" Inherits="SAVING.Form_Parameter.CustodianCompany" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 150px">CODE
                </td>
                <td>
                    <asp:TextBox ID="TXT_CODE" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>COMPANY NAME
                </td>
                <td>
                    <asp:TextBox ID="TXT_COMPANY_NAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>COMPANY TYPE
                </td>
                <td>
                    <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>COMPANY CATEGORY
                </td>
                <td>
                    <asp:DropDownList ID="DDL_CATEGORY" runat="server" CssClass="ASPDropDownList">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>LINE OF BUSINESS
                </td>
                <td>
                    <asp:DropDownList ID="DDL_LOB" runat="server" CssClass="ASPDropDownList">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>ADDRESS 1</td>
                <td>
                    <asp:TextBox ID="TXT_ADDRESS1" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>ADDRESS 2</td>
                <td>
                    <asp:TextBox ID="TXT_ADDRESS2" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>CITY</td>
                <td>
                    <asp:TextBox ID="TXT_KOTAMADYA" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="90%"></asp:TextBox>
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
                <td>PIC 1</td>
                <td>
                    <asp:TextBox ID="TXT_PIC1" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>PIC 2</td>
                <td>
                    <asp:TextBox ID="TXT_PIC2" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>PIC TITLE</td>
                <td>
                    <asp:TextBox ID="TXT_PICTITLE" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>REGISTERED DATE</td>
                <td>
                    <asp:TextBox ID="TXT_REGDATE" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="150px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>NPWP</td>
                <td>
                    <asp:TextBox ID="TXT_NPWP" runat="server" CssClass="ASPTextBox" MaxLength="25" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" Width="100" />
                </td>
            </tr>
        </table>
        <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
