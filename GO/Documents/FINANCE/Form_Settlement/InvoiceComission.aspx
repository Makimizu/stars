<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceComission.aspx.cs" Inherits="FINANCE.Form_Settlement.InvoiceComission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 150px;">ALREADY PROCESSED</td>
                            <td>
                                <asp:DropDownList ID="DDL_DONE" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>INVOICE OUTSTANDING</td>
                            <td>
                                <asp:DropDownList ID="DDL_OUTS" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="=0">= 0</asp:ListItem>
                                    <asp:ListItem Value=">0">&gt; 0</asp:ListItem>
                                    <asp:ListItem Value="">-- ALL --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>DESCRIPTION</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BENEFICIARY</td>
                            <td>
                                <asp:TextBox ID="TXT_BENEF" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>REQUEST DATE</td>
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
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="CARI" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" OnItemCommand="DGR_ItemCommand" AutoGenerateColumns="False">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_PROSES" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="PROCEED" CommandName="Proceed" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="INVOICENO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="INVOICE">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 40px">NO</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_INVOICENO" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>OUTST</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_OUTS" runat="server" CssClass="ASPLabel" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-Width="250px">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                                <ItemStyle Wrap="True" Width="210px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFICIARY" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_BANK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="BENEFICIARY">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 70px">NAME</td>
                                            <td>
                                                <asp:TextBox ID="TXT_BENEF" runat="server" CssClass="ASPTextBox" Width="180px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>ACC NO</td>
                                            <td>
                                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" Width="120px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>ACC NAME</td>
                                            <td>
                                                <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" Width="180px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>ACC BANK</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_BENSAVE" runat="server" Text="SAVE" CssClass="ASPButton" CommandName="SaveBenef"/></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REQUEST" HeaderText="REQUEST"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REKAPID" HeaderText="SETTLEMENT<BR>DOC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRXID" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
