<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice_Adjustment.aspx.cs" Inherits="FINANCE.Form_Collection.Invoice_Adjustment" MaintainScrollPositionOnPostback="true" %>

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
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">APPLICATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList">
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
                                        <td>CUSTOMER NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CUSTOMER CODE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NOPOL" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 80px;">INVOICE DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_INVDATE" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_INVDATE">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_INVDATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_INVDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">A/R DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ARDATE" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ARDATE">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_ARDATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ARDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>AGING</td>
                                        <td>
                                            <asp:TextBox ID="TXT_AGE1" runat="server" CssClass="ASPTextBoxNumber" Width="30px"></asp:TextBox>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_AGE2" runat="server" CssClass="ASPTextBoxNumber" Width="30px"></asp:TextBox>
                                            &nbsp;DAYS</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
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
                            <asp:TemplateColumn HeaderText="INVOICE NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_INVOICENO" runat="server" CommandName="Show" CssClass="ASPLabel"></asp:LinkButton>
                                    &nbsp;<asp:Button ID="BT_RECEIPT" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="R" Visible="False" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CUSTOMER_CODE" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="CUSTOMER NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_DATE" HeaderText="INVOICE DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AR_DATE" HeaderText="A/R DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INVOICE_TYPE_DESCR" HeaderText="INVOICE TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="BILLED">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAYMENT_AMOUNT" HeaderText="PAYMENT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DISCOUNT" HeaderText="DISCOUNT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KOMISI" HeaderText="COMM" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPN" HeaderText="PPN" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPH" HeaderText="PPH" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ADJ_C" HeaderText="ADJ -" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICYFEE" HeaderText="POLICY FEE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAMPFEE" HeaderText="STAMP FEE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ADJ_D" HeaderText="ADJ +" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="WRITEOFF" HeaderText="WRITE OFF">

                                <HeaderStyle BackColor="Red" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />

                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DISCOUNT">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_DISC" runat="server" BackColor="Pink" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle BackColor="Red" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COMMISION">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_COMM" runat="server" BackColor="Pink" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle BackColor="Red" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PPN">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PPN" runat="server" BackColor="Pink" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle BackColor="Red" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PPH">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PPH" runat="server" BackColor="Pink" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle BackColor="Red" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ADJ -">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_ADJMIN" runat="server" BackColor="Pink" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle BackColor="Red" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="POLICY FEE">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_POLFEE" runat="server" BackColor="LightGreen" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle BackColor="Green" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="STAMP FEE">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_STAMPFEE" runat="server" BackColor="LightGreen" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle BackColor="Green" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ADJ +">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_ADJPLUS" runat="server" BackColor="LightGreen" CssClass="ASPTextBoxNumber" Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle BackColor="Green" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Font-Bold="True" Text="SAVE" />
                                </HeaderTemplate>
                                <ItemTemplate>                                    
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                    <asp:Button ID="BT_HST" runat="server" BackColor="Silver" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="H"/>
                                    <asp:Button ID="BT_WO" runat="server" BackColor="Red" CommandName="WriteOff" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="WO" Visible="False" />                                    
                                </ItemTemplate>
                            </asp:TemplateColumn>
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
