<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_TAX.aspx.cs" Inherits="AGR.PARAMETER_MASTER_TAX" %>

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
                <td style="width: 150px;">COMPANY</td>
                <td>
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
            <tr>
                <td>START DATE</td>
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

        <div class="row" style="width: 90%;">

            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                    <tr>
                        <td>PRODUCT</td>
                        <td>
                            <asp:DropDownList ID="DDL_PRODUCT" runat="server"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td>CHANNEL</td>
                        <td>
                            <asp:DropDownList ID="DDL_CHANNEL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td>LEVEL</td>
                        <td>
                            <asp:DropDownList ID="DDL_LEVEL" runat="server"></asp:DropDownList>

                        </td>
                    </tr>


                    <tr>
                        <td style="width: 150px;">YEAR</td>
                        <td>
                            <asp:TextBox ID="TXT_YEAR" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">MIN AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TXT_MIN" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">MAX AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TXT_MAX" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">DESCRIPTION</td>
                        <td>
                            <asp:TextBox ID="TXT_DESCRIPTION" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>NETT</td>
                        <td>
                            <asp:DropDownList ID="DDL_NETT" runat="server" CssClass="ASPDropDownList">
                                <asp:ListItem Value="1">NETT</asp:ListItem>
                                <asp:ListItem Value="0">GROSS</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">COMMPCT</td>
                        <td>
                            <asp:TextBox ID="TXT_COMMPCT" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">TRANS TYPE</td>
                        <td>
                            <asp:TextBox ID="TXT_TRANS_TYPE" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: x-small;">

                    <tr>
                        <td style="width: 150px;">OR1</td>
                        <td>
                            <asp:TextBox ID="TXT_OR1" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">OR2</td>
                        <td>
                            <asp:TextBox ID="TXT_OR2" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">OR3</td>
                        <td>
                            <asp:TextBox ID="TXT_OR3" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">OR4</td>
                        <td>
                            <asp:TextBox ID="TXT_OR4" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">OR5</td>
                        <td>
                            <asp:TextBox ID="TXT_OR5" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">OR6</td>
                        <td>
                            <asp:TextBox ID="TXT_OR6" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">OR7</td>
                        <td>
                            <asp:TextBox ID="TXT_OR7" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">OR8</td>
                        <td>
                            <asp:TextBox ID="TXT_OR8" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="SAVE" CssClass="ButtonColor" Width="80" OnClick="BT1_SAVE_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>

            <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="95%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" OnItemCommand="DGR_ItemCommand">
                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn>
                    <asp:TemplateColumn>
                        <ItemStyle Width="30" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Button ID="BT_DELETE" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" ForeColor="White" Text="X" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <EditItemStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
            </asp:DataGrid>

        </div>
    </form>
</body>
</html>