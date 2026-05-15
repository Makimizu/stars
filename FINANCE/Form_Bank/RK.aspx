<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RK.aspx.cs" Inherits="FINANCE.Form_Bank.RK" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 120px;">ACC NO</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_NOREK" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>FLAG</td>
                                        <td>
                                            <asp:TextBox ID="TXT_VALIDATION" runat="server" CssClass="ASPTextBox" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">POST DATE</td>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="TXT_POSTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_POSTDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DESCRIPTION</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 120px;">UPLOAD BATCH</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BATCH" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DEBET</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DEBET1" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>&nbsp;-
                                            <asp:TextBox ID="TXT_DEBET2" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">CREDIT</td>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="TXT_CREDIT1" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>&nbsp;-
                                            <asp:TextBox ID="TXT_CREDIT2" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ORDER BY</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ORDERBY" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value="a.POST_DATE">POST DATE</asp:ListItem>
                                                <asp:ListItem Value="a.BALANCE">BALANCE</asp:ListItem>
                                                <asp:ListItem Value="a.CREDIT">CREDIT</asp:ListItem>
                                                <asp:ListItem Value="a.DEBET">DEBET</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DDL_ORDERSHORT" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem>ASC</asp:ListItem>
                                                <asp:ListItem>DESC</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" />
                                        &nbsp;<asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" OnClick="BT_XLS_Click" BackColor="Green" Font-Bold="True" ForeColor="White" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>Records</td>
                                        <td>
                                            <asp:Label ID="LB_RECORDS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Debet</td>
                                        <td>
                                            <asp:Label ID="LB_DEBET" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Credit</td>
                                        <td>
                                            <asp:Label ID="LB_CREDIT" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Green"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Balance</td>
                                        <td>
                                            <asp:Label ID="LB_BALANCE" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td>

                    <asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                        PageSize="40" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" ItemStyle-Wrap="true" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="false" />
                        <Columns>
                            <asp:BoundColumn DataField="TRXID" HeaderText="TRXID">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ENABLE_DEBET" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENABLE_REFUND" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POST_DATE" HeaderText="POST DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FLAG_DATE" HeaderText="BOOK DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION">
                                <HeaderStyle Width="400px" />
                                <ItemStyle Wrap="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DEBET" HeaderText="DEBET">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CREDIT" HeaderText="CREDIT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_VALIDASI_DESCR" HeaderText="FLAG">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOREK" HeaderText="ACC. NO">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK_DESCR" HeaderText="BANK">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE_AMOUNT" HeaderText="BANK" Visible="false">
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_HST" runat="server" BackColor="Silver" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="H"/>
                                    <asp:Button ID="BT_STL" runat="server" BackColor="Green" CommandName="Settle" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S" Visible="False" />
                                    <asp:Button ID="BT_RFD" runat="server" BackColor="Orange" CommandName="Refund" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="R" Visible="False" />
                                    <asp:Button ID="BT_FLAG" runat="server" BackColor="Blue" CommandName="Flag" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="F" Visible="False" />
                                    <asp:Button ID="BT_PINDAH_DANA" runat="server" BackColor="Blue" CommandName="Transfer" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="P" Visible="False" ToolTip="Pindah Dana" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
        <div style="background-color: white !important; opacity: 0.9;">
            <asp:Panel ID="FlagPopup" runat="server" BackColor="White" Height="120px" Width="600px" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none;">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                             <table style="border-spacing: 0px; width: 100%; height: 30px;">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Font-Bold="True" Font-Size="Small">Flagging Bank Statement</asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" onClientClick="document.getElementById('FlagPopup').style.display = 'none';return false;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%; background-color: #E7E7FF;">
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:DropDownList ID="DDL_FLAG" runat="server" CssClass="ASPDropDownList">
                                        </asp:DropDownList>
                                        <asp:Button ID="BT_SET" runat="server" BackColor="#0033CC" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S" OnClick="BT_SET_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="background-color: #4A3C8C; color: white; font-weight: bold;">
                                    <td style="padding: 5px; width: 100px;">TRXID</td>
                                    <td style="padding: 5px; width: 65px;">POST DATE</td>
                                    <td style="padding: 5px;">DESCRIPTION</td>
                                    <td style="text-align: right;padding: 5px;">AMOUNT</td>
                                </tr>
                                <tr style="background-color: #E7E7FF;">
                                    <td style="padding: 5px;"><asp:Label ID="LB_TRXID" runat="server"></asp:Label></td>
                                    <td style="padding: 5px;"><asp:Label ID="LB_POST_DATE" runat="server"></asp:Label></td>
                                    <td style="padding: 5px;"><asp:Label ID="LB_DESCRIPTION" runat="server"></asp:Label></td>
                                    <td style="text-align: right;padding: 5px;">
                                        <asp:Label ID="LB_AMOUNT" runat="server"></asp:Label>
                                        <asp:Label ID="LB_BALANCE_AMOUNT" runat="server" Visible="false"></asp:Label>
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
