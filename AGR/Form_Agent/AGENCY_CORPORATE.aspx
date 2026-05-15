<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENCY_CORPORATE.aspx.cs" Inherits="AGR.AGENCY_CORPORATE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 130px">CODE</td>
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
                        <tr>
                            <td>TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>CATEGORY</td>
                            <td>
                                <asp:DropDownList ID="DDL_CATEGORY" runat="server" CssClass="ASPDropDownList" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>LINE OF BUSINESS</td>
                            <td>
                                <asp:DropDownList ID="DDL_LOB" runat="server" CssClass="ASPDropDownList" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>ADDRESS 1</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS1" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="300px" Height="30px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>ADDRESS 2</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS2" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="300px" Height="30px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CITY</td>
                            <td>
                                <asp:TextBox ID="TXT_KOTAMADYA" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PROVINCE</td>
                            <td>
                                <asp:DropDownList ID="DDL_PROPINSI" runat="server" CssClass="ASPDropDownList" Width="200px">
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
                            <td>AREA</td>
                            <td>
                                <asp:DropDownList ID="DDL_AREA" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 130px">PHONE</td>
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
                            <td>PIC TITLE</td>
                            <td>
                                <asp:TextBox ID="TXT_PICTITLE" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>REG. DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_REGDATE" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="150px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TAX NO</td>
                            <td>
                                <asp:TextBox ID="TXT_NPWP" runat="server" CssClass="ASPTextBox" MaxLength="25" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>LAST CHANGED BY</td>
                            <td>
                                <asp:TextBox ID="TXT_LASTCHANGEBY" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="150px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>LAST CHANGED DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_LASTCHANGEDATE" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="150px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Width="100" Text="SAVE" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div id="DV_BUTTONS" runat="server">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Button ID="BT1" runat="server" CssClass="ButtonColor" Text="RO" Width="100%" OnClick="BT1_Click" Font-Size="X-Small" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:Button ID="BT2" runat="server" CssClass="ButtonColor" Text="PARAM REMUN" Width="100%" OnClick="BT2_Click" Font-Size="X-Small" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:Button ID="BT4" runat="server" CssClass="ButtonColor" Text="AGENT" Width="100%" OnClick="BT4_Click" Font-Size="X-Small" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:Button ID="BT3" runat="server" CssClass="ButtonColor" Text="ARCHIEVE" Width="100%" OnClick="BT3_Click" Font-Size="X-Small" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #DCDCDC; color: black; border: 2px groove #FFFFFF; text-align: center;">
                        <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="x-Small"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
