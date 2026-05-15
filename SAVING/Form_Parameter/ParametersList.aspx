<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametersList.aspx.cs" Inherits="SAVING.Form_Parameter.ParametersList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 250px; text-align: right;">
                    <asp:Label ID="LB_PREFIX" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LB_PAGE" runat="server" Visible="false"></asp:Label>
                    <asp:TextBox ID="TXT_PARAM" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_PARAM_TextChanged" Width="95%"></asp:TextBox>
                    <asp:DataGrid ID="DGR0" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
                        GridLines="Vertical" OnItemCommand="DGR0_ItemCommand" PageSize="20"
                        ShowHeader="False" Width="100%" ForeColor="Black">
                        <SelectedItemStyle BackColor="#00CC00" Font-Bold="True" ForeColor="White"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <FooterStyle BackColor="#CCCC99" />
                        <Columns>
                            <asp:BoundColumn DataField="name" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="alias" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT" runat="server" CommandName="Select" Font-Bold="True"
                                        Font-Names="Tahoma"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>

