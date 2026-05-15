<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="REAS.Form_Company.Company" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
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
                                            <asp:TextBox ID="TXT_ADDRESS1" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>ADDRESS 2</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ADDRESS2" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
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
                                        <td>BANK NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BANKNAME" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BRANCH</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BANCH" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ACCOUNT NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BANK_ACCOUNTNO" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ACCOUNT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ACCOUNT_NAME" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                     <tr>
                                        <td>SWIFTCODE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_SWIFTCODE" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;">&nbsp;</td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 120px">PHONE</td>
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
                                        <td>LICENCE NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_LICENCENO" runat="server" CssClass="ASPTextBox" MaxLength="25" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>LICENCE DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_LICENCEDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_LICENCEDATE">
                                            </ajaxToolkit:CalendarExtender>
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>

