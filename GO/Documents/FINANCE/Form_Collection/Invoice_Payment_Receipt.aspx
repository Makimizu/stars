<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice_Payment_Receipt.aspx.cs" Inherits="FINANCE.Form_Collection.Invoice_Payment_Receipt" %>

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
                            <td style="width: 100px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged">
                                </asp:DropDownList>
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
                            <td>CUSTOMER NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CUSTOMER CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_CUSTCODE" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>SETTLE DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_STLDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STLDATE1">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-
                                            <asp:TextBox ID="TXT_STLDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STLDATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>SENDED</td>
                            <td>
                                <asp:DropDownList ID="DDL_SENDED" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="is null">NOT SENDED</asp:ListItem>
                                    <asp:ListItem Value="is not null">SENDED</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SUBMIT" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SUBMIT_Click" Width="100px" />
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
                            <asp:BoundColumn DataField="DOCNO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="RECEIPT NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_DOCNO" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SETTLE_DATE" HeaderText="SETTLE DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAYMENT_AMOUNT" HeaderText="PAYMENT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="COMPANY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE_DESCR" HeaderText="TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PIC_EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                    <asp:Button ID="BT_EMAIL" runat="server" CommandName="Send" CssClass="ASPButton" Text="SEND" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FIRST_SEND" HeaderText="FIRST SEND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_SEND" HeaderText="LAST SEND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL_TYPE" Visible="False"></asp:BoundColumn>
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
