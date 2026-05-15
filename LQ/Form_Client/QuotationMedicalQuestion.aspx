<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationMedicalQuestion.aspx.cs" Inherits="LQ.Form_Client.QuotationMedicalQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        *, ::after, ::before {
            text-shadow: none !important;
            box-shadow: none !important;
        }

        *, ::after, ::before {
            box-sizing: border-box;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_QUESTION" runat="server" AutoGenerateColumns="False" CellPadding="3" Font-Names="Tahoma" Font-Size="8pt" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="90%" CssClass="ASPDatagrid" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" VerticalAlign="Top" ForeColor="#4A3C8C" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEXT_QUESTION" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VALUE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VALUE_NEXT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr style="vertical-align: top;">
                                                        <td>
                                                            <asp:Label ID="LB_DESCR" runat="server" /></td>
                                                        <td style="text-align: right;">
                                                            <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False" ForeColor="Red"></asp:DropDownList>
                                                            <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="150px" MaxLength="255" ForeColor="Red"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="TR_NEXTQUESTION" runat="server" visible="false">
                                            <td>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr style="vertical-align: top; border-top: ridge;">
                                                        <td>
                                                            <asp:Label ID="LB_QUESTION" runat="server" Visible="false" />
                                                            <br />
                                                            <asp:TextBox ID="TXT_NEXTVAL" runat="server" CssClass="ASPTextBox" Width="80%" MaxLength="255" Visible="false" BackColor="LightYellow"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                <ItemTemplate>
                                    <asp:Label ID="LB_UNCHECKED" runat="server" ForeColor="Red" Visible="false">
                                                <span class="fa fa-bell"></span>
                                    </asp:Label>
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
