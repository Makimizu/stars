<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Cancellation.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Cancellation" %>

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
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 100px;">NO POLIS</td>
                            <td>
                                <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PERUSAHAAN</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BATAL</td>
                            <td>
                                <asp:DropDownList ID="DDL_CANCEL" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_CANCEL_SelectedIndexChanged">
                                    <asp:ListItem Value="0">BELUM</asp:ListItem>
                                    <asp:ListItem Value="1">SUDAH</asp:ListItem>
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
            <tr>
                <td>
                    <asp:Label ID="LB_RECORD" runat="server" Text="" Font-Bold="true" CssClass="ASPLabel"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#CC3300" CellPadding="2" PageSize="40" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="Black" OnItemCommand="DGR_ItemCommand" BackColor="LightGoldenrodYellow" BorderWidth="1px">
                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <ItemStyle Wrap="False" />
                        <AlternatingItemStyle BackColor="PaleGoldenrod" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" Wrap="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="POLICY NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB" runat="server" CommandName="View"></asp:LinkButton>
                                    &nbsp;<asp:Button ID="BT_CANCEL" runat="server" CommandName="Cancel" CssClass="ASPButton" Text="C" BackColor="Red" Font-Bold="True" ForeColor="White" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="AWAL<BR>PERIODE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="AKHIR<BR>PERIODE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_DESCR" HeaderText="NB/RN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MOP_DESCR" HeaderText="MOP">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGEN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROCESSDATE" HeaderText="TGL PROSES"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="Tan" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    </asp:DataGrid>

                    <asp:DataGrid ID="DGR_CANCELED" runat="server" BorderColor="Maroon" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" OnItemCommand="DGR_CANCELED_ItemCommand">
                        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="White" />
                        <HeaderStyle BackColor="Red" Font-Bold="True" ForeColor="White" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="POLICY NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB0" runat="server" CommandName="View"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="AWAL<BR>PERIODE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="AKHIR<BR>PERIODE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CANCEL_DATE" HeaderText="TGL BATAL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="USERBY" HeaderText="USER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" HeaderText="CATATAN">
                                <HeaderStyle Width="400px" />
                                <ItemStyle Width="400px" Wrap="true" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="260px" Width="600px" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small" Text="PEMBATALAN"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%; border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">NO POLIS</td>
                                    <td>
                                        <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="LB_PERIODID" runat="server" Visible="false"></asp:Label></td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>PERUSAHAAN</td>
                                    <td>
                                        <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PERIODE</td>
                                    <td>
                                        <asp:Label ID="LB_PERIODDATE" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>TGL BATAL</td>
                                    <td>
                                        <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Yellow" style="text-align:center;"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr style="vertical-align:top;">
                                    <td>ALASAN</td>
                                    <td>
                                        <asp:TextBox ID="TXT_REMARK" runat="server" TextMode="MultiLine" MaxLength="4000" Width="100%" Height="100px" BackColor="Yellow"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="BT_REMARKSAVE" runat="server" Text="SAVE" CssClass="ASPButton" OnClick="BT_REMARKSAVE_Click"/></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LB_ERROR" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
