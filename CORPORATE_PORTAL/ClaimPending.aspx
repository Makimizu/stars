<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="ClaimPending.aspx.cs" Inherits="CORPORATE_PORTAL.ClaimPending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="include/css/controls.css" rel="stylesheet" />
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />


    <form id="form1" runat="server">
        <asp:Label ID="LB_RESULT" runat="server" Text="" Font-Size="8pt"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="DataGrid" AutoGenerateColumns="False" ForeColor="#333333" BorderColor="Transparent" Width="100%" OnItemCommand="DGR_ItemCommand" Font-Size="8pt" >
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
            <AlternatingItemStyle BackColor="White" />
            <HeaderStyle BackColor="#CCCCCC" ForeColor="#666666"
                Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:BoundColumn DataField="CLAIM_NO" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="NAMA" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="TP" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Claim No / Name">
                    <ItemTemplate>
                        <asp:LinkButton ID="LB_VIEW" runat="server" CommandName="View" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="BENEFIT_DESCR" HeaderText="Benefit"></asp:BoundColumn>
                <asp:BoundColumn DataField="PR" HeaderText="P/R">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ACT_DATE" HeaderText="Activity Date">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INCURRED" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="REJECTED" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVED" Visible="false"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Amount">
                    <HeaderStyle Width="50px" HorizontalAlign="Right" />
                    <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <table style="border-spacing: 0px; width: 180px;">
                            <tr style="color: green;">
                                <td style="width: 70px;">Incurred</td>
                                <td>:</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="LBL_INCURRED" runat="server"></asp:Label></td>
                            </tr>
                            <tr style="color: red;">
                                <td>Reject</td>
                                <td>:</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="LBL_REJECT" runat="server"></asp:Label></td>
                            </tr>
                            <tr style="color: blue;">
                                <td>Approved</td>
                                <td>:</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="LBL_APPROVED" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
        </asp:DataGrid>


    </form>

</asp:Content>

