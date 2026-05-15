<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationMCUInvoice.aspx.cs" Inherits="GLIFE.Form_App.ApplicationMCUInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Class/Global.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 33%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">INVOICE NO.</td>
                                        <td>
                                            <asp:TextBox ID="TXT_INVOICENO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                                            <asp:Label ID="LB_BATCHID" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>MEDICAL LAB</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_LAB" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>INVOICE DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_INVOICEDATE" runat="server" CssClass="ASPTextBox" Width="70px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_INVOICEDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>INVOICE AMOUNT</td>
                                        <td>
                                            <asp:TextBox ID="TXT_AMOUNT" runat="server" Width="100px" Style="FONT-FAMILY: Tahoma; FONT-SIZE: xx-small; text-align: right; -webkit-border-radius: 5px; -moz-border-radius: 5px;"
                                                onkeypress="return CheckNumeric();" onkeyup="FormatCurrency(this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%;">
                                <iframe id="IF1" runat="server" visible="false" style="width: 100%; height: auto; border: 0;"></iframe>
                            </td>
                            <td style="width: 33%;">
                                <center>
                                <asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" Text="APPROVE" ForeColor="Blue" Font-Bold="true" Width="120px" OnClick="BT_APPROVE_Click" />
                                </center>
                                <iframe id="IF2" runat="server" visible="false" style="width: 100%; height: auto; border: 0;"></iframe>
                                <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_DETAIL" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 33%;">
                                <table id="TBL_UNSELECTED" runat="server" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">UN-SELECTED APPLICATION
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="DGR_REGNO" runat="server" BackColor="White"
                                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_REGNO_ItemCommand" ShowHeader="False">
                                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBT_DESCR" runat="server" CommandName="Select"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="COMPANY_NAME"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="START_DATE">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CHARGE">
                                                        <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">SELECTED APPLICATION
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="DGR_REGNOSELECTED" runat="server" BackColor="White"
                                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_REGNOSELECTED_ItemCommand" ShowHeader="False">
                                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBT_DESCR2" runat="server" CommandName="Select"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="COMPANY_NAME"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="START_DATE">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CHARGE">
                                                        <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" CommandName="Delete" Text="X" ForeColor="White" BackColor="Red" ToolTip="Rollback to Quotation" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">ARCHIEVE
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <iframe id="IF" runat="server" src="" style="width: 98%; height: 50vh;"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
