<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationClosing.aspx.cs" Inherits="HLP.Form_Quot.QuotationClosing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Script/PleaseWait.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_SAVE_Click" Text="SAVE" Width="100px" />
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_ERROR" runat="server" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="4" ForeColor="#333333" BorderColor="#CC0000">
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle HorizontalAlign="Left" BackColor="#990000" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#FFFBD6" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:Label ID="LB_QUOTNO" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_VER" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_PROD" runat="server" Visible="False"></asp:Label>

                                <asp:DataGrid ID="DGR_ADD_INFO" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" GridLines="None">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GROUP_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_LEN" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GROUP_DESCR" HeaderText="GROUP" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ADD_DESCR" HeaderText="ADDITIONAL INFO">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="VALUE">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_VAL" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="354px"></asp:TextBox>
                                                <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Visible="False" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>

                                <br />
                                <asp:DataGrid ID="DGR_TERM" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="None" CellPadding="4" ForeColor="#333333" OnItemDataBound="DGR_TERM_ItemDataBound" ShowFooter="True">
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SEQ" HeaderText="TERMIN">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="INVOICE_DATE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="DUE DATE">
                                            <FooterTemplate>
                                                <table style="border-spacing: 0px;">
                                                    <tr>
                                                        <td>TOTAL INVOICE</td>
                                                        <td>:</td>
                                                    </tr>
                                                    <tr>
                                                        <td>TOTAL PREMIUM</td>
                                                        <td>:</td>
                                                    </tr>
                                                    <tr>
                                                        <td>DIFF</td>
                                                        <td>:</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="DUE_DATE" HeaderText="DUE DATE" Visible="False">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="AMOUNT">
                                            <FooterTemplate>
                                                <table style="border-spacing: 0px;">
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="LB_INV" runat="server" ForeColor="Yellow"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="LB_PREMIUM" runat="server" ForeColor="Yellow"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="LB_DIFF" runat="server" ForeColor="Yellow"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_INVAMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <ItemStyle BackColor="#E3EAEB" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>

                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <asp:DataGrid ID="DGR_AGENT" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="4" ForeColor="Black" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" VerticalAlign="Top" Wrap="false" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="COMM_TYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMM_PCT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AGENT_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMM_TYPE_DESCR" HeaderText="COMMISSION TYPE"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="AGENT CODE">
                                            <ItemStyle Width="60" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_AGENTCODE" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CHANNEL" HeaderText="CHANNEL"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="40" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_COMMVAL" runat="server" CssClass="ASPTextBoxNumber" Width="90%"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <ItemStyle BackColor="#F7F7DE" Wrap="false" VerticalAlign="Top" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                </asp:DataGrid>
                                <br />
                                <table style="border-spacing: 0px;">
                                    <tr style="vertical-align: top;">
                                        <td>
                                            <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="4" ForeColor="Black" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                                <FooterStyle BackColor="#CCCC99" />
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="LOADING_CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="READONLY" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="LOADING_DESCR" HeaderText="LOADING UJROH">
                                                        <HeaderStyle Width="150px" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="% VALUE">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_LOADING" runat="server" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Font-Size="Small" />
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                                        <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <ItemStyle BackColor="#F7F7DE" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                                                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            </asp:DataGrid>
                                        </td>
                                        <td>
                                            <asp:DataGrid ID="DGR_TABARRU" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="4" ForeColor="Black" OnItemDataBound="DGR_TABARRU_ItemDataBound" ShowFooter="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="LOADING_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>                                        
                                        <asp:BoundColumn DataField="LOADING_DESCR" HeaderText="LOADING TABARRU">
                                            <HeaderStyle Width="150px" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="% VALUE">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_LOADING_TABARRU" runat="server" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Font-Size="Small" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                            <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:BoundColumn>                  
                                    </Columns>
                                    <ItemStyle BackColor="#F7F7DE" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                </asp:DataGrid>
                                        </td>
                                        <td>
                                            <asp:DataGrid ID="DGR_PRM_TOTAL" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="2" ForeColor="Black" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" ShowHeader="False" OnItemDataBound="DGR_PRM_TOTAL_ItemDataBound" ShowFooter="True">
                                                <FooterStyle BackColor="Tan" />
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Tan" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="PaleGoldenrod" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="AMOUNT" Visible="False">
                                                        <ItemStyle Width="100px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR">
                                                        <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left" />
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <FooterTemplate>
                                                            <asp:Label ID="LB_AMOUNT_TOTAL" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="120px" Font-Bold="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
