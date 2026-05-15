<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchEmployeCompany.aspx.cs" Inherits="HEALTH.Form_Tools.SearchEmployeCompany" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../standard/CommonStyle.css" type="text/css" rel="stylesheet"></link>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing:0px; width:100%;">
            <tr>
                <td valign="middle">
                    <table style="border-spacing:0px;">
                        <tr>
                            <td style="width:100px;">NO REGISTRASI</td>
                            <td>
                                <asp:TextBox ID="TXT_REGNO" runat="server" CssClass="ASPTextBox" Width="309px"
                                    Style="margin-left: 0px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NAMA PESERTA
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" Width="309px" Style="margin-left: 0px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>FAMILY GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_FAMILY" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>STATUS</td>
                            <td class="auto-style2">
                                <asp:DropDownList ID="DDL_STAT" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Selected="True" Value="1">ACTIVE</asp:ListItem>
                                    <asp:ListItem Value="2">NON ACTIVE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>PERUSAHAAN
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="309px" Style="margin-left: 0px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CABANG
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_CABANG" runat="server" CssClass="ASPTextBox" Width="309px" Style="margin-left: 0px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL LAHIR
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upBody" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_DOB" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="TXT_DOB">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="BT_CARI" runat="server" OnClick="BT_CARI_Click" Text="CARI" Style="height: 20px"
                                    Width="66px" CssClass="ASPButton" />
                                <asp:Label ID="LB1" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB2" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB3" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB4" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="callbak" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_COUNT" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:DataGrid ID="DGR1" runat="server" CellPadding="4" CssClass="ASPDatagrid" GridLines="None"
                        OnItemCommand="DGR1_ItemCommand" Width="100%" ForeColor="#333333" AllowPaging="True" OnPageIndexChanged="DGR1_PageIndexChanged" PageSize="15">
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                            Wrap="False" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonColumn>
                        </Columns>
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
