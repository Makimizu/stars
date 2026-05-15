<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="ClaimHistory.aspx.cs" Inherits="CORPORATE_PORTAL.ClaimHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="include/css/controls.css" rel="stylesheet" />
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />


    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%; font-size: small; color: grey;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 120px;">Status</td>
                            <td>
                                <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_STAT_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Benefit</td>
                            <td>
                                <asp:DropDownList ID="DDL_BENEFIT" runat="server" CssClass="DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_BENEFIT_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 20px;"></td>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 120px;">Claimant</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Care Date</td>
                            <td>
                                <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="TextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                &nbsp;-
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                </ajaxToolkit:CalendarExtender>
                                <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="TextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 20px;"></td>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ButtonColor" Text="SEARCH" Width="100px" OnClick="BT_SEARCH_Click" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LB_RESULT" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20" GridLines="Vertical" CssClass="DataGrid" AutoGenerateColumns="False" ForeColor="#333333" BorderColor="Transparent" Width="100%" OnItemCommand="DGR_ItemCommand" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" Font-Size="8pt">
            <PagerStyle Mode="NumericPages" Position="TopAndBottom" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
            <AlternatingItemStyle BackColor="White" />
            <HeaderStyle BackColor="#CCCCCC" ForeColor="#666666"
                Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:BoundColumn DataField="CLAIM_NO" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="NAMA" Visible="false"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Claim No / Name">
                    <ItemTemplate>
                        <asp:LinkButton ID="LB_VIEW" runat="server" CommandName="View" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="BENEFIT_DESCR" HeaderText="Benefit"></asp:BoundColumn>
                <asp:BoundColumn DataField="PR" HeaderText="P/R">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ACT_DATE" HeaderText="Care Date">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INCURRED" HeaderText="INCURRED">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REJECTED" HeaderText="REJECTED">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVED" HeaderText="APPROVED">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Amount" Visible="False">
                    <HeaderStyle Width="50px" HorizontalAlign="Right" />
                    <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <table style="border-spacing: 0px; font-size: small; width: 180px;">
                            <tr style="color: green;">
                                <td style="width: 70px;">Incurred</td>
                                <td>:</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="LBL_INCURRED" runat="server"></asp:Label></td>
                            </tr>
                            <tr style="color: red;">
                                <td>Reject</td>
                                <td>:</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="LBL_REJECT" runat="server"></asp:Label></td>
                            </tr>
                            <tr style="color: blue;">
                                <td>Approved</td>
                                <td>:</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="LBL_APPROVED" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
        </asp:DataGrid>


    </form>

</asp:Content>
