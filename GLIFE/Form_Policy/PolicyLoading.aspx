<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyLoading.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyLoading" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table id="TBL_AGENT" runat="server" style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">AGENT COMMISSION</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_SAVE_AGENT" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE COMMISSION" Width="150px" OnClick="BT_SAVE_AGENT_Click" />
                    <asp:DataGrid ID="DGR_AGENT" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="4" ForeColor="Black" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT_CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="COMMISSION TYPE"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="AGENT CODE">
                                <HeaderStyle Width="100" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AGENTCODE" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CHANNEL" HeaderText="DIVISION IN CHARGE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="%">
                                <HeaderStyle Width="50" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_COMM" runat="server" CssClass="ASPTextBoxNumber" Width="90%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle BackColor="#F7F7DE" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>
                    <br />
                </td>
            </tr>
        </table>

        <table id="TBL_LOADING" runat="server" style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td id="TD_CHANNELS" runat="server">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">CHANNELS</td>
                        </tr>
                        <tr>
                            <td style="width: 300px;">
                                <asp:DataGrid ID="DGR_CHANNEL" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_CHANNEL_ItemCommand" ShowHeader="False">
                                    <ItemStyle VerticalAlign="Top" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle
                                        Wrap="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SUB_CODE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MARKET_SEGMENT_DESCR"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="SUB CHANNEL">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBT_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="VAL" HeaderText="% LOADING">
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Bold="true" Font-Italic="False" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="START_DATE">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">LOADING SET</td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 80px;">SUB CHANNEL</td>
                                        <td>
                                            <asp:Label ID="LB_CHANNELCODE" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LB_CHANNEL" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>LOADING</td>
                                        <td>
                                            <div style="width: 100%; height: 200px; overflow: auto;">
                                                <asp:DataGrid ID="DGR_LOADING" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <ItemStyle VerticalAlign="Top" BackColor="#FFFBD6" ForeColor="#333333" />
                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle
                                                        Wrap="False" BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingItemStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="NETT" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="READONLY" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCR" HeaderText="LOADING NAME">
                                                            <ItemStyle Width="200px" />
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="% LOADING">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: right;"></asp:TextBox>
                                                                <asp:Label ID="LB_VAL" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="NETT">
                                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CB" runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                </asp:DataGrid>
                                            </div>
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
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="vertical-align: top;">
            </tr>
        </table>
        <table id="TBL_PERIODIC" runat="server" style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">LOADING SET</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 130px;">PERIODIC MODE</td>
                            <td>
                                <asp:DropDownList ID="DDL_PERIODIC" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>TRANSACTION TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TRANS_TYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_TRANS_TYPE_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_TXT" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR_LOADING_PERIODIC" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnItemDataBound="DGR_LOADING_PERIODIC_ItemDataBound">
                        <ItemStyle VerticalAlign="Top" BackColor="#FFFBD6" ForeColor="#333333" />
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL01" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL02" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL03" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL04" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL05" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL06" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL07" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL08" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL09" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL10" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="PERIODIC LOADING">
                                <ItemStyle Width="200px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="#1">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL01" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#2">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL02" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#3">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL03" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#4">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL04" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#5">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL05" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#6">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL06" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#7">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL07" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#8">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL08" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#9">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL09" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#10">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL10" runat="server" CssClass="ASPTextBox" Width="40px" Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:DataGrid></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_SAVE_PERIODIC" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_SAVE_PERIODIC_Click" /></td>
            </tr>
        </table>

    </form>
</body>
</html>
