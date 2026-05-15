<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationGTL.aspx.cs" Inherits="GLIFE_PROPOSAL.QuotationGTL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="row">
            <div class="col-xl-6 col-sm-4 mb-3" style="padding: 0px;">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <table style="border-spacing: 0px; width: 100%; font-size: 9pt;">
                        <tr id="TR_QUOTNO" runat="server">
                            <td style="width: 100px;">QUOTATION NO</td>
                            <td>
                                <asp:Label ID="LB_QUOTNO" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>POLICY NO</td>
                            <td>
                                <asp:Label ID="LB_POLICYNO" runat="server"></asp:Label>
                                <asp:Label ID="LB_POLICYID" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">COMPANY</td>
                            <td>
                                <asp:Label ID="LB_COMPANY" runat="server"></asp:Label>
                                <asp:Label ID="LB_COMPANYCODE" runat="server" Visible="False"></asp:Label></td>
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
                        <tr id="TR_BTSAVE" runat="server">
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ButtonColor" Width="100px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="col-xl-3 col-sm-4 mb-3" style="padding: 0px;">
                <div class="card-body" style="width: 100%;">
                    <table style="border-spacing: 0px; width: 100%; font-size: 9pt; text-wrap: none;">
                        <tr>
                            <td>VERSION NO</td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DDL_VERNO" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_VERNO_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="BT_NEW" runat="server" CssClass="ButtonColor" OnClick="BT_NEW_Click" Text="New" ForeColor="Green" BackColor="Gainsboro" Font-Italic="true" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BT_COPY" runat="server" CssClass="ButtonColor" OnClick="BT_COPY_Click" Text="Copy" ForeColor="Blue" BackColor="Gainsboro" Font-Italic="true" Visible="false" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BT_DEL" runat="server" CssClass="ButtonColor" Text="Del" Font-Bold="False" ForeColor="Red" BackColor="Gainsboro" Font-Italic="true" OnClick="BT_DEL_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>PERIOD</td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Font-Size="8" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td>- </td>
                                        <td>
                                            <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Font-Size="8" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>MEMBER</td>
                            <td>
                                <asp:Label ID="LB_MEMBER" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>PREMIUM</td>
                            <td>
                                <asp:Label ID="LB_PREMIUM" runat="server" ForeColor="DarkGreen"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="col-xl-3 col-sm-4 mb-3" style="padding: 0px;">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <asp:DataGrid ID="DGR_REPORT" runat="server" Font-Size="X-Small" GridLines="None"
                        PageSize="20" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_REPORT_ItemCommand">
                        <ItemStyle Wrap="False" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_REPORT" runat="server" CssClass="ASPButton" Width="200" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:Label ID="LB_STAT" runat="server" ForeColor="Red" Font-Bold="True" Font-Size="X-Small" Font-Underline="False"></asp:Label>
                </div>
            </div>

        </div>


        <div id="DV_BUTTONS" runat="server">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 20%;">
                                    <asp:Button ID="BT_PACKAGE" runat="server" CssClass="ButtonColor" Text="PRODUCT SPEC" Width="100%" OnClick="BT_PACKAGE_Click" Font-Size="8pt" />
                                </td>
                                <td style="width: 20%;">
                                    <asp:Button ID="BT_MEMBER" runat="server" CssClass="ButtonColor" Text="MEMBER" Width="100%" OnClick="BT_MEMBER_Click" Font-Size="8pt" />
                                </td>
                                <td style="width: 20%;">
                                    <asp:Button ID="BT_BRANCH" runat="server" CssClass="ButtonColor" Text="BRANCH" Width="100%" OnClick="BT_BRANCH_Click" Font-Size="8pt" />
                                </td>
                                <td style="width: 20%;">
                                    <asp:Button ID="BT_ARCHIEVE" runat="server" CssClass="ButtonColor" Text="ARCHIEVE" Width="100%" OnClick="ARCHIEVE_Click" Font-Size="8pt" />
                                </td>
                                <td style="width: 20%;">
                                    <asp:Button ID="BT_ACTUARY" runat="server" CssClass="ButtonColor" Text="CLOSING" Width="100%" OnClick="ACTUARY_Click" Font-Size="8pt" Enabled="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #DCDCDC; color: black; border: 2px groove #FFFFFF; text-align: center;">
                        <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
