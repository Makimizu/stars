<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationList.aspx.cs" Inherits="LIFE.Form_POS.RegistrationList" %>

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
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">APPLICATION NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_REGNO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY HOLDER</td>
                                        <td>
                                            <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ENDORSEMENT TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">PRODUCT</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PRODUCT" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>UNITIZE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_UNITIZE" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value="1">UNITIZE</asp:ListItem>
                                                <asp:ListItem Value="0">NON UNITIZE</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>REGISTERED DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_REGDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_REGDATE1">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_REGDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_REGDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" Width="100px" OnClick="BT_SEARCH_Click" />
                                            <asp:Label ID="LB_TRACK" runat="server" Visible="False"></asp:Label>
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
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="POLICY NO">
                                <HeaderStyle Width="100" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_REGNO" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="POLICY HOLDER">
                                <ItemStyle ForeColor="Green" Font-Bold="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT" HeaderText="PRODUCT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENDORSEMENT_TYPE_DESCR" HeaderText="ENDORSEMENT TYPE">
                                <ItemStyle ForeColor="Green" Font-Size="6pt" Font-Italic="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REG_DATE" HeaderText="REG. DATE">
                                <HeaderStyle HorizontalAlign="Center" Width="60" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                             <asp:BoundColumn DataField="STAT_TRACK" HeaderText="STAT_TRACK" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_GROUP" HeaderText="PRODUCT_GROUP" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CUSTODYNAVDATE" HeaderText="CUSTODY NAV DATE">
                            <HeaderStyle HorizontalAlign="Center" Width="60" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="REG_BY" HeaderText="REG. BY">
                                <HeaderStyle Width="100" />
                                <ItemStyle ForeColor="Gray" Font-Size="6pt" Font-Italic="true" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" CommandName="Delete" Text="X" ForeColor="White" BackColor="Red" ToolTip="Delete Registration" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="100px" Width="500px" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <center>
                    <br />
                    <br />
                    <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="BT_BACK" runat="server" Text="BACK" BackColor="Red" ForeColor="White" CssClass="ASPButton" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;"></asp:Button>
                </center>
            </asp:Panel>
        </div>

    </form>
</body>
</html>

