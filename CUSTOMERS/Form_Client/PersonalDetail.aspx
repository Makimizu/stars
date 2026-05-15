<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalDetail.aspx.cs" Inherits="CUSTOMERS.Form_Client.PersonalDetail" %>

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
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">FULL NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" MaxLength="255" Width="90%" Style="text-transform: uppercase;" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>GENDER</td>
                            <td>
                                <asp:DropDownList ID="DDL_SEX" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="M">MALE</asp:ListItem>
                                    <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>MOTHER MAIDEN NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_MMN" runat="server" MaxLength="255" Width="90%" Style="text-transform: uppercase;" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>DATE OF BIRTH</td>
                            <td>
                                <asp:TextBox ID="TXT_DOB" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" BackColor="#CCCCCC" Enabled="False"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>PLACE OF BIRTH</td>
                            <td>
                                <asp:TextBox ID="TXT_POB" runat="server" MaxLength="255" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>JOB</td>
                            <td>
                                <asp:DropDownList ID="DDL_JOB" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>RELIGION</td>
                            <td>
                                <asp:DropDownList ID="DDL_RELIGION" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>MARITAL STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_MARITAL" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ID TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_IDTYPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ID NO.</td>
                            <td>
                                <asp:TextBox ID="TXT_IDNO" runat="server" MaxLength="50" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TAX NO.</td>
                            <td>
                                <asp:TextBox ID="TXT_TAXNO" runat="server" MaxLength="50" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK NAME</td>
                            <td>
                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList" Width="90%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ACCOUNT NAME</td>
                            <td>
                                <asp:TextBox CssClass="ASPTextBox" ID="TXT_ACCNAMA" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ACCOUNT NUMBER</td>
                            <td>
                                <asp:TextBox CssClass="ASPTextBox" ID="TXT_ACCNO" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">CITIZENSHIP</td>
                            <td>
                                <asp:DropDownList ID="DDL_CITIZENSHIP" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>PHONE 1</td>
                            <td>
                                <asp:TextBox ID="TXT_PHONE1" runat="server" MaxLength="50" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PHONE 2</td>
                            <td>
                                <asp:TextBox ID="TXT_PHONE2" runat="server" MaxLength="50" Width="90%" CssClass="ASPTextBox"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>EMAIL</td>
                            <td>
                                <asp:TextBox ID="TXT_EMAIL" runat="server" MaxLength="100" Width="90%" CssClass="ASPTextBox"></asp:TextBox></td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>ADDRESS 1</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS1" runat="server" MaxLength="255" Width="90%" TextMode="MultiLine" Height="60px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>ADDRESS 2</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS2" runat="server" MaxLength="255" Width="90%" TextMode="MultiLine" Height="60px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CITY</td>
                            <td>
                                <asp:TextBox ID="TXT_CITY" runat="server" MaxLength="50" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PROVINCE</td>
                            <td>
                                <asp:DropDownList ID="DDL_PROVINCE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>COUNTRY</td>
                            <td>
                                <asp:DropDownList ID="DDL_COUNTRY" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ZIP CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_ZIPCODE" runat="server" MaxLength="10" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>        
    </form>
</body>
</html>
