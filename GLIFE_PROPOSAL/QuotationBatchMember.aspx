<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationBatchMember.aspx.cs" Inherits="GLIFE_PROPOSAL.QuotationBatchMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_POLICYID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_QUOTNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_VERNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: 9pt;">
            <tr>
                <td>AGENT</td>
                <td>
                    <asp:DropDownList ID="DDL_AGENT" runat="server" Font-Size="8"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>BRANCH</td>
                <td>
                    <asp:DropDownList ID="DDL_BRANCH" runat="server" Font-Size="8"></asp:DropDownList>
                    &nbsp;
                        <asp:LinkButton ID="LB_BRANCHXLS" runat="server" ToolTip="Download Branch Code" ForeColor="Green" OnClick="LB_BRANCHXLS_Click">
                        <span class="fa fa-list"></span>
                        </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>UPLOAD FORMAT</td>
                <td>
                    <asp:DropDownList ID="DDL_FORMAT" runat="server" Font-Size="8"></asp:DropDownList>
                    &nbsp;
                        <asp:LinkButton ID="LBT_TEMPLATE_XLS" runat="server" ToolTip="Download Excel Template" ForeColor="Green" OnClick="LBT_TEMPLATE_XLS_Click">
                        <span class="fa fa-download"></span>
                        </asp:LinkButton></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:FileUpload ID="FU" runat="server" />
                    &nbsp;
                        <asp:LinkButton ID="LBT_UPLOAD" runat="server" ToolTip="Upload" OnClick="LBT_UPLOAD_Click">
                        <span class="fa fa-upload"></span>
                        </asp:LinkButton></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="LB_ERR" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Label ID="LB_RESULT" runat="server"></asp:Label>

        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
            CellSpacing="1" Font-Names="Tahoma" Font-Size="8pt" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="40">
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
            <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="LBT_REGNO" runat="server" CommandName="Select"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GENDER" HeaderText="GENDER"></asp:BoundColumn>
                <asp:BoundColumn DataField="UW_CODE" HeaderText="U/W<BR>CODE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SUMINS" HeaderText="SUM INSURED">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PREMIUM" HeaderText="PREMIUM">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="DarkGreen" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REGDATE" HeaderText="REG. DATE" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn Visible="false">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="LBT_CHECK" runat="server" ForeColor="White" CommandName="Check" ToolTip="Confirm checked items">
                                    <span class="fa fa-check"></span>                            
                                    </asp:LinkButton></td>
                                <td style="text-align: right;">
                                    <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_ALL_CheckedChanged" />
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="LBT_DELETE" runat="server" CommandName="Delete" ForeColor="Red" Font-Size="Small" ToolTip="Delete">
                                    <span class="fa fa-trash"></span>
                                    </asp:LinkButton>
                                </td>
                                <td style="text-align: right;">
                                    <asp:CheckBox ID="CB" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="MQ" Visible="False"></asp:BoundColumn>
            </Columns>
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
        </asp:DataGrid>
    </form>
</body>
</html>
