<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationClinicalPathway.aspx.cs" Inherits="HLP.Form_Quot.QuotationClinicalPathway" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ccl" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function ReturnConfirm() {
            var agree = confirm("Apakah anda yakin??");
            if (agree)
                return true;
            else
                return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="width: 300px;">
                            <td>PROVIDER</td>
                            <td>
                                <asp:TextBox ID="txt_provider_search" runat="server" CssClass="ASPTextBox" Width="184px"></asp:TextBox>
                                <ccl:AutoCompleteExtender ID="AutocompleteextenderProvider" runat="server" ServiceMethod="SearchProvider"
                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txt_provider_search" FirstRowSelected="false">
                                </ccl:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>DIAGNOSA</td>
                            <td>
                                <asp:TextBox ID="txt_diagnosa_search" runat="server" CssClass="ASPTextBox" Width="184px"></asp:TextBox>
                                <ccl:AutoCompleteExtender ID="AutocompleteextenderDiagnosa" runat="server" ServiceMethod="SearchDiagnosa"
                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txt_diagnosa_search" FirstRowSelected="false">
                                </ccl:AutoCompleteExtender>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="CARI" />
                                <asp:Button ID="BT_CLEAR" runat="server" CssClass="ASPButton" OnClick="BT_CLEAR_Click" Text="CLEAR" />
                                <asp:Button ID="BT_EXPORT" runat="server" CssClass="ASPButton" OnClick="BT_EXPORT_Click" Text="EXPORT to Excel" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_CLINICAL_PATHWAY" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        OnItemCommand="DGR_CLINICAL_PATHWAY_ItemCommand"
                        BorderWidth="1px" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False">
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KODE_PROVIDER" HeaderText="KODE PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA_PROVIDER" HeaderText="NAMA PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KODE_DIAGNOSA" HeaderText="ICDX"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DIAGNOSA" HeaderText="DIAGNOSA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAMA_RAWAT" HeaderText="LAMA RAWAT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KELAS_KAMAR" HeaderText="KELAS KAMAR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BIAYA_KAMAR_FORMAT" HeaderText="BIAYA KAMAR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL_BIAYA_CP_FORMAT" HeaderText="TOTAL BIAYA CP"></asp:BoundColumn>
                           <%-- <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_VIEW" runat="server" CommandName="View" CssClass="ASPButton" Text="VIEW" />
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <PagerStyle BackColor="#999999" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>



                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>

    </form>
</body>
</html>
