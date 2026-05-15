<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancellationEmail.aspx.cs" Inherits="FINANCE.Form_Collection.CancellationEmail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Send Email - Cancellation Member</title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
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
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">APPLICATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APPLICATION_TYPE" runat="server" CssClass="ASPDropDownList" Width="90%">
                                                <asp:ListItem Value="">SELECT</asp:ListItem>
                                                <asp:ListItem Value="1">CORPORATE LIFE</asp:ListItem>
                                                <asp:ListItem Value="2">HEALTH</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">SENDED</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS_EMAIL" runat="server" CssClass="ASPDropDownList" Width="90%">
                                                <asp:ListItem Value="">SELECT</asp:ListItem>
                                                <asp:ListItem Value="1">SENDED</asp:ListItem>
                                                <asp:ListItem Value="2">NOT SENDED</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">INVOICE NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_INVOICE_NO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">REGNO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_REGNO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>FULLNAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>COMPANY NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">INVOICE TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_INVOICE_TYPE" runat="server" CssClass="ASPDropDownList" Width="90%">

                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>INVOICE DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_INVOICE_DATE_START" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_INVOICE_DATE_START">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_INVOICE_DATE_END" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_INVOICE_DATE_END">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CANCEL DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CANCEL_DATE_START" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CANCEL_DATE_START">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_CANCEL_DATE_END" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CANCEL_DATE_END">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SEND DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_SEND_DATE_START" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="yyyy-MM-dd" TargetControlID="TXT_SEND_DATE_START">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_SEND_DATE_END" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="yyyy-MM-dd" TargetControlID="TXT_SEND_DATE_END">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>AGENT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_AGENT_NAME" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BRANCH</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BRANCH_NAME" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" Width="100px" OnClick="BT_SEARCH_Click" />
                                            <asp:Button ID="BTN_EXPORT" runat="server" CssClass="ASPButton" Text="EXPORT EXCEL" Width="100px" OnClick="BTN_EXPORT_Click" />
                                            <asp:Label ID="LB_TRACK" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" 
                        OnPageIndexChanged="DGR_PageIndexChanged"
                        Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="REGNO" >
                                <ItemTemplate>
                                    <a href="<%# Eval("REPORT_URL") %>" target="_blank"><%# Eval("REGNO") %></a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULL NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BRANCH_NAME" HeaderText="BRANCH"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO "></asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE" HeaderText="INVOICE TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_DATE" HeaderText="INVOICE DATE" DataFormatString = "{0:yyyy-MM-dd}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CANCEL_DATE" HeaderText="CANCEL DATE" DataFormatString = "{0:yyyy-MM-dd}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGING" HeaderText="AGING CANCEL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_TRACK_DESCR" HeaderText="LAST STATUS"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="EMAIL TO">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" Width="200px" Text='<%# Eval("RECIPIENT") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="EMAIL CC">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL_CC" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CB_ALL" runat="server" CssClass="ASPTextBox" Text="ALL" AutoPostBack="True" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="true">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="BTN_SEND" runat="server" CssClass="ASPButton" CommandName="Send" Text="SEND" ForeColor="White" BackColor="Red" ToolTip="Send Email" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="SENDED" HeaderText="REPORT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEND_DATE" HeaderText="SEND DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REASON" HeaderText="REASON">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_CODE" HeaderText="REPORT CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCNO" HeaderText="DOCNO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REGNO" HeaderText="REGNO" Visible="false"></asp:BoundColumn>
                            
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>

</body>
</html>
