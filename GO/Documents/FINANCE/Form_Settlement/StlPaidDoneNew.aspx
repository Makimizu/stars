<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StlPaidDoneNew.aspx.cs" Inherits="FINANCE.Form_Settlement.StlPaidDoneNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />
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
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr id="TR_APPID" runat="server">
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="APPLICATION" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr id="TR_TIPE" runat="server">
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="TYPE" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>ACC SOURCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ACCSOURCE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">
                                            <asp:Label ID="Label2" runat="server" Text="DESTINATION ACC" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_DESTACC" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">
                                            <asp:Label ID="Label1" runat="server" Text="DOC NO" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_ID" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="PAID DATE" CssClass="ASPLabel"></asp:Label></td>
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
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="EXECUTE DATE" CssClass="ASPLabel"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_EXEDATE1" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_EXEDATE1">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_EXEDATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_EXEDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" Width="100px" />
                                            <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" OnClick="BT_XLS_Click" BackColor="Green" Font-Bold="True" ForeColor="White" Visible="False" />

                                            <asp:Label ID="LB_SQL" runat="server" Visible="False"></asp:Label>
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
                    <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#006600" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" GridLines="Vertical">
                        <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" Wrap="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="DOCNO BAGIAN KLAIM">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_REKAPID" runat="server" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REKAPID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL_DONE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NO" HeaderText="NOMOR POLIS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NAME" HeaderText="NAMA LEMBAGA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOREK_SOURCE_DESCR" HeaderText="SOURCE ACC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESTINATION_ACC" HeaderText="DESTINATION ACC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EXECUTEBY" HeaderText="EXECUTE BY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#006600" ItemStyle-BorderWidth="1px">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" BackColor="#006600" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" BackColor="#66FF99" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="APPROVE_DATE" HeaderText="APPROVE DATE" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#006600" ItemStyle-BorderWidth="1px">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" BackColor="#006600" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Center" BackColor="#66FF99" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAID_DATE" HeaderText="PAID DATE" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#006600" ItemStyle-BorderWidth="1px">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" BackColor="#006600" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Center" BackColor="#66FF99" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RK_DESCR" HeaderText="BANK STATEMENT DESCR" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#006600" ItemStyle-BorderWidth="1px">
                                <HeaderStyle BackColor="#006600" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle BackColor="#66FF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SLA" HeaderText="SLA"></asp:BoundColumn>
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
