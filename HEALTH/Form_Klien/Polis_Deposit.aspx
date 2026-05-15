<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Deposit.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Deposit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        <!--
        .DataGridFixedHeader {
            position: relative;
            top: expression(this.offsetParent.scrollTop);
        }
        -->
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;">
            <tr id="TR_SEARCH" runat="server">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">TIPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>ASO</asp:ListItem>
                                    <asp:ListItem Value="EXC">EXCESS</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>PERUSAHAAN</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NO POLIS</td>
                            <td>
                                <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>STATUS POLIS</td>
                            <td>
                                <asp:DropDownList ID="DDL_INFORCE" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value=" convert(date,GETDATE()) &lt;= convert(date,a.END_DATE)">INFORCE</asp:ListItem>
                                    <asp:ListItem Value=" convert(date,GETDATE()) &gt; convert(date,a.END_DATE)">LAPSE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="CARI" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_DGR" runat="server">
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="POLICY_PERIOD_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="NO POLIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_DEPOSIT" HeaderText="TIPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="AWAL PERIODE">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="AKHIR PERIODE">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DEPOSIT_AMOUNT" HeaderText="DEPOSIT&lt;BR&gt;AWAL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOPUP" HeaderText="TOPUP">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="USED" HeaderText="DIGUNAKAN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCTRNS" HeaderText="ACCOUNT&lt;BR&gt;TRNS">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REFUND" HeaderText="REFUND">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SISA" HeaderText="SISA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PCT_SISA" HeaderText="% SISA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DEPOSIT_MIN" HeaderText="% BATAS">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CC3300" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COLOR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DETAIL_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENABLE_REFUND" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DETAIL" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="D" />
                                    &nbsp;<asp:Button ID="BT_TOPUP" runat="server" BackColor="Green" CommandName="TOPUP" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="TOPUP" />
                                    <asp:Button ID="BT_TRNS" runat="server" BackColor="Blue" CommandName="ACCOUNT TRANSFER" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="ACC TRNS" Visible="false" />
                                    <asp:Button ID="BT_REFUND" runat="server" BackColor="Red" CommandName="REFUND" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="REFUND" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="700px" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td class="TDBGColor">
                            <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom-style: ridge;">
                            <table style="border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">PEMEGANG POLIS</td>
                                    <td>
                                        <asp:Label ID="LB_COMPANY" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        <asp:Label ID="LB_POLICY_PERIOD_ID" runat="server" CssClass="ASPLabel" Font-Bold="True" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>NO POLIS</td>
                                    <td>
                                        <asp:Label ID="LB_POLICYNO" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PERIODE POLIS</td>
                                    <td>
                                        <asp:Label ID="LB_PERIOD" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>TIPE DEPOSIT</td>
                                    <td>
                                        <asp:Label ID="LB_TIPE" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="TR_TOPUP" runat="server" visible="false">
                        <td>
                            <table style="border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">DEPOSIT AWAL</td>
                                    <td>
                                        <asp:Label ID="LB_AWAL" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>SISA DEPOSIT</td>
                                    <td>
                                        <asp:Label ID="LB_SISA" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>NILAI TOPUP</td>
                                    <td>
                                        <asp:TextBox ID="TXT_TOPUP_AMOUNT" runat="server" BackColor="Yellow" CssClass="ASPTextBoxNumber" Font-Bold="True" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="BT_TOPUP_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" OnClick="BT_TOPUP_SAVE_Click" Text="SAVE" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="TR_ACCTRNS" runat="server" visible="false">
                        <td>
                            <table style="border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">TRANSFER AMOUNT</td>
                                    <td>
                                        <asp:TextBox ID="TXT_ACCTRNS_AMT" runat="server" BackColor="Yellow" CssClass="ASPTextBoxNumber" Font-Bold="True" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="vertical-align: top;">
                                    <td>TRANSFER TO</td>
                                    <td>
                                        <asp:UpdatePanel ID="UP_ACCTRANS" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="TXT_ACCTRANS_SEARCH" runat="server" CssClass="ASPTextBox" Width="150px" OnTextChanged="TXT_ACCTRANS_SEARCH_TextChanged" CausesValidation="True"></asp:TextBox>
                                                <asp:Button ID="BT_ACCTRANS_SEARCH" runat="server" CssClass="ASPButton" BackColor="Yellow" ForeColor="Black" Text="!" Font-Bold="True" OnClick="BT_ACCTRANS_SEARCH_Click" />
                                                <div style="OVERFLOW: auto; HEIGHT: 200px; width: 100%;">
                                                    <asp:DataGrid ID="DGR_ACCTRNS" runat="server" BorderColor="#003300" CellPadding="4" PageSize="10" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" OnPageIndexChanged="DGR_ACCTRNS_PageIndexChanged" Style="position: relative; top: expression(this.offsetParent.scrollTop);">
                                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                                                        <AlternatingItemStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                            Wrap="False" CssClass="ms-formlabel DataGridFixedHeader" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="POLICY_PERIOD_ID" Visible="False"></asp:BoundColumn>
                                                            <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LB_ACCTRANS_SELECT" runat="server" CommandName="Select">LinkButton</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="NO POLIS" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="TIPE_DEPOSIT" HeaderText="TIPE"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="PERIOD" HeaderText="PERIOD">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SISA" HeaderText="SISA">
                                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Right" />
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                        <EditItemStyle BackColor="#7C6F57" />
                                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                    </asp:DataGrid>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="BT_ACCTRNS_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" OnClick="BT_ACCTRNS_SAVE_Click" Text="SAVE" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="TR_REFUND" runat="server" visible="false">
                        <td>
                            <table style="border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">REFUND AMOUNT</td>
                                    <td>
                                        <asp:TextBox ID="TXT_REFUND_AMT" runat="server" BackColor="Yellow" CssClass="ASPTextBoxNumber" Font-Bold="True" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ACC NO</td>
                                    <td>
                                        <asp:TextBox ID="TXT_REFUND_ACCNO" runat="server" CssClass="ASPTextBox" Font-Bold="True" Width="150px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ACC NAME</td>
                                    <td>
                                        <asp:TextBox ID="TXT_REFUND_ACCNAME" runat="server" CssClass="ASPTextBox" Font-Bold="True" Width="150px" MaxLength="255"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ACC BANK</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_REFUND_ACCBANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="BT_REFUND_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" OnClick="BT_REFUND_SAVE_Click" Text="SAVE" />
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
