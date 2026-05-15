<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_AGENCY.aspx.cs" Inherits="AGR.AGENT_AGENCY" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Bootstrap core CSS-->
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom fonts for this template-->
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <!-- Page level plugin CSS-->
    <link href="../include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="../include/css/sb-admin.css" rel="stylesheet" />
    <link href="../include/css/controls.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">

        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <div class="row">
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">

                    <tr>
                        <td style="width: 100px;">START DATE</td>
                        <td>
                            <asp:TextBox ID="TXT_START_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px;">END DATE</td>
                        <td>
                            <asp:TextBox ID="TXT_END_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>

                    <tr>
                        <td>UPLINER CODE</td>
                        <td>
                            <asp:TextBox ID="TXT_UPLINER" runat="server" Width="90%"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td>SUB</td>
                        <td>
                            <asp:DropDownList ID="DDL_SUB" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ButtonColor" Width="80" OnClick="BT_SAVE_Click" />
                        </td>
                        <td>
                            <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="col-xl-6 col-sm-6 mb-3">
                <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
                    GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                    <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                    <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                    <Columns>
                        <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="UPLINER_NAME" HeaderText="UPLINER"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCR" HeaderText="FROM"></asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                </asp:DataGrid>


            </div>
        </div>

    </form>
</body>
</html>
