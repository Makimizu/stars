<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParamHoliday.aspx.cs" Inherits="GO.ParamHoliday" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; left: 0px; top: 0px; width: 100%;">
            <tr>
                <td>YEAR :
                    <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_YEAR_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="S" Font-Bold="True" ForeColor="White" BackColor="Green" OnClick="BT_SAVE_Click" />
                    <br />
                    <br />
                    <div style="overflow: auto; height: 400px; width: 450px; border-top-style:ridge;">

                        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal"
                            PageSize="20" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" Width="100%">
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <Columns>
                                <asp:BoundColumn DataField="THEDATE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DAY_NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="HOLIDAY_DESCR" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DAY_OF_WEEK" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                Mode="NumericPages" />
                        </asp:DataGrid>

                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
