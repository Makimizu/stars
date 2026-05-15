<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimPending.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimPending" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.1.js"></script>
    <script src="../Scripts/sweetalert2.all.min.js"></script>
    <script type="text/javascript">

        function showLoading() { 
            Swal.fire({
                title: 'Please wait...',
                html: '<div class="swal-loading"></div>',
                allowOutsideClick: false,
                showConfirmButton: false,
                didOpen: () => {
                    Swal.showLoading(); // Show loading animation
                }
            }); 
        } 
         
        function hideLoading() {
            Swal.close(); // Close the Swal popup
        }

        function sweetConfirmation() {
            LB_TRACK = document.getElementById("LB_TRACK");
            var sMsg = "";
            if (LB_TRACK.innerHTML == "1") {
                sMsg = "verifikasi";
            }
            else if (LB_TRACK.innerHTML == "2") {
                sMsg = "approve";
            }

            let timerInterval;

            Swal.fire({
                title: "Apakah anda yakin " + sMsg + " data klaim?",
                type: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve()
                        }, 1000)
                    })
                }
            }).then(function (result) {
                if (result.value == true) {
                    document.getElementById("buttonHide").click();
                }
            })

            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label1" runat="server" Text="NAMA PESERTA"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label8" runat="server" Text="TGL LAHIR"></asp:Label></td>

                                        <td>

                                            <asp:TextBox ID="TXT_DOB" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>

                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB">

                                            </ajaxToolkit:CalendarExtender>

                                        &nbsp;DD/MM/YYYY</td>

                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label2" runat="server" Text="PERUSAHAAN"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label3" runat="server" Text="DATA REKENING"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_REK" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value="1">LENGKAP</asp:ListItem>
                                                <asp:ListItem Value="0">TIDAK LENGKAP</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="border-spacing: 0px">
                                    <tr>
                                        <td class="auto-style7"></td>
                                        <td style="width: 80px;">CLAIM NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CLAIMNO" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style7">
                                            <asp:Label ID="LB_TRACK" runat="server"  style="visibility:hidden;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="P/R"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PR" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style7"></td>

                    <td style="width: 80px;">PROVIDER</td>

                    <td>

                        <asp:TextBox ID="PROVIDER" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>

                    </td>

                </tr>

                <tr>
                                        <td class="auto-style7">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="TIPE"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style7">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="CP"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CP" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td>
                                <table style="border-spacing: 0px">
                                    <tr>
                                        <td class="auto-style8">&nbsp;</td>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label6" runat="server" Text="TGL KLAIM"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                            </ajaxToolkit:CalendarExtender>
                                            -
                                            <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        &nbsp;DD/MM/YYYY</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style8">&nbsp;</td>
                                        <td class="auto-style5">DOC. SOURCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_DOC_SOURCE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                        </tr><tr>

                    <td class="auto-style8">&nbsp;</td>

                    <td class="auto-style5">TOTAL BAYAR</td>

                    <td>

                        <asp:TextBox ID="TOT_BAYAR1" runat="server" CssClass="ASPTextBox" Width="100px" onkeypress="return CheckNumeric();" Style="text-align: left;"></asp:TextBox> &nbsp;-&nbsp; 

                        <asp:TextBox ID="TOT_BAYAR2" runat="server" CssClass="ASPTextBox" Width="100px" onkeypress="return CheckNumeric();" Style="text-align: left;"></asp:TextBox> 

                    </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style8">&nbsp;</td>
                                        <td class="auto-style5">NOMOR SM</td>
                                        <td>
                                            <asp:TextBox ID="TXT_SM" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                            <asp:Button ID="BT_CARI" runat="server" CssClass="ASPButton" Text="CARI"
                                                OnClientClick="showLoading();" OnClick="BT_CARI_Click" />
                                            &nbsp;<asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <button runat="server" id="buttonHide" onserverclick="BT_PROSES_Click" style="visibility:hidden" >Button Hide</button>
            <tr>
                <td id="TD_WARNING" runat="server" style="padding:15px; border:2px solid red; margin:0px 10px" visible="false">
                    <asp:Label ID="LB_WARNING" runat="server" ForeColor="Red" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td> 
                    <asp:DropDownList ID="DDL_NEXTTRACK" runat="server" CssClass="ASPDropDownList" Visible="False">
                    </asp:DropDownList>  
                    <%-- Push by Firman --%>
                    <%--<asp:Button ID="BT_PROSES" runat="server" BackColor="#66FF99" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SUBMIT" OnClick="BT_PROSES_Click" />--%>
                    <%--<asp:Button ID="BT_PROSES" runat="server" BackColor="#66FF99" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SUBMIT" OnClientClick="return sweetConfirmation(); return false;" />--%>
                    <asp:Button ID="Button1" runat="server" CssClass="ASPButton" Text="Clinical Pathway" Width="100px" ForeColor="Green" OnClick="ShowCPPanel" Visible="false" />
                    <asp:Button ID="BT_PROSES" runat="server" BackColor="#66FF99" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="SUBMIT" OnClick="BT_PROSES2_Click" />
                    <asp:Button ID="BT_PROSES2" runat="server" BackColor="#66FF99" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text=" " Visible="false" OnClick="BT_PROSES2_Click" />

                    &nbsp;<asp:Label ID="LB_CNT" runat="server" Font-Bold="True"></asp:Label><br />
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3"  DataKeyField="AMOUNT_BAYAR"
                        GridLines="Vertical" CssClass="ASPDatagrid" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" PageSize="40">
                        <ItemStyle Wrap="False" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="LBT_ALL" runat="server" CommandName="All" CssClass="ASPLabel" Font-Bold="True" ForeColor="White">ALL</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" Style="Height: 10px;" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CLAIM_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOC_NO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="NOREG CLAIM">
                                <ItemTemplate>
                                    <asp:Label ID="LB_CLAIMNO" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SURAT MASUK" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LB_DOC_NO" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PR_DESCR" HeaderText="P/R"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_CLAIM_DESCR" HeaderText="TIPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_KLAIM" HeaderText="TGL KLAIM"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVIDER" HeaderText="PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PEC" HeaderText="PEC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ICD_CODE" HeaderText="KODE ICD"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_PENGAJUAN" HeaderText="PENGAJUAN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_CASH" HeaderText="CASH">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_BAYAR" HeaderText="BAYAR">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="JUMLAH_TOLAK" HeaderText="TOLAK">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EKSES" HeaderText="EXCESS">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REFUND" HeaderText="REFUND">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DONE" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="APL" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    &nbsp;<asp:Button ID="BT_CETAKAPL" runat="server" BackColor="Blue" CommandName="CetakAPL" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="CETAK" />
                                    &nbsp;<asp:Button ID="BT_APPAPL" runat="server" BackColor="Green" CommandName="AppAPL" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="APPROVE" Visible="false" />
                                    &nbsp;
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ICD_CODE" HeaderText="ICD"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS_CP" HeaderText="CP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL_BIAYA_CP" HeaderText="TOTAL BIAYA CP"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
                                <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="800px" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td colspan="2" class="TDBGColor">
                                                <asp:LinkButton ID="LB_TITLE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Black"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <center>
                                                <iframe id="ifClaim" runat="server" src=""  width="100%" height="450"></iframe>
                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
    </form>
</body>
</html>
