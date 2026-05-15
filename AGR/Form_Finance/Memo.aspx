<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Memo.aspx.cs" Inherits="AGR.Form_Finance.Memo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="DV_TITLE" runat="server">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td class="TDBGColor">
                        <asp:Label ID="LB_TITLE" runat="server" CssClass="ASPLabel" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div id="DV_CONTENT" runat="server">
            <asp:Label ID="LB_APP" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>
            <table style="position: absolute; top: 20px; left: 0px; border-spacing: 0px;">
                <tr id="TR_STL1" runat="server">
                    <td>
                        <table style="border-spacing: 0px; font-size: x-small;">
                            <tr id="TR_TIPE" runat="server">
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="TIPE" CssClass="ASPLabel"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_TIPE_SelectedIndexChanged"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>AMOUNT RANGE</td>
                                <td>
                                    <asp:DropDownList ID="DDL_RANGE" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_RANGE_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px;">
                                    <asp:Label ID="Label1" runat="server" Text="DOC NO" CssClass="ASPLabel"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="DESCRIPTION" CssClass="ASPLabel"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="GENERATE DATE" CssClass="ASPLabel"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                    </ajaxToolkit:CalendarExtender>
                                    &nbsp;-
                                <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" Width="100px" />
                                    &nbsp;<asp:Button ID="BT_REKAP" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_REKAP_Click" Text="R" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="TR_STL2" runat="server">
                    <td>
                        <asp:Button ID="BT_APPROCE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_APPROCE_Click" Text="APPROVE" />
                        <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                            BorderWidth="1px" CellPadding="3" PageSize="40"
                            GridLines="Vertical" CssClass="ASPDatagrid"
                            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                            <ItemStyle Wrap="False" />
                            <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                            <AlternatingItemStyle BackColor="#DCDCDC" />
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="DOC NO">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LB_REKAPID" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="LB_ALL" runat="server" CommandName="All" CssClass="ASPLabel" ForeColor="White">ALL</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="REKAPID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AUTH" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESTINATION_BANK_DESCR" HeaderText="DESTINATION BANK"></asp:BoundColumn>
                                <asp:BoundColumn DataField="GENERATE" HeaderText="GENERATE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr id="TR_RFD" runat="server" visible="false">
                    <td>
                        <table style="border-spacing: 0px;">
                            <tr style="vertical-align: top;">
                                <td style="width: 100px;">DESCRIPTION</td>
                                <td>
                                    <asp:TextBox ID="TXT_RFD_DESCR" runat="server" CssClass="ASPTextBox" Height="40px" MaxLength="1000" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_PROCESS_RFD" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Green" OnClick="BT_PROCESS_RFD_Click" Text="SUBMIT REFUND" /></td>
                            </tr>
                        </table>
                        <asp:DataGrid ID="DGR_RFD" runat="server" BorderColor="#003300" CellPadding="4" PageSize="40"
                            GridLines="Vertical" CssClass="ASPDatagrid"
                            OnPageIndexChanged="DGR_PageIndexChanged" AutoGenerateColumns="False" OnItemCommand="DGR_RFD_ItemCommand" ForeColor="#333333">
                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="LB_ALL0" runat="server" CommandName="All" CssClass="ASPLabel" ForeColor="White">ALL</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB0" runat="server" CssClass="ASPTextBox" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="APP_ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TRXID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPE_SETTLEMENT" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CUSTOMER_CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DOCNO" HeaderText="DOC NO."></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COMPENSATED&lt;BR&gt;AMOUNT">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_AMOUNT" runat="server" BackColor="Yellow" CssClass="ASPTextBoxNumber" Font-Bold="True" Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="COMPANY" HeaderText="COMPANY"></asp:BoundColumn>
                            </Columns>
                            <EditItemStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <ItemStyle Wrap="False" BackColor="#E3EAEB" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                        </asp:DataGrid>

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
