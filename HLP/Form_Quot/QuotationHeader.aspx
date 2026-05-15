<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationHeader.aspx.cs" Inherits="HLP.Form_Quot.QuotationHeader" %>

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
        <asp:Label ID="LB_READONLY" runat="server" Font-Bold="False" Visible="False"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">QUOT NO.</td>
                                        <td>
                                            <asp:Label ID="LB_QUOTNO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">VER NO.</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_VER" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_VER_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" OnClick="BT_NEW_Click" Text="NEW VER" />
                                            &nbsp;<asp:Button ID="BT_COPY" runat="server" CssClass="ASPButton" OnClick="BT_COPY_Click" Text="COPY VER" />
                                            &nbsp;<asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" Text="DELETE VER" Font-Bold="True" ForeColor="Red" OnClick="BT_DEL_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">COMPANY</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_COMPANY" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_VER_SelectedIndexChanged" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">PRODUCT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PRODUCT" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_VER_SelectedIndexChanged" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">TPA</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TPA" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_VER_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" GridLines="None">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GROUP_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_LEN" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GROUP_DESCR" HeaderText="GROUP" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ADD_DESCR" HeaderText="ADDITIONAL INFO">
                                            <ItemStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="VALUE">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_VAL" runat="server" CssClass="ASPDropDownList" disabled="disabled" Visible="False">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="354px"></asp:TextBox>
                                                <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Visible="False" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 80px;">AGENT</td>
                                        <td>
                                            <asp:Label ID="LB_AGENT" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CHANNEL</td>
                                        <td>
                                            <asp:Label ID="LB_CHANNEL" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CREATE BY</td>
                                        <td>
                                            <asp:Label ID="LB_CREATEBY" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CREATE DATE</td>
                                        <td>
                                            <asp:Label ID="LB_CREATEDATE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" />
                                <asp:Button ID="BT_VERIFY" runat="server" CssClass="ASPButton" OnClick="BT_VERIFY_Click" BackColor="OrangeRed" Text="VERIFY" />
                                <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px;">
                        <tr>
                            <td id="TD_01" runat="server" style="width: 10%;">
                                <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="TERM &amp; CONDITION" Width="100%" OnClick="BT1_Click" />
                            </td>
                            <td id="TD_02" runat="server" style="width: 10%;">
                                <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="PAKET &amp; PLAN" Width="100%" OnClick="BT2_Click" />
                            </td>
                            <td id="TD_06" runat="server" style="width: 10%;">
                                <asp:Button ID="BT6" runat="server" CssClass="ASPButton" Text="DEPOSIT &amp; FACTOR" Width="100%" OnClick="BT6_Click" />
                            </td>
                            <td id="TD_03" runat="server" style="width: 10%;">
                                <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Text="BENEFIT" Width="100%" OnClick="BT3_Click" />
                            </td>
                            <td id="TD_04" runat="server" style="width: 10%;">
                                <asp:Button ID="BT4" runat="server" CssClass="ASPButton" Text="MEMBER" Width="100%" OnClick="BT4_Click" />
                            </td>
                            <td id="TD_08" runat="server" style="width: 10%;">
                                <asp:Button ID="BT8" runat="server" CssClass="ASPButton" Text="BRANCH" Width="100%" OnClick="BT8_Click" />
                            </td>
                            <td id="TD_07" runat="server" style="width: 10%;">
                                <asp:Button ID="BT7" runat="server" CssClass="ASPButton" Text="CLOSING" Width="100%" OnClick="BT7_Click" />
                            </td>
                            <td id="TD_05" runat="server" style="width: 10%;">
                                <asp:Button ID="BT5" runat="server" CssClass="ASPButton" Text="CETAK" Width="100%" OnClick="BT5_Click" />
                            </td>
                            <td id="TD_09" runat="server" style="width: 10%;">
                                <asp:Button ID="BT9" runat="server" CssClass="ASPButton" Text="ARSIP" Width="100%" OnClick="BT9_Click" />
                            </td>
                            <td id="TD_10" runat="server" style="width: 10%;">
                                <asp:Button ID="Button1" runat="server" CssClass="ASPButton" Text="CINICAL PATHWAY" Width="100%" OnClick="BT10_Click" />
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
    </form>
</body>
</html>
