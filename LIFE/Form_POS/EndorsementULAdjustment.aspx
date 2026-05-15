<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementULAdjustment.aspx.cs" Inherits="LIFE.Form_POS.EndorsementULAdjustment" %>

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
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>CYCLE DATE</td>
                            <td>
                                <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px;">MODE</td>
                            <td>
                                <asp:DropDownList ID="DDL_MODE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_MODE_SelectedIndexChanged">
                                    <asp:ListItem Value="C">SUBSCRIPTION</asp:ListItem>
                                    <asp:ListItem Value="D">REDEMPTION</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>FUND</td>
                            <td>
                                <asp:DropDownList ID="DDL_FUND" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_FUND_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>TRANSACTION</td>
                            <td>
                                <asp:DropDownList ID="DDL_TRANS" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>AMOUNT/UNIT</td>
                            <td>
                                <asp:DropDownList ID="DDL_AMOUNTUNIT" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_AMOUNTUNIT_SelectedIndexChanged"></asp:DropDownList>
                                <asp:TextBox ID="TXT_AMOUNTUNIT" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SUBMIT" runat="server" Text="INSERT" CssClass="ASPButton" Width="100" OnClick="BT_SUBMIT_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DGR" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_ItemCommand">
            <Columns>
                <asp:BoundColumn DataField="FUND_CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="TRANS_TYPE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="DC" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="THEDATE" HeaderText="CYCLE DATE">
                    <HeaderStyle HorizontalAlign="Center" Width="80" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DC_DESCR" HeaderText="MODE"></asp:BoundColumn>
                <asp:BoundColumn DataField="FUND" HeaderText="FUND"></asp:BoundColumn>
                <asp:BoundColumn DataField="TRANS" HeaderText="TRANSACTION"></asp:BoundColumn>
                <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" Width="80" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="UNIT" HeaderText="UNIT">
                    <HeaderStyle HorizontalAlign="Right" Width="80" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle HorizontalAlign="Right" Width="30" />
                    <ItemTemplate>
                        <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" Text="X" CommandName="Delete" BackColor="Red" ForeColor="White" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
            <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
            <AlternatingItemStyle BackColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        </asp:DataGrid>
    </form>
</body>
</html>
