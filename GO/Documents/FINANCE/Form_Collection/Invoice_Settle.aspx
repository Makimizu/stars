<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice_Settle.aspx.cs" Inherits="FINANCE.Form_Collection.Invoice_Settle" MaintainScrollPositionOnPostback="true" %>

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
        <asp:Label ID="LB_MODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:DataGrid ID="DGR_INFO" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="500px">
                                    <ItemStyle VerticalAlign="Top" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle
                                        Wrap="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR">
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>Records</td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label ID="LB_RECORDS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Total Amount</td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label ID="LB_AMOUNTS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr id="TR_INV" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <table id="TBL_RESULT" runat="server" visible="false" style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">APPLICATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>INVOICE NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>VIRT. ACC</td>
                                        <td>
                                            <asp:TextBox ID="TXT_VA" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CUSTOMER NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NOPOL" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">INVOICE DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_INVDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_INVDATE">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_INVDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_INVDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>INVOICE TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BT_INV_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_INV_SEARCH_Click" Text="SEARCH" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TR_INVOICE_MASTER" runat="server">
                            <td>
                                <asp:DataGrid ID="DGR_INV" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="40"
                                    GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_INV_ItemCommand">
                                    <ItemStyle Wrap="False" />
                                    <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="#DCDCDC" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_STL" runat="server" BackColor="Green" CommandName="Settle" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE NO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ENABLE_DETAIL" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="VA" HeaderText="VIRT. ACC"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CUSTOMER_CODE" HeaderText="POLICY NO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="COMPANY"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="INVOICE_DATE" HeaderText="INVOICE DATE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="INVOICE_TYPE_DESCR" HeaderText="TYPE"></asp:BoundColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr id="TR_INVOCIE_DETAIL" runat="server" visible="false">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="LBT_BACK" runat="server" Text="Back ..." ForeColor="Red" OnClick="LBT_BACK_Click"></asp:LinkButton>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">INVOICE NO</td>
                                        <td>
                                            <asp:Label ID="LB_INVDET_INVOICENO" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY NO</td>
                                        <td>
                                            <asp:Label ID="LB_INVDET_POLICYNO" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>COMPANY</td>
                                        <td>
                                            <asp:Label ID="LB_INVDET_COMPANY" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>INVOICE DATE</td>
                                        <td>
                                            <asp:Label ID="LB_INVDET_INVOICEDATE" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>INVOICE TYPE</td>
                                        <td>
                                            <asp:Label ID="LB_INVDET_INVOICETYPE" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>OUTSTANDING</td>
                                        <td>
                                            <asp:Label ID="LB_INVDET_OUTSTANDING" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TOBE PAID</td>
                                        <td>
                                            <asp:Label ID="LB_INVDET_TOBEPAID" runat="server" Font-Bold="True" ForeColor="Blue" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:DataGrid ID="DGR_INV_DETAIL" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="40"
                                    GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_INV_DETAIL_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                    <ItemStyle Wrap="False" />
                                    <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="#DCDCDC" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="INVOICENO" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="APP_ID" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="INVOICE_TYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DOC_NO" HeaderText="MEMBER NO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MEMBER_NAME" HeaderText="MEMBER NAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Button ID="BT_INVDET_SETTLE" runat="server" Text="SETTLE" CssClass="ASPButton" CommandName="Settle" /><br />
                                                <asp:CheckBox ID="CB_ALL" runat="server" OnCheckedChanged="CB_ALL_CheckedChanged" AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB_INVDET" runat="server" AutoPostBack="True" OnCheckedChanged="CB_INVDET_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_RK" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">DESCRIPTION</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RK_DESCR" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POST DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POSTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_POSTDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_POSTDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BALANCE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BALANCE1" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_BALANCE2" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_RK_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_RK_SEARCH_Click" Text="SEARCH" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_RK" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="40"
                                    GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" OnItemCommand="DGR_RK_ItemCommand">
                                    <ItemStyle Wrap="False" />
                                    <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="#DCDCDC" />
                                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_STL" runat="server" BackColor="Green" CommandName="Settle" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="S" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="TRXID" HeaderText="TRX ID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BOOK_NAME" HeaderText="BOOK NAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="POST_DATE" HeaderText="POST DATE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
