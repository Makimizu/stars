<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice_Email_Paid.aspx.cs" Inherits="FINANCE.Form_Collection.Invoice_Email_Paid" %>
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
            <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table>
                                    <tr style="vertical-align: top;">
                                        <td>
                                            <table style="border-spacing: 0px;">
                                                <tr>
                                                    <td style="width: 120px;">APPLICATION</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>SENDED</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_SENDED" runat="server" CssClass="ASPDropDownList">
                                                            <asp:ListItem Value="FIRST_SEND is null">NOT SENDED</asp:ListItem>
                                                            <asp:ListItem Value="FIRST_SEND is not null ">SENDED</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr style="visibility:hidden">
                                                    <td>INVOICE TYPE</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr style="visibility:hidden">
                                                    <td>INVOICE NO</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_NO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td></td>
                                        <td>
                                            <table style="border-spacing: 0px;">
                                                <tr>
                                                    <td style="width: 100px;">POLICY NO</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_NOPOL" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>CUSTOMER NAME</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;">SETTLE DATE</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_INVDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_INVDATE">
                                                        </ajaxToolkit:CalendarExtender>
                                                        &nbsp;-
                                            <asp:TextBox ID="TXT_INVDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_INVDATE2">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td style="width: 100px;">DUE DATE</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_DUEDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DUEDATE">
                                                        </ajaxToolkit:CalendarExtender>
                                                        &nbsp;-
                                                        <asp:TextBox ID="TXT_DUEDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DUEDATE2">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td style="width: 100px;">FIRST SEND DATE</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_SENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_SENDDATE">
                                                        </ajaxToolkit:CalendarExtender>
                                                        &nbsp;-
                                                        <asp:TextBox ID="TXT_SENDDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_SENDDATE2">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td style="text-align: center;">
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" BorderColor="#003300" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged">
                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" Wrap="false" />
                        <Columns>
                            <%--<asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE NO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="INVOICE NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                            <asp:BoundColumn DataField="INVOICE_DATE" HeaderText="STL DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CUSTOMER_CODE" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="COMPANY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PAYMENT_AMOUNT" HeaderText="PAYMENT AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="SETTLE AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FIRST_SEND" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_SEND" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL_TIPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PIC_EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL_ENG" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_HST" runat="server" BackColor="Silver" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="H" />
                                                <asp:Button ID="BT_DETAIL" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="D" />
                                                <asp:Button ID="BT_COVLTR" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="L" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_EMAIL" runat="server" BackColor="Yellow" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="E" CommandName="Send" />
                                                <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LB_LASTSEND" runat="server" CssClass="ASPLabel" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="false" />
                        <HeaderStyle
                            Wrap="False" BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
