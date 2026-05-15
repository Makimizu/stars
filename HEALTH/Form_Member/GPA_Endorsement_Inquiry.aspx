<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Endorsement_Inquiry.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Endorsement_Inquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">BATCH ID</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PERUSAHAAN</td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NO POLIS</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICY_NO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 130px;">TIPE ENDORSEMENT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TGL BATCH</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;-
                                                    <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                                    </ajaxToolkit:CalendarExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="CARI" OnClick="BT_SEARCH_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>

                    <asp:Label ID="LB_MODE" runat="server" Font-Bold="True" CssClass="ASPLabel" Visible="False"></asp:Label>
                    <asp:Label ID="LB_TIPE" runat="server" Font-Bold="True" CssClass="ASPLabel" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORD" runat="server" Text="" Font-Bold="true" CssClass="ASPLabel"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="20" OnPageIndexChanged="DGR_PageIndexChanged"
                        AllowPaging="True" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="BATCH ID">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_BATCH" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="BATCH_ID" HeaderText="BATCH ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_ENDORS_DESCR" HeaderText="TIPE&lt;BR&gt;ENDORSEMENT">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CNT" HeaderText="#">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="NO POLIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_BATCH" HeaderText="TGL. BATCH">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_ENDORS" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" CommandName="Delete" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
