<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HalodocRPT.aspx.cs" Inherits="HEALTH.Form_Tools.HalodocRPT" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 120px;
            height: 28px;
        }
        .auto-style2 {
            height: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 120px;">REPORT TYPE</td>
                <td>
                    <asp:DropDownList ID="ddreportType" runat="server" CssClass="ASPDropDownList" Width="40%" BackColor="Yellow">
                        <asp:ListItem Value="0">HALODOC DETAIL</asp:ListItem>
                        <asp:ListItem Value="1">HALODOC HEADER</asp:ListItem>
                        <asp:ListItem Value="2">ADMEDIKA ECLAIM</asp:ListItem>

                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 120px;">POLICY NO</td>
                <td>
                    <asp:TextBox ID="txtPolicyNo" runat="server" CssClass="ASPTextBox" Width="35%"></asp:TextBox>&nbsp;&nbsp
                    <asp:CheckBox ID="chkNull" runat="server" Text="NULL" AutoPostBack="true" OnCheckedChanged="chkNull_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td style="width: 120px;">START DATE</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="ASPTextBox" Width="20%"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtStartDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 120px;">END DATE</td>
                <td><asp:TextBox ID="txtEndDate" runat="server" CssClass="ASPTextBox" Width="20%" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtEndDate">
                    </ajaxToolkit:CalendarExtender>

                </td>
            </tr>
            <tr>
                <td style="width: 120px;">
                    <asp:Button ID="btSearch" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="btSearch_Click"/>
                </td>
                <td><asp:Label ID="lblError" runat ="server"></asp:Label></td>
            </tr>
        </table>
        <div id="records"></div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_LIST" runat="server" BackColor="White" Visible="false"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical"  Width="100%" ItemStyle-Wrap="true" 
                                    AutoGenerateColumns="False" AllowPaging="True"   PageSize="10" CssClass="ASPDatagrid">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                         <AlternatingItemStyle BackColor="#F7F7F7" />
                                         <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                         <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                                         <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />

                                    <Columns>
                                         <asp:BoundColumn DataField="CLAIM_NUMBER" HeaderText="CLAIM NUMBER">
                                         <HeaderStyle HorizontalAlign="Center" />
                               
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="CLAIM_OCCURANCE_NO" HeaderText="CLAIM OCCURANCE NO" >
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="PLAN_ID" HeaderText="PLAN ID">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>

                         <asp:BoundColumn DataField="TPA_COVERAGE_TYPE" HeaderText="TPA COVERAGE TYPE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>

                         <asp:BoundColumn DataField="PAYOR_ID" HeaderText="PAYOR ID">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        
                         <asp:BoundColumn DataField="CORPORATE_ID" HeaderText="CORPORATE ID">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                        
                        
                         <asp:BoundColumn DataField="CONTRACT_NUMBER" HeaderText="CONTRACT NUMBER">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="MEMBER_ID" HeaderText="MEMBER ID">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="BENEFIT_CODE" HeaderText="BENEFIT CODE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="BENEFIT_DESCRIPTION" HeaderText="BENEFIT DESCRIPTION">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="AMOUNT_INCURRED" HeaderText="AMOUNT INCURRED">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="HMO_SHARE" HeaderText="HMO SHARE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MEMBER_SHARE" HeaderText="MEMBER SHARE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT_EXCESS" HeaderText="AMOUNT EXCESS">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="REASON_CODE" HeaderText="REASON CODE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <%--<asp:BoundColumn DataField="RECEIVE_DATE" HeaderText="RECEIVE DATE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>--%>
                                    </Columns>
                                    <PagerStyle BackColor="#4A3C8C" ForeColor="#E7E7FF" HorizontalAlign="Right" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_LIST2" runat="server" BackColor="White" Visible="false"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical"  Width="100%" ItemStyle-Wrap="true" 
                                    AutoGenerateColumns="False" AllowPaging="True"   PageSize="10" CssClass="ASPDatagrid">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                         <AlternatingItemStyle BackColor="#F7F7F7" />
                                         <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                         <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                                         <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="PAYOR_ID" HeaderText="PAYOR ID">
                                         <HeaderStyle HorizontalAlign="Center" />
                               
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="CORPORATE_ID" HeaderText="CORPORATE ID" >
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="CONTRACT_NUMBER" HeaderText="CONTRACT NUMBER">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="MEMBER_ID" HeaderText="MEMBER ID">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="CLIENT_NAME" HeaderText="CLIENT NAME">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="MEMBER_STATUS" HeaderText="MEMBER STATUS">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="CLAIM_TYPE" HeaderText="CLAIM TYPE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="CLAIM_PROCESS_STATUS" HeaderText="CLAIM PROCESS STATUS">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="CLAIM_NUMBER" HeaderText="CLAIM NUMBER">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CLAIM_OCCURANCE_NO" HeaderText="CLAIM OCCURANCE NO">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TPA_CLAIM_NUMBER" HeaderText="TPA CLAIM NUMBER">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PROVIDER_CODE" HeaderText="PROVIDER CODE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ADMISSION_DATE" HeaderText="ADMISSION DATE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="DISCHARGE_DATE" HeaderText="DISCHARGE DATE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>

                                        <asp:BoundColumn DataField="DURATION_DAY" HeaderText="DURATION DAY">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>


                        <asp:BoundColumn DataField="TPA_COVERAGE_TYPE" HeaderText="TPA COVERAGE TYPE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>

                                        <asp:BoundColumn DataField="PLAN_ID" HeaderText="PLAN ID">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                <asp:BoundColumn DataField="DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                <asp:BoundColumn DataField="DIAGNOSIS_DESCRIPTION" HeaderText="DIAGNOSIS DESCRIPTION">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOTAL_INCURRED_AMOUNT" HeaderText="TOTAL INCURRED AMOUNT">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOTAL_APPROVED_AMOUNT" HeaderText="TOTAL APPROVED AMOUNT">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOTAL_AMOUNT_NOT_APPROVED" HeaderText="TOTAL AMOUNT NOT APPROVED">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOTAL_EXCESS_CLAIM" HeaderText="TOTAL EXCESS CLAIM">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                <asp:BoundColumn DataField="REMARKS_FROM_TPA" HeaderText="REMARKS FROM TPA">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                <asp:BoundColumn DataField="SECONDARY_DIAGNOSIS_LIST" HeaderText="SECONDARY DIAGNOSIS LIST">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                <asp:BoundColumn DataField="APPROVED_DATE" HeaderText="APPROVED DATE">
                                                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="AUTHORISED_BY" HeaderText="AUTHORISED BY">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="RECEIVE_DATE" HeaderText="RECEIVE DATE">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="HOSPITAL_INVOICE_NO" HeaderText="HOSPITAL INVOICE NO">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TPA_ERROR_DESCRIPTION" HeaderText="TPA ERROR DESCRIPTION">
                             <HeaderStyle HorizontalAlign="Center" />
                         </asp:BoundColumn>



                                    </Columns>
                                    <PagerStyle BackColor="#4A3C8C" ForeColor="#E7E7FF" HorizontalAlign="Right" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_LIST3" runat="server" BackColor="White" Visible="false"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical"  Width="100%" ItemStyle-Wrap="true" 
                                    AutoGenerateColumns="False" AllowPaging="True"   PageSize="10" CssClass="ASPDatagrid">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                         <AlternatingItemStyle BackColor="#F7F7F7" />
                                         <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                         <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                                         <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CLAIM_ID" HeaderText="CLAIM ID">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PAYMENT_VOUCHER_NO" HeaderText="PAYMENT VOUCHER NO" >
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="PAYMENT_VOUCHER_DATE" HeaderText="PAYMENT VOUCHER DATE">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="AMOUNT_TOTAL" HeaderText="AMOUNT TOTAL">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="BANK_CODE" HeaderText="BANK CODE">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>      
                                         <asp:BoundColumn DataField="BANK_NAME" HeaderText="BANK NAME">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="ACCOUNT_NAME" HeaderText="ACCOUNT NAME">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="ACCOUNT_NO" HeaderText="ACCOUNT NO">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="CATEGORY_PROCESS" HeaderText="CATEGORY PROCESS">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#4A3C8C" ForeColor="#E7E7FF" HorizontalAlign="Right" Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>

                    </table>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <asp:Button ID="btnExportCSV" runat="server" CssClass="ASPButton" Text="EXPORT TXT" Visible="false" OnClick="btnExport_Click"/>&nbsp
                                <asp:Button ID="btnExportExcel" runat="server" CssClass="ASPButton" Text="EXPORT EXCEL" Visible="false" OnClick="btnExportExcel_Click"/>&nbsp
                                <asp:Button ID="btnExportPDF" runat="server" CssClass="ASPButton" Text="EXPORT PDF" Visible="false" OnClick="btnExportPDF_Click"/>&nbsp
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
