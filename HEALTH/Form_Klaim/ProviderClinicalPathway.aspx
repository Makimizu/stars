<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderClinicalPathway.aspx.cs" Inherits="HEALTH.Form_Klaim.ProviderClinicalPathway" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ccl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function ReturnConfirm() {
            var agree = confirm("Apakah anda yakin??");
            if (agree)
                return true;
            else
                return false;
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 186px;
        }

        .auto-style2 {
            height: 30px;
        }

        .auto-style3 {
            width: 186px;
            height: 30px;
        }


        .ASPDatagrid {
            font-family: Arial, sans-serif;
            font-size: 12px;
            border-collapse: collapse;
            margin: 5px 0;
        }

            .ASPDatagrid td, .ASPDatagrid th {
                padding: 6px 8px;
            }

        .auto-style4 {
            height: 181px;
        }
    </style>
</head>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<body>

    <form id="form1" runat="server" enctype="multipart/form-data">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server" EnablePageMethods="true">
        </ajaxToolkit:ToolkitScriptManager>


        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_NAME" runat="server" Visible="false"></asp:Label>
        <table>
      

                <td class="auto-style4">
                    <table style="border-spacing: 0px;">
                        Clinical Pathway By ICD
           <tr style="width: 300px;">

               <td>DOWNLOAD TEMPLATE EXCEL</td>
               <td class="auto-style1">
                   <asp:Button ID="BT_TEMPLATE_DOWNLOAD_PROV" runat="server" CssClass="ASPButton" OnClick="BT_TEMPLATE_DOWNLOAD_PROV_Click" Text="TEMPLATE EXCEL" />
               </td>
           </tr>
                        <tr>
                            <td>GENERETE DATA BULK INSERT / UPDATE</td>
                            <td class="auto-style1"></td>
                        </tr>
                        <tr>
                            <td>UPLOAD TEMPLATE</td>
                            <td class="auto-style1">&nbsp;<input id="TXT_FILE_UPLOAD_BULK_PROV" runat="server" name="TXT_FILE_UPLOAD_BULK_PROV" type="file"
                                class="ASPTextBox" size="20" /></td>
                        </tr>
                        <tr>
                            <%-- <td>UPLOAD LAMPIRAN</td>
               <td class="auto-style1">
                   <input id="TXT_FILE_LAMPIRAN_PROV" runat="server" name="TXT_FILE_LAMPIRAN_PROV" type="file"
                       class="ASPTextBox" />
               </td>--%>
                        </tr>
                        <tr>
                            <td class="auto-style2"></td>
                            <td class="auto-style3">
                                <asp:Button ID="BT_GENERETE_PROV" runat="server" CssClass="ASPButton" Text="UPLOAD DATA" OnClick="BT_GENERETE_PROV_Click" Height="26px" />
                            </td>




                        </tr>
                            </table>

                    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="700px" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                                    <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <center>
                                        <iframe id="ifClaim" runat="server" src="" width="100%" height="450"></iframe>
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>


                    <table style="width: 100%; border: 1px solid yellow; background-color: blue;">
                        <tr></tr>
                        <tr></tr>
                        <tr>
                            <td style="text-align: center; color: red; font-weight: bold; font-size: 12px;">UPLOAD ERROR LOG
                            </td>
                        </tr>
                    </table>


                    <asp:DataGrid ID="DGR_CLINICAL_PATHWAY_LOG" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="ASPDatagrid"
                        Width="100%" BorderStyle="None"
                        GridLines="None" CellPadding="4"
                        ItemStyle-BackColor="#FFFFFF"
                        AlternatingItemStyle-BackColor="#DCDCDC"
                        HeaderStyle-BackColor="#000084"
                        HeaderStyle-ForeColor="White"
                        HeaderStyle-Font-Bold="True"
                        HeaderStyle-Font-Underline="True"
                        HeaderStyle-HorizontalAlign="Center"
                        OnItemCommand="DGR_CLINICAL_PATHWAY_LOG_ItemCommand" Font-Size="Smaller">

                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIPTION" HeaderText="DESKRIPSI ERROR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CREATE_DATE" HeaderText="WAKTU UPLOAD"></asp:BoundColumn>

                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <PagerStyle BackColor="#999999" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>





            <div class="">
                  <tr>
      <td style="text-align: center; color: black; font-weight: bold; font-size: 12px;">UPLOAD RESULT
      </td>
  </tr>



 
            
            <asp:DataGrid ID="DGR_CLINICAL_PATHWAY" runat="server" AutoGenerateColumns="False" AllowSorting="True" 
    AllowPaging="True" PageSize="10"  OnPageIndexChanged="DGR_CLINICAL_PATHWAY_PageIndexChanged"   OnItemCommand="DGR_CLINICAL_PATHWAY_ItemCommand"
   CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="Both">
    
    <Columns>
          <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
        <asp:BoundColumn DataField="KODE_DIAGNOSA" HeaderText="KODE_DIAGNOSA" SortExpression="KODE_DIAGNOSA"></asp:BoundColumn>
        <asp:BoundColumn DataField="DIAGNOSA" HeaderText="DIAGNOSA" SortExpression="DIAGNOSA"></asp:BoundColumn>                             
        <asp:BoundColumn DataField="LOS" HeaderText="LOS" SortExpression="LOS"></asp:BoundColumn>  
        <asp:BoundColumn DataField="Kelas 3" HeaderText="Kelas 3" SortExpression="Kelas 3"></asp:BoundColumn> 
        <asp:BoundColumn DataField="Kelas 2" HeaderText="Kelas 2" SortExpression="Kelas 2"></asp:BoundColumn> 
        <asp:BoundColumn DataField="Kelas 1" HeaderText="Kelas 1" SortExpression="Kelas 1"></asp:BoundColumn> 
        <asp:BoundColumn DataField="Kelas Utama" HeaderText="Kelas Utama" SortExpression="Kelas Utama"></asp:BoundColumn> 
        <asp:BoundColumn DataField="VVIP" HeaderText="VVIP" SortExpression="VVIP"></asp:BoundColumn> 
        <asp:BoundColumn DataField="VIP" HeaderText="VIP" SortExpression="VIP"></asp:BoundColumn>                          
      <asp:BoundColumn DataField="Super VIP" HeaderText="Super VIP" SortExpression="Super VIP"></asp:BoundColumn>       
        <asp:BoundColumn DataField="CreateDate" HeaderText="Waktu Upload" SortExpression="CreateDate"></asp:BoundColumn>
                  <asp:TemplateColumn>
              <ItemTemplate>
                  &nbsp;<asp:Button ID="BT_DELETE" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="DELETE" />
              </ItemTemplate>
          </asp:TemplateColumn>
          </Columns>

    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
</asp:DataGrid>
            </div>
            <td valign="top">


 

                  

            </td>
             
            <tr>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
