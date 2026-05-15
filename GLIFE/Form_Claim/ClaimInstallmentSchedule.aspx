<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimInstallmentSchedule.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimInstallmentSchedule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Class/Global.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 54px;
        }
    </style>
</head>
<body>
    <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="LB_READONLY" runat="server" Visible="false"></asp:Label>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td class="auto-style1">
                    <table style="top: 0px; left: 0px; border-spacing: 0px;">
                        <tr>
                            <td style="background-color: #CCCCCC; border-style: outset; text-align: left; width: 100px;">PAID DATE</td>
                            <td style="background-color: #CCCCCC; border-style: outset; text-align: right; width: 100px;">AMOUNT
                            </td>
                            <td style="width: 100px;"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TXT_PAIDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PAIDDATE">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_AMOUNT" runat="server" Width="100px" BackColor="#d2e9ff" Style="FONT-FAMILY: Tahoma; FONT-SIZE: xx-small; text-align: right; -webkit-border-radius: 5px; -moz-border-radius: 5px;"
                                        onkeypress="return CheckNumeric();" onkeyup="FormatCurrency(this);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="ADD" Width="100px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                        </table>
                </td>
                <td class="auto-style1"></td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:DataGrid ID="DGRQUERY" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
                        GridLines="Vertical" PageSize="20" ForeColor="#333333" BorderColor="#6699FF" OnItemCommand="DGRQUERY_ItemCommand" ShowHeader="False">
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="PAID_DATE">
                                <ItemStyle Width="150px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="60px" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" Text="X" ForeColor="White" BackColor="Red" CommandName="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

