<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementSwitching.aspx.cs" Inherits="LIFE.Form_POS.EndorsementSwitching" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Class/Global.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_LINK" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="FUND_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FUND_CODE_DESTINATION" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNIT_AMOUNT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PCT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FUND_DESCR" HeaderText="FUND SOURCE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAV_DATE" HeaderText="NAV DATE">
                                <HeaderStyle Width="60" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NAV" HeaderText="NAV PRICE">
                                <HeaderStyle Width="60" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AVAILABLE_UNIT" HeaderText="AVAILABLE<BR>UNIT">
                                <HeaderStyle Width="70" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AVAILABLE_AMOUNT" HeaderText="ESTIMATED<BR>BALANCE">
                                <HeaderStyle Width="70" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FUND_DESTINATION" HeaderText="FUND DESTINATION">
                                <ItemStyle ForeColor="Blue" Font-Bold="true" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="AMOUNT / UNIT / %">
                                <HeaderStyle Width="180" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_UNIT_LINK" runat="server" CssClass="ASPTextBoxNumber" Width="40"
                                        BackColor="LightCyan" ForeColor="Blue" onkeyup="FormatCurrency(this);"></asp:TextBox>

                                    <asp:TextBox ID="TXT_AMOUNT_LINK" runat="server" CssClass="ASPTextBoxNumber" Width="80"
                                        BackColor="LightGreen" ForeColor="DarkGreen" onkeyup="FormatCurrency(this);" 
                                        Style="display:none"></asp:TextBox>

                                    <asp:TextBox ID="TXT_PCT_LINK" runat="server" CssClass="ASPTextBoxNumber" Width="40"
                                        BackColor="Gainsboro" onkeyup="FormatCurrency(this);" 
                                        Style="display:none"></asp:TextBox>

                                    <asp:DropDownList ID="DDL_TOGGLE" CssClass="ASPDropDownList" AutoPostBack="true"
                                        runat="server" OnSelectedIndexChanged="DDL_TOGGLE_SelectedIndexChanged"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <%--<asp:TemplateColumn HeaderText="AMOUNT / UNIT / %">
                                <HeaderStyle Width="180" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_UNIT_LINK" runat="server" CssClass="ASPTextBoxNumber" Width="40" BackColor="LightCyan" ForeColor="Blue" onkeyup="FormatCurrency(this);"></asp:TextBox>
                                    <asp:TextBox ID="TXT_AMOUNT_LINK" runat="server" CssClass="ASPTextBoxNumber" Width="80" BackColor="LightGreen" ForeColor="DarkGreen" onkeyup="FormatCurrency(this);" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="TXT_PCT_LINK" runat="server" CssClass="ASPTextBoxNumber" Width="40" BackColor="Gainsboro" onkeyup="FormatCurrency(this);" Visible="false"></asp:TextBox>
                                    <asp:DropDownList ID="DDL_TOGGLE" CssClass="ASPDropDownList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDL_TOGGLE_SelectedIndexChanged"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                            <%--<asp:TemplateColumn HeaderText="SWITCH TO">
                                <HeaderStyle Width="100" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_FUND" CssClass="ASPDropDownList" runat="server" Width="98%"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                        </Columns>
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
