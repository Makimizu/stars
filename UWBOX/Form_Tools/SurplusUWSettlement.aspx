<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurplusUWSettlement.aspx.cs" Inherits="UWBOX.Form_Tools.SurplusUWSettlement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" style="width:100%">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" Font-Names="Tahoma" Font-Size="XX-Small" GridLines="None" OnItemCommand="DGR_ItemCommand" ShowHeader="False" Width="90%">
                        <ItemStyle VerticalAlign="Top" Wrap="false" />
                        <Columns>
                            <asp:BoundColumn DataField="URL" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="BT_MEMO" Width="100%" Font-Size="XX-Small" CommandName="Show" Style="white-space: normal;" />
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

