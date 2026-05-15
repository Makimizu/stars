<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementParametersList.aspx.cs" Inherits="UWBOX.Form_TC.EndorsementParametersList" %>

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
                <td style="width: 250px;">
                    <asp:DataGrid ID="DGR0" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
                        GridLines="Vertical" OnItemCommand="DGR0_ItemCommand" PageSize="20"
                        ShowHeader="False" Width="100%" ForeColor="Black">
                        <SelectedItemStyle BackColor="#00CC00" ForeColor="White"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <FooterStyle BackColor="#CCCC99" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT" runat="server" CommandName="Select"
                                        Font-Names="Tahoma"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>

