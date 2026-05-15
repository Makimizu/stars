<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StlRKMatch.aspx.cs" Inherits="FINANCE.Form_Settlement.StlRKMatch" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 150px;">APPLICATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>DOC NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BENEFICIARY</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BENEF" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width:150px;">PAYMENT DESCR</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STLDESCR" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BANK POST DATE</td>
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
                                        <td>BANK STATEMENT DESCR</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RKDESCR" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" />
                                            &nbsp;<asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
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
                    <asp:Button ID="BT_SETTLE" runat="server" BackColor="#CCFF99" CssClass="ASPButton" Font-Bold="True" ForeColor="#006600" OnClick="BT_SETTLE_Click" Text="SETTLE CHECKED ITEMS" Visible="False" />
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="DOC NO">
                                <HeaderTemplate>
                                    DOC NO
                                    <asp:Button ID="BT_ALL" runat="server" CommandName="All" CssClass="ASPButton" Font-Bold="True" Text="ALL" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                    &nbsp;<asp:CheckBox ID="CB" runat="server" CssClass="ASPLabel" AutoPostBack="True" OnCheckedChanged="CB_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REKAPID" HeaderText="DOC NO" Visible="False">
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#000099" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_BANK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NAME" HeaderText="BENEFICIARY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STL_DESCR" HeaderText="PAYMENT DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DEBET" HeaderText="RK AMOUNT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" BackColor="#00CC00" ForeColor="Black" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" BackColor="#99FF99" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="POST_DATE" HeaderText="BANK POST DATE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" BackColor="#00CC00" ForeColor="Black" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" BackColor="#99FF99" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RK_DESCR" HeaderText="BANK STATEMENT DESCRIPTION">
                                <HeaderStyle BackColor="#00CC00" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                <ItemStyle BackColor="#99FF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOREK_SOURCE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRXID" Visible="False"></asp:BoundColumn>
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
