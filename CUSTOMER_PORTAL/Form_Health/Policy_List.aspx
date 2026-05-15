<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Policy_List.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Health.Policy_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; top: 0px; left: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 150px;">POLICY NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>COMPANY NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY START DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE1" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE1">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_STARTDATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY END DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ENDDATE1" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE1">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_ENDDATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NB/RN</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_NBRN" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 150px;">MOP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MOP" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CHANNEL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CHANNEL" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>AGENT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_AGENT" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TPA</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TPA" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>STATUS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem>INFORCE</asp:ListItem>
                                                <asp:ListItem>LAPSE</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" /><br />
                                <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" BackColor="Green" ForeColor="White" Text="XLS" OnClick="BT_XLS_Click" Visible="False" />
                                <br />
                                <br />
                                <br />
                                <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#336600" CellPadding="4" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" ForeColor="#333333">
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="POLICY NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID" HeaderText="USER ID">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PASSWORD" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PASSWORD">
                                <ItemTemplate>
                                    <asp:Label ID="LB_PWD" runat="server" ForeColor="#c0c0c0"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="START&lt;BR&gt;DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="END&lt;BR&gt;DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_DESCR" HeaderText="NB/RN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MOP_DESCR" HeaderText="MOP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT" HeaderText="PRODUCT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TPA_DESCR" HeaderText="TPA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUB_CODE_DESCR" HeaderText="CHANNEL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT" HeaderText="STATUS"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <ItemStyle Wrap="False" BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
