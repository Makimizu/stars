<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyOtherSetup.aspx.cs" Inherits="GLIFE.Form_Policy.PolicyOtherSetup" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <asp:DataGrid ID="DGR_INVOICE" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="95%">
                                    <ItemStyle VerticalAlign="Top" BackColor="#EFF3FB" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle
                                        Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BATCH" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="U/W TYPE"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="BATCH">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="30px" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" AutoPostBack="True" OnCheckedChanged="CB_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                                <br />
                                <asp:DataGrid ID="DGR_EXCELFORMAT" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="95%">
                                    <ItemStyle VerticalAlign="Top" BackColor="#EFF3FB" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle
                                        Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="U/W EXCEL UPLOAD FORMAT"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="YES">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="30px" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB_TAKEN" runat="server" AutoPostBack="True" OnCheckedChanged="CB_TAKEN_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </td>
                            <td style="width: 50%;">
                                <asp:DataGrid ID="DGR_FUND" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnItemCommand="DGR_FUND_ItemCommand" Width="95%">
                                    <ItemStyle VerticalAlign="Top" BackColor="#EFF3FB" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle
                                        Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PCT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="AKAD NAME"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_FUND_PCT" runat="server" CssClass="ASPTextBoxNumber" Width="100%"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="BT_FUND_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" CommandName="Save" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%;">
                    <table id="TBL_CHARGE" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 180px;">
                                <asp:DropDownList ID="DDL_TRXTYPE" runat="server" CssClass="ASPDropDownList" Width="100%"></asp:DropDownList>
                            </td>
                            <td style="width: 180px;">
                                <asp:DropDownList ID="DDL_CHGTYPE" runat="server" CssClass="ASPDropDownList" Width="100%"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="BT_CHGADD" runat="server" CssClass="ASPButton" Text="ADD" OnClick="BT_CHGADD_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="DGR_CHARGE" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="95%" OnItemCommand="DGR_CHARGE_ItemCommand">
                        <ItemStyle VerticalAlign="Top" BackColor="#EFF3FB" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="TRANS_TYPE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CHARGE_TYPE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PCT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAPSE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRANS_DESCR" HeaderText="TRANS. TYPE">
                                <ItemStyle Width="180px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CHARGE_DESCR" HeaderText="CHARGE TYPE">
                                <ItemStyle Width="180px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_CHARGE_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="%">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PCT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="BT_CHARGE_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" CommandName="Save" />
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="LAPSE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB_LAPSE" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>

                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                    <br />
                    <iframe src="../Form_Tools/OtherSetup.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>&MODE=POL&s=<%=Session["s"]%>" width="95%" style="height: 200px; border: 0;"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
