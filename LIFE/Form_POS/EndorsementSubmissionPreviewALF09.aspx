<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementSubmissionPreviewALF09.aspx.cs" Inherits="LIFE.Form_POS.EndorsementSubmissionPreviewALF09" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">PREVIEW<br />
                    DOB CHANGES</td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" ItemStyle-Wrap="true" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="MEMBER NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB<BR>NEW DOB">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="80" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="START_AGE" HeaderText="START AGE">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="40" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_START_AGE" HeaderText="NEW START AGE">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="40" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGE_CHANGE" HeaderText="AGE CHANGE">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="40" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFICIARY" HeaderText="BENEFICIARY">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="70" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#EFF3FB" />
                        <AlternatingItemStyle BackColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
