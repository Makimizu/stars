<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimRejectPending.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimRejectPending" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_CLAIMNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TRACK" runat="server" Visible="false"></asp:Label>
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <asp:Label ID="LB_MODE_DESCR" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">NILAI KLAIM
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_AMOUNTTP" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                <asp:Button ID="BT_SUBMITTP" runat="server" CssClass="ASPButton" OnClick="BT_SUBMITTP_Click" Text="SAVE" />
                                <asp:Label ID="LB_TP_ERROR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="true" EnableViewState="false"></asp:Label>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td style="width: 100px;">ALASAN</td>
                            <td>
                                <asp:DropDownList ID="LB_TP" runat="server" BackColor="Yellow" CssClass="ASPDropDownList" Width="400px">
                                </asp:DropDownList>
                                <asp:Button ID="BT_ADDREASON" runat="server" CssClass="ASPButton" OnClick="BT_ADDREASON_Click" Text="ADD" />
                                <asp:DataGrid ID="DGR_REASONTP" runat="server" CellPadding="2" PageSize="20" GridLines="None" CssClass="ASPDatagrid" BorderColor="Tan" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderWidth="1px" OnItemCommand="DGR_REASONTP_ItemCommand" ShowHeader="False" ForeColor="Black">
                                    <AlternatingItemStyle BackColor="PaleGoldenrod" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="ALASAN" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="KETERANGAN">
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px;">
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 100px;"><b>ALASAN</b>:</td>
                                                        <td>:</td>
                                                        <td>
                                                            <asp:Label ID="LB_REASON" runat="server" CssClass="ASPLabel"></asp:Label></td>
                                                    </tr>
                                                    <tr style="vertical-align: top;">
                                                        <td><b>KETERANGAN</b></td>
                                                        <td>:</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_TPREMARK" runat="server" CssClass="ASPTextBox" MaxLength="1000" Width="300px" TextMode="MultiLine" Height="40px"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_TPDEL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" BackColor="Red" Text="X" ForeColor="White" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" HorizontalAlign="Left" Wrap="False" />
                                    <ItemStyle VerticalAlign="Top" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr id="TR_DOK" runat="server" style="vertical-align: top;">
                            <td>DOKUMEN YG<br />
                                DIBUTUHKAN</td>
                            <td>
                                <asp:DropDownList ID="LB_DOK" runat="server" BackColor="Yellow" CssClass="ASPDropDownList" Width="400px"></asp:DropDownList>
                                <asp:Button ID="BT_ADDDOC" runat="server" CssClass="ASPButton" OnClick="BT_ADDDOC_Click" Text="ADD" />
                                <asp:DataGrid ID="DGR_DOK" runat="server" CellPadding="4" PageSize="20" GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_DOK_ItemCommand" ShowHeader="False" ForeColor="#333333">
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REQUEST_DATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REQUEST_BY" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMPLETE_DATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMPLETE_BY" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ARSIP_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="REMARK">
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px;">
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 100px;"><b>DOKUMEN</b></td>
                                                        <td>:</td>
                                                        <td>
                                                            <asp:Label ID="LB_DOC" runat="server" CssClass="ASPLabel"></asp:Label></td>
                                                    </tr>
                                                    <tr style="vertical-align: top;">
                                                        <td><b>KETERANGAN</b></td>
                                                        <td>:</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_DOKREMARK" runat="server" CssClass="ASPTextBox" Width="300px" TextMode="MultiLine" Height="30px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 100px;">REQ. DATE</td>
                                                        <td>:</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_DOKREQ" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="calDOKREQ" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOKREQ">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr style="vertical-align: top;">
                                                        <td>COMP. DATE</td>
                                                        <td>:</td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_DOKCOM" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="calDOKCOM" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOKCOM">
                                                            </ajaxToolkit:CalendarExtender>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_DOKDEL" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" BackColor="Red" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                                    <ItemStyle BackColor="#E3EAEB" VerticalAlign="Top" Wrap="false" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
