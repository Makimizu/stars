<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="BatchUpload.aspx.cs" Inherits="GLIFE_PROPOSAL.BatchUpload" %>
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
        <div id="DV_POLICY" runat="server">
            <table style="border-spacing: 0px; width: 100%; font-size: small;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%; font-size: small;">
                            <tr>
                                <td style="width: 120px;">SEARCH POLICY</td>
                                <td>
                                    <asp:TextBox ID="TXT_POLICYSEARCH" runat="server" AutoPostBack="true" Width="100%" OnTextChanged="TXT_POLICYSEARCH_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="DGR_POLICY" runat="server" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical" OnItemCommand="DGR_POLICY_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_POLICY_PageIndexChanged">
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TC_ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="POLICY NO">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBT_POLICY" runat="server" CommandName="Select"></asp:LinkButton>
                                        <asp:LinkButton ID="UPLOAD" runat="server" CommandName="Upload" Visible="false"></asp:LinkButton>
                                        <asp:LinkButton ID="POLICY_CHECK" runat="server" CommandName="PolicyCheck" Visible="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DESCR" HeaderText="PRODUCT"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="POLICY HOLDER"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PERIOD_START" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PERIOD_END" Visible="False"></asp:BoundColumn>
                            </Columns>
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>

        <div id="DV_UPLOAD" runat="server" visible="false">
            <table style="border-spacing: 0px; width: 100%; font-size: small;">
                <tr>
                    <td>
                        <asp:LinkButton ID="LBT_POLICYBACK" runat="server" ForeColor="Red" ToolTip="Back to policy selection ..." OnClick="LBT_POLICYBACK_Click">
                            <span class="fa fa-backward"></span>&nbsp;BACK
                        </asp:LinkButton>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 120px;">POLICY NO</td>
                    <td>
                        <asp:Label ID="LB_POLICYNO" runat="server"></asp:Label>
                        <asp:Label ID="LB_POLICYID" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>POLICY HOLDER</td>
                    <td>
                        <asp:Label ID="LB_COMPANY" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>PRODUCT</td>
                    <td>
                        <asp:Label ID="LB_PRODUCT" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>AGENT</td>
                    <td>
                        <asp:DropDownList ID="DDL_AGENT" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>BRANCH</td>
                    <td>
                        <asp:DropDownList ID="DDL_BRANCH" runat="server"></asp:DropDownList>
                        &nbsp;
                        <asp:LinkButton ID="LB_BRANCHXLS" runat="server" ToolTip="Download Branch Code" ForeColor="Green" OnClick="LB_BRANCHXLS_Click" >
                        <span class="fa fa-list"></span>
                        </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>UPLOAD FORMAT</td>
                    <td>
                        <asp:DropDownList ID="DDL_FORMAT" runat="server"></asp:DropDownList>
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
                    <td>POLICY TYPE</td>
                    <td>
                        <asp:Label ID="LB_POLICY_TYPE" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="height: 21px">POLICY PERIODE START</td>
                    <td style="width: 100px; height: 21px;">
                        <asp:Label ID="LB_POLICY_PERIODE_START" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>POLICY PERIODE END</td>
                        <td style="width: 100px;">
                        <asp:Label ID="LB_POLICY_PERIODE_END" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="LB_ERR" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>STNC DATE</td>
                    <td>
                        <asp:TextBox ID="TXT_STNC" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                        <ajaxtoolkit:calendarextender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STNC">
                        </ajaxtoolkit:calendarextender>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>
