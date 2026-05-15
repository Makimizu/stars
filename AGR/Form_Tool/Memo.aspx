<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Memo.aspx.cs" Inherits="AGR.Form_Tool.Memo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_APP" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr id="TR_STL1" runat="server">
                <td>
                    <table style="border-spacing: 0px; font-size: x-small;">
                        <tr>
                            <td>AMOUNT RANGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_RANGE" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_RANGE_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">DOC. NO</td>
                            <td>
                                <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>DESCRIPTION</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>GENERATE DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-
                                <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEACRH" />
                                &nbsp;
                                <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_STL2" runat="server">
                <td>

                    <asp:Button ID="BT_APPROCE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_APPROCE_Click" Text="APPROVE" />
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="1" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%">
                        <ItemStyle VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" ForeColor="White" Height="30" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="DOC NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_REKAPID" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="LB_ALL" runat="server" CommandName="All" CssClass="ASPLabel" ForeColor="White">ALL</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                </ItemTemplate>
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REKAPID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CNT" HeaderText="#">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="30px" />
                                <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESTINATION_BANK_DESCR" HeaderText="DESTINATION BANK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGING" HeaderText="AGING">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AUTH" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_X" runat="server" Text="X" CommandName="Delete" CssClass="ASPButton" BackColor="Red" ForeColor="White" />
                                </ItemTemplate>
                                <HeaderStyle Width="30" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
