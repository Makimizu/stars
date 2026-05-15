<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimApp.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
   <%-- <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.1.js"></script>
    <script src="../Scripts/sweetalert2.all.min.js"></script>--%>

    <%--<script type="text/javascript">
        function sweetConfirmation() 
        {
               Swal.fire({
                   title: "Pengajuan klaim lebih dari 90 Hari Kalender (dihitung dari tanggal risiko s/d tanggal dokumen lengkap), apakah anda akan melanjutkan proses klaim ini?",
                //type: 'question',
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
    </script>--%>

     
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.js"></script>
    <link href="../Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ShowPopup(message) {
            $("#dialog").html(message);
            $("#dialog").dialog({
                //title: "Information",
                modal: true,
                width: 800,
                buttons: {
                    Yes: function () {
                        $(this).dialog('close');
                        $('[id*=buttonHide]').trigger('click');
                    },
                    
                    Cancel: function () {
                        $(this).dialog('close');
                        $('[id*=btnCancel]').trigger('click');
                    }
                }
            }).css("font-size", "18px", "font-family", "Verdana");;
        };
</script>
     
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        

        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dialog" style="display: none">
                </div>
                    
            </ContentTemplate>
        </asp:UpdatePanel>

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 40%; border-right-style: groove;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">CLAIM NO</td>
                                        <td>
                                            <asp:Label ID="LB_REGNO" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                            &nbsp;-&nbsp;
                                            <asp:DropDownList ID="DDL_SEQ" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_SEQ_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>FULL NAME</td>
                                        <td>
                                            <asp:Label ID="LB_NAME" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>DATE OF BIRTH</td>
                                        <td>
                                            <asp:Label ID="LB_DOB" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>GENDER / START AGE</td>
                                        <td>
                                            <asp:Label ID="LB_GENDER" runat="server" Font-Bold="True"></asp:Label>&nbsp;-
                                            <asp:Label ID="LB_AGE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">POLICY NO</td>
                                        <td>
                                            <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="True"></asp:Label>&nbsp;-
                                            <asp:Label ID="LB_PRODUCT" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="LB_TC_ID" runat="server" Font-Bold="False" Visible="False"></asp:Label>
                                            <asp:Label ID="LB_READONLY" runat="server" Font-Bold="False" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLICY HOLDER</td>
                                        <td>
                                            <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>PERIOD</td>
                                        <td>
                                            <asp:Label ID="LB_PERIOD" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>SUM INSURED</td>
                                        <td>
                                            <asp:Label ID="LB_SUMINS" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>U/W CODE</td>
                                        <td>
                                            <asp:Label ID="LB_UWCODE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr id="TR_TYPE" runat="server" visible="false">
                                        <td>CLAIM TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">CLAIM DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CLAIMDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CLAIMDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DOC RECEIVE DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RECEIVEDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_RECEIVEDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DOC COMPLETE DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPLETEDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_COMPLETEDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>OCCURED DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_OCCUREDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_OCCUREDDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>LOCATION</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_LOCATION" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="TR_MEMBERSHIP" runat="server" visible="false" style="vertical-align: top;">
                                        <td>LENGTH OF MEMBERSHIP</td>
                                        <td>
                                            <asp:Label ID="LB_LOM" runat="server"></asp:Label>
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td></td> 
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" Font-Bold="True" ForeColor="Blue" OnClick="BT_SAVE_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_PENDING" runat="server" CssClass="ASPButton" Text="PENDING" Width="100px" BackColor="Yellow" ForeColor="Red" OnClick="BT_PENDING_Click" Visible="False" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 40%;">
                                <asp:DataGrid ID="DGR_TRACK" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_TRACK_ItemCommand" ShowHeader="False">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMMENT" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 100px;">
                                                            <asp:Button ID="BT_TRACK" runat="server" CommandName="Next" CssClass="ASPButton" Width="100px"/></td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_COMMENT" runat="server" Height="25px" Width="98%" Visible="false" MaxLength="1000" TextMode="MultiLine" CssClass="ASPTextBox" placeholder="Type your reason comment ..."></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>

                                <table id="TBL_STAT" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px; text-align: left;">STATUS</td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="LB_STAT" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td style="text-align: left;">COMMENT</td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="LB_STATCOMMENT" runat="server"></asp:Label></td>
                                    </tr>
                                </table>

                                <table id="TBL_BL" runat="server" style="border-spacing: 0px; width: 100%;" visible="false">
                                    <tr style="vertical-align: top;">
                                        <td style="text-align: left;" colspan="2">
                                            <asp:Label ID="LB_BLACKLIST" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                                <!--AHMAD ZULFAHMI 20211027-->
                                <table style="border-spacing: 0px; width: 100%;" id="TB_UNAPPROVE">
                                    <tr style="vertical-align: top;">
                                        <td style="width: 100px;">
                                            <asp:Button ID="BTN_UNAPPROVE" Visible="false" runat="server" CssClass="ASPButton" Text="UNAPPROVE" Width="100px" Font-Bold="True" ForeColor="Blue" OnClick="BT_UNAPPROVE_Click" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_UNAPPROVE" Visible="false" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td style="text-align: left;"></td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="UNAPPROVE_REMARK" runat="server" Height="25px" Width="98%" Visible="false" MaxLength="1000" TextMode="MultiLine" CssClass="ASPTextBox" placeholder="Type your reason comment ..."></asp:TextBox>

                                        </td>
                                    </tr>
                                </table>
                                <!--AHMAD ZULFAHMI 20211027-->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="TR_BLACKLIST" visible="false">
                <td>
                    <table runat="server" id="TBL_BLACKLIST" style="border-spacing:0px; width:100%;">
                        <tr style="vertical-align:top;">
                            <td style="text-align:left;padding:10px;">
                                <asp:Label ID="TEXT_BLACKLIST" Text="" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_BUTTONS" runat="server" visible="false">
                <td>
                    <table id="TBL_BAWAH" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="SUBMISSION" Width="100%" OnClick="BT1_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="DOCUMENTS & ARCHIEVE" Width="100%" OnClick="BT2_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT4" runat="server" CssClass="ASPButton" Text="UNDERWRITING INFO" Width="100%" OnClick="BT4_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT6" runat="server" CssClass="ASPButton" Text="PRODUCT INFO" Width="100%" OnClick="BT6_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT5" runat="server" CssClass="ASPButton" Text="REMARK & NOTIFICATION" Width="100%" OnClick="BT5_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Text="SHARES & PAYMENT" Width="100%" OnClick="BT3_Click" />
                                        </td>
                                        <td style="width: 14%;">
                                            <asp:Button ID="BT7" runat="server" CssClass="ASPButton" Text="TRACKS" Width="100%" OnClick="BT7_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">
                                <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td><button runat="server" id="buttonHide" onserverclick="buttonHide_ServerClick" style="visibility:hidden" >Button Hide</button></td>
            </tr>
        </table>

    </form>
</body>
</html>




