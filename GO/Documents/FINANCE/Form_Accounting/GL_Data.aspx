<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GL_Data.aspx.cs" Inherits="FINANCE.Form_Accounting.GL_Data" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">PERIOD</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PERIOD" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_PERIOD_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>JOURNAL CODE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CODE" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_CODE_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DATE</td>
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
                                        <td>DOC NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DESCRIPTION</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CONFIRMED</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CONF" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_CONF_SelectedIndexChanged">
                                                <asp:ListItem Value="= a.ISCONFIRMED">-- ALL --</asp:ListItem>
                                                <asp:ListItem Value="= 0">NO</asp:ListItem>
                                                <asp:ListItem Value="= 1">YES</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POSTED</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_POST" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_POST_SelectedIndexChanged">
                                                <asp:ListItem Value="= a.ISPOSTED">-- ALL --</asp:ListItem>
                                                <asp:ListItem Value="= 0">NO</asp:ListItem>
                                                <asp:ListItem Value="= 1">YES</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="border-spacing:0px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" />
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="LB_RECORDS" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
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

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                        PageSize="40" ItemStyle-Wrap="true" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="false" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DETAIL" runat="server" BackColor="Blue" CommandName="Detail" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="D" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="VOUCHERNO" HeaderText="VOUCHER NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CODE" HeaderText="JOURNAL CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PARAM_KEY" HeaderText="DOC NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="THEDATE" HeaderText="DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PERIOD" HeaderText="PERIODE CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ISCONFIRMED" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ISPOSTED" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CB_CONFALL" runat="server" AutoPostBack="True" OnCheckedChanged="CB_CONFALL_CheckedChanged" Text="&lt;BR&gt;CONF. ALL" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB_CONF" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CB_POSTALL" runat="server" AutoPostBack="True" OnCheckedChanged="CB_POSTALL_CheckedChanged" Text="&lt;BR&gt;POST ALL" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB_POST" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
