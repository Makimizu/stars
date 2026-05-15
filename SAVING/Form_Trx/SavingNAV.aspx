<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SavingNAV.aspx.cs" Inherits="SAVING.Form_Trx.SavingNAV" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="width: 100%; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px;">
                        <tr>
                            <td style="width: 50%;">
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 130px;">APPLICATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_APPID" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_APPID_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>FUND</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_FUND" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_FUND_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 130px;">NEW AMOUNT</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAV" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>VALUATION DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAVDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_NAVDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SUBMIT" runat="server" CssClass="ASPButton" Text="SUBMIT" OnClick="BT_SUBMIT_Click" />
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
                    <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal"
                        PageSize="40" Width="100%" ItemStyle-Wrap="true" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" OnPageIndexChanged="DGR_PageIndexChanged">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="APP_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FUND_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENABLE_CHG" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BATCH_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RESULT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RESULT_URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="APP_NAME" HeaderText="APPLICATION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FUND_DESCR" HeaderText="FUND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="THEDATE" HeaderText="DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="USERBY" HeaderText="INSERT BY"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_S" runat="server" BackColor="Green" CommandName="Save" Text="S" ForeColor="White" CssClass="ASPButton" />
                                    <asp:Button ID="BT_X" runat="server" BackColor="Red" CommandName="Delete" Text="X" ForeColor="White" CssClass="ASPButton" />
                                    <asp:TextBox ID="TXT_AMOUNT" CssClass="ASPTextBoxNumber" runat="server" Width="100px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DETAIL">
                                <HeaderStyle Width="100" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DETAIL" runat="server" CssClass="ASPButton" Width="100" CommandName="Detail" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
