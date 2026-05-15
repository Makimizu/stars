<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_DEL.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Peserta_Deletion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="LB_COMPANYCODE" runat="server" Visible="False"></asp:Label>
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="border-bottom: ridge;">
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 80px;">
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
                </td>
            </tr>
            <tr id="TR_INSERT" runat="server" style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 80px;">PESERTA</td>
                            <td>
                                <asp:DropDownList ID="DDL_REGNO" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                                <asp:Button ID="BTN_CARI_REGNO_EMP" runat="server" CssClass="ASPButton" Text="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>TGL KELUAR</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" AutoPostBack="True" OnTextChanged="TXT_DATE_TextChanged" Width="80px" Style="text-align: center;"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Label ID="LB_TGLKELUAR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                <asp:Button ID="BT_ADD" runat="server" CssClass="ASPButton" OnClick="BT_ADD_Click" Text="SUBMIT" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_UPLOAD" runat="server" style="vertical-align: top;" visible="false">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>Gunakan template excel ini untuk upload data DELETION : <a href="GPA_DELETION.xls">GPA_DELETION.xls</a></td>
                        </tr>
                        <tr>
                            <td>
                                <input id="TXT_FILE_UPLOAD" runat="server" name="TXT_FILE_UPLOAD" type="file"
                                    class="ASPTextBox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" OnClick="BT_UPLOAD_Click" Text="UPLOAD EXCEL" Font-Bold="True" ForeColor="Blue" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_STATUS" runat="server" Font-Bold="True" ForeColor="Red" CssClass="ASPLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

