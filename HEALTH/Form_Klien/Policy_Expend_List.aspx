<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Policy_Expend_List.aspx.cs" Inherits="HEALTH.Form_Klien.Policy_Expend_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.1.js"></script>
    <script src="../Scripts/sweetPolicy_Expend2.all.min.js"></script>
    <script type="text/javascript">
        function CheckNumeric() {
            return event.keyCode >= 48 && event.keyCode <= 57;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>POLICY NO&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICYNO" runat="server" MaxLength="50" Width="50%" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PERIODE POLIS&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;&nbsp;-&nbsp;&nbsp;
                                                    <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                                    </ajaxToolkit:CalendarExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>EXPEND NO&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="TXT_EXPENDNO" runat="server" MaxLength="50" Width="50%" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PROCESS DATE&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_PROCESSDATE1" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PROCESSDATE1">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;&nbsp;-&nbsp;&nbsp;
                                                    <asp:TextBox ID="TXT_PROCESSDATE2" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PROCESSDATE2">
                                                    </ajaxToolkit:CalendarExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>STATUS&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                <asp:ListItem Value="REG">REGISTER</asp:ListItem>
                                                <asp:ListItem Value="REJ">REJECT</asp:ListItem>
                                                <asp:ListItem Value="APP">APPROVE</asp:ListItem>
                                            </asp:DropDownList>
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
            <tr>
                <td>
                    <asp:Label ID="LB_RECORD" runat="server" Text="" Font-Bold="true" CssClass="ASPLabel"></asp:Label>
                    <br />
                    <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" OnClick="BT_XLS_Click" BackColor="Green" Font-Bold="True" ForeColor="White" />
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="NO POLIS">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server" CssClass="ASPLabel" CommandName="View"></asp:LinkButton>
                                    &nbsp;<asp:Button ID="BT_RESPOND" runat="server" BackColor="Green" CommandName="Respon" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="PROCESS" />
                                    &nbsp;<asp:Button ID="BT_QUOTATION" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="HIST" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="POLICY_PERIOD_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EXPENDNO" HeaderText="NO EXPEND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PERIOD" HeaderText="PERIODE POLIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="JUMLAH PENGAJUAN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NO" HeaderText="ACC NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NAME" HeaderText="ACC NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK" HeaderText="BANK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="USERBY" HeaderText="PROCESS BY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROCESS_DATE" HeaderText="PROCESS DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ROLBCK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="280px" Width="415px" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small" Text="EXPEND UJRAH RESERVE PROCESS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="LB_PERIOD" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="LB_EXPENDNO" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="LB_ROLBCK" Visible="false"></asp:Label>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td>
                                        <table style="border-spacing: 0px; width: 100%;">
                                            <tr>
                                                <td style="width: 140px;">NO POLIS</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="LB_POLICYNO" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>PERUSAHAAN</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="LB_COMPANY" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>PERIODE</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="LB_PERIODDATE" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>% CADANGAN UJROH</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="LB_CUVAL" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true" ForeColor="Blue"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>CADANGAN UJROH</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="LB_CADUJROH" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true" ForeColor="Green"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>SISA CADANGAN UJROH</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="LB_SISACADUJROH" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>PENGAJUAN PENCAIRAN</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_CAIR" runat="server" Text="" CssClass="ASPTextBoxNumber" Width="30%" Font-Bold="true" onkeypress="return CheckNumeric();" BackColor="LightYellow"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>ACCOUNT NO</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_ACCNO" runat="server" Text=""  MaxLength="50" Width="50%" CssClass="ASPTextBox" Font-Bold="true" onkeypress="return CheckNumeric();" BackColor="LightYellow"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ACCOUNT NAME</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_ACCNAME" runat="server" Text="" MaxLength="255" Width="80%" CssClass="ASPTextBox" Font-Bold="true" BackColor="LightYellow"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>BANK</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>ALASAN</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_REASON" runat="server" Text="" MaxLength="255" Width="100%" CssClass="ASPTextBox" Font-Bold="true" BackColor="Yellow"></asp:TextBox>
                                                </td>
                                            </tr
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: left">
                                                    <asp:Button ID="BT_CRSAVE" runat="server" Text="UPDATE" CssClass="ASPButton" OnClick="BT_CRSAVE_Click" ForeColor="White" BackColor="SteelBlue"/>&nbsp;&nbsp;
                                                    <asp:Button ID="BT_APPROVE" runat="server" Text="APPROVE" CssClass="ASPButton" OnClick="BT_Approve_Click" ForeColor="White" BackColor="ForestGreen"/>&nbsp;&nbsp;
                                                    <asp:Button ID="BT_REJECT" runat="server" Text="REJECT" CssClass="ASPButton" OnClick="BT_Reject_Click" ForeColor="White" BackColor="IndianRed"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan ="3">
                                                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                    <asp:Label ID="LB_PERIOD2" runat="server" Font-Bold="True" ForeColor="Red" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
