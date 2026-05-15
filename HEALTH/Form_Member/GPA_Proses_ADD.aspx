<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_ADD.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Proses_ADD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <<link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_BATCH_ID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="LB_COMPANYCODE" runat="server" Visible="False"></asp:Label>
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="border-bottom: ridge;">
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">
                                <asp:Label ID="LB_MODE" runat="server" Text="MODE" CssClass="ASPLabel" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_MODE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_MODE_SelectedIndexChanged">
                                    <asp:ListItem Value="INSERT">INSERT</asp:ListItem>
                                    <asp:ListItem Value="UPLOAD">UPLOAD</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr id="TR_INSERT" runat="server" style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">GRUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_GRUP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_GRUP_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>PESERTA UTAMA</td>
                            <td>
                                <asp:DropDownList ID="DDL_REGNO_EMP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_REGNO_EMP_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Button ID="BTN_CARI_REGNO_EMP" runat="server" CssClass="ASPButton" Text="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>NAMA</td>
                            <td>
                                <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BRANCH</td>
                            <td>
                                <asp:DropDownList ID="DDL_BRANCH" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>GENDER</td>
                            <td>
                                <asp:DropDownList ID="DDL_GENDER" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>VIP</td>
                            <td>
                                <asp:DropDownList ID="DDL_VIP" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="0">TIDAK</asp:ListItem>
                                    <asp:ListItem Value="1">YA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL LAHIR</td>
                            <td>
                                <asp:TextBox ID="TXT_DOB" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL MASUK</td>
                            <td>
                                <asp:TextBox ID="TXT_TGLMASUK" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TGLMASUK">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>PAKET</td>
                            <td>
                                <asp:DropDownList ID="DDL_PACKAGE" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>TIPE ID</td>
                            <td>
                                <asp:DropDownList ID="DDL_TIPEID" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>NOMOR ID</td>
                            <td>
                                <asp:TextBox ID="TXT_NOID" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TELEPON</td>
                            <td>
                                <asp:TextBox ID="TXT_PHONE" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>EMAIL</td>
                            <td>
                                <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>ACC BANK</td>
                            <td>
                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ACC NO</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ACC NAMA</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAMA" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SUBMIT" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_UPLOAD" runat="server" visible="false" style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <asp:Button ID="BT_EXCEL" runat="server" CssClass="ASPButton" OnClick="BT_EXCEL_Click" Text="DOWNLOAD EXCEL TEMPLATE FILE" Width="250px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="TXT_FILE_UPLOAD" runat="server" name="TXT_FILE_UPLOAD" type="file"
                                    class="ASPTextBox" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BT_UPLOAD" runat="server" Height="19px" OnClick="BT_UPLOAD_Click"
                                    Text="UPLOAD EXCEL MEMBER DATA" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Width="250px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
