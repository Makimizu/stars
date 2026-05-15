<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationEndorsementGeneric.aspx.cs" Inherits="GLIFE.Form_App.ApplicationEndorsementGeneric" %>


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
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="95%" ItemStyle-Wrap="true" AutoGenerateColumns="False" PageSize="20" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="FIELD_SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FIELD_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OLD_VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_VAL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CHANGE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="ATTRIBUTE"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="OLD VALUE">
                                <HeaderStyle />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:Label ID="LB_OLDVAL" runat="server"></asp:Label>
                                    <asp:DropDownList ID="DDL_OLDVAL" runat="server" CssClass="ASPDropDownList" Visible="false"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NEW VALUE">
                                <HeaderStyle />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_NEWVAL" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                    <asp:TextBox ID="TXT_NEWDATEVAL" runat="server" CssClass="ASPTextBox" Visible="false" Width="70px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_NEWDATEVAL">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:DropDownList ID="DDL_NEWVAL" runat="server" CssClass="ASPDropDownList" Visible="false"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>

                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                    <br />
                    <br />
                    <asp:DataGrid ID="DGR_ADDINFO" runat="server" Font-Names="Tahoma" Width="95%" ItemStyle-Wrap="true" AutoGenerateColumns="False" PageSize="20" CssClass="ASPDatagrid" ShowHeader="False">
                        <ItemStyle Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="SUBJECT">
                                <ItemStyle Width="200px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>

                    <asp:DataGrid ID="DGR_VALIDATION" runat="server" CellPadding="4" Font-Names="Tahoma" GridLines="None" Width="95%" ItemStyle-Wrap="true" AutoGenerateColumns="False" PageSize="20" CssClass="ASPDatagrid" ForeColor="#333333">
                        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#990000" VerticalAlign="Top" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                        <Columns>
                            <asp:BoundColumn DataField="REMARK" HeaderText="VALIDATION FINDING"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    </asp:DataGrid>

                </td>
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center; width: 50%;">ACCOUNT RECEIVABLE</td>
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center; width: 50%;">ACCOUNT PAYABLE</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <asp:DataGrid ID="DGR_AR" runat="server" Font-Names="Tahoma" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" PageSize="20" CssClass="ASPDatagrid" ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_AR_ItemCommand">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle VerticalAlign="Top" BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_ARAMT" runat="server" CssClass="ASPTextBoxNumber" ForeColor="Green" BackColor="#e1ffe1"></asp:TextBox>
                                                <asp:Button ID="BT_ARSAVE" runat="server" CommandName="Save" Text="S" ForeColor="White" BackColor="Green" CssClass="ASPButton" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>

                            </td>
                            <td style="width: 50%;">
                                <asp:DataGrid ID="DGR_AP" runat="server" Font-Names="Tahoma" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" PageSize="20" CssClass="ASPDatagrid" ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_AP_ItemCommand">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#FFFBD6" ForeColor="#333333" />
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle VerticalAlign="Top" BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_APAMT" runat="server" CssClass="ASPTextBoxNumber" ForeColor="Red" BackColor="#e1ffe1"></asp:TextBox>
                                                <asp:Button ID="BT_APSAVE" runat="server" CommandName="Save" Text="S" ForeColor="White" BackColor="Red" CssClass="ASPButton" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                </asp:DataGrid>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TBL_AR" runat="server" style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td>TOTAL</td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_AR" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table id="TBL_AP" runat="server" style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td>TOTAL</td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_AP" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <iframe id="I1" runat="server" visible="false" style="width: 100%; height: auto; border: 0;" name="I1"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
