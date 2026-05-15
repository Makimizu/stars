<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyHeader.aspx.cs" Inherits="CUSTOMERS.Form_Client.CompanyHeader" %>
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
                                        <td style="width: 120px">
                                            <asp:Label runat="server" ID="LB1" CssClass="ASPLabel" Text="KODE"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_CODE" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB2" CssClass="ASPLabel" Text="NAMA PERUSAHAAN"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY_NAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB4" CssClass="ASPLabel" Text="TIPE"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB5" CssClass="ASPLabel" Text="KATEGORI"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CATEGORY" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB6" CssClass="ASPLabel" Text="LINE OF BUSINESS"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_LOB" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB7" CssClass="ASPLabel" Text="ALAMAT 1"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_ADDRESS1" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB8" CssClass="ASPLabel" Text="ALAMAT 2"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_ADDRESS2" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB9" CssClass="ASPLabel" Text="KOTAMADYA"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_KOTAMADYA" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB10" CssClass="ASPLabel" Text="PROPINSI"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PROPINSI" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB11" CssClass="ASPLabel" Text="KODE POS"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_ZIPCODE" runat="server" CssClass="ASPTextBox" MaxLength="10" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;">&nbsp;</td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 120px">
                                            <asp:Label runat="server" ID="LB12" CssClass="ASPLabel" Text="TELEPON"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_PHONE" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB13" CssClass="ASPLabel" Text="FAX"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_FAX" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB14" CssClass="ASPLabel" Text="EMAIL"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB15" CssClass="ASPLabel" Text="PIC 1"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_PIC1" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB16" CssClass="ASPLabel" Text="PIC 2"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_PIC2" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB17" CssClass="ASPLabel" Text="JABATAN PIC"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_PICTITLE" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB18" CssClass="ASPLabel" Text="TGL REGISTRASI"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_REGDATE" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="150px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB19" CssClass="ASPLabel" Text="NPWP"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_NPWP" runat="server" CssClass="ASPTextBox" MaxLength="25" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="LB20" CssClass="ASPLabel" Text="STATUS"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="ASPDropDownList" Enabled="False">
                                            </asp:DropDownList>
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
            <tr id="TR_BUTTON" runat="server">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 33%;">
                                            <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="CABANG" Width="100%" OnClick="BT1_Click" />
                                        </td>
                                        <td style="width: 33%;">
                                            <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="AGEN" Width="100%" OnClick="BT2_Click" />
                                        </td>
                                        <td style="width: 33%;">
                                            <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Text="ARSIP" Width="100%" OnClick="BT3_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
