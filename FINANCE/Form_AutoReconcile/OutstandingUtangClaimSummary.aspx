<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutstandingUtangClaimSummary.aspx.cs" Inherits="FINANCE.Form_AutoReconcile.OutstandingUtangClaimSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="loading" style="display:none; position:fixed; top:50%; left:50%; transform:translate(-50%, -50%); z-index:9999; background-color: rgba(255, 255, 255, 0.8); padding: 20px; border-radius: 10px; text-align: center;">
            <img src="../Content/loading.gif" alt="Loading..." style="border: 0; width: 100px;" />
            <p>Loading...</p>
        </div>

        <asp:Label ID="LB_APP" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>

        <asp:HiddenField ID="hdnStartDate" runat="server" />
        <asp:HiddenField ID="hdnEndDate" runat="server" />
         <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
               <tr id="TR_STL1" runat="server">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                             <td style="width: 100px;">CHANNEL</td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_CHANNEL_SelectedIndexChanged">
                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">DOCNO</td>
                            <td>
                                <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">POLICY NO</td>
                            <td>
                                <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">STL DOCNO</td>
                            <td>
                                <asp:TextBox ID="TXT_STL_DOCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">NAMA LEMBAGA</td>
                            <td>
                                <asp:TextBox ID="TXT_NAMALEMBAGA" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">NAMA PESERTA</td>
                            <td>
                                <asp:TextBox ID="TXT_NAMAPESERTA" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_STARTDATE" runat="server" Text="START DATE" CssClass="ASPLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;<asp:Label ID="LBL_ENDDATE" runat="server" Text="END DATE" CssClass="ASPLabel"></asp:Label>
                                <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" OnClientClick="showLoading();" />
                                <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" OnClick="BT_DOWNLOAD_Click" OnClientClick="getDownload();" Text="DOWNLOAD" />                        
                             </td>
                        </tr>
                    </table>
                </td>
            </tr>
             <tr id="TR_STL2" runat="server">
                <td>
                    
                    <br />
                    <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                    <br />


                     <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" 
                        OnItemCommand="DGR_ItemCommand" Width="100%" ItemStyle-Wrap="true" 
                        AutoGenerateColumns="False" AllowPaging="True" 
                        OnPageIndexChanged="DGR_PageIndexChanged" PageSize="50" CssClass="ASPDatagrid">

                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" Wrap="false"/>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Left" Mode="NumericPages" />
                        <Columns>

                          
                            
                            <asp:BoundColumn DataField="DATE" HeaderText="DATE" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
<asp:BoundColumn DataField="IS_HKP" HeaderText="IS HKP"></asp:BoundColumn>
<asp:BoundColumn DataField="IS_LINK" HeaderText="IS LINK"></asp:BoundColumn>
<asp:BoundColumn DataField="DOCNO" HeaderText="DOC NO"></asp:BoundColumn>
<asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
<asp:BoundColumn DataField="PRODUCT_CODE" HeaderText="PRODUCT CODE"></asp:BoundColumn>
<asp:BoundColumn DataField="PRODUCT_NAME" HeaderText="PRODUCT DESCR"></asp:BoundColumn>
<asp:BoundColumn DataField="PRODUCT_GROUP" HeaderText="PRODUCT GROUP"></asp:BoundColumn>
<asp:BoundColumn DataField="LINE_OF_BUSINESS" HeaderText="LINE OF BUSINESS"></asp:BoundColumn>
<asp:BoundColumn DataField="STL_DOCNO" HeaderText="STL DOCNO"></asp:BoundColumn>

<asp:BoundColumn DataField="LEMBAGA" HeaderText="LEMBAGA"></asp:BoundColumn>
<asp:BoundColumn DataField="ALAMAT_LEMBAGA" HeaderText="ALAMAT LEMBAGA"></asp:BoundColumn>
<asp:BoundColumn DataField="CONTACT_NO_LEMBAGA" HeaderText="CONTACT NO LEMBAGA"></asp:BoundColumn>

<asp:BoundColumn DataField="PESERTA" HeaderText="NAMA PESERTA"></asp:BoundColumn>
<asp:BoundColumn DataField="ALAMAT_PESERTA" HeaderText="ALAMAT PESERTA"></asp:BoundColumn>
<asp:BoundColumn DataField="CONTACT_NO_PESERTA" HeaderText="CONTACT NO LEMBAGA"></asp:BoundColumn>

<asp:BoundColumn DataField="KETERANGAN_CLAIM" HeaderText="KETERANGAN CLAIM"></asp:BoundColumn>

<asp:BoundColumn DataField="APPROVE_DATE" HeaderText="APPROVE DATE" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
<asp:BoundColumn DataField="STL_DATE" HeaderText="SETTLE DATE" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>

<asp:BoundColumn DataField="JENIS_KLAIM" HeaderText="JENIS KLAIM"></asp:BoundColumn>
<asp:BoundColumn DataField="AKAD" HeaderText="AKAD"></asp:BoundColumn>

<asp:BoundColumn DataField="AMOUNT_BEBANKLAIM" HeaderText="BEBAN KLAIM" DataFormatString="{0:N2}">
    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
    <ItemStyle Font-Bold="True" ForeColor="Green" HorizontalAlign="Right" />
</asp:BoundColumn>
<asp:BoundColumn DataField="AMOUNT_UTANGKLAIM" HeaderText="HUTANG KLAIM" DataFormatString="{0:N2}">
    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
    <ItemStyle Font-Bold="True" ForeColor="Green" HorizontalAlign="Right" />
</asp:BoundColumn>
<asp:BoundColumn DataField="AMOUNT_PAID" HeaderText="AMOUNT PAID STARS" DataFormatString="{0:N2}">
    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
    <ItemStyle Font-Bold="True" ForeColor="Green" HorizontalAlign="Right" />
</asp:BoundColumn>
<asp:BoundColumn DataField="AMOUNT_COLDEP" HeaderText="AMOUNT COLLDEP STARS" DataFormatString="{0:N2}">
    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
    <ItemStyle Font-Bold="True" ForeColor="Green" HorizontalAlign="Right" />
</asp:BoundColumn>
<asp:BoundColumn DataField="AMOUNT_RETURIN" HeaderText="AMOUNT RETUR IN" DataFormatString="{0:N2}">
    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
    <ItemStyle Font-Bold="True" ForeColor="Green" HorizontalAlign="Right" />
</asp:BoundColumn>
<asp:BoundColumn DataField="AMOUNT_RETUROUT" HeaderText="AMOUNT RETUR OUT" DataFormatString="{0:N2}">
    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
    <ItemStyle Font-Bold="True" ForeColor="Green" HorizontalAlign="Right" />
</asp:BoundColumn>
<asp:BoundColumn DataField="TGL_PAID" HeaderText="TANGGAL BAYAR STARS" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
<asp:BoundColumn DataField="TGL_COLDEP" HeaderText="TANGGAL COLLDEP" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
<asp:BoundColumn DataField="TGL_RETURIN" HeaderText="TANGGAL RETUR" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
<asp:BoundColumn DataField="TGL_RETUROUT" HeaderText="TANGGAL PBY RETUR" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
<asp:BoundColumn DataField="OUTSTANDING" HeaderText="OUTSTANDING" DataFormatString="{0:N2}">
    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
    <ItemStyle Font-Bold="True" ForeColor="Green" HorizontalAlign="Right" />
</asp:BoundColumn>
  <asp:templatecolumn headertext="#" itemstyle-width="300px" >
                                <itemtemplate>

                                    <asp:linkbutton id="lb_detail" runat="server" cssclass="asplabel" text="VIEW" />
                                     
                                </itemtemplate>
                                <headerstyle font-bold="true" font-italic="false" font-overline="false" font-strikeout="false" font-underline="false" horizontalalign="center" />
                                <itemstyle horizontalalign="center" />
                            </asp:templatecolumn>



                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Font-Size="Medium" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        function showLoading() {
            document.getElementById("loading").style.display = "block";
        }

        function hideLoading() {
            document.getElementById("loading").style.display = "none";
        }

        // Menyembunyikan loading setelah halaman selesai dimuat
        window.onload = function () {
            hideLoading();
        };
    </script>
</body>
</html>
