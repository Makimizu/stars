<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="BatchUploadList.aspx.cs" Inherits="GLIFE_PROPOSAL.BatchUploadList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Bootstrap core CSS-->
    <link href="include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom fonts for this template-->
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <!-- Page level plugin CSS-->
    <link href="include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="include/css/sb-admin.css" rel="stylesheet" />
    <link href="include/css/controls.css" rel="stylesheet" />

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="DV_LIST" runat="server">
            <table style="border-spacing: 0px; width: 100%; font-size: small;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%; font-size: small;">
                            <tr>
                                <td style="width: 120px;">POLICY</td>
                                <td>
                                    <asp:TextBox ID="TXT_POLICYSEARCH" runat="server" AutoPostBack="true" Width="100%"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>UPLOAD DATE</td>
                                <td>
                                    <asp:TextBox ID="TXT_REGDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_REGDATE1">
                                    </ajaxToolkit:CalendarExtender>
                                    &nbsp;-&nbsp;
                            <asp:TextBox ID="TXT_REGDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_REGDATE2">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ButtonColor" Text="SEARCH" OnClick="BT_SEARCH_Click" /></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="DGR_POLICY" runat="server" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" Font-Size="8pt" GridLines="Vertical" OnItemCommand="DGR_POLICY_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_POLICY_PageIndexChanged" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <Columns>
                                <asp:BoundColumn DataField="BATCH_ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TC_DESCR" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="POLICY NO">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBT_POLICY" runat="server" CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="POLICY HOLDER<BR>BRANCH"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TOTAL" HeaderText="UPLOADED<BR>EXPORTED">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FORMAT_DESCR" HeaderText="FORMAT"></asp:BoundColumn>
                                <asp:BoundColumn DataField="UPLOADBY" HeaderText="UPLOAD BY"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Button ID="BT_RAW" runat="server" CssClass="ButtonColor" Text="R" CommandName="Raw" />
                                        <asp:LinkButton ID="LBT_DELETE" runat="server" CommandName="Delete" ForeColor="Red" Font-Size="Small" ToolTip="Delete">
                                    <span class="fa fa-trash"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
        <div id="DV_SHOW" runat="server" visible="false">
            <table style="border-spacing: 0px; width: 100%; font-size: 8pt;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td>
                                    <asp:Button ID="BT_BACK" runat="server" CssClass="Button" OnClick="BT_BACK_Click" Text="BACK ..." />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 100px;">POLICY NO</td>
                                <td>
                                    <asp:Label ID="LB_POLICYNO" runat="server"></asp:Label>
                                    <asp:Label ID="LB_BATCHID" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>POLICY HOLDER</td>
                                <td>
                                    <asp:Label ID="LB_COMPANY" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BT_EMAIL" runat="server" CssClass="Button" Text="EMAIL TO" OnClick="BT_EMAIL_Click" /></td>
                                <td>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="IF" runat="server" src="" style="height: 800px; width: 100%; border: 0;"></iframe>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>
