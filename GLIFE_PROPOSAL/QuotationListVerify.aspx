<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="QuotationListVerify.aspx.cs" Inherits="GLIFE_PROPOSAL.QuotationListVerify" %>

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
        <div class="row">
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: small;">
                    <tr>
                        <td style="width: 120px;">REGNO</td>
                        <td>
                            <asp:TextBox ID="TXT_REGNO" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>FULLNAME</td>
                        <td>
                            <asp:TextBox ID="TXT_FULLNAME" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>POLICY HOLDER</td>
                        <td>
                            <asp:TextBox ID="TXT_COMPANY" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: small;">
                    <tr>
                        <td style="width: 120px;">PRODUCT</td>
                        <td>
                            <asp:TextBox ID="TXT_PRODUCT" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>REG. DATE</td>
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
	                    <td>U/W CODE</td>
	                    <td>
		                    <asp:DropDownList ID="DDL_UW" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_UW_SelectedIndexChanged">
			                    <asp:ListItem Value="and UW_CODE in ('AC','FC','HD')">FREE COVER</asp:ListItem>
			                    <asp:ListItem Value="and UW_CODE='NM'">NON MEDICAL</asp:ListItem>
			                    <asp:ListItem Value="and UW_CODE not in ('AC','FC','NM','00','', 'HD')">MEDICAL</asp:ListItem>
			                    <asp:ListItem Value="and isnull(UW_CODE,'') in ('00','')">UNDEFINED</asp:ListItem>
		                    </asp:DropDownList>
	                    </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ButtonColor" Text="SEARCH" Width="100px" OnClick="BT_SEARCH_Click" />
                            &nbsp;
                            <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
            CellSpacing="1" Font-Names="Tahoma" Font-Size="8pt" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20">
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
                <asp:BoundColumn DataField="FULLNAME" HeaderText="MEMBER<BR><table style='width:100%;'><tr><td>DOB<td/><td style='text-align:right;'>GENDER</td></tr></table>"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="POLICY HOLDER<BR><table style='width:100%;'><tr><td>PRODUCT NAME<td/><td style='text-align:right;'>POLICY NO</td></tr></table>"></asp:BoundColumn>
                <asp:BoundColumn DataField="UW_CODE" HeaderText="U/W<BR>CODE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PREMIUM" HeaderText="SUM INSURED<BR>PREMIUM">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REGDATE" HeaderText="REG. DATE"></asp:BoundColumn>
                <asp:TemplateColumn>
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
                <asp:BoundColumn DataField="SUBCD" Visible="False"></asp:BoundColumn>
            </Columns>
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
        </asp:DataGrid>


    </form>
</asp:Content>
