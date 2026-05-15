<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polis_Period_Biaya.aspx.cs" Inherits="HEALTH.Form_Klien.Polis_Period_Biaya" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style4 {
            position: absolute;
            top: 0px;
            left: 0px;
            width: 551px;
            height: 333px;
        }
        .auto-style5 {
            width: 54px;
        }
        .auto-style6 {
            width: 322px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; " class="auto-style4">
            <tr>


            
            <td >
                    <asp:Button ID="BT_SAVE0" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="107px" OnClick="BT_SAVE0_Click" />
                    <asp:Label ID="LB_PERIOD0" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                    <asp:Label ID="LB_ERR0" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>

                    <asp:DataGrid ID="DGR0" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="Vertical" CellPadding="4" ForeColor="Black" ShowFooter="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Width="224px">
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="TIPE_BIAYA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="TIPE BIAYA"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="AMOUNT">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_LOADING0" runat="server" CssClass="ASPTextBoxNumber" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Font-Size="Small" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle BackColor="#F7F7DE" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>

            </tr>
        </table>


    </form>

</body>
</html>
