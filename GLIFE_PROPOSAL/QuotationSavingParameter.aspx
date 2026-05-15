<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationSavingParameter.aspx.cs" Inherits="GLIFE_PROPOSAL.QuotationSavingParameter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Bootstrap core CSS-->
    <link href="include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="include/css/controls.css" rel="stylesheet" />
    <link href="include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_QUOTNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_VERNO" runat="server" Visible="false"></asp:Label>

        <div class="row">
            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <table style="border-spacing: 0px; width: 100%; font-size: x-small; left: 0px; top: 0px;">
                        <tr>
                            <td>PERIOD START DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">TENOR in MONTHS</td>
                            <td>
                                <asp:TextBox ID="TXT_TENOR" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: right;"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>MAXIMUM AGE</td>
                            <td>
                                <asp:TextBox ID="TXT_MAXAGE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: right;"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>ADMIN FEE</td>
                            <td>
                                <asp:TextBox ID="TXT_ADMFEE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: right;"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>&permil; COI RATE</td>
                            <td>
                                <asp:TextBox ID="TXT_COIRATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: right;"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>% INV. ANNUAL RATE</td>
                            <td>
                                <asp:TextBox ID="TXT_INVRET" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: right;"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>% INV. CHARGE RATE</td>
                            <td>
                                <asp:TextBox ID="TXT_INVCHG" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: right;"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>CYCLE TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TENOR" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ButtonColor" Width="100px" OnClick="BT_SAVE_Click" OnClientClick="ShowProgress();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">

                    <asp:DataGrid ID="DGR_LOADING" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" PageSize="15">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="YEAR_SEQ" HeaderText="LOADING<BR>#YEAR">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SV_CLUN" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SV_CREG" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SV_TOP" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CONTR.<BR>LUNSUMP">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_CLUNRET" runat="server" CssClass="ASPTextBox" Width="50px" Style="text-align: right;"></asp:TextBox>&nbsp;%</td>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CONTR.<BR>REGULER">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_CREGRET" runat="server" CssClass="ASPTextBox" Width="50px" Style="text-align: right;"></asp:TextBox>&nbsp;%</td>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TOPUP<BR>REGULER">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_TOPRET" runat="server" CssClass="ASPTextBox" Width="50px" Style="text-align: right;"></asp:TextBox>&nbsp;%</td>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>

                    <asp:Button ID="BT_SAVE_LOADING" runat="server" Text="SAVE" CssClass="ButtonColor" Width="100px" OnClick="BT_SAVE_LOADING_Click" OnClientClick="ShowProgress();" />

                </div>
            </div>
        </div>


    </form>
</body>
</html>
