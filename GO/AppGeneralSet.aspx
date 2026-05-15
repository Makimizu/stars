<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppGeneralSet.aspx.cs" Inherits="GO.AppGeneralSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <strong>APPLICATION : </strong>
                    <asp:DropDownList ID="DDL_APP" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DataGrid ID="DGR" runat="server" PageSize="5" AutoGenerateColumns="False"
                        CssClass="ASPDatagrid" ShowHeader="False">
                        <Columns>
                            <asp:BoundColumn DataField="PARAMETER" HeaderText="PARAMETER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VALUE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ENCRYPT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VALUE" runat="server" CssClass="ASPTextBox" Width="500px" MaxLength="1000"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle Wrap="False" VerticalAlign="Top" />
                    </asp:DataGrid>

                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" />

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
