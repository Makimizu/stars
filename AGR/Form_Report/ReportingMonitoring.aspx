<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportingMonitoring.aspx.cs" Inherits="AGR.Form_Report.ReportingMonitoring" %>

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
                        <asp:ListItem Value="0">POLIS INDIVIDU</asp:ListItem>
                        <asp:ListItem Value="1">POLIS CORPORATE</asp:ListItem>
                        <asp:ListItem Value="2">POLIS KESEHATAN</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <%--<tr>
                <td style="width: 120px;">POLICY NO</td>
                <td>
                    <asp:TextBox ID="txtPolicyNo" runat="server" CssClass="ASPTextBox" Width="35%"></asp:TextBox>&nbsp;&nbsp
                    <asp:CheckBox ID="chkNull" runat="server" Text="NULL" AutoPostBack="true" OnCheckedChanged="chkNull_CheckedChanged" />
                </td>
            </tr>--%>
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
                                        <asp:BoundColumn DataField="POLICY_NO" HeaderText="No. Polis">
                                            <HeaderStyle HorizontalAlign="Center" /> 
                                        </asp:BoundColumn> 
                                        <asp:BoundColumn DataField="application_regist_date" HeaderText="Register Date" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="PRODUCT_START" HeaderText="Product Start">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="PRODUCT_DESCR" HeaderText="Production Deskripsi">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="PRODUCT_GROUP" HeaderText="Product Group">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="Policy_product_period" HeaderText="Polis Product Period">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="SUMINS" HeaderText="SUMINS">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="AGENT_CODE" HeaderText="Agent Code">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="AGENT_NAME" HeaderText="Agent Name">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="AGENT_LEVEL_DESCR" HeaderText="Agent Level Deskripsi">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="AGENT_AGENCY_NAME" HeaderText="Agent Agency Name">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="AGENT_CHANNEL_DIST" HeaderText="Agent Agency Distribusi">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="AGENT_MARKET_SEGMENT" HeaderText="Agent Market Segment">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="BASICPREMIUM" HeaderText="Basic Premium">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="TOPUP_REGULER" HeaderText="Top Up Reguler">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="TOPUP_SINGLE" HeaderText="Top Up Single">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="FOP_DESCR" HeaderText="Fop Description">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="payment_sequent" HeaderText="Payment Seq">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="GWP" HeaderText="Amount">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="claim_paid_sequent" HeaderText="Claim Paid Seq">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="TOTAL_CLAIM" HeaderText="Total Claim">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="STAT_DESCR" HeaderText="Stat Description">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="CANCEL_DATE" HeaderText="Cancel Date">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_cancel_reason" HeaderText="Policy Cancel Reason">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="FULLNAME" HeaderText="Fullname">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="SEX" HeaderText="Gender">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="MARITAL_STATUS" HeaderText="Martial Status">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="CITY" HeaderText="CITY">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="EMAIL" HeaderText="Email">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="PHONE_1" HeaderText="No. HandPhone">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
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
                                        <asp:BoundColumn DataField="POLICY_NO" HeaderText="No. Polis">
                                            <HeaderStyle HorizontalAlign="Center" /> 
                                        </asp:BoundColumn> 
                                        <asp:BoundColumn DataField="Application_Regis" HeaderText="Register Date" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="policy_product_name" HeaderText="Product Name">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_product_group" HeaderText="Product Group">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_product_period" HeaderText="Product Policy Period">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_total_insured" HeaderText="Sum Ins">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>  
                                         <asp:BoundColumn DataField="AGENT_CODE" HeaderText="Agent Code">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="AGENT_NAME" HeaderText="Agent Name">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="AGENT_LEVEL" HeaderText="Agent Level">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>  
                                         <asp:BoundColumn DataField="AGENT_LEVEL_DESCR" HeaderText="Agent Level Deskripsi">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>  
                                         <asp:BoundColumn DataField="AGENT_CHANNEL_DIST" HeaderText="Agent Agency Distribusi">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>  
                                         <asp:BoundColumn DataField="PREMIUM" HeaderText="Premium">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="REG_TOPUP" HeaderText="Top Up Reguler">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="SINGLE_TOPUP" HeaderText="Top Up Single">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="PAYMENT_METHOD" HeaderText="Payment Method">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="PAYMENT_SEQ" HeaderText="Payment Seq">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="GWP" HeaderText="Amount">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="CLAIM_PAID_SEQ" HeaderText="Claim Paid Seq">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="TOBE_PAID" HeaderText="To be Paid">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="STAT" HeaderText="Stat">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_cancel_date" HeaderText="Cancel Date">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_cancel_REASON" HeaderText="Policy Cancel Reason">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="FULLNAME" HeaderText="Fullname">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="GENDER" HeaderText="Gender">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="MARITAL_STATUS" HeaderText="Martial Status">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="City" HeaderText="City">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>  
                                         <asp:BoundColumn DataField="EMAIL" HeaderText="Email">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="PHONE" HeaderText="No. HandPhone">
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
                                        <asp:BoundColumn DataField="POLICY_NO" HeaderText="No. Polis">
                                            <HeaderStyle HorizontalAlign="Center" /> 
                                        </asp:BoundColumn> 
                                        <asp:BoundColumn DataField="application_regist_date" HeaderText="Register Date" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                        <asp:BoundColumn DataField="START_DATE" HeaderText="Start Date" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_product_name" HeaderText="Production Deskripsi">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_product_group" HeaderText="Product Group">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_total_insured" HeaderText="Total Insured">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>  
                                         <asp:BoundColumn DataField="AGENT_CODE" HeaderText="Agent Code">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="AGENT_NAME" HeaderText="Agent Name">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>
                                         <asp:BoundColumn DataField="AGENT_LEVEL_DESCR" HeaderText="Agent Level Deskripsi">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="Company">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="AGENT_CHANNEL_DIST" HeaderText="Agent Agency Distribusi">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn>  
                                         <asp:BoundColumn DataField="contribution" HeaderText="Contribution">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="regular_top_up" HeaderText="Top Up Reguler">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="single_top_up" HeaderText="Top Up Single">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="payment_method" HeaderText="Payment Method">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="payment_sequent" HeaderText="Payment Seq">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="GWP" HeaderText="Amount">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="claim_paid_sequent" HeaderText="Claim Paid Seq">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="claim_paid_amount" HeaderText="Claim Paid">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_status" HeaderText="Polis Status">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_cancel_date" HeaderText="Cancel Date">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_cancel_reason" HeaderText="Policy Cancel Reason">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="NAMA" HeaderText="Fullname">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="SEX" HeaderText="Gender">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_holder_marital_status" HeaderText="Martial Status">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="policy_holder_city" HeaderText="CITY">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="EMAIL" HeaderText="Email">
                                             <HeaderStyle HorizontalAlign="Center" />
                                         </asp:BoundColumn> 
                                         <asp:BoundColumn DataField="PHONE" HeaderText="No. HandPhone">
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
                <asp:Button ID="btnExportExcel" runat="server" CssClass="ASPButton" Text="EXPORT EXCEL" Visible="false" OnClick="btnExportExcel_Click"/>&nbsp
                </td>
            </tr>
        </table>
    </form>
</body>
</html>