<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StlPending.aspx.cs" Inherits="FINANCE.Form_Settlement.StlPending" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_APP" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr id="TR_APPID" runat="server">
                                        <td style="width: 100px;">
                                            <asp:Label ID="Label5" runat="server" Text="APPLICATION" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr id="TR_TIPE" runat="server">
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="TIPE" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_TIPE_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="DOC NO" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ACC SOURCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ACCSOURCE" runat="server" CssClass="ASPDropDownList" ></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">
                                            <asp:Label ID="Label2" runat="server" Text="DESCRIPTION" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="APPROVAL DATE" CssClass="ASPLabel"></asp:Label></td>
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
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" Width="100px" />
                                            &nbsp;
                                            <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
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
                    <asp:Button ID="BT_APPROCE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_APPROCE_Click" Text="PROCESS" />
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <ItemStyle Wrap="False" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="DOC NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_REKAPID" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="LB_ALL" runat="server" CommandName="All" CssClass="ASPLabel" ForeColor="White">ALL</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" CommandName="Delete" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REKAPID" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="SOURCE">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_SOURCE" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TRANSFER VIA">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_TRANSFER" runat="server" CssClass="ASPDropDownList">
                                        <asp:ListItem>CLEARING</asp:ListItem>
                                        <asp:ListItem>RTGS</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CNT" HeaderText="#">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="30px" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESTINATION_BANK_DESCR" HeaderText="DESTINATION BANK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="APPROVAL" HeaderText="APPROVAL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DEFAULT_ACC_SOURCE" Visible="False"></asp:BoundColumn>
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
