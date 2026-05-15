<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_PreRenewal.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_PreRenewal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
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
                                        <td>PERIODE</td>
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
                                    &nbsp;<asp:Button ID="BT_RESPOND" runat="server" BackColor="Green" CommandName="Respon" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="R" />
                                    &nbsp;<asp:Button ID="BT_QUOTATION" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="Q" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="AWAL PERIODE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="AKHIR PERIODE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE" HeaderText="NB/RN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MOP" HeaderText="MOP">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MEMBER" HeaderText="MEMBER">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RESERVE_BILLED" HeaderText="PREMIUM BILLED">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RESERVE_EARNED" HeaderText="PREMIUM EARNED">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LOADING" HeaderText="% LOADING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#6600FF" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TABARRU" HeaderText="% TABARRU">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#666666" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LOADING_AMT" HeaderText="LOADING AMT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#6600FF" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TABBARU_AMT" HeaderText="TABBARU AMT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#666666" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAIM_PAID" HeaderText="CLAIM">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REFUND_PREMIUM" HeaderText="REFUND PREMIUM">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAIM_RATIO" HeaderText="% CR">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MOP" HeaderText="MOP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT" HeaderText="PRODUCT">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TPA" HeaderText="TPA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SURPLUS" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAIM_RATIO_CALC" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="400px" Width="700px" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small" Text="POLICY PRERENEWAL RESPONDS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="LB_PERIOD" Visible="false"></asp:Label>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td>
                                        <table style="border-spacing: 0px; width: 100%;">
                                            <tr>
                                                <td style="width: 100px;">NO POLIS</td>
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
                                        </table>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="DGR_RESPOND" runat="server" BackColor="LightGoldenrodYellow" BorderColor="#996633"
                                BorderWidth="1px" CellPadding="2"
                                GridLines="Vertical" CssClass="ASPDatagrid"
                                AutoGenerateColumns="False" Width="100%" ForeColor="Black" AllowPaging="false" OnItemCommand="DGR_RESPOND_ItemCommand">
                                <ItemStyle Wrap="true" VerticalAlign="Top" BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <AlternatingItemStyle BackColor="PaleGoldenrod" VerticalAlign="Top" Wrap="true" />
                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundColumn DataField="POLICY_PERIOD_ID" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="UNIT_CODE" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DECISION" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="REMARK" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COLOR" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="UNIT_DESCR" HeaderText="DEPARTMENT"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="REMARK" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DDL_DECISION" runat="server" CssClass="ASPDropDownList" BackColor="Yellow"></asp:DropDownList>
                                            <asp:TextBox runat="server" ID="TXT_REMARK" TextMode="MultiLine" CssClass="ASPTextBox" Width="400px" Height="30px" BackColor="Yellow"></asp:TextBox><br />
                                            <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" CommandName="Save" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="300px" />
                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="USERBY" HeaderText="USER"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="USERDATE" HeaderText="DATE"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">TABBARU</td>
                                    <td>:</td>
                                    <td style="text-align: right">
                                        <asp:Label ID="LB_TABBARU" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>CLAIM PAID</td>
                                    <td>:</td>
                                    <td style="text-align: right">
                                        <asp:Label ID="LB_CLAIM" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>% CR DEFAULT</td>
                                    <td>:</td>
                                    <td style="text-align: right">
                                        <asp:Label ID="LB_CRVAL" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>% CR</td>
                                    <td>:</td>
                                    <td style="text-align: right">
                                        <asp:TextBox ID="TXT_CRVAL" runat="server" CssClass="ASPTextBoxNumber" Width="80px" Font-Bold="true" BackColor="Yellow"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align: right">
                                        <asp:Button ID="BT_CRSAVE" runat="server" Text="SAVE" CssClass="ASPButton" OnClick="BT_CRSAVE_Click" /></td>
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
