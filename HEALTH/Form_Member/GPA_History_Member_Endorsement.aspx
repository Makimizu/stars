<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_History_Member_Endorsement.aspx.cs" Inherits="HEALTH.Form_Member.GPA_History_Member_Endorsement" MaintainScrollPositionOnPostback="true" %>

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
                        <tr>
                            <td>TIPE ENDORSEMENT</td>
                            <td>
                                <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">BATCH ID</td>
                            <td>
                                <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NAMA PESERTA</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PERUSAHAAN</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NO POLIS</td>
                            <td>
                                <asp:TextBox ID="TXT_POLICY_NO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>   
                        <tr>
                            <td>TGL EFEKTIF</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                        </ajaxToolkit:CalendarExtender>
                                        &nbsp;-
                                                    <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="CARI" OnClick="BT_SEARCH_Click" />
                                <asp:Label ID="LB_ERROR" runat="server" EnableViewState="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RECORD" runat="server" Text="" Font-Bold="true" CssClass="ASPLabel"></asp:Label>
                    <br />
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="20" OnPageIndexChanged="DGR_PageIndexChanged"
                        AllowPaging="True" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_VIEW" runat="server" CssClass="ASPButton" Font-Bold="True" Text="PREVIEW" />
                                    <asp:Button ID="BT_REPORT" runat="server" CssClass="ASPButton" Font-Bold="True" Text="REPORT" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="BATCH_ID" HeaderText="BATCH ID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_ENDORS" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_ENDORS_DESCR" HeaderText="TIPE ENDORSEMENT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCNO" HeaderText="DOCUMENT NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CNT" HeaderText="#">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_BATCH" HeaderText="TGL. BATCH">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_STATUS" HeaderText="TGL. STATUS">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_SSRS_URL" Visible="False"></asp:BoundColumn>
                            
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
