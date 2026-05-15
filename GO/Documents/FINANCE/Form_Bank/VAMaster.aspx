<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VAMaster.aspx.cs" Inherits="FINANCE.Form_Bank.VAMaster" MaintainScrollPositionOnPostback="true" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">BANK ACCOUNT/td>
                            <td>
                                            <asp:DropDownList ID="DDL_NOREK" runat="server" CssClass="ASPDropDownList" CausesValidation="True" OnSelectedIndexChanged="DDL_NOREK_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                        </tr>
                        <tr>
                            <td>ACC NO</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ACC NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CREATE DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_CREATEDATE" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CREATEDATE">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-
                                            <asp:TextBox ID="TXT_CREATEDATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CREATEDATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>TAKEN</td>
                            <td>
                                <asp:DropDownList ID="DDL_TAKEN" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="and a.FIRST_TAKENBY is null ">NO</asp:ListItem>
                                    <asp:ListItem Value="and a.FIRST_TAKENBY is not null ">YES</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                            &nbsp;<asp:Button ID="BT_EXPORT" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_EXPORT_Click" Text="EXPORT" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                                <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="40"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False">
                        <ItemStyle Wrap="False" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="True" CausesValidation="True" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ACCNO" HeaderText="ACC NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCNAME" HeaderText="ACC NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CREATEDATE" HeaderText="CREATE DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOREK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FIRST_TAKENBY" HeaderText="FIRST&lt;BR&gt;TAKEN BY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FIRST_TAKENDATE" HeaderText="FIRST&lt;BR&gt;TAKEN DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_TAKENBY" HeaderText="LAST&lt;BR&gt;TAKEN BY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_TAKENDATE" HeaderText="LAST&lt;BR&gt;TAKEN DATE"></asp:BoundColumn>
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
