<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DATA_PRODUCTION.aspx.cs" Inherits="AGR.DATA_PRODUCTION" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function hourglass() {
            document.body.style.cursor = "wait";   
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="width: 100%; border-spacing: 0px; font-size: x-small;">
            <tr>
                <td class="TDBGColor" style="font-size: small;">OUTPUT : DATA PRODUCTION</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td>PRODUCT GROUP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PRODUCT_GROUP" CssClass="ASPDropDownList" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">PRODUCT NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PRODUCT_NAME" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CHANNEL & LEVEL</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CHANNEL" CssClass="ASPDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:DropDownList ID="DDL_LEVEL" CssClass="ASPDropDownList" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td>TRANS. TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TRANSTYPE" CssClass="ASPDropDownList" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">AGENT NAME/CODE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_AGENT" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>PERIOD</td>
                                        <td>
                                            <asp:TextBox ID="TXT_START_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE">
                                            </ajaxToolkit:CalendarExtender>
                                            - 
                                            <asp:TextBox ID="TXT_END_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" Text="SEARCH" CssClass="ASPButton" Width="80" OnClick="BT_SEARCH_Click" OnClientClick="hourglass(); return true;" />
                                            &nbsp;<asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
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
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                        GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="95%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged">
                        <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_NAME" HeaderText="PRODUCT NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_GROUP" HeaderText="PRODUCT GROUP" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRANS_TYPE" HeaderText="TRANS TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGENT" HeaderText="AGENT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BASIC_COMM" HeaderText="BASIC COMM"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UPLINER1" HeaderText="UPLINER 1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OR1" HeaderText="OR 1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UPLINER2" HeaderText="UPLINER 2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OR2" HeaderText="OR 2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UPLINER3" HeaderText="UPLINER 3"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OR3" HeaderText="OR 3"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REFERENCE_NO" HeaderText="REFERENCE NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="YEAR" HeaderText="#YEAR">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SETTLE_DATE" HeaderText="SETTLE DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DUE_DATE" HeaderText="DUE DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="PAID AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                    </asp:DataGrid>


                </td>
            </tr>
        </table>
    </form>
</body>
</html>
