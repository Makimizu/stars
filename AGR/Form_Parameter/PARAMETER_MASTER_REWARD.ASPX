<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_REWARD.aspx.cs" Inherits="AGR.PARAMETER_MASTER_REWARD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>

            <table style="width: 100%; border-spacing: 0px; font-size: x-small;">
            <tr>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: x-small;">
                        <tr>
                            <td class="auto-style1">COMPANY</td>
                            <td class="auto-style2">
                                <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>DESCRIPTION</td>
                            <td>
                                <asp:Label ID="LB_DESCR" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>REMUN TYPE</td>
                            <td>
                                <asp:Label ID="LB_TYPE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width: 100%; border-spacing: 0px; font-size: x-small;">
                        <tr>
                            <td class="auto-style1">START DATE</td>
                            <td>
                                <asp:Label ID="LB_STARTDATE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>END DATE</td>
                            <td>
                                <asp:Label ID="LB_ENDDATE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>



        <div>

            <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="95%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                <Columns>
                    <asp:BoundColumn DataField="CHANNEL" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="LEVEL" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="MIN_AMOUNT" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="MAX_AMOUNT" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NETT" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COMMPCT" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="OR1" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="OR2" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="OR3" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="OR4" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="OR5" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="OR6" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="OR7" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="OR8" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CHANNEL_DESCR" HeaderText="CHANNEL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="LEVEL_DESCR" HeaderText="LEVEL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="YEAR" HeaderText="#YEAR"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="MIN AMOUNT">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_MIN_AMT" runat="server" CssClass="ASPTextBoxNumber" Width="80"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="MAX AMOUNT">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_MAX_AMT" runat="server" CssClass="ASPTextBoxNumber" Width="80"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="NETT">
                        <ItemTemplate>
                            <asp:CheckBox ID="CB_NETT" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="% COMM">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_COMM" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="% OR1">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_OR1" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="% OR2">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_OR2" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="% OR3">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_OR3" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="% OR4">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_OR4" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="% OR5">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_OR5" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="% OR6">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_OR6" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="% OR7">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_OR7" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="% OR8">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_OR8" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn></asp:TemplateColumn>
                </Columns>
                <EditItemStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
            </asp:DataGrid>

        </div>
    </form>
</body>
</html>