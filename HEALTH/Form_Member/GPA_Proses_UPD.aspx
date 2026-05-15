<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_UPD.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Peserta_UpdateData" %>

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
        <table style="position: absolute; top: 0px; left: 0px;">
            <tr id="TR_UPLOAD" runat="server">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">DOWNLOAD TEMPLATE</td>
                            <td>
                                <asp:Button ID="BT_TEMPLATE" runat="server" CssClass="ASPButton" Text="XLS" BackColor="Green" ForeColor="White" OnClick="BT_TEMPLATE_Click" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td>UPLOAD DATA</td>
                            <td>
                                <asp:FileUpload ID="FU1" runat="server" CssClass="ASPButton" />
                                <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" Text="UPLOAD" OnClick="BT_UPLOAD_Click" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_ENTRY" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">PESERTA</td>
                            <td>
                                <asp:DropDownList ID="DDL_REGNO" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_REGNO_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Button ID="BTN_CARI_REGNO_EMP" runat="server" CssClass="ASPButton" Text="..." />
                                <asp:Button ID="BT_SET" runat="server" CssClass="ASPButton" OnClick="BT_SET_Click" Text="!" BackColor="Green" Font-Bold="True" ForeColor="White" />
                            </td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" PageSize="20" ShowHeader="False">
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR">
                                <ItemStyle Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FINANCIAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FIELD_NAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TABLE_NAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_LEN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_FIELD" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REF" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_VAL" runat="server" CssClass="ASPDropDownList" Visible="False">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="354px"></asp:TextBox>
                                    <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FINANCIAL_DESCR"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:Button ID="BT_SUBMIT" runat="server" CssClass="ASPButton" Font-Bold="True" OnClick="BT_SUBMIT_Click" Text="SUBMIT" />
                </td>

            </tr>
            <tr>
                <td>&nbsp;</td>

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
