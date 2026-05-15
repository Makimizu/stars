<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimRemark.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimRemark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 95%;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_REMARK" runat="server" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_REMARK_ItemCommand">
                        <ItemStyle Wrap="True" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="TYPE_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TYPE_DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CREATEBY" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CREATEDATE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LASTCHANGEBY" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LASTCHANGEDATE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr>
                                                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">
                                                            <asp:Label ID="LB_DESCR" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100%" Font-Bold="true" CommandName="Save" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" Width="100%" Height="100px" TextMode="MultiLine" MaxLength="4000" BackColor="#ffffea"></asp:TextBox></td>
                                        </tr>

                                    </table>
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
