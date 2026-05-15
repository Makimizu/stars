<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PindahBuku.aspx.cs" Inherits="FINANCE.Form_Settlement.PindahBuku" %>

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

        <div id="DV_PORCESS" runat="server">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td style="width: 150px;">YEAR</td>
                    <td>
                        <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>MONTH</td>
                    <td>
                        <asp:DropDownList ID="DDL_MONTH" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_MONTH_SelectedIndexChanged">
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>

            <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="40"
                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_ItemCommand" ForeColor="#333333">
                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                <ItemStyle BackColor="#EFF3FB" VerticalAlign="Top" />
                <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Top" />
                <AlternatingItemStyle BackColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="SOURCE_TYPE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DESTINATION_TYPE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPE_SETTLEMENT" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ACCNO" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="REPORT_URL" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="THEDATE" HeaderText="DATE">
                        <HeaderStyle Width="70" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCR" HeaderText="TRANSFER TYPE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="AMOUNT" HeaderText="TOTAL AMOUNT">
                        <HeaderStyle Width="100" HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" ForeColor="Green" Font-Bold="true" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RECORDS" HeaderText="#RECORDS">
                        <HeaderStyle Width="80" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" ForeColor="Blue" Font-Bold="true" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="BANK ACCOUNT">
                        <ItemTemplate>
                            <table style="border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">TRANSFER FROM</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_SOURCE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>TRANSFER TO</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_DESTINATION" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="100" />
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Button ID="BT_EXEC" runat="server" Text="PROCESS" CssClass="ASPButton" BackColor="Green" ForeColor="White" CommandName="Process" Width="80" /><br />
                            <asp:Button ID="BT_DETAIL" runat="server" Text="DETAIL" CssClass="ASPButton" BackColor="Blue" ForeColor="White" CommandName="Detail" Width="80" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <EditItemStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            </asp:DataGrid>
        </div>
        <div id="DV_DETAIL" runat="server" visible="false">
            <asp:LinkButton ID="LB_BACK" runat="server" Text="Back .." ForeColor="Red" OnClick="LB_BACK_Click"></asp:LinkButton>
            <iframe id="IF" runat="server" style="width: 100%; height: 80vh;"></iframe>
        </div>
    </form>
</body>
</html>
