<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyBranch.aspx.cs" Inherits="GLIFE_PROPOSAL.CompanyBranch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <link href="include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: 8pt; font-family: Verdana;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="border-bottom: ridge;">
                            <td>EXISTING BRANCH
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_BRANCH" runat="server" CausesValidation="True" CssClass="ASPDropDownList" BackColor="#ffffcc" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DDL_BRANCH_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px;">BRANCH CODE
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_KDCAB" runat="server" Enabled="False" MaxLength="30" Width="179px"
                                    CssClass="ASPTextBox"></asp:TextBox>
                                <asp:Button ID="BT_NEWCAB" runat="server" OnClick="BT_NEWCAB_Click" Text="NEW" CssClass="ASPButton" Width="50px" />
                                &nbsp;<asp:Button ID="BT_SAVE_CAB" runat="server" OnClick="BT_SAVE_CAB_Click" Text="SAVE" CssClass="ASPButton" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td>BRANCH NAME
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_NAMACAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CLIENT CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_KODECAB2" runat="server" MaxLength="25" Width="179px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ADDRESS 1</td>
                            <td>
                                <asp:TextBox ID="TXT_ALAMATCAB1" runat="server" MaxLength="255" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ADDRESS 2</td>
                            <td>
                                <asp:TextBox ID="TXT_ALAMATCAB2" runat="server" MaxLength="255" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PHONE</td>
                            <td>
                                <asp:TextBox ID="TXT_PHNCAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>FAX</td>
                            <td>
                                <asp:TextBox ID="TXT_FAXCAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>EMAIL
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_EMAILCAB" runat="server" MaxLength="100" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PIC
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_PICCAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PIC TITLE</td>
                            <td>
                                <asp:TextBox ID="TXT_PICTITLECAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>EMAIL PIC
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_PICEMAILCAB" runat="server" MaxLength="100" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CITY
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_KOTAMADYACAB" runat="server" MaxLength="50" Width="363px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PROVINCE&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_PROPCAB" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>CLIENT AREA CODE&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_KODEWIL" runat="server" MaxLength="50" Width="100px"
                                    CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ZIP CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_KODEPOSCAB" runat="server" MaxLength="10" Width="100px" CssClass="ASPTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;<asp:Label ID="LB_ERR" runat="server" Font-Bold="False" Font-Size="XX-Small" ForeColor="Red"
                                CssClass="ASPLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_ACCOUNT" runat="server" CellPadding="4" ForeColor="#333333" OnItemCommand="DGR_ACCOUNT_ItemCommand" AutoGenerateColumns="False" Font-Size="X-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Width="95%" GridLines="None">
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="TIPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_DESCR" HeaderText="ACCOUNT TYPE">
                                <HeaderStyle Width="200" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCNAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCBANK" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="#ACC">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_ACCNO0" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ACC NAME">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_NAMAREK0" runat="server" CssClass="ASPTextBox" Width="250px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="BANK">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_BANK0" Width="200px" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_KORESPONDEN" runat="server" CellPadding="4" ForeColor="#333333" OnItemCommand="DGR_ACCOUNT_ItemCommand" AutoGenerateColumns="False" GridLines="None" Font-Size="X-Small" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Width="95%">
                        <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="TIPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_DESCR" HeaderText="CORESPONDENCE TYPE">
                                <HeaderStyle Width="200" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PIC_NAMA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PIC_SALUTATION" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PIC_TITLE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PIC_PHONE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PIC_EMAIL" Visible="False"></asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="SALUTATION">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_PICSALUTATION" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NAME">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PICNAMA" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="100"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TITLE">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PICTITLE" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="30"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PHONE">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PICPHONE" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="100"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="EMAIL">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PICEMAIL" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="1000"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
