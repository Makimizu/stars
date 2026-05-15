<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimBenefitICD.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimBenefitICD" %>

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
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr id="TR_UNSELECTED" runat="server">
                            <td><asp:TextBox ID="TXT_ICDSEARCH" runat="server" Width="95%" CssClass="ASPTextBox" placeholder="Search for ICD code or name ..." AutoPostBack="True" OnTextChanged="TXT_ICDSEARCH_TextChanged"></asp:TextBox>
                                <div style="width: 100%; height: 100px; overflow: auto;">
                                    <asp:DataGrid ID="DGR_ICD_UNSELECTED" runat="server" BackColor="White"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_ICD_UNSELECTED_ItemCommand">
                                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CODE">
                                                <ItemStyle Width="50px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LBT_SELECTED" runat="server" CommandName="Select"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr id="TR_SELECTED" runat="server" visible="false">
                            <td>
                                <asp:DataGrid ID="DGR_ICD_SELECTED" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_ICD_SELECTED_ItemCommand">
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" HeaderText="CODE">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="SELECTED DIAGNOSE"></asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="Delete" Text="Delete">
                                            <ItemStyle Width="80px" />
                                        </asp:ButtonColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
