<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StlPaidDoneTrx.aspx.cs" Inherits="FINANCE.Form_Settlement.StlPaidDoneTrx" %>

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
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">
                                            <asp:Label ID="Label5" runat="server" Text="APPLICATION" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>TRANSACTION TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DOC NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>EXTERNAL DOC NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_EXTDOCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>MEMBER NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>COMPANY NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">POLICY NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>#ACC</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BENEFICIARY</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BENEF" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TRANSFER DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                            </ajaxToolkit:CalendarExtender>
                                            -
                                            <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                            </ajaxToolkit:CalendarExtender>
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
                            <td>

                    <asp:Label ID="LB_RECORD" runat="server" Text="" Font-Bold="true" CssClass="ASPLabel"></asp:Label>

                            </td>
                        </tr>
                    </table>


                </td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="40" OnPageIndexChanged="DGR_PageIndexChanged"
                        AllowPaging="True" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="DOC NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="DOCNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EXTDOC" HeaderText="EXTERNAL DOC NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="MEMBER NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POST_DATE" HeaderText="TRANSFER DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFICIARY" HeaderText="BENEFICIARY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NO" HeaderText="#ACC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK" HeaderText="BANK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RK_DESCR" HeaderText="BANK STATEMENT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REKAPID" Visible="False"></asp:BoundColumn>
                        </Columns>
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

