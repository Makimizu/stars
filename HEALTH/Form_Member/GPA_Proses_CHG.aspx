<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_CHG.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Peserta_ChangePlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
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
        <table style="position: absolute; top: 0px; left: 0px; width: 100%;">
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
                </td>
            </tr>
            <tr id="TR_INSERT" runat="server" style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width:100px;">PESERTA</td>
                            <td>
                                <asp:DropDownList ID="DDL_REGNO" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_REGNO_SelectedIndexChanged" OnTextChanged="DDL_REGNO_TextChanged">
                                </asp:DropDownList>
                                <asp:Button ID="BTN_CARI_REGNO_EMP" runat="server" CssClass="ASPButton" Text="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>PLAN LAMA</td>
                            <td>
                                <asp:TextBox ID="TXT_OLD_PLAN" runat="server" CssClass="ASPTextBox" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Button ID="BT_PLAN" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_PLAN_Click" Text="!" />
                            </td>
                        </tr>
                        <tr>
                            <td >PLAN BARU</td>
                            <td>
                                <asp:DropDownList ID="DDL_PLAN" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >TGL MUTASI</td>
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
                            <td >&nbsp;</td>
                            <td>
                                <asp:Label ID="LB_TGLMUTASI" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                <asp:Button ID="BT_ADD" runat="server" CssClass="ASPButton" OnClick="BT_ADD_Click" Text="SUBMIT" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_UPLOAD" runat="server" visible="false" style="vertical-align: top;">
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td>Gunakan template excel ini untuk upload data CHANGE PLAN : <a href="GPA_CHANGEPLAN.xls">GPA_CHANGEPLAN.xls</a></td>
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
                    <asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_COMPANYCODE" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
