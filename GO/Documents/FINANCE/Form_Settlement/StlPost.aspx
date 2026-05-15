<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StlPost.aspx.cs" Inherits="FINANCE.Form_Settlement.StlPost" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />

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
                                    <tr id="TR_APPID" runat="server">
                                        <td style="width: 120px;">APPLICATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr id="TR_TIPE" runat="server">
                                        <td>TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_TIPE_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>ACC SOURCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ACCSOURCE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_ACCSOURCE_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>INHOUSE/CLEARING</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_BANKCONN" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value="and a.ACC_SOURCE_BANK_CODE = a.ACC_BANK_CODE">INHOUSE</asp:ListItem>
                                                <asp:ListItem Value="and a.ACC_SOURCE_BANK_CODE &lt;&gt; a.ACC_BANK_CODE">CLEARING</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">DOC NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>APPROVAL DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BENEFICIARY</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BENEF" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
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
                        OnPageIndexChanged="DGR_PageIndexChanged" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Button ID="BT_FILE_ALL" runat="server" BackColor="Blue" CommandName="IBALL" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="IB" />

                                    <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnCheckedChanged="CB_ALL_CheckedChanged" />

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="BT_SETTLE" runat="server" BackColor="Lime" CommandName="Settle" CssClass="ASPButton" Font-Bold="True" ForeColor="Black" Text="S" />
                                    <asp:Button ID="BT_FILE" runat="server" BackColor="Blue" CommandName="IB" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="IB" Visible="False" />
                                    <asp:Button ID="BT_SLIP" runat="server" BackColor="#006600" CommandName="TL" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="TL" />
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DOC NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_REKAPID" runat="server" CssClass="ASPLabel" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REKAPID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PAYMENT_METHOD" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CHARGE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CNT" HeaderText="#">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="30px" />
                                <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NO" HeaderText="ACC NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NAME" HeaderText="ACC NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_BANK" HeaderText="ACC BANK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFICIARY" HeaderText="BENEFICIARY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_SETTLEMENT_DESCR" HeaderText="TRANS. TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="APPROVALBY" HeaderText="APPROVAL<BR>BY">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="APPROVALDATE" HeaderText="APPROVAL<BR>DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CHARGE">
                                <HeaderTemplate>
                                    CHARGE<br />
                                    <asp:Button ID="BT_SAVECHARGE" runat="server" CommandName="SaveCharge" CssClass="ASPButton" Text="SAVE" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_CHARGE" runat="server" BackColor="Yellow" CssClass="ASPTextBoxNumber" Font-Bold="True" ForeColor="Red" Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TOTAL" HeaderText="TOTAL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important; opacity: 0.9; ">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="300px" Width="600px" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td>
                                        <table style="border-spacing: 0px;">
                                            <tr>
                                                <td style="width: 100px;">DOC NO</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_DOCNO" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ACC NO</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_ACCNO" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ACC NAME</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_ACCNAME" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ACC BANK</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_ACCBANK" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>TRANS. TYPE</td>
                                                <td>
                                                    <asp:Label runat="server" ID="LB_TRANSTYPE" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="DGR_DETAIL" runat="server" BackColor="LightGoldenrodYellow" BorderColor="#996633"
                                BorderWidth="1px" CellPadding="2"
                                GridLines="Vertical" CssClass="ASPDatagrid"
                                AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="600px" AllowPaging="True" ForeColor="Black" OnPageIndexChanged="DGR_DETAIL_PageIndexChanged">
                                <ItemStyle Wrap="true" VerticalAlign="Top" BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <AlternatingItemStyle BackColor="PaleGoldenrod" VerticalAlign="Top" Wrap="true" />
                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundColumn DataField="NBR" HeaderText="NO">
                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DOCNO" HeaderText="DOC NO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="RESERVED_PARAM1" HeaderText="REFF DOC NO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Red" />
                                    </asp:BoundColumn>
                                </Columns>
                                <FooterStyle BackColor="Tan" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
